using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using frm_PowerPlanTweak.Core;

namespace frm_PowerPlanTweak
{
    public partial class frmPowerPlanTweak : Form
    {
        private const string FeedbackUrl = "https://www.facebook.com/PycCodeTools";

        public frmPowerPlanTweak()
        {
            InitializeComponent();

            try
            {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            catch
            {
                // Fall back to the WinForms default icon if extraction fails for any reason.
            }

            cboBoostMode.Items.AddRange(BoostModeOption.All);
            cboBoostMode.SelectedIndex = 2; // Aggressive — matches the tool's previous default

            FormClosing += frmPowerPlanTweak_FormClosing;
        }

        private void frmPowerPlanTweak_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            if (AppSettingsHelper.DontShowSupportDialog) return;

            using (var dialog = new frmSupport())
            {
                dialog.ShowDialog(this);

                if (dialog.DontShowAgain)
                {
                    AppSettingsHelper.DontShowSupportDialog = true;
                }
            }
        }

        private void lnkFeedback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(FeedbackUrl) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show(
                    $"Không thể mở trình duyệt. Vui lòng truy cập thủ công: {FeedbackUrl}",
                    "Feedback",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            btnRun.Enabled = false;
            cboBoostMode.Enabled = false;
            txtLog.Text = string.Empty;
            progressBar.Value = 0;
            lblStatus.Text = "Đang chạy...";
            lblStatus.ForeColor = Color.Black;

            int boostModeValue = ((BoostModeOption)cboBoostMode.SelectedItem).Value;
            var progress = new Progress<LogEntry>(OnLog);
            var tweaker = new PowerPlanTweaker();

            TweakResult result = await Task.Run(() => tweaker.Run(progress, boostModeValue));

            progressBar.Value = progressBar.Maximum;
            if (result.Success)
            {
                lblStatus.Text = "Thành công";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = result.RolledBack ? "Thất bại — đã rollback" : "Thất bại";
                lblStatus.ForeColor = Color.Red;
            }

            btnRun.Enabled = true;
            cboBoostMode.Enabled = true;
        }

        private void OnLog(LogEntry entry)
        {
            txtLog.AppendText($"[{entry.Timestamp:HH:mm:ss}] {entry.Message}{Environment.NewLine}");
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();

            if (entry.Message.StartsWith("=== Tầng", StringComparison.Ordinal))
            {
                progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
            }
        }
    }
}
