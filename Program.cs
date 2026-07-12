using System;
using System.Windows.Forms;
using frm_PowerPlanTweak.Core;

namespace frm_PowerPlanTweak
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!WindowsVersionHelper.IsSupported())
            {
                MessageBox.Show(
                    "Ứng dụng yêu cầu tối thiểu Windows 10 phiên bản 1809 (Build 17763) trở lên.\n" +
                    $"Phiên bản Windows hiện tại: {WindowsVersionHelper.CurrentVersionDescription()}.\n" +
                    "Vui lòng cập nhật Windows rồi chạy lại chương trình.",
                    "Phiên bản Windows không được hỗ trợ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!AdminHelper.IsAdministrator())
            {
                AdminHelper.RelaunchElevated();
                return;
            }

            if (!DotNetFrameworkChecker.IsNet48OrLaterInstalled())
            {
                bool installed = DotNetFrameworkChecker.DownloadAndInstallSilently(msg =>
                    System.Diagnostics.Debug.WriteLine(msg));

                if (!installed)
                {
                    MessageBox.Show(
                        "Không thể tự động cài đặt .NET Framework 4.8. Vui lòng cài đặt thủ công rồi chạy lại chương trình.",
                        "Cài đặt .NET Framework 4.8 thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DotNetFrameworkChecker.WarnAndRelaunch();
                return;
            }

            Application.Run(new frmPowerPlanTweak());
        }
    }
}
