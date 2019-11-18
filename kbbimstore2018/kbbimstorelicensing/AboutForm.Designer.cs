using com.softwarekey.Client.Licensing;

namespace KbBimstore.KBRevitLicensing
{
    partial class AboutForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.licenseStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.statusListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusImageList = new System.Windows.Forms.ImageList(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.activateOnlineButton = new System.Windows.Forms.Button();
            this.activateManuallyButton = new System.Windows.Forms.Button();
            this.refreshLicenseButton = new System.Windows.Forms.Button();
            this.registrationInfoLabel = new System.Windows.Forms.Label();
            this.statusHeaderLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.registrationInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.deactivateButton = new System.Windows.Forms.Button();
            this.licenseStatusGroupBox.SuspendLayout();
            this.registrationInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // licenseStatusGroupBox
            // 
            this.licenseStatusGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.licenseStatusGroupBox.Controls.Add(this.statusListView);
            this.licenseStatusGroupBox.Location = new System.Drawing.Point(12, 109);
            this.licenseStatusGroupBox.Name = "licenseStatusGroupBox";
            this.licenseStatusGroupBox.Size = new System.Drawing.Size(475, 258);
            this.licenseStatusGroupBox.TabIndex = 6;
            this.licenseStatusGroupBox.TabStop = false;
            this.licenseStatusGroupBox.Text = "License Status";
            // 
            // statusListView
            // 
            this.statusListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.statusColumn});
            this.statusListView.FullRowSelect = true;
            this.statusListView.GridLines = true;
            this.statusListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.statusListView.Location = new System.Drawing.Point(6, 19);
            this.statusListView.Name = "statusListView";
            this.statusListView.ShowItemToolTips = true;
            this.statusListView.Size = new System.Drawing.Size(463, 233);
            this.statusListView.SmallImageList = this.statusImageList;
            this.statusListView.TabIndex = 1;
            this.statusListView.UseCompatibleStateImageBehavior = false;
            this.statusListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 150;
            // 
            // statusColumn
            // 
            this.statusColumn.Text = "Status";
            this.statusColumn.Width = 297;
            // 
            // statusImageList
            // 
            this.statusImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("statusImageList.ImageStream")));
            this.statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.statusImageList.Images.SetKeyName(0, "default_16x16.png");
            this.statusImageList.Images.SetKeyName(1, "error_16x16.png");
            this.statusImageList.Images.SetKeyName(2, "info_16x16.png");
            this.statusImageList.Images.SetKeyName(3, "unavailable_16x16.png");
            this.statusImageList.Images.SetKeyName(4, "warning_16x16.png");
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(412, 373);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "O&K";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // activateOnlineButton
            // 
            this.activateOnlineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activateOnlineButton.Location = new System.Drawing.Point(317, 373);
            this.activateOnlineButton.Name = "activateOnlineButton";
            this.activateOnlineButton.Size = new System.Drawing.Size(89, 23);
            this.activateOnlineButton.TabIndex = 4;
            this.activateOnlineButton.Text = "Activate &Online";
            this.activateOnlineButton.UseVisualStyleBackColor = true;
            this.activateOnlineButton.Click += new System.EventHandler(this.activateOnlineButton_Click);
            // 
            // activateManuallyButton
            // 
            this.activateManuallyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activateManuallyButton.Location = new System.Drawing.Point(207, 373);
            this.activateManuallyButton.Name = "activateManuallyButton";
            this.activateManuallyButton.Size = new System.Drawing.Size(104, 23);
            this.activateManuallyButton.TabIndex = 3;
            this.activateManuallyButton.Text = "Activate &Manually";
            this.activateManuallyButton.UseVisualStyleBackColor = true;
            this.activateManuallyButton.Click += new System.EventHandler(this.activateManuallyButton_Click);
            // 
            // refreshLicenseButton
            // 
            this.refreshLicenseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshLicenseButton.Location = new System.Drawing.Point(12, 373);
            this.refreshLicenseButton.Name = "refreshLicenseButton";
            this.refreshLicenseButton.Size = new System.Drawing.Size(104, 23);
            this.refreshLicenseButton.TabIndex = 1;
            this.refreshLicenseButton.Text = "&Refresh License";
            this.refreshLicenseButton.UseVisualStyleBackColor = true;
            this.refreshLicenseButton.Visible = false;
            this.refreshLicenseButton.Click += new System.EventHandler(this.refreshLicenseButton_Click);
            // 
            // registrationInfoLabel
            // 
            this.registrationInfoLabel.AutoEllipsis = true;
            this.registrationInfoLabel.Location = new System.Drawing.Point(6, 16);
            this.registrationInfoLabel.Name = "registrationInfoLabel";
            this.registrationInfoLabel.Size = new System.Drawing.Size(463, 35);
            this.registrationInfoLabel.TabIndex = 6;
            // 
            // statusHeaderLabel
            // 
            this.statusHeaderLabel.AutoSize = true;
            this.statusHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusHeaderLabel.Location = new System.Drawing.Point(12, 9);
            this.statusHeaderLabel.Name = "statusHeaderLabel";
            this.statusHeaderLabel.Size = new System.Drawing.Size(174, 24);
            this.statusHeaderLabel.TabIndex = 7;
            this.statusHeaderLabel.Text = "BIMeta Plugin";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(15, 33);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(42, 13);
            this.versionLabel.TabIndex = 8;
            this.versionLabel.Text = "Version";
            // 
            // registrationInfoGroupBox
            // 
            this.registrationInfoGroupBox.Controls.Add(this.registrationInfoLabel);
            this.registrationInfoGroupBox.Location = new System.Drawing.Point(12, 49);
            this.registrationInfoGroupBox.Name = "registrationInfoGroupBox";
            this.registrationInfoGroupBox.Size = new System.Drawing.Size(475, 54);
            this.registrationInfoGroupBox.TabIndex = 9;
            this.registrationInfoGroupBox.TabStop = false;
            this.registrationInfoGroupBox.Text = "Registered To:";
            // 
            // deactivateButton
            // 
            this.deactivateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deactivateButton.Location = new System.Drawing.Point(122, 373);
            this.deactivateButton.Name = "deactivateButton";
            this.deactivateButton.Size = new System.Drawing.Size(79, 23);
            this.deactivateButton.TabIndex = 2;
            this.deactivateButton.Text = "&Deactivate";
            this.deactivateButton.UseVisualStyleBackColor = true;
            this.deactivateButton.Visible = false;
            this.deactivateButton.Click += new System.EventHandler(this.deactivateButton_Click);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(499, 408);
            this.Controls.Add(this.deactivateButton);
            this.Controls.Add(this.activateManuallyButton);
            this.Controls.Add(this.registrationInfoGroupBox);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.statusHeaderLabel);
            this.Controls.Add(this.refreshLicenseButton);
            this.Controls.Add(this.activateOnlineButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.licenseStatusGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About BIMeta";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.licenseStatusGroupBox.ResumeLayout(false);
            this.registrationInfoGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox licenseStatusGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button activateOnlineButton;
        private System.Windows.Forms.Button activateManuallyButton;
        private System.Windows.Forms.Button refreshLicenseButton;
        private System.Windows.Forms.Label registrationInfoLabel;
        private System.Windows.Forms.ListView statusListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader statusColumn;
        private System.Windows.Forms.Label statusHeaderLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.GroupBox registrationInfoGroupBox;
        private System.Windows.Forms.ImageList statusImageList;
        private System.Windows.Forms.Button deactivateButton;
    }
}