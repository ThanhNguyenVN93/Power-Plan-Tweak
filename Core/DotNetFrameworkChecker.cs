using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;

namespace frm_PowerPlanTweak.Core
{
    internal static class DotNetFrameworkChecker
    {
        // Official Microsoft permanent redirect for the .NET Framework 4.8 offline installer.
        private const string OfflineInstallerUrl = "https://go.microsoft.com/fwlink/?LinkId=2085155";

        // Microsoft's documented minimum "Release" DWORD value for .NET Framework 4.8 (valid as a
        // ">=" threshold across every Windows version the installer targets).
        private const int Net48ReleaseThreshold = 528040;

        public static bool IsNet48OrLaterInstalled()
        {
            using (var ndpKey = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
            {
                if (ndpKey == null) return false;
                object release = ndpKey.GetValue("Release");
                if (release == null) return false;
                return Convert.ToInt32(release) >= Net48ReleaseThreshold;
            }
        }

        /// <summary>
        /// Downloads the official offline installer and runs it silently (/q /norestart).
        /// Returns true if the install succeeded (exit code 0/3010/1641); false otherwise.
        /// </summary>
        public static bool DownloadAndInstallSilently(Action<string> log)
        {
            string installerPath = Path.Combine(Path.GetTempPath(), "ndp48-offline-installer.exe");
            string installLogPath = Path.Combine(Path.GetTempPath(), "ndp48-install.log");

            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                log($"Đang tải .NET Framework 4.8 offline installer về {installerPath} ...");
                using (var client = new WebClient())
                {
                    client.DownloadFile(OfflineInstallerUrl, installerPath);
                }
                log("Tải xong. Đang cài đặt âm thầm (/q /norestart) ...");

                var psi = new ProcessStartInfo
                {
                    FileName = installerPath,
                    Arguments = $"/q /norestart /log \"{installLogPath}\"",
                    UseShellExecute = false
                };

                using (var process = Process.Start(psi))
                {
                    process.WaitForExit();

                    switch (process.ExitCode)
                    {
                        case 0:
                            log(".NET Framework 4.8 đã cài đặt thành công.");
                            return true;
                        case 3010:
                        case 1641:
                            log(".NET Framework 4.8 đã cài đặt thành công (khuyến nghị khởi động lại máy sau khi hoàn tất).");
                            return true;
                        default:
                            log($"Cài đặt .NET Framework 4.8 thất bại, mã lỗi {process.ExitCode}. Chi tiết: {installLogPath}");
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log($"Lỗi khi tải/cài đặt .NET Framework 4.8: {ex.Message}");
                return false;
            }
            finally
            {
                try { if (File.Exists(installerPath)) File.Delete(installerPath); } catch { /* best-effort cleanup */ }
            }
        }

        /// <summary>
        /// Warns the technician that a second UAC prompt is about to appear (the process was already
        /// elevated once for Main() steps 1-2, and relaunching after install re-triggers UAC), then
        /// relaunches the app elevated.
        /// </summary>
        public static void WarnAndRelaunch()
        {
            MessageBox.Show(
                "Đã cài xong .NET Framework 4.8. Chương trình sẽ khởi động lại và YÊU CẦU CẤP QUYỀN " +
                "ADMIN LẦN NỮA — vui lòng bấm \"Yes\" trên hộp thoại UAC tiếp theo để tiếp tục.",
                "Cần xác nhận UAC lần nữa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            AdminHelper.RelaunchElevated();
        }
    }
}
