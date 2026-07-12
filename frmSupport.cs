using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace frm_PowerPlanTweak
{
    public partial class frmSupport : Form
    {
        public bool DontShowAgain => chkDontShow.Checked;

        public frmSupport()
        {
            InitializeComponent();

            picMomo.Image = TryLoadEmbeddedImage("frm_PowerPlanTweak.Assets.momo.jpg");
            picTcb.Image = TryLoadEmbeddedImage("frm_PowerPlanTweak.Assets.tcb.jpg");
        }

        /// <summary>
        /// Loads a JPEG baked in as an embedded resource (see the EmbeddedResource/LogicalName
        /// entries in the csproj) so the QR images ship inside the assembly rather than as loose
        /// files next to the exe. The manifest resource stream is copied into an owned MemoryStream
        /// before disposal — Image.FromStream keeps a live reference to whatever stream it's given,
        /// so disposing the original stream immediately would corrupt the image the first time it's
        /// actually drawn.
        /// </summary>
        private static Image TryLoadEmbeddedImage(string logicalName)
        {
            try
            {
                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(logicalName))
                {
                    if (resourceStream == null) return null;

                    var buffer = new MemoryStream();
                    resourceStream.CopyTo(buffer);
                    buffer.Position = 0;
                    return Image.FromStream(buffer);
                }
            }
            catch
            {
                return null; // missing/corrupt asset shouldn't crash the dialog
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            btnYes.Enabled = false;
            btnNo.Enabled = false;

            picMomo.Visible = true;
            picTcb.Visible = true;
            lblMomoCaption.Visible = true;
            lblTcbCaption.Visible = true;

            Height += 240;
        }

        #region Borderless title-bar dragging

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        #endregion
    }
}
