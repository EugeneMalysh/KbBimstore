namespace KbBimstore
{
    partial class CadDetailConverterOutputForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelTextStyle = new System.Windows.Forms.Label();
            this.comboBoxTextStyle = new System.Windows.Forms.ComboBox();
            this.dataGridViewLineStyles = new System.Windows.Forms.DataGridView();
            this.columnAutoCadColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRevitLineStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.labelLineStyle = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLineStyles)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTextStyle
            // 
            this.labelTextStyle.AutoSize = true;
            this.labelTextStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextStyle.Location = new System.Drawing.Point(7, 18);
            this.labelTextStyle.Name = "labelTextStyle";
            this.labelTextStyle.Size = new System.Drawing.Size(176, 16);
            this.labelTextStyle.TabIndex = 13;
            this.labelTextStyle.Text = "CHOOSE STYLE FOR TEXT";
            // 
            // comboBoxTextStyle
            // 
            this.comboBoxTextStyle.FormattingEnabled = true;
            this.comboBoxTextStyle.Location = new System.Drawing.Point(189, 13);
            this.comboBoxTextStyle.Name = "comboBoxTextStyle";
            this.comboBoxTextStyle.Size = new System.Drawing.Size(324, 21);
            this.comboBoxTextStyle.TabIndex = 14;
            this.comboBoxTextStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextStyle_SelectedIndexChanged);
            // 
            // dataGridViewLineStyles
            // 
            this.dataGridViewLineStyles.AllowUserToAddRows = false;
            this.dataGridViewLineStyles.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLineStyles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLineStyles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLineStyles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnAutoCadColor,
            this.columnRevitLineStyle});
            this.dataGridViewLineStyles.Location = new System.Drawing.Point(9, 83);
            this.dataGridViewLineStyles.Name = "dataGridViewLineStyles";
            this.dataGridViewLineStyles.RowHeadersVisible = false;
            this.dataGridViewLineStyles.Size = new System.Drawing.Size(504, 197);
            this.dataGridViewLineStyles.TabIndex = 15;
            // 
            // columnAutoCadColor
            // 
            this.columnAutoCadColor.HeaderText = "AUTOCAD LINE STYLE COLOR";
            this.columnAutoCadColor.Name = "columnAutoCadColor";
            this.columnAutoCadColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnAutoCadColor.Width = 215;
            // 
            // columnRevitLineStyle
            // 
            this.columnRevitLineStyle.HeaderText = "REVIT LINE STYLE NAME";
            this.columnRevitLineStyle.Name = "columnRevitLineStyle";
            this.columnRevitLineStyle.Width = 270;
            // 
            // labelLineStyle
            // 
            this.labelLineStyle.AutoSize = true;
            this.labelLineStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLineStyle.Location = new System.Drawing.Point(12, 58);
            this.labelLineStyle.Name = "labelLineStyle";
            this.labelLineStyle.Size = new System.Drawing.Size(496, 16);
            this.labelLineStyle.TabIndex = 16;
            this.labelLineStyle.Text = "CHOOSE AUTOCAD LINE STYLES CONVERSION TO REVIT STYLES BY COLOR";
            // 
            // buttonImport
            // 
            this.buttonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImport.Location = new System.Drawing.Point(438, 296);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 17;
            this.buttonImport.Text = "IMPORT";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExport.Location = new System.Drawing.Point(9, 296);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 18;
            this.buttonExport.Text = "EXPORT";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonConvert
            // 
            this.buttonConvert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConvert.Location = new System.Drawing.Point(221, 296);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 19;
            this.buttonConvert.Text = "CONVERT";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "CadDetailsSettings";
            this.openFileDialog.Filter = "*.xml|";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "*.xml|";
            // 
            // CadDetailConverterOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 333);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.labelLineStyle);
            this.Controls.Add(this.dataGridViewLineStyles);
            this.Controls.Add(this.comboBoxTextStyle);
            this.Controls.Add(this.labelTextStyle);
            this.MaximumSize = new System.Drawing.Size(530, 360);
            this.MinimumSize = new System.Drawing.Size(530, 360);
            this.Name = "CadDetailConverterOutputForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CAD TO REVIT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CadDetailConverterOutputForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CadDetailConverterOutputForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLineStyles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label labelTextStyle;
        private System.Windows.Forms.ComboBox comboBoxTextStyle;
        private System.Windows.Forms.DataGridView dataGridViewLineStyles;
        private System.Windows.Forms.Label labelLineStyle;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAutoCadColor;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnRevitLineStyle;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

