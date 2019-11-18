namespace KbBimstore
{
    partial class AutoSynchronizeForm
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
            this.labelAutosyncHr = new System.Windows.Forms.Label();
            this.numericUpDownAutosaveHr = new System.Windows.Forms.NumericUpDown();
            this.labelAutosyncMin = new System.Windows.Forms.Label();
            this.numericUpDownAutosaveMin = new System.Windows.Forms.NumericUpDown();
            this.buttonAutosyncStart = new System.Windows.Forms.Button();
            this.numericUpDownAutosyncMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownAutosyncHr = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.autoSaveOffRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.autoSaveOnRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.autoSyncOffRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.autoSyncOnRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosaveHr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosaveMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosyncMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosyncHr)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAutosyncHr
            // 
            this.labelAutosyncHr.AutoSize = true;
            this.labelAutosyncHr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutosyncHr.Location = new System.Drawing.Point(77, 26);
            this.labelAutosyncHr.Name = "labelAutosyncHr";
            this.labelAutosyncHr.Size = new System.Drawing.Size(20, 13);
            this.labelAutosyncHr.TabIndex = 1;
            this.labelAutosyncHr.Text = "Hr";
            // 
            // numericUpDownAutosaveHr
            // 
            this.numericUpDownAutosaveHr.Location = new System.Drawing.Point(99, 23);
            this.numericUpDownAutosaveHr.Name = "numericUpDownAutosaveHr";
            this.numericUpDownAutosaveHr.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAutosaveHr.TabIndex = 2;
            // 
            // labelAutosyncMin
            // 
            this.labelAutosyncMin.AutoSize = true;
            this.labelAutosyncMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutosyncMin.Location = new System.Drawing.Point(157, 25);
            this.labelAutosyncMin.Name = "labelAutosyncMin";
            this.labelAutosyncMin.Size = new System.Drawing.Size(27, 13);
            this.labelAutosyncMin.TabIndex = 3;
            this.labelAutosyncMin.Text = "Min";
            // 
            // numericUpDownAutosaveMin
            // 
            this.numericUpDownAutosaveMin.Location = new System.Drawing.Point(187, 23);
            this.numericUpDownAutosaveMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownAutosaveMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutosaveMin.Name = "numericUpDownAutosaveMin";
            this.numericUpDownAutosaveMin.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAutosaveMin.TabIndex = 4;
            this.numericUpDownAutosaveMin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // buttonAutosyncStart
            // 
            this.buttonAutosyncStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAutosyncStart.Location = new System.Drawing.Point(197, 130);
            this.buttonAutosyncStart.Name = "buttonAutosyncStart";
            this.buttonAutosyncStart.Size = new System.Drawing.Size(75, 23);
            this.buttonAutosyncStart.TabIndex = 5;
            this.buttonAutosyncStart.Text = "OK";
            this.buttonAutosyncStart.UseVisualStyleBackColor = true;
            this.buttonAutosyncStart.Click += new System.EventHandler(this.buttonAutosyncStart_Click);
            // 
            // numericUpDownAutosyncMin
            // 
            this.numericUpDownAutosyncMin.Location = new System.Drawing.Point(187, 25);
            this.numericUpDownAutosyncMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownAutosyncMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutosyncMin.Name = "numericUpDownAutosyncMin";
            this.numericUpDownAutosyncMin.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAutosyncMin.TabIndex = 10;
            this.numericUpDownAutosyncMin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(157, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Min";
            // 
            // numericUpDownAutosyncHr
            // 
            this.numericUpDownAutosyncHr.Location = new System.Drawing.Point(99, 25);
            this.numericUpDownAutosyncHr.Name = "numericUpDownAutosyncHr";
            this.numericUpDownAutosyncHr.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAutosyncHr.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hr";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.autoSaveOffRadioButton);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.autoSaveOnRadioButton);
            this.groupBox1.Controls.Add(this.numericUpDownAutosaveHr);
            this.groupBox1.Controls.Add(this.labelAutosyncHr);
            this.groupBox1.Controls.Add(this.labelAutosyncMin);
            this.groupBox1.Controls.Add(this.numericUpDownAutosaveMin);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 53);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto-Save";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Off";
            // 
            // autoSaveOffRadioButton
            // 
            this.autoSaveOffRadioButton.AutoSize = true;
            this.autoSaveOffRadioButton.Location = new System.Drawing.Point(48, 32);
            this.autoSaveOffRadioButton.Name = "autoSaveOffRadioButton";
            this.autoSaveOffRadioButton.Size = new System.Drawing.Size(14, 13);
            this.autoSaveOffRadioButton.TabIndex = 6;
            this.autoSaveOffRadioButton.TabStop = true;
            this.autoSaveOffRadioButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "On";
            // 
            // autoSaveOnRadioButton
            // 
            this.autoSaveOnRadioButton.AutoSize = true;
            this.autoSaveOnRadioButton.Checked = true;
            this.autoSaveOnRadioButton.Location = new System.Drawing.Point(17, 32);
            this.autoSaveOnRadioButton.Name = "autoSaveOnRadioButton";
            this.autoSaveOnRadioButton.Size = new System.Drawing.Size(14, 13);
            this.autoSaveOnRadioButton.TabIndex = 5;
            this.autoSaveOnRadioButton.TabStop = true;
            this.autoSaveOnRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.autoSyncOffRadioButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.autoSyncOnRadioButton);
            this.groupBox2.Controls.Add(this.numericUpDownAutosyncHr);
            this.groupBox2.Controls.Add(this.numericUpDownAutosyncMin);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 58);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Auto-Sync";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Off";
            // 
            // autoSyncOffRadioButton
            // 
            this.autoSyncOffRadioButton.AutoSize = true;
            this.autoSyncOffRadioButton.Location = new System.Drawing.Point(48, 34);
            this.autoSyncOffRadioButton.Name = "autoSyncOffRadioButton";
            this.autoSyncOffRadioButton.Size = new System.Drawing.Size(14, 13);
            this.autoSyncOffRadioButton.TabIndex = 8;
            this.autoSyncOffRadioButton.TabStop = true;
            this.autoSyncOffRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "On";
            // 
            // autoSyncOnRadioButton
            // 
            this.autoSyncOnRadioButton.AutoSize = true;
            this.autoSyncOnRadioButton.Checked = true;
            this.autoSyncOnRadioButton.Location = new System.Drawing.Point(17, 34);
            this.autoSyncOnRadioButton.Name = "autoSyncOnRadioButton";
            this.autoSyncOnRadioButton.Size = new System.Drawing.Size(14, 13);
            this.autoSyncOnRadioButton.TabIndex = 7;
            this.autoSyncOnRadioButton.TabStop = true;
            this.autoSyncOnRadioButton.UseVisualStyleBackColor = true;
            // 
            // AutoSynchronizeForm
            // 
            this.AcceptButton = this.buttonAutosyncStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonAutosyncStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(300, 200);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "AutoSynchronizeForm";
            this.Text = "AutoSync";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosaveHr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosaveMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosyncMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutosyncHr)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelAutosyncHr;
        private System.Windows.Forms.NumericUpDown numericUpDownAutosaveHr;
        private System.Windows.Forms.Label labelAutosyncMin;
        private System.Windows.Forms.NumericUpDown numericUpDownAutosaveMin;
        private System.Windows.Forms.Button buttonAutosyncStart;
        private System.Windows.Forms.NumericUpDown numericUpDownAutosyncMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownAutosyncHr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton autoSaveOffRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton autoSaveOnRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton autoSyncOffRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton autoSyncOnRadioButton;
    }
}