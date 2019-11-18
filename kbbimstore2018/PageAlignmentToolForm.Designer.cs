namespace KbBimstore
{
    partial class PageAlignmentToolForm
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
            this.labelScaleSelect = new System.Windows.Forms.Label();
            this.comboBoxSelectScale = new System.Windows.Forms.ComboBox();
            this.buttonSelectScale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelScaleSelect
            // 
            this.labelScaleSelect.AutoSize = true;
            this.labelScaleSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScaleSelect.Location = new System.Drawing.Point(12, 9);
            this.labelScaleSelect.Name = "labelScaleSelect";
            this.labelScaleSelect.Size = new System.Drawing.Size(121, 17);
            this.labelScaleSelect.TabIndex = 0;
            this.labelScaleSelect.Text = "SELECT SCALE";
            // 
            // comboBoxSelectScale
            // 
            this.comboBoxSelectScale.FormattingEnabled = true;
            this.comboBoxSelectScale.Location = new System.Drawing.Point(139, 9);
            this.comboBoxSelectScale.Name = "comboBoxSelectScale";
            this.comboBoxSelectScale.Size = new System.Drawing.Size(241, 21);
            this.comboBoxSelectScale.TabIndex = 1;
            // 
            // buttonSelectScale
            // 
            this.buttonSelectScale.Location = new System.Drawing.Point(189, 48);
            this.buttonSelectScale.Name = "buttonSelectScale";
            this.buttonSelectScale.Size = new System.Drawing.Size(191, 23);
            this.buttonSelectScale.TabIndex = 2;
            this.buttonSelectScale.Text = "SELECT START AND END POINTS";
            this.buttonSelectScale.UseVisualStyleBackColor = true;
            this.buttonSelectScale.Click += new System.EventHandler(this.buttonSelectScale_Click);
            // 
            // PageAlignmentToolForm
            // 
            this.AcceptButton = this.buttonSelectScale;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 83);
            this.Controls.Add(this.buttonSelectScale);
            this.Controls.Add(this.comboBoxSelectScale);
            this.Controls.Add(this.labelScaleSelect);
            this.MaximumSize = new System.Drawing.Size(400, 110);
            this.MinimumSize = new System.Drawing.Size(400, 110);
            this.Name = "PageAlignmentToolForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PageAlignmentToolForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelScaleSelect;
        private System.Windows.Forms.ComboBox comboBoxSelectScale;
        private System.Windows.Forms.Button buttonSelectScale;
    }
}