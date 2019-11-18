namespace KbBimstore
{
    partial class SetDoorOffsetForm
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
            this.labelOffset = new System.Windows.Forms.Label();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.buttonOffset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelOffset
            // 
            this.labelOffset.AutoSize = true;
            this.labelOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOffset.Location = new System.Drawing.Point(13, 13);
            this.labelOffset.Name = "labelOffset";
            this.labelOffset.Size = new System.Drawing.Size(77, 13);
            this.labelOffset.TabIndex = 0;
            this.labelOffset.Text = "Offset Value";
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(96, 10);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(124, 20);
            this.textBoxOffset.TabIndex = 1;
            this.textBoxOffset.Text = "0";
            this.textBoxOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxOffset_KeyDown);
            this.textBoxOffset.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxOffset_Validating);
            // 
            // buttonOffset
            // 
            this.buttonOffset.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOffset.Location = new System.Drawing.Point(145, 42);
            this.buttonOffset.Name = "buttonOffset";
            this.buttonOffset.Size = new System.Drawing.Size(75, 23);
            this.buttonOffset.TabIndex = 2;
            this.buttonOffset.Text = "Set Offset";
            this.buttonOffset.UseVisualStyleBackColor = true;
            this.buttonOffset.Click += new System.EventHandler(this.buttonOffset_Click);
            // 
            // SetDoorOffsetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 73);
            this.Controls.Add(this.buttonOffset);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.labelOffset);
            this.MaximumSize = new System.Drawing.Size(240, 100);
            this.MinimumSize = new System.Drawing.Size(240, 100);
            this.Name = "SetDoorOffsetForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetDoorOffsetForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOffset;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Button buttonOffset;
    }
}