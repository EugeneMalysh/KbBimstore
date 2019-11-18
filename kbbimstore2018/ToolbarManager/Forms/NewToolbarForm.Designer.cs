namespace KbBimstore.ToolbarManager.Forms
{
    partial class NewToolbarForm
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
            this.toolbarNameLabel = new System.Windows.Forms.Label();
            this.toolbarNameTextbox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toolbarNameLabel
            // 
            this.toolbarNameLabel.AutoSize = true;
            this.toolbarNameLabel.Location = new System.Drawing.Point(5, 28);
            this.toolbarNameLabel.Name = "toolbarNameLabel";
            this.toolbarNameLabel.Size = new System.Drawing.Size(92, 15);
            this.toolbarNameLabel.TabIndex = 0;
            this.toolbarNameLabel.Text = "Toolbar Name: ";
            // 
            // toolbarNameTextbox
            // 
            this.toolbarNameTextbox.Location = new System.Drawing.Point(103, 25);
            this.toolbarNameTextbox.Name = "toolbarNameTextbox";
            this.toolbarNameTextbox.Size = new System.Drawing.Size(196, 21);
            this.toolbarNameTextbox.TabIndex = 1;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(103, 65);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(86, 36);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(213, 65);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 36);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // NewToolbarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 113);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.toolbarNameTextbox);
            this.Controls.Add(this.toolbarNameLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewToolbarForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Toolbar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label toolbarNameLabel;
        private System.Windows.Forms.TextBox toolbarNameTextbox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
    }
}