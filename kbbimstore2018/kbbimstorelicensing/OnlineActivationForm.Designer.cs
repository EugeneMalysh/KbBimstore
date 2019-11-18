namespace KbBimstore.KBRevitLicensing
{
    partial class OnlineActivationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineActivationForm));
            this.activateButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.licenseIDLabel = new System.Windows.Forms.Label();
            this.licenseIDTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.MaskedTextBox();
            this.installationNameLabel = new System.Windows.Forms.Label();
            this.installationNameExampleLabel = new System.Windows.Forms.Label();
            this.installationNameTextBox = new System.Windows.Forms.TextBox();
            this.credentialsGroupBox = new System.Windows.Forms.GroupBox();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.credentialsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activateButton
            // 
            this.activateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activateButton.Location = new System.Drawing.Point(338, 195);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(75, 23);
            this.activateButton.TabIndex = 3;
            this.activateButton.Text = "&Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(257, 195);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "&Cancel";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // licenseIDLabel
            // 
            this.licenseIDLabel.AutoSize = true;
            this.licenseIDLabel.Location = new System.Drawing.Point(35, 22);
            this.licenseIDLabel.Name = "licenseIDLabel";
            this.licenseIDLabel.Size = new System.Drawing.Size(61, 13);
            this.licenseIDLabel.TabIndex = 0;
            this.licenseIDLabel.Text = "License ID:";
            // 
            // licenseIDTextBox
            // 
            this.licenseIDTextBox.Location = new System.Drawing.Point(102, 19);
            this.licenseIDTextBox.MaxLength = 10;
            this.licenseIDTextBox.Name = "licenseIDTextBox";
            this.licenseIDTextBox.Size = new System.Drawing.Size(142, 20);
            this.licenseIDTextBox.TabIndex = 1;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(40, 48);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(102, 45);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(142, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // installationNameLabel
            // 
            this.installationNameLabel.AutoSize = true;
            this.installationNameLabel.Location = new System.Drawing.Point(5, 74);
            this.installationNameLabel.Name = "installationNameLabel";
            this.installationNameLabel.Size = new System.Drawing.Size(91, 13);
            this.installationNameLabel.TabIndex = 4;
            this.installationNameLabel.Text = "Installation Name:";
            // 
            // installationNameExampleLabel
            // 
            this.installationNameExampleLabel.AutoSize = true;
            this.installationNameExampleLabel.Location = new System.Drawing.Point(250, 74);
            this.installationNameExampleLabel.Name = "installationNameExampleLabel";
            this.installationNameExampleLabel.Size = new System.Drawing.Size(145, 13);
            this.installationNameExampleLabel.TabIndex = 6;
            this.installationNameExampleLabel.Text = "(Optional - e.g.: \"My Laptop\")";
            // 
            // installationNameTextBox
            // 
            this.installationNameTextBox.Location = new System.Drawing.Point(102, 71);
            this.installationNameTextBox.MaxLength = 50;
            this.installationNameTextBox.Name = "installationNameTextBox";
            this.installationNameTextBox.Size = new System.Drawing.Size(142, 20);
            this.installationNameTextBox.TabIndex = 5;
            // 
            // credentialsGroupBox
            // 
            this.credentialsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.credentialsGroupBox.Controls.Add(this.installationNameTextBox);
            this.credentialsGroupBox.Controls.Add(this.installationNameExampleLabel);
            this.credentialsGroupBox.Controls.Add(this.installationNameLabel);
            this.credentialsGroupBox.Controls.Add(this.passwordTextBox);
            this.credentialsGroupBox.Controls.Add(this.passwordLabel);
            this.credentialsGroupBox.Controls.Add(this.licenseIDTextBox);
            this.credentialsGroupBox.Controls.Add(this.licenseIDLabel);
            this.credentialsGroupBox.Location = new System.Drawing.Point(12, 92);
            this.credentialsGroupBox.Name = "credentialsGroupBox";
            this.credentialsGroupBox.Size = new System.Drawing.Size(401, 97);
            this.credentialsGroupBox.TabIndex = 1;
            this.credentialsGroupBox.TabStop = false;
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionsLabel.Location = new System.Drawing.Point(6, 16);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(389, 55);
            this.instructionsLabel.TabIndex = 0;
            this.instructionsLabel.Text = resources.GetString("instructionsLabel.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.instructionsLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // OnlineActivationForm
            // 
            this.AcceptButton = this.activateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(425, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.credentialsGroupBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.activateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OnlineActivationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activate Online";
            this.Shown += new System.EventHandler(this.OnlineActivationForm_Shown);
            this.credentialsGroupBox.ResumeLayout(false);
            this.credentialsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button activateButton;
        internal System.Windows.Forms.Button exitButton;
        internal System.Windows.Forms.Label licenseIDLabel;
        internal System.Windows.Forms.TextBox licenseIDTextBox;
        internal System.Windows.Forms.Label passwordLabel;
        internal System.Windows.Forms.MaskedTextBox passwordTextBox;
        internal System.Windows.Forms.Label installationNameLabel;
        private System.Windows.Forms.Label installationNameExampleLabel;
        internal System.Windows.Forms.TextBox installationNameTextBox;
        internal System.Windows.Forms.GroupBox credentialsGroupBox;
        internal System.Windows.Forms.Label instructionsLabel;
        private System.Windows.Forms.GroupBox groupBox1;



    }
}