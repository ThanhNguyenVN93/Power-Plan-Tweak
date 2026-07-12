using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace frm_PowerPlanTweak.Core
{
    internal static class AdminHelper
    {
        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Relaunches the current executable elevated via UAC and exits the current (non-elevated) process.
        /// Returns false (without exiting) if the user cancels the UAC prompt.
        /// </summary>
        public static bool RelaunchElevated()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Application.ExecutablePath,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(startInfo);
                Application.Exit();
                return true;
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                MessageBox.Show(
                    "Ứng dụng cần quyền Administrator để chỉnh sửa Power Plan của Windows. " +
                    "Vui lòng chạy lại và bấm \"Yes\" trên hộp thoại UAC.",
                    "Cần quyền Administrator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}
