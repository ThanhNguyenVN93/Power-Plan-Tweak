namespace frm_PowerPlanTweak
{
    partial class frmPowerPlanTweak
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLog = new Guna.UI2.WinForms.Guna2Panel();
            this.txtLog = new Guna.UI2.WinForms.Guna2TextBox();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlBoostMode = new Guna.UI2.WinForms.Guna2Panel();
            this.cboBoostMode = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblBoostMode = new System.Windows.Forms.Label();
            this.lnkFeedback = new System.Windows.Forms.LinkLabel();
            this.btnRun = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLog.SuspendLayout();
            this.pnlBoostMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.txtLog);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLog.Location = new System.Drawing.Point(12, 12);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Padding = new System.Windows.Forms.Padding(4);
            this.pnlLog.Size = new System.Drawing.Size(660, 360);
            this.pnlLog.TabIndex = 0;
            // 
            // txtLog
            // 
            this.txtLog.BorderRadius = 6;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLog.DefaultText = "";
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(4, 4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.PlaceholderText = "";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.SelectedText = "";
            this.txtLog.Size = new System.Drawing.Size(652, 352);
            this.txtLog.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(12, 372);
            this.progressBar.Maximum = 5;
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.MediumSeaGreen;
            this.progressBar.Size = new System.Drawing.Size(660, 20);
            this.progressBar.TabIndex = 1;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(12, 392);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(660, 30);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Sẵn sàng";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBoostMode
            // 
            this.pnlBoostMode.Controls.Add(this.cboBoostMode);
            this.pnlBoostMode.Controls.Add(this.lblBoostMode);
            this.pnlBoostMode.Controls.Add(this.lnkFeedback);
            this.pnlBoostMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoostMode.Location = new System.Drawing.Point(12, 422);
            this.pnlBoostMode.Name = "pnlBoostMode";
            this.pnlBoostMode.Size = new System.Drawing.Size(660, 36);
            this.pnlBoostMode.TabIndex = 3;
            // 
            // cboBoostMode
            // 
            this.cboBoostMode.BackColor = System.Drawing.Color.Transparent;
            this.cboBoostMode.BorderRadius = 6;
            this.cboBoostMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboBoostMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBoostMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoostMode.FocusedColor = System.Drawing.Color.Empty;
            this.cboBoostMode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboBoostMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboBoostMode.ItemHeight = 30;
            this.cboBoostMode.Location = new System.Drawing.Point(110, 0);
            this.cboBoostMode.Name = "cboBoostMode";
            this.cboBoostMode.Size = new System.Drawing.Size(430, 36);
            this.cboBoostMode.TabIndex = 1;
            // 
            // lblBoostMode
            // 
            this.lblBoostMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBoostMode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBoostMode.Location = new System.Drawing.Point(0, 0);
            this.lblBoostMode.Name = "lblBoostMode";
            this.lblBoostMode.Size = new System.Drawing.Size(110, 36);
            this.lblBoostMode.TabIndex = 0;
            this.lblBoostMode.Text = "Boost Mode:";
            this.lblBoostMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkFeedback
            // 
            this.lnkFeedback.Dock = System.Windows.Forms.DockStyle.Right;
            this.lnkFeedback.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkFeedback.LinkColor = System.Drawing.Color.DeepSkyBlue;
            this.lnkFeedback.Location = new System.Drawing.Point(540, 0);
            this.lnkFeedback.Name = "lnkFeedback";
            this.lnkFeedback.Size = new System.Drawing.Size(120, 36);
            this.lnkFeedback.TabIndex = 2;
            this.lnkFeedback.TabStop = true;
            this.lnkFeedback.Text = "Feedback";
            this.lnkFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkFeedback.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFeedback_LinkClicked);
            // 
            // btnRun
            // 
            this.btnRun.BorderRadius = 6;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRun.FillColor = System.Drawing.Color.SteelBlue;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(12, 458);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(660, 40);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Chạy Tweak";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // frmPowerPlanTweak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 510);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlBoostMode);
            this.Controls.Add(this.btnRun);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "frmPowerPlanTweak";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PowerPlanTweak";
            this.pnlLog.ResumeLayout(false);
            this.pnlBoostMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLog;
        private Guna.UI2.WinForms.Guna2TextBox txtLog;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private Guna.UI2.WinForms.Guna2Panel pnlBoostMode;
        private Guna.UI2.WinForms.Guna2ComboBox cboBoostMode;
        private System.Windows.Forms.Label lblBoostMode;
        private System.Windows.Forms.LinkLabel lnkFeedback;
        private Guna.UI2.WinForms.Guna2Button btnRun;
    }
}
