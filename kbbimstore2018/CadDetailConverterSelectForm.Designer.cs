namespace KbBimstore
{
    partial class CadDetailConverterSelectForm
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
            this.openAutoCADFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SelectAutoCADFile = new System.Windows.Forms.Button();
            this.labelSelectAutoCAD = new System.Windows.Forms.Label();
            this.comboBoxImportView = new System.Windows.Forms.ComboBox();
            this.labelSelectView = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openAutoCADFileDialog
            // 
            this.openAutoCADFileDialog.Filter = "*.dwg|*.DWG";
            this.openAutoCADFileDialog.Title = "SELECT .dwg FILE";
            // 
            // SelectAutoCADFile
            // 
            this.SelectAutoCADFile.Location = new System.Drawing.Point(204, 86);
            this.SelectAutoCADFile.Name = "SelectAutoCADFile";
            this.SelectAutoCADFile.Size = new System.Drawing.Size(82, 23);
            this.SelectAutoCADFile.TabIndex = 21;
            this.SelectAutoCADFile.Text = "SELECT";
            this.SelectAutoCADFile.UseVisualStyleBackColor = true;
            this.SelectAutoCADFile.Click += new System.EventHandler(this.SelectAutoCADFile_Click);
            // 
            // labelSelectAutoCAD
            // 
            this.labelSelectAutoCAD.AutoSize = true;
            this.labelSelectAutoCAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectAutoCAD.Location = new System.Drawing.Point(10, 89);
            this.labelSelectAutoCAD.Name = "labelSelectAutoCAD";
            this.labelSelectAutoCAD.Size = new System.Drawing.Size(188, 17);
            this.labelSelectAutoCAD.TabIndex = 20;
            this.labelSelectAutoCAD.Text = "SELECT AUTOCAD FILE:";
            // 
            // comboBoxImportView
            // 
            this.comboBoxImportView.FormattingEnabled = true;
            this.comboBoxImportView.Location = new System.Drawing.Point(13, 38);
            this.comboBoxImportView.Name = "comboBoxImportView";
            this.comboBoxImportView.Size = new System.Drawing.Size(273, 21);
            this.comboBoxImportView.TabIndex = 22;
            this.comboBoxImportView.SelectedIndexChanged += new System.EventHandler(this.comboBoxImportView_SelectedIndexChanged);
            // 
            // labelSelectView
            // 
            this.labelSelectView.AutoSize = true;
            this.labelSelectView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectView.Location = new System.Drawing.Point(9, 9);
            this.labelSelectView.Name = "labelSelectView";
            this.labelSelectView.Size = new System.Drawing.Size(262, 17);
            this.labelSelectView.TabIndex = 23;
            this.labelSelectView.Text = "SELECT VIEW WHERE TO IMPORT";
            // 
            // CadDetailConverterSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 125);
            this.Controls.Add(this.labelSelectView);
            this.Controls.Add(this.comboBoxImportView);
            this.Controls.Add(this.SelectAutoCADFile);
            this.Controls.Add(this.labelSelectAutoCAD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 150);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 150);
            this.Name = "CadDetailConverterSelectForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "IMPORT FROM AUTOCAD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openAutoCADFileDialog;
        private System.Windows.Forms.Button SelectAutoCADFile;
        private System.Windows.Forms.Label labelSelectAutoCAD;
        private System.Windows.Forms.ComboBox comboBoxImportView;
        private System.Windows.Forms.Label labelSelectView;

    }
}