using System;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

namespace frm_PowerPlanTweak.Core
{
    internal sealed class BoostMeasurement
    {
        public double IdlePercent { get; set; }
        public double LoadPercent { get; set; }
        public double IdleClockMHz { get; set; }
    }

    internal static class CpuInfo
    {
        /// <summary>
        /// WMI's Win32_Processor.CurrentClockSpeed, for log context only — NOT used to decide
        /// whether the boost tweak had an effect (see MeasureBoostEffect for why).
        /// </summary>
        public static double GetCurrentClockSpeedMHz()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    using (mo)
                    {
                        return Convert.ToDouble(mo["CurrentClockSpeed"]);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Measures whether the processor actually boosts under load, without anchoring the
        /// comparison to any OEM-reported "rated" clock speed. Win32_Processor.MaxClockSpeed comes
        /// from SMBIOS "Max Speed" and is frequently neither the real base clock nor the real turbo
        /// ceiling — comparing an achieved speed against it can flag a healthy boost as "no change"
        /// or a completely inert boost as "changed", depending on how the OEM populated that field.
        /// Instead, this samples the same "% Processor Performance" counter twice — once idle, once
        /// under stress — and compares those two readings to each other. Both readings come from the
        /// exact same counter under the exact same scaling reference, so the comparison is internally
        /// consistent regardless of what any single OEM firmware reports.
        /// </summary>
        public static BoostMeasurement MeasureBoostEffect()
        {
            double idleClockMHz = GetCurrentClockSpeedMHz();

            using (var counter = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total"))
            {
                counter.NextValue(); // priming call — always 0, discard
                Thread.Sleep(1000);  // give the counter a real interval to measure

                float idlePercent = counter.NextValue(); // baseline, still idle

                using (var cts = new CancellationTokenSource())
                {
                    var stressTask = Task.Run(() => RunStress(cts.Token));
                    try
                    {
                        Thread.Sleep(2000); // let clocks ramp/stabilize under load

                        float loadPercent = counter.NextValue();
                        return new BoostMeasurement
                        {
                            IdlePercent = idlePercent,
                            LoadPercent = loadPercent,
                            IdleClockMHz = idleClockMHz
                        };
                    }
                    finally
                    {
                        cts.Cancel();
                        stressTask.Wait();
                    }
                }
            }
        }

        private static void RunStress(CancellationToken token)
        {
            Parallel.For(0, Environment.ProcessorCount, _ =>
            {
                double accumulator = 0;
                while (!token.IsCancellationRequested)
                {
                    for (int i = 0; i < 1_000_000 && !token.IsCancellationRequested; i++)
                    {
                        accumulator += Math.Sqrt(i);
                    }
                }
                // Prevent the compiler/JIT from optimizing the loop away entirely.
                if (accumulator < 0) throw new InvalidOperationException();
            });
        }
    }
}
