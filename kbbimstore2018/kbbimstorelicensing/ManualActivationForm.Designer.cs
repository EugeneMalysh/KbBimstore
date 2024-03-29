namespace KbBimstore.KBRevitLicensing
{
    partial class ManualActivationForm
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
            this.activateButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.licenseIDTextBox = new System.Windows.Forms.TextBox();
            this.activationCodeTextBox = new System.Windows.Forms.TextBox();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.licenseIDLabel = new System.Windows.Forms.Label();
            this.generateRequestButton = new System.Windows.Forms.Button();
            this.activationPageButton = new System.Windows.Forms.Button();
            this.requestGroupBox = new System.Windows.Forms.GroupBox();
            this.activationRequestTextBox = new System.Windows.Forms.TextBox();
            this.activationRequestLabel = new System.Windows.Forms.Label();
            this.copyButton = new System.Windows.Forms.Button();
            this.activationCodeLabel = new System.Windows.Forms.Label();
            this.responseGroupBox = new System.Windows.Forms.GroupBox();
            this.pasteButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.inputGroupBox.SuspendLayout();
            this.requestGroupBox.SuspendLayout();
            this.responseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // activateButton
            // 
            this.activateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activateButton.Enabled = false;
            this.activateButton.Location = new System.Drawing.Point(302, 484);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(75, 23);
            this.activateButton.TabIndex = 8;
            this.activateButton.Text = "&Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(10, 48);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password:";
            // 
            // licenseIDTextBox
            // 
            this.licenseIDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.licenseIDTextBox.Location = new System.Drawing.Point(72, 19);
            this.licenseIDTextBox.MaxLength = 10;
            this.licenseIDTextBox.Name = "licenseIDTextBox";
            this.licenseIDTextBox.Size = new System.Drawing.Size(367, 20);
            this.licenseIDTextBox.TabIndex = 1;
            // 
            // activationCodeTextBox
            // 
            this.activationCodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activationCodeTextBox.Enabled = false;
            this.activationCodeTextBox.Location = new System.Drawing.Point(6, 33);
            this.activationCodeTextBox.Multiline = true;
            this.activationCodeTextBox.Name = "activationCodeTextBox";
            this.activationCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.activationCodeTextBox.Size = new System.Drawing.Size(431, 124);
            this.activationCodeTextBox.TabIndex = 0;
            this.activationCodeTextBox.TextChanged += new System.EventHandler(this.activationCodeTextBox_TextChanged);
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputGroupBox.Controls.Add(this.passwordTextBox);
            this.inputGroupBox.Controls.Add(this.passwordLabel);
            this.inputGroupBox.Controls.Add(this.licenseIDTextBox);
            this.inputGroupBox.Controls.Add(this.licenseIDLabel);
            this.inputGroupBox.Location = new System.Drawing.Point(11, 11);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(445, 71);
            this.inputGroupBox.TabIndex = 0;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Step 1: Enter your activation information and click Generate Request:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(72, 45);
            this.passwordTextBox.MaxLength = 15;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(367, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // licenseIDLabel
            // 
            this.licenseIDLabel.AutoSize = true;
            this.licenseIDLabel.Location = new System.Drawing.Point(5, 22);
            this.licenseIDLabel.Name = "licenseIDLabel";
            this.licenseIDLabel.Size = new System.Drawing.Size(61, 13);
            this.licenseIDLabel.TabIndex = 0;
            this.licenseIDLabel.Text = "License ID:";
            // 
            // generateRequestButton
            // 
            this.generateRequestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.generateRequestButton.Location = new System.Drawing.Point(302, 88);
            this.generateRequestButton.Name = "generateRequestButton";
            this.generateRequestButton.Size = new System.Drawing.Size(154, 23);
            this.generateRequestButton.TabIndex = 1;
            this.generateRequestButton.Text = "Generate &Request";
            this.generateRequestButton.UseVisualStyleBackColor = true;
            this.generateRequestButton.Click += new System.EventHandler(this.generateRequestButton_Click);
            // 
            // activationPageButton
            // 
            this.activationPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activationPageButton.Enabled = false;
            this.activationPageButton.Location = new System.Drawing.Point(302, 286);
            this.activationPageButton.Name = "activationPageButton";
            this.activationPageButton.Size = new System.Drawing.Size(154, 23);
            this.activationPageButton.TabIndex = 5;
            this.activationPageButton.Text = "Open Activation &Web Page";
            this.activationPageButton.UseVisualStyleBackColor = true;
            this.activationPageButton.Click += new System.EventHandler(this.activationPageButton_Click);
            // 
            // requestGroupBox
            // 
            this.requestGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requestGroupBox.Controls.Add(this.activationRequestTextBox);
            this.requestGroupBox.Controls.Add(this.activationRequestLabel);
            this.requestGroupBox.Location = new System.Drawing.Point(11, 126);
            this.requestGroupBox.Name = "requestGroupBox";
            this.requestGroupBox.Size = new System.Drawing.Size(445, 154);
            this.requestGroupBox.TabIndex = 3;
            this.requestGroupBox.TabStop = false;
            this.requestGroupBox.Text = "Step 2: Copy the activation request and paste it into the activation web page:";
            // 
            // activationRequestTextBox
            // 
            this.activationRequestTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activationRequestTextBox.Location = new System.Drawing.Point(6, 37);
            this.activationRequestTextBox.Multiline = true;
            this.activationRequestTextBox.Name = "activationRequestTextBox";
            this.activationRequestTextBox.ReadOnly = true;
            this.activationRequestTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.activationRequestTextBox.Size = new System.Drawing.Size(433, 111);
            this.activationRequestTextBox.TabIndex = 0;
            // 
            // activationRequestLabel
            // 
            this.activationRequestLabel.AutoSize = true;
            this.activationRequestLabel.Location = new System.Drawing.Point(7, 21);
            this.activationRequestLabel.Name = "activationRequestLabel";
            this.activationRequestLabel.Size = new System.Drawing.Size(100, 13);
            this.activationRequestLabel.TabIndex = 11;
            this.activationRequestLabel.Text = "Activation Request:";
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyButton.Enabled = false;
            this.copyButton.Location = new System.Drawing.Point(223, 286);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(73, 23);
            this.copyButton.TabIndex = 4;
            this.copyButton.Text = "&Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // activationCodeLabel
            // 
            this.activationCodeLabel.AutoSize = true;
            this.activationCodeLabel.Location = new System.Drawing.Point(7, 17);
            this.activationCodeLabel.Name = "activationCodeLabel";
            this.activationCodeLabel.Size = new System.Drawing.Size(85, 13);
            this.activationCodeLabel.TabIndex = 9;
            this.activationCodeLabel.Text = "Activation Code:";
            // 
            // responseGroupBox
            // 
            this.responseGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.responseGroupBox.Controls.Add(this.activationCodeTextBox);
            this.responseGroupBox.Controls.Add(this.activationCodeLabel);
            this.responseGroupBox.Location = new System.Drawing.Point(13, 315);
            this.responseGroupBox.Name = "responseGroupBox";
            this.responseGroupBox.Size = new System.Drawing.Size(443, 163);
            this.responseGroupBox.TabIndex = 6;
            this.responseGroupBox.TabStop = false;
            this.responseGroupBox.Text = "Step 3: Copy the Activation Code from the web page, paste it below, and click Act" +
    "ivate:";
            // 
            // pasteButton
            // 
            this.pasteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pasteButton.Enabled = false;
            this.pasteButton.Location = new System.Drawing.Point(223, 484);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(73, 23);
            this.pasteButton.TabIndex = 7;
            this.pasteButton.Text = "&Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(383, 484);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(73, 23);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Close";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // ManualActivationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(468, 519);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.activateButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.generateRequestButton);
            this.Controls.Add(this.activationPageButton);
            this.Controls.Add(this.inputGroupBox);
            this.Controls.Add(this.requestGroupBox);
            this.Controls.Add(this.responseGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ManualActivationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activate Manually";
            this.Shown += new System.EventHandler(this.ManualActivationForm_Shown);
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.requestGroupBox.ResumeLayout(false);
            this.requestGroupBox.PerformLayout();
            this.responseGroupBox.ResumeLayout(false);
            this.responseGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button activateButton;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox licenseIDTextBox;
        private System.Windows.Forms.TextBox activationCodeTextBox;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.Button generateRequestButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label licenseIDLabel;
        private System.Windows.Forms.Button activationPageButton;
        private System.Windows.Forms.GroupBox requestGroupBox;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.TextBox activationRequestTextBox;
        private System.Windows.Forms.Label activationRequestLabel;
        private System.Windows.Forms.Label activationCodeLabel;
        private System.Windows.Forms.GroupBox responseGroupBox;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button exitButton;


    }
}