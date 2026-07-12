using Microsoft.Win32;

namespace frm_PowerPlanTweak.Core
{
    /// <summary>
    /// Tiny per-user preference store backed directly by the registry instead of
    /// System.Configuration/app.config — this app ships as a single exe with no companion
    /// .exe.config file, so ApplicationSettingsBase (which needs that config's userSettings
    /// section to know the setting schema) isn't an option here.
    /// </summary>
    internal static class AppSettingsHelper
    {
        private const string RegistryKeyPath = @"Software\frm_PowerPlanTweak";
        private const string DontShowSupportDialogValueName = "DontShowSupportDialog";

        public static bool DontShowSupportDialog
        {
            get
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    object value = key?.GetValue(DontShowSupportDialogValueName);
                    return value != null && (int)value != 0;
                }
            }
            set
            {
                using (var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
                {
                    key.SetValue(DontShowSupportDialogValueName, value ? 1 : 0, RegistryValueKind.DWord);
                }
            }
        }
    }
}
