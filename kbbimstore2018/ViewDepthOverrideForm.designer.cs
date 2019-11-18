namespace KbBimstore
{
    partial class ViewDepthOverrideForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numForegroundWeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radForegroundCut = new System.Windows.Forms.RadioButton();
            this.radFroegroundProj = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radMiddle1Cut = new System.Windows.Forms.RadioButton();
            this.radMiddle1Proj = new System.Windows.Forms.RadioButton();
            this.numMiddle1Weight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radBackgroundCut = new System.Windows.Forms.RadioButton();
            this.radBackgroundProj = new System.Windows.Forms.RadioButton();
            this.numBackgroundWeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radMiddle2Cut = new System.Windows.Forms.RadioButton();
            this.radMiddle2Proj = new System.Windows.Forms.RadioButton();
            this.numMiddle2Weight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radMiddle3Cut = new System.Windows.Forms.RadioButton();
            this.radMiddle3Proj = new System.Windows.Forms.RadioButton();
            this.numMiddle3Weight = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numForegroundWeight)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle1Weight)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackgroundWeight)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle2Weight)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle3Weight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(250, 357);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Execute";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(158, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // numForegroundWeight
            // 
            this.numForegroundWeight.Location = new System.Drawing.Point(88, 30);
            this.numForegroundWeight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numForegroundWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numForegroundWeight.Name = "numForegroundWeight";
            this.numForegroundWeight.Size = new System.Drawing.Size(55, 20);
            this.numForegroundWeight.TabIndex = 2;
            this.numForegroundWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numForegroundWeight.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Line Weight:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radForegroundCut);
            this.groupBox1.Controls.Add(this.radFroegroundProj);
            this.groupBox1.Controls.Add(this.numForegroundWeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 59);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Foreground Elements";
            // 
            // radForegroundCut
            // 
            this.radForegroundCut.AutoSize = true;
            this.radForegroundCut.Location = new System.Drawing.Point(180, 33);
            this.radForegroundCut.Name = "radForegroundCut";
            this.radForegroundCut.Size = new System.Drawing.Size(41, 17);
            this.radForegroundCut.TabIndex = 5;
            this.radForegroundCut.Text = "Cut";
            this.radForegroundCut.UseVisualStyleBackColor = true;
            // 
            // radFroegroundProj
            // 
            this.radFroegroundProj.AutoSize = true;
            this.radFroegroundProj.Checked = true;
            this.radFroegroundProj.Location = new System.Drawing.Point(228, 33);
            this.radFroegroundProj.Name = "radFroegroundProj";
            this.radFroegroundProj.Size = new System.Drawing.Size(72, 17);
            this.radFroegroundProj.TabIndex = 4;
            this.radFroegroundProj.TabStop = true;
            this.radFroegroundProj.Text = "Projection";
            this.radFroegroundProj.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radMiddle1Cut);
            this.groupBox2.Controls.Add(this.radMiddle1Proj);
            this.groupBox2.Controls.Add(this.numMiddle1Weight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 59);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Middle1 Elements";
            // 
            // radMiddle1Cut
            // 
            this.radMiddle1Cut.AutoSize = true;
            this.radMiddle1Cut.Location = new System.Drawing.Point(180, 33);
            this.radMiddle1Cut.Name = "radMiddle1Cut";
            this.radMiddle1Cut.Size = new System.Drawing.Size(41, 17);
            this.radMiddle1Cut.TabIndex = 5;
            this.radMiddle1Cut.Text = "Cut";
            this.radMiddle1Cut.UseVisualStyleBackColor = true;
            // 
            // radMiddle1Proj
            // 
            this.radMiddle1Proj.AutoSize = true;
            this.radMiddle1Proj.Checked = true;
            this.radMiddle1Proj.Location = new System.Drawing.Point(228, 33);
            this.radMiddle1Proj.Name = "radMiddle1Proj";
            this.radMiddle1Proj.Size = new System.Drawing.Size(72, 17);
            this.radMiddle1Proj.TabIndex = 4;
            this.radMiddle1Proj.TabStop = true;
            this.radMiddle1Proj.Text = "Projection";
            this.radMiddle1Proj.UseVisualStyleBackColor = true;
            // 
            // numMiddle1Weight
            // 
            this.numMiddle1Weight.Location = new System.Drawing.Point(88, 30);
            this.numMiddle1Weight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMiddle1Weight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMiddle1Weight.Name = "numMiddle1Weight";
            this.numMiddle1Weight.Size = new System.Drawing.Size(55, 20);
            this.numMiddle1Weight.TabIndex = 2;
            this.numMiddle1Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMiddle1Weight.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Line Weight:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radBackgroundCut);
            this.groupBox3.Controls.Add(this.radBackgroundProj);
            this.groupBox3.Controls.Add(this.numBackgroundWeight);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 59);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Background Elements";
            // 
            // radBackgroundCut
            // 
            this.radBackgroundCut.AutoSize = true;
            this.radBackgroundCut.Location = new System.Drawing.Point(180, 33);
            this.radBackgroundCut.Name = "radBackgroundCut";
            this.radBackgroundCut.Size = new System.Drawing.Size(41, 17);
            this.radBackgroundCut.TabIndex = 5;
            this.radBackgroundCut.Text = "Cut";
            this.radBackgroundCut.UseVisualStyleBackColor = true;
            // 
            // radBackgroundProj
            // 
            this.radBackgroundProj.AutoSize = true;
            this.radBackgroundProj.Checked = true;
            this.radBackgroundProj.Location = new System.Drawing.Point(228, 33);
            this.radBackgroundProj.Name = "radBackgroundProj";
            this.radBackgroundProj.Size = new System.Drawing.Size(72, 17);
            this.radBackgroundProj.TabIndex = 4;
            this.radBackgroundProj.TabStop = true;
            this.radBackgroundProj.Text = "Projection";
            this.radBackgroundProj.UseVisualStyleBackColor = true;
            // 
            // numBackgroundWeight
            // 
            this.numBackgroundWeight.Location = new System.Drawing.Point(88, 30);
            this.numBackgroundWeight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numBackgroundWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBackgroundWeight.Name = "numBackgroundWeight";
            this.numBackgroundWeight.Size = new System.Drawing.Size(55, 20);
            this.numBackgroundWeight.TabIndex = 2;
            this.numBackgroundWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBackgroundWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Line Weight:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radMiddle2Cut);
            this.groupBox4.Controls.Add(this.radMiddle2Proj);
            this.groupBox4.Controls.Add(this.numMiddle2Weight);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(12, 158);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(315, 59);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Middle2 Elements";
            // 
            // radMiddle2Cut
            // 
            this.radMiddle2Cut.AutoSize = true;
            this.radMiddle2Cut.Location = new System.Drawing.Point(180, 33);
            this.radMiddle2Cut.Name = "radMiddle2Cut";
            this.radMiddle2Cut.Size = new System.Drawing.Size(41, 17);
            this.radMiddle2Cut.TabIndex = 5;
            this.radMiddle2Cut.Text = "Cut";
            this.radMiddle2Cut.UseVisualStyleBackColor = true;
            // 
            // radMiddle2Proj
            // 
            this.radMiddle2Proj.AutoSize = true;
            this.radMiddle2Proj.Checked = true;
            this.radMiddle2Proj.Location = new System.Drawing.Point(228, 33);
            this.radMiddle2Proj.Name = "radMiddle2Proj";
            this.radMiddle2Proj.Size = new System.Drawing.Size(72, 17);
            this.radMiddle2Proj.TabIndex = 4;
            this.radMiddle2Proj.TabStop = true;
            this.radMiddle2Proj.Text = "Projection";
            this.radMiddle2Proj.UseVisualStyleBackColor = true;
            // 
            // numMiddle2Weight
            // 
            this.numMiddle2Weight.Location = new System.Drawing.Point(88, 30);
            this.numMiddle2Weight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMiddle2Weight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMiddle2Weight.Name = "numMiddle2Weight";
            this.numMiddle2Weight.Size = new System.Drawing.Size(55, 20);
            this.numMiddle2Weight.TabIndex = 2;
            this.numMiddle2Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMiddle2Weight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Line Weight:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radMiddle3Cut);
            this.groupBox5.Controls.Add(this.radMiddle3Proj);
            this.groupBox5.Controls.Add(this.numMiddle3Weight);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(12, 223);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(315, 59);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Middle3 Elements";
            // 
            // radMiddle3Cut
            // 
            this.radMiddle3Cut.AutoSize = true;
            this.radMiddle3Cut.Location = new System.Drawing.Point(180, 33);
            this.radMiddle3Cut.Name = "radMiddle3Cut";
            this.radMiddle3Cut.Size = new System.Drawing.Size(41, 17);
            this.radMiddle3Cut.TabIndex = 5;
            this.radMiddle3Cut.Text = "Cut";
            this.radMiddle3Cut.UseVisualStyleBackColor = true;
            // 
            // radMiddle3Proj
            // 
            this.radMiddle3Proj.AutoSize = true;
            this.radMiddle3Proj.Checked = true;
            this.radMiddle3Proj.Location = new System.Drawing.Point(228, 33);
            this.radMiddle3Proj.Name = "radMiddle3Proj";
            this.radMiddle3Proj.Size = new System.Drawing.Size(72, 17);
            this.radMiddle3Proj.TabIndex = 4;
            this.radMiddle3Proj.TabStop = true;
            this.radMiddle3Proj.Text = "Projection";
            this.radMiddle3Proj.UseVisualStyleBackColor = true;
            // 
            // numMiddle3Weight
            // 
            this.numMiddle3Weight.Location = new System.Drawing.Point(88, 30);
            this.numMiddle3Weight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMiddle3Weight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMiddle3Weight.Name = "numMiddle3Weight";
            this.numMiddle3Weight.Size = new System.Drawing.Size(55, 20);
            this.numMiddle3Weight.TabIndex = 2;
            this.numMiddle3Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numMiddle3Weight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Line Weight:";
            // 
            // ViewDepthOverrideForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(348, 397);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDepthOverrideForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Depth Override Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numForegroundWeight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle1Weight)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackgroundWeight)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle2Weight)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiddle3Weight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numForegroundWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radForegroundCut;
        private System.Windows.Forms.RadioButton radFroegroundProj;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radMiddle1Cut;
        private System.Windows.Forms.RadioButton radMiddle1Proj;
        private System.Windows.Forms.NumericUpDown numMiddle1Weight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radBackgroundCut;
        private System.Windows.Forms.RadioButton radBackgroundProj;
        private System.Windows.Forms.NumericUpDown numBackgroundWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radMiddle2Cut;
        private System.Windows.Forms.RadioButton radMiddle2Proj;
        private System.Windows.Forms.NumericUpDown numMiddle2Weight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radMiddle3Cut;
        private System.Windows.Forms.RadioButton radMiddle3Proj;
        private System.Windows.Forms.NumericUpDown numMiddle3Weight;
        private System.Windows.Forms.Label label5;
    }
}