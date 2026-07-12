using System;

namespace frm_PowerPlanTweak.Core
{
    internal static class WindowsVersionHelper
    {
        // Windows 10 version 1809 (October 2018 Update) — minimum supported baseline.
        // Windows 11 also reports Major=10 (Build >= 22000), so it passes the same check.
        private const int MinimumSupportedBuild = 17763;

        /// <summary>
        /// Relies on app.manifest declaring &lt;supportedOS&gt; entries for Windows 7/8/8.1/10 —
        /// without that, Environment.OSVersion lies about the real build number on Windows 8.1+
        /// (compatibility shim) and this check would be meaningless.
        /// </summary>
        public static bool IsSupported()
        {
            var version = Environment.OSVersion.Version;
            if (version.Major < 10) return false;
            if (version.Major == 10 && version.Build < MinimumSupportedBuild) return false;
            return true;
        }

        public static string CurrentVersionDescription()
        {
            var v = Environment.OSVersion.Version;
            return $"{v.Major}.{v.Minor}.{v.Build}";
        }
    }
}
