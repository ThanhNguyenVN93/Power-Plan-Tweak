namespace frm_PowerPlanTweak
{
    partial class frmSupport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTitleBar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnYes = new Guna.UI2.WinForms.Guna2Button();
            this.btnNo = new Guna.UI2.WinForms.Guna2Button();
            this.lblHeart = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblSupportNote = new System.Windows.Forms.Label();
            this.lblClickHint = new System.Windows.Forms.Label();
            this.chkDontShow = new Guna.UI2.WinForms.Guna2CheckBox();
            this.picMomo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.picTcb = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblMomoCaption = new System.Windows.Forms.Label();
            this.lblTcbCaption = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMomo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTcb)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Controls.Add(this.btnClose);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(110)))), ((int)(((byte)(199)))));
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(520, 42);
            this.pnlTitleBar.TabIndex = 0;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            //
            // lblTitle
            //
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(478, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Support PowerPlanTweak";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            //
            // btnClose
            //
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(478, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 42);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // pnlBody
            //
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.pnlBody.Controls.Add(this.lblHeart);
            this.pnlBody.Controls.Add(this.lblHeading);
            this.pnlBody.Controls.Add(this.lblMessage);
            this.pnlBody.Controls.Add(this.lblSupportNote);
            this.pnlBody.Controls.Add(this.lblClickHint);
            this.pnlBody.Controls.Add(this.chkDontShow);
            this.pnlBody.Controls.Add(this.picMomo);
            this.pnlBody.Controls.Add(this.picTcb);
            this.pnlBody.Controls.Add(this.lblMomoCaption);
            this.pnlBody.Controls.Add(this.lblTcbCaption);
            this.pnlBody.Controls.Add(this.pnlButtons);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 42);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(520, 300);
            this.pnlBody.TabIndex = 1;
            //
            // pnlButtons
            //
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.pnlButtons.Controls.Add(this.btnYes);
            this.pnlButtons.Controls.Add(this.btnNo);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 236);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(520, 64);
            this.pnlButtons.TabIndex = 10;
            //
            // btnYes
            //
            this.btnYes.BorderRadius = 6;
            this.btnYes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.btnYes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnYes.ForeColor = System.Drawing.Color.Black;
            this.btnYes.Location = new System.Drawing.Point(24, 10);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(220, 44);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            //
            // btnNo
            //
            this.btnNo.BorderRadius = 6;
            this.btnNo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(54)))));
            this.btnNo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(266, 10);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(220, 44);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            //
            // lblHeart
            //
            this.lblHeart.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblHeart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblHeart.Location = new System.Drawing.Point(24, 18);
            this.lblHeart.Name = "lblHeart";
            this.lblHeart.Size = new System.Drawing.Size(44, 44);
            this.lblHeart.TabIndex = 0;
            this.lblHeart.Text = "♥";
            //
            // lblHeading
            //
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(74, 24);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(420, 30);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = "Thanks for using PowerPlanTweak!";
            //
            // lblMessage
            //
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMessage.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblMessage.Location = new System.Drawing.Point(24, 74);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(470, 50);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "If you found this tool helpful, please consider making a small donation to supp" +
    "ort development.";
            //
            // lblSupportNote
            //
            this.lblSupportNote.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSupportNote.ForeColor = System.Drawing.Color.White;
            this.lblSupportNote.Location = new System.Drawing.Point(24, 134);
            this.lblSupportNote.Name = "lblSupportNote";
            this.lblSupportNote.Size = new System.Drawing.Size(470, 20);
            this.lblSupportNote.TabIndex = 3;
            this.lblSupportNote.Text = "Your support helps keep this project going!";
            //
            // lblClickHint
            //
            this.lblClickHint.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblClickHint.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblClickHint.Location = new System.Drawing.Point(24, 158);
            this.lblClickHint.Name = "lblClickHint";
            this.lblClickHint.Size = new System.Drawing.Size(470, 20);
            this.lblClickHint.TabIndex = 4;
            this.lblClickHint.Text = "Click \'Yes\' to show your support!";
            //
            // chkDontShow
            //
            this.chkDontShow.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(110)))), ((int)(((byte)(199)))));
            this.chkDontShow.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(110)))), ((int)(((byte)(199)))));
            this.chkDontShow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDontShow.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkDontShow.Location = new System.Drawing.Point(24, 192);
            this.chkDontShow.Name = "chkDontShow";
            this.chkDontShow.Size = new System.Drawing.Size(240, 24);
            this.chkDontShow.TabIndex = 5;
            this.chkDontShow.Text = "Don\'t show this again";
            //
            // picMomo
            //
            this.picMomo.BorderRadius = 6;
            this.picMomo.Location = new System.Drawing.Point(24, 224);
            this.picMomo.Name = "picMomo";
            this.picMomo.Size = new System.Drawing.Size(210, 200);
            this.picMomo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMomo.TabIndex = 6;
            this.picMomo.TabStop = false;
            this.picMomo.Visible = false;
            //
            // picTcb
            //
            this.picTcb.BorderRadius = 6;
            this.picTcb.Location = new System.Drawing.Point(250, 224);
            this.picTcb.Name = "picTcb";
            this.picTcb.Size = new System.Drawing.Size(210, 200);
            this.picTcb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTcb.TabIndex = 7;
            this.picTcb.TabStop = false;
            this.picTcb.Visible = false;
            //
            // lblMomoCaption
            //
            this.lblMomoCaption.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblMomoCaption.Location = new System.Drawing.Point(24, 428);
            this.lblMomoCaption.Name = "lblMomoCaption";
            this.lblMomoCaption.Size = new System.Drawing.Size(210, 20);
            this.lblMomoCaption.TabIndex = 8;
            this.lblMomoCaption.Text = "MoMo";
            this.lblMomoCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMomoCaption.Visible = false;
            //
            // lblTcbCaption
            //
            this.lblTcbCaption.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTcbCaption.Location = new System.Drawing.Point(250, 428);
            this.lblTcbCaption.Name = "lblTcbCaption";
            this.lblTcbCaption.Size = new System.Drawing.Size(210, 20);
            this.lblTcbCaption.TabIndex = 9;
            this.lblTcbCaption.Text = "Techcombank";
            this.lblTcbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTcbCaption.Visible = false;
            //
            // frmSupport
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(520, 342);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSupport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSupport";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMomo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTcb)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnYes;
        private Guna.UI2.WinForms.Guna2Button btnNo;
        private System.Windows.Forms.Label lblHeart;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblSupportNote;
        private System.Windows.Forms.Label lblClickHint;
        private Guna.UI2.WinForms.Guna2CheckBox chkDontShow;
        private Guna.UI2.WinForms.Guna2PictureBox picMomo;
        private Guna.UI2.WinForms.Guna2PictureBox picTcb;
        private System.Windows.Forms.Label lblMomoCaption;
        private System.Windows.Forms.Label lblTcbCaption;
    }
}
