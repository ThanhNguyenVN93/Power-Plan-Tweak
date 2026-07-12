using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using Microsoft.Win32;

namespace frm_PowerPlanTweak.Core
{
    /// <summary>
    /// Orchestrates the 5-stage power-plan tweak flow: safety net, detection, unlock hidden
    /// settings, execute processor tweaks, and acceptance verification. Any failed powercfg
    /// command in Stage 4 triggers an immediate rollback from the Stage 1 backup.
    /// </summary>
    internal sealed class PowerPlanTweaker
    {
        private const string PowerSettingsRegistryPath = @"SYSTEM\CurrentControlSet\Control\Power\PowerSettings";

        private const string SubProcessor = "54533251-82be-4824-96c1-47b60b740d00";
        private const string SettingMinProcState = "893dee8e-2bef-41e0-89c6-b8792c26c4bb";
        private const string SettingMaxProcState = "bc5038f7-23e0-4960-96da-33abaf5935ec";
        private const string SettingPerfBoostMode = "be337238-0d82-4146-a960-4f3749d470c7";

        public TweakResult Run(IProgress<LogEntry> progress, int boostModeValue)
        {
            var result = new TweakResult();

            try
            {
                Log(progress, LogLevel.Info, "=== Tầng 1: An toàn ===");

                string activeGuid = GetActiveSchemeGuid();
                if (activeGuid == null)
                {
                    return Fail(progress, result, "Không lấy được GUID của power scheme đang active.");
                }

                string restorePointError;
                if (RestorePointHelper.TryCreate("PowerPlanTweak - before tweak", out restorePointError))
                {
                    Log(progress, LogLevel.Success, "Đã tạo System Restore Point.");
                }
                else
                {
                    Log(progress, LogLevel.Warning, $"Không tạo được Restore Point ({restorePointError}), tiếp tục.");
                }

                string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string backupDir = Path.Combine(programData, "frm_PowerPlanTweak", "Backup");
                Directory.CreateDirectory(backupDir);
                string backupPath = Path.Combine(backupDir, $"{activeGuid}_{DateTime.Now:yyyyMMdd_HHmmss}.pow");

                var exportResult = PowerCfgRunner.Run($"/export \"{backupPath}\" {activeGuid}");
                if (!exportResult.Succeeded)
                {
                    return Fail(progress, result, $"Backup power scheme thất bại (exit {exportResult.ExitCode}): {exportResult.StdErr}");
                }
                Log(progress, LogLevel.Success, $"Đã backup power scheme vào {backupPath}");

                Log(progress, LogLevel.Info, "=== Tầng 2: Nhận diện ===");
                bool isLaptop = DetectIsLaptop();
                bool hasBattery = DetectHasBattery();
                bool applyDc = isLaptop || hasBattery;
                Log(progress, LogLevel.Info,
                    $"PCSystemType laptop: {isLaptop}, có pin: {hasBattery} => " +
                    (applyDc ? "sẽ áp dụng tweak DC (an toàn, giữ pin)" : "bỏ qua DC (desktop không pin)"));

                Log(progress, LogLevel.Info, "=== Tầng 3: Mở khóa ===");
                List<UnhideRecord> unhideRecords = UnhideAllHiddenSettings();
                Log(progress, LogLevel.Success, $"Đã unhide {unhideRecords.Count} power setting đang bị ẩn (kể cả setting OEM nếu có).");

                Log(progress, LogLevel.Info, "=== Tầng 4: Thực thi ===");
                if (!ApplyProcessorTweaks(activeGuid, applyDc, boostModeValue, progress))
                {
                    Log(progress, LogLevel.Error, "Thực thi thất bại — tiến hành rollback.");
                    Rollback(activeGuid, backupPath, progress);
                    RestoreHiddenSettings(unhideRecords, progress);
                    result.Success = false;
                    result.RolledBack = true;
                    result.FailureReason = "Một lệnh powercfg trả về mã lỗi khác 0.";
                    Log(progress, LogLevel.Error, "=== Kết thúc: Thất bại ===");
                    return result;
                }
                Log(progress, LogLevel.Success, "Đã áp dụng xong thông số processor cho AC" + (applyDc ? " và DC." : "."));

                Log(progress, LogLevel.Info, "=== Tầng 5: Nghiệm thu ===");
                // Stage 4 already succeeded and is active at this point — the machine's config is
                // already in its intended final state. A failure measuring the effect here is a
                // "couldn't verify" situation, not a "tweak failed" situation, so it gets its own
                // try/catch: it must never trigger Rollback/RestoreHiddenSettings or flip the overall
                // result to failure, since doing so would misreport a config that's actually applied
                // and active as "Thất bại" with no rollback actually having happened.
                try
                {
                    Log(progress, LogLevel.Info, "Đang chạy stress test ~3 giây (đo % Processor Performance lúc rảnh và lúc tải)...");
                    BoostMeasurement measurement = CpuInfo.MeasureBoostEffect();
                    result.IdlePerformancePercent = measurement.IdlePercent;
                    result.LoadPerformancePercent = measurement.LoadPercent;

                    // Idle→load always ramps up regardless of boost mode (that's just normal P-state
                    // scaling off idle) so it can't tell us whether boost is in effect. What can is
                    // whether the load reading exceeds the ~100% "nominal/base clock" mark: only
                    // turbo/boost can push a core above its own 100% reference point.
                    bool boostRequested = boostModeValue != 0; // 0 = Disabled
                    bool exceededBase = measurement.LoadPercent > 105.0;
                    string verdict = boostRequested
                        ? (exceededBase ? "boost hoạt động đúng" : "boost KHÔNG hiệu lực — firmware/OEM có thể đang chặn")
                        : (exceededBase ? "CẢNH BÁO: Disabled nhưng CPU vẫn vượt base — policy không được tôn trọng" : "Disabled hoạt động đúng, CPU bị khóa ở base");

                    Log(progress, LogLevel.Info,
                        $"% Processor Performance: rảnh {measurement.IdlePercent:F0}% → tải {measurement.LoadPercent:F0}% " +
                        $"(CurrentClockSpeed lúc rảnh: {measurement.IdleClockMHz:F0} MHz, chỉ để tham khảo) — {verdict}.");
                    Log(progress, LogLevel.Success, "Kết luận: Vô hại — đã ghi log, không rollback.");
                }
                catch (Exception ex)
                {
                    Log(progress, LogLevel.Warning,
                        $"Không đo được hiệu quả boost ({ex.Message}), nhưng cấu hình đã áp dụng thành công lên máy — không rollback.");
                }

                result.Success = true;
                Log(progress, LogLevel.Success, "=== Kết thúc: Thành công ===");
                return result;
            }
            catch (Exception ex)
            {
                return Fail(progress, result, $"Lỗi không mong muốn: {ex.Message}");
            }
        }

        private static TweakResult Fail(IProgress<LogEntry> progress, TweakResult result, string reason)
        {
            Log(progress, LogLevel.Error, reason);
            Log(progress, LogLevel.Error, "=== Kết thúc: Thất bại ===");
            result.Success = false;
            result.FailureReason = reason;
            return result;
        }

        private static void Log(IProgress<LogEntry> progress, LogLevel level, string message)
        {
            progress.Report(new LogEntry(level, message));
        }

        private static string GetActiveSchemeGuid()
        {
            var r = PowerCfgRunner.Run("/getactivescheme");
            return r.Succeeded ? PowerCfgRunner.ExtractGuid(r.StdOut) : null;
        }

        private static bool DetectIsLaptop()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT PCSystemType FROM Win32_ComputerSystem"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    using (mo)
                    {
                        int type = Convert.ToInt32(mo["PCSystemType"]);
                        return type == 2; // 2 = Mobile (laptop)
                    }
                }
            }
            return false;
        }

        private static bool DetectHasBattery()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    mo.Dispose();
                    return true;
                }
            }
            return false;
        }

        private sealed class UnhideRecord
        {
            public string SubgroupName;
            public string SettingName; // null => this record is the subgroup key itself
            public int OriginalAttributes;
        }

        /// <summary>
        /// Sweeps every subgroup/setting under the power-settings registry tree (not scoped to any
        /// one scheme) and clears the hidden bit (Attributes &amp; 1) wherever it's set. This is what
        /// naturally reveals OEM-added hidden settings without needing a hardcoded GUID database.
        /// Every change is recorded (original Attributes value) so a Stage-4 failure can fully revert
        /// this stage too, not just the AC/DC value indexes — otherwise "rollback" would leave every
        /// previously-hidden setting permanently visible even on a failed run.
        /// </summary>
        private static List<UnhideRecord> UnhideAllHiddenSettings()
        {
            var records = new List<UnhideRecord>();
            using (var root = Registry.LocalMachine.OpenSubKey(PowerSettingsRegistryPath, writable: true))
            {
                if (root == null) return records;

                foreach (var subgroupName in root.GetSubKeyNames())
                {
                    using (var subgroupKey = root.OpenSubKey(subgroupName, writable: true))
                    {
                        if (subgroupKey == null) continue;

                        int? subgroupOriginal = TryUnhide(subgroupKey);
                        if (subgroupOriginal.HasValue)
                        {
                            records.Add(new UnhideRecord { SubgroupName = subgroupName, SettingName = null, OriginalAttributes = subgroupOriginal.Value });
                        }

                        foreach (var settingName in subgroupKey.GetSubKeyNames())
                        {
                            using (var settingKey = subgroupKey.OpenSubKey(settingName, writable: true))
                            {
                                if (settingKey == null) continue;
                                int? settingOriginal = TryUnhide(settingKey);
                                if (settingOriginal.HasValue)
                                {
                                    records.Add(new UnhideRecord { SubgroupName = subgroupName, SettingName = settingName, OriginalAttributes = settingOriginal.Value });
                                }
                            }
                        }
                    }
                }
            }
            return records;
        }

        /// <returns>The original (pre-unhide) Attributes value if it was hidden and got cleared; null if untouched.</returns>
        private static int? TryUnhide(RegistryKey key)
        {
            object attrObj = key.GetValue("Attributes");
            if (attrObj == null) return null;

            int attr = Convert.ToInt32(attrObj);
            if ((attr & 1) == 0) return null; // not hidden

            key.SetValue("Attributes", attr & ~1, RegistryValueKind.DWord);
            return attr;
        }

        private static void RestoreHiddenSettings(List<UnhideRecord> records, IProgress<LogEntry> progress)
        {
            if (records.Count == 0) return;

            using (var root = Registry.LocalMachine.OpenSubKey(PowerSettingsRegistryPath, writable: true))
            {
                if (root == null) return;

                foreach (var record in records)
                {
                    try
                    {
                        using (var subgroupKey = root.OpenSubKey(record.SubgroupName, writable: true))
                        {
                            if (subgroupKey == null) continue;

                            if (record.SettingName == null)
                            {
                                subgroupKey.SetValue("Attributes", record.OriginalAttributes, RegistryValueKind.DWord);
                            }
                            else
                            {
                                using (var settingKey = subgroupKey.OpenSubKey(record.SettingName, writable: true))
                                {
                                    settingKey?.SetValue("Attributes", record.OriginalAttributes, RegistryValueKind.DWord);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log(progress, LogLevel.Warning,
                            $"Không khôi phục được Attributes gốc cho {record.SubgroupName}\\{record.SettingName}: {ex.Message}");
                    }
                }
            }

            Log(progress, LogLevel.Success, $"Đã khôi phục trạng thái ẩn gốc cho {records.Count} power setting.");
        }

        private static bool ApplyProcessorTweaks(string schemeGuid, bool applyDc, int boostModeValue, IProgress<LogEntry> progress)
        {
            // AC is scoped to just Max processor state + Boost mode — CPU still idles/parks down
            // normally when there's no load, it's only allowed to boost to full when there is one.
            // (Min processor state / core parking forcing was intentionally dropped: it kept the CPU
            // pinned at full clock even at idle, which just means heat/fan noise for no benefit.)
            var commands = new List<string>
            {
                $"-setacvalueindex {schemeGuid} {SubProcessor} {SettingMaxProcState} 100",
                $"-setacvalueindex {schemeGuid} {SubProcessor} {SettingPerfBoostMode} {boostModeValue}"
            };

            if (applyDc)
            {
                // DC (battery) kept conservative: min state stays low (5%) so the CPU still idles down
                // on battery. Max state is 100 here too — harmless on its own since min state is what
                // actually controls idle behavior. Boost mode uses the same user-selected value as AC.
                commands.Add($"-setdcvalueindex {schemeGuid} {SubProcessor} {SettingMinProcState} 5");
                commands.Add($"-setdcvalueindex {schemeGuid} {SubProcessor} {SettingMaxProcState} 100");
                commands.Add($"-setdcvalueindex {schemeGuid} {SubProcessor} {SettingPerfBoostMode} {boostModeValue}");
            }

            foreach (var args in commands)
            {
                var r = PowerCfgRunner.Run(args);
                if (!r.Succeeded)
                {
                    Log(progress, LogLevel.Error, $"Lệnh thất bại (exit {r.ExitCode}): powercfg {args} — {r.StdErr}");
                    return false;
                }
                Log(progress, LogLevel.Info, $"powercfg {args} => OK");
            }

            var activate = PowerCfgRunner.Run($"-setactive {schemeGuid}");
            if (!activate.Succeeded)
            {
                Log(progress, LogLevel.Error, $"Không kích hoạt lại được scheme (exit {activate.ExitCode}): {activate.StdErr}");
                return false;
            }

            return true;
        }

        private static void Rollback(string schemeGuid, string backupPath, IProgress<LogEntry> progress)
        {
            var import = PowerCfgRunner.Run($"-import \"{backupPath}\" {schemeGuid}");
            if (!import.Succeeded)
            {
                Log(progress, LogLevel.Error,
                    $"Rollback thất bại khi import (exit {import.ExitCode}): {import.StdErr}. " +
                    $"File backup vẫn còn tại {backupPath}, có thể import thủ công.");
                return;
            }

            var activate = PowerCfgRunner.Run($"-setactive {schemeGuid}");
            if (activate.Succeeded)
            {
                Log(progress, LogLevel.Success, "Rollback thành công — đã khôi phục power scheme gốc.");
            }
            else
            {
                Log(progress, LogLevel.Error,
                    $"Import thành công nhưng không kích hoạt lại được scheme gốc (exit {activate.ExitCode}): {activate.StdErr}");
            }
        }
    }
}
