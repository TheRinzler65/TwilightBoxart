namespace TwilightBoxart.GUI
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnBrowseSd = new System.Windows.Forms.Button();
            txtSdRoot = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            txtLog = new System.Windows.Forms.TextBox();
            txtBoxart = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            btnBrowseBoxart = new System.Windows.Forms.Button();
            chkManualBoxartLocation = new System.Windows.Forms.CheckBox();
            btnDetect = new System.Windows.Forms.Button();
            folderBrowseDlg = new System.Windows.Forms.FolderBrowserDialog();
            btnStart = new System.Windows.Forms.Button();
            numWidth = new System.Windows.Forms.NumericUpDown();
            numHeight = new System.Windows.Forms.NumericUpDown();
            lblSize2 = new System.Windows.Forms.Label();
            lblSize1 = new System.Windows.Forms.Label();
            chkBoxartSize = new System.Windows.Forms.CheckBox();
            btnGithub = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            chkAspectRatio = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            SuspendLayout();
            // 
            // btnBrowseSd
            // 
            btnBrowseSd.Location = new System.Drawing.Point(369, 21);
            btnBrowseSd.Margin = new System.Windows.Forms.Padding(2);
            btnBrowseSd.Name = "btnBrowseSd";
            btnBrowseSd.Size = new System.Drawing.Size(89, 35);
            btnBrowseSd.TabIndex = 2;
            btnBrowseSd.Text = "Parcourir...";
            btnBrowseSd.UseVisualStyleBackColor = true;
            btnBrowseSd.Click += btnBrowseSd_Click;
            // 
            // txtSdRoot
            // 
            txtSdRoot.BackColor = System.Drawing.SystemColors.ControlLightLight;
            txtSdRoot.Location = new System.Drawing.Point(7, 28);
            txtSdRoot.Margin = new System.Windows.Forms.Padding(2);
            txtSdRoot.Name = "txtSdRoot";
            txtSdRoot.Size = new System.Drawing.Size(358, 23);
            txtSdRoot.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 10);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(182, 15);
            label1.TabIndex = 2;
            label1.Text = "Emplacement Racine SD / Roms :";
            // 
            // txtLog
            // 
            txtLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            txtLog.Location = new System.Drawing.Point(6, 167);
            txtLog.Margin = new System.Windows.Forms.Padding(4);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(583, 218);
            txtLog.TabIndex = 10;
            // 
            // txtBoxart
            // 
            txtBoxart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            txtBoxart.Location = new System.Drawing.Point(7, 82);
            txtBoxart.Margin = new System.Windows.Forms.Padding(2);
            txtBoxart.Name = "txtBoxart";
            txtBoxart.ReadOnly = true;
            txtBoxart.Size = new System.Drawing.Size(358, 23);
            txtBoxart.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 65);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(151, 15);
            label2.TabIndex = 5;
            label2.Text = "Emplacement de la boxart :";
            // 
            // btnBrowseBoxart
            // 
            btnBrowseBoxart.Enabled = false;
            btnBrowseBoxart.Location = new System.Drawing.Point(369, 77);
            btnBrowseBoxart.Margin = new System.Windows.Forms.Padding(2);
            btnBrowseBoxart.Name = "btnBrowseBoxart";
            btnBrowseBoxart.Size = new System.Drawing.Size(89, 35);
            btnBrowseBoxart.TabIndex = 5;
            btnBrowseBoxart.Text = "Parcourir...";
            btnBrowseBoxart.UseVisualStyleBackColor = true;
            btnBrowseBoxart.Click += btnBrowseBoxart_Click;
            // 
            // chkManualBoxartLocation
            // 
            chkManualBoxartLocation.AutoSize = true;
            chkManualBoxartLocation.Location = new System.Drawing.Point(462, 84);
            chkManualBoxartLocation.Margin = new System.Windows.Forms.Padding(4);
            chkManualBoxartLocation.Name = "chkManualBoxartLocation";
            chkManualBoxartLocation.Size = new System.Drawing.Size(141, 19);
            chkManualBoxartLocation.TabIndex = 6;
            chkManualBoxartLocation.Text = "Définir manuellement";
            chkManualBoxartLocation.UseVisualStyleBackColor = true;
            chkManualBoxartLocation.CheckedChanged += chkManualBoxartLocation_CheckedChanged;
            // 
            // btnDetect
            // 
            btnDetect.Location = new System.Drawing.Point(462, 21);
            btnDetect.Margin = new System.Windows.Forms.Padding(2);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new System.Drawing.Size(89, 35);
            btnDetect.TabIndex = 3;
            btnDetect.Text = "Détecter SD";
            toolTip.SetToolTip(btnDetect, "Will try to detect your (Twilight++) SD card");
            btnDetect.UseVisualStyleBackColor = true;
            btnDetect.Click += btnDetect_Click;
            // 
            // btnStart
            // 
            btnStart.Enabled = false;
            btnStart.Location = new System.Drawing.Point(500, 389);
            btnStart.Margin = new System.Windows.Forms.Padding(2);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(89, 35);
            btnStart.TabIndex = 12;
            btnStart.Text = "Démarrer";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // numWidth
            // 
            numWidth.Location = new System.Drawing.Point(63, 117);
            numWidth.Margin = new System.Windows.Forms.Padding(4);
            numWidth.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new System.Drawing.Size(69, 23);
            numWidth.TabIndex = 7;
            numWidth.Value = new decimal(new int[] { 128, 0, 0, 0 });
            numWidth.Visible = false;
            // 
            // numHeight
            // 
            numHeight.Location = new System.Drawing.Point(202, 117);
            numHeight.Margin = new System.Windows.Forms.Padding(4);
            numHeight.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new System.Drawing.Size(69, 23);
            numHeight.TabIndex = 8;
            numHeight.Value = new decimal(new int[] { 115, 0, 0, 0 });
            numHeight.Visible = false;
            // 
            // lblSize2
            // 
            lblSize2.AutoSize = true;
            lblSize2.Location = new System.Drawing.Point(140, 119);
            lblSize2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblSize2.Name = "lblSize2";
            lblSize2.Size = new System.Drawing.Size(56, 15);
            lblSize2.TabIndex = 13;
            lblSize2.Text = "Hauteur :";
            lblSize2.Visible = false;
            // 
            // lblSize1
            // 
            lblSize1.AutoSize = true;
            lblSize1.Location = new System.Drawing.Point(4, 119);
            lblSize1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblSize1.Name = "lblSize1";
            lblSize1.Size = new System.Drawing.Size(53, 15);
            lblSize1.TabIndex = 14;
            lblSize1.Text = "Largeur :";
            lblSize1.Visible = false;
            // 
            // chkBoxartSize
            // 
            chkBoxartSize.AutoSize = true;
            chkBoxartSize.Location = new System.Drawing.Point(407, 118);
            chkBoxartSize.Margin = new System.Windows.Forms.Padding(4);
            chkBoxartSize.Name = "chkBoxartSize";
            chkBoxartSize.Size = new System.Drawing.Size(176, 19);
            chkBoxartSize.TabIndex = 9;
            chkBoxartSize.Text = "Modifier la taille de la boxart";
            chkBoxartSize.UseVisualStyleBackColor = true;
            chkBoxartSize.CheckedChanged += chkBoxartSize_CheckedChanged;
            // 
            // btnGithub
            // 
            btnGithub.BackgroundImage = Properties.Resources.GitHub_Mark_64px;
            btnGithub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnGithub.Location = new System.Drawing.Point(6, 389);
            btnGithub.Margin = new System.Windows.Forms.Padding(2);
            btnGithub.Name = "btnGithub";
            btnGithub.Size = new System.Drawing.Size(34, 35);
            btnGithub.TabIndex = 11;
            toolTip.SetToolTip(btnGithub, "Visit the Github Repository.");
            btnGithub.UseVisualStyleBackColor = true;
            btnGithub.Click += btnGithub_Click;
            // 
            // chkAspectRatio
            // 
            chkAspectRatio.AutoSize = true;
            chkAspectRatio.Location = new System.Drawing.Point(407, 142);
            chkAspectRatio.Margin = new System.Windows.Forms.Padding(4);
            chkAspectRatio.Name = "chkAspectRatio";
            chkAspectRatio.Size = new System.Drawing.Size(193, 19);
            chkAspectRatio.TabIndex = 15;
            chkAspectRatio.Text = "Ajuster au ratio d'aspect correct";
            toolTip.SetToolTip(chkAspectRatio, "Will resize the boxart to TwilightMenu++ compatible aspect ratio's");
            chkAspectRatio.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(600, 434);
            Controls.Add(chkAspectRatio);
            Controls.Add(btnGithub);
            Controls.Add(chkBoxartSize);
            Controls.Add(lblSize1);
            Controls.Add(lblSize2);
            Controls.Add(numHeight);
            Controls.Add(numWidth);
            Controls.Add(btnStart);
            Controls.Add(btnDetect);
            Controls.Add(chkManualBoxartLocation);
            Controls.Add(btnBrowseBoxart);
            Controls.Add(label2);
            Controls.Add(txtBoxart);
            Controls.Add(txtLog);
            Controls.Add(label1);
            Controls.Add(txtSdRoot);
            Controls.Add(btnBrowseSd);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "TwilightBoxart";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnBrowseSd;
        private System.Windows.Forms.TextBox txtSdRoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtBoxart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseBoxart;
        private System.Windows.Forms.CheckBox chkManualBoxartLocation;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.FolderBrowserDialog folderBrowseDlg;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label lblSize2;
        private System.Windows.Forms.Label lblSize1;
        private System.Windows.Forms.CheckBox chkBoxartSize;
        private System.Windows.Forms.Button btnGithub;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkAspectRatio;
    }
}
