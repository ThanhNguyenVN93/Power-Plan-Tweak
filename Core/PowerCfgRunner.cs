using System.Diagnostics;
using System.Text.RegularExpressions;

namespace frm_PowerPlanTweak.Core
{
    internal sealed class PowerCfgResult
    {
        public int ExitCode { get; set; }
        public string StdOut { get; set; }
        public string StdErr { get; set; }
        public bool Succeeded => ExitCode == 0;
    }

    internal static class PowerCfgRunner
    {
        private static readonly Regex GuidPattern = new Regex(
            @"\{?([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})\}?",
            RegexOptions.Compiled);

        public static PowerCfgResult Run(string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "powercfg.exe",
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                string stdOut = process.StandardOutput.ReadToEnd();
                string stdErr = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return new PowerCfgResult
                {
                    ExitCode = process.ExitCode,
                    StdOut = stdOut,
                    StdErr = stdErr
                };
            }
        }

        /// <summary>
        /// Extracts a GUID out of arbitrary text (WMI InstanceID, powercfg stdout, etc.) regardless
        /// of prefix/braces — OEM-modified WMI providers can alter the usual "Microsoft:PowerPlan\"
        /// prefix, so we look for the GUID shape itself instead of assuming a fixed prefix.
        /// </summary>
        public static string ExtractGuid(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var match = GuidPattern.Match(text);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}
