namespace KbBimstore
{
    partial class DesignOptionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonCreateDesignOptions = new System.Windows.Forms.Button();
            this.labelDesignSetsNum = new System.Windows.Forms.Label();
            this.numericUpDownViewNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.labelTitleBlocks = new System.Windows.Forms.Label();
            this.labelScales = new System.Windows.Forms.Label();
            this.comboBoxScales = new System.Windows.Forms.ComboBox();
            this.comboBoxTitleBlocks = new System.Windows.Forms.ComboBox();
            this.dataGridViewDesignOptions = new System.Windows.Forms.DataGridView();
            this.designSetName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.designOptionName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.viewName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.level = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sheetNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sheetNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDesignOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateDesignOptions
            // 
            this.buttonCreateDesignOptions.Location = new System.Drawing.Point(794, 338);
            this.buttonCreateDesignOptions.Name = "buttonCreateDesignOptions";
            this.buttonCreateDesignOptions.Size = new System.Drawing.Size(210, 23);
            this.buttonCreateDesignOptions.TabIndex = 0;
            this.buttonCreateDesignOptions.Text = "CREATE DESIGN OPTIONS";
            this.buttonCreateDesignOptions.UseVisualStyleBackColor = true;
            this.buttonCreateDesignOptions.Click += new System.EventHandler(this.buttonCreateDesignOptions_Click);
            // 
            // labelDesignSetsNum
            // 
            this.labelDesignSetsNum.AutoSize = true;
            this.labelDesignSetsNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesignSetsNum.Location = new System.Drawing.Point(12, 66);
            this.labelDesignSetsNum.Name = "labelDesignSetsNum";
            this.labelDesignSetsNum.Size = new System.Drawing.Size(124, 13);
            this.labelDesignSetsNum.TabIndex = 3;
            this.labelDesignSetsNum.Text = "AMOUNT OF VIEWS";
            // 
            // numericUpDownViewNum
            // 
            this.numericUpDownViewNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownViewNum.Location = new System.Drawing.Point(15, 84);
            this.numericUpDownViewNum.Name = "numericUpDownViewNum";
            this.numericUpDownViewNum.Size = new System.Drawing.Size(93, 20);
            this.numericUpDownViewNum.TabIndex = 4;
            this.numericUpDownViewNum.ValueChanged += new System.EventHandler(this.numericUpDownDesignSetsNum_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(151, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(358, 32);
            this.label5.TabIndex = 10;
            this.label5.Text = "DESIGN OPTIONS MAKER";
            // 
            // labelTitleBlocks
            // 
            this.labelTitleBlocks.AutoSize = true;
            this.labelTitleBlocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleBlocks.Location = new System.Drawing.Point(359, 66);
            this.labelTitleBlocks.Name = "labelTitleBlocks";
            this.labelTitleBlocks.Size = new System.Drawing.Size(155, 13);
            this.labelTitleBlocks.TabIndex = 13;
            this.labelTitleBlocks.Text = "TITLEBLOCK SELECTION";
            // 
            // labelScales
            // 
            this.labelScales.AutoSize = true;
            this.labelScales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScales.Location = new System.Drawing.Point(147, 65);
            this.labelScales.Name = "labelScales";
            this.labelScales.Size = new System.Drawing.Size(119, 13);
            this.labelScales.TabIndex = 14;
            this.labelScales.Text = "SCALE FOR VIEWS";
            // 
            // comboBoxScales
            // 
            this.comboBoxScales.FormattingEnabled = true;
            this.comboBoxScales.Location = new System.Drawing.Point(151, 83);
            this.comboBoxScales.Name = "comboBoxScales";
            this.comboBoxScales.Size = new System.Drawing.Size(180, 21);
            this.comboBoxScales.TabIndex = 23;
            this.comboBoxScales.SelectedIndexChanged += new System.EventHandler(this.comboBoxScales_SelectedIndexChanged);
            // 
            // comboBoxTitleBlocks
            // 
            this.comboBoxTitleBlocks.FormattingEnabled = true;
            this.comboBoxTitleBlocks.Location = new System.Drawing.Point(362, 83);
            this.comboBoxTitleBlocks.Name = "comboBoxTitleBlocks";
            this.comboBoxTitleBlocks.Size = new System.Drawing.Size(369, 21);
            this.comboBoxTitleBlocks.TabIndex = 24;
            this.comboBoxTitleBlocks.SelectedIndexChanged += new System.EventHandler(this.comboBoxTitleBlocks_SelectedIndexChanged);
            // 
            // dataGridViewDesignOptions
            // 
            this.dataGridViewDesignOptions.AllowUserToAddRows = false;
            this.dataGridViewDesignOptions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDesignOptions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDesignOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDesignOptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.designSetName,
            this.designOptionName,
            this.viewName,
            this.level,
            this.sheetNumber,
            this.sheetNames});
            this.dataGridViewDesignOptions.Location = new System.Drawing.Point(15, 119);
            this.dataGridViewDesignOptions.Name = "dataGridViewDesignOptions";
            this.dataGridViewDesignOptions.RowHeadersVisible = false;
            this.dataGridViewDesignOptions.Size = new System.Drawing.Size(989, 210);
            this.dataGridViewDesignOptions.TabIndex = 25;
            this.dataGridViewDesignOptions.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewDesignOptions_CurrentCellDirtyStateChanged);
            // 
            // designSetName
            // 
            this.designSetName.HeaderText = "NAME OF DESIGN SET";
            this.designSetName.Name = "designSetName";
            this.designSetName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.designSetName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.designSetName.Width = 150;
            // 
            // designOptionName
            // 
            this.designOptionName.HeaderText = "NAME OF DESIGN OPTION";
            this.designOptionName.Name = "designOptionName";
            this.designOptionName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.designOptionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.designOptionName.Width = 180;
            // 
            // viewName
            // 
            this.viewName.HeaderText = "NAME OF VIEW";
            this.viewName.Name = "viewName";
            this.viewName.Width = 180;
            // 
            // level
            // 
            this.level.HeaderText = "SELECT LEVEL";
            this.level.Name = "level";
            this.level.Width = 115;
            // 
            // sheetNumber
            // 
            this.sheetNumber.HeaderText = "NUMBER OF SHEET";
            this.sheetNumber.Name = "sheetNumber";
            this.sheetNumber.Width = 180;
            // 
            // sheetNames
            // 
            this.sheetNames.HeaderText = "NAME OF SHEET";
            this.sheetNames.Name = "sheetNames";
            this.sheetNames.Width = 180;
            // 
            // DesignOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 373);
            this.Controls.Add(this.dataGridViewDesignOptions);
            this.Controls.Add(this.comboBoxTitleBlocks);
            this.Controls.Add(this.comboBoxScales);
            this.Controls.Add(this.labelScales);
            this.Controls.Add(this.labelTitleBlocks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownViewNum);
            this.Controls.Add(this.labelDesignSetsNum);
            this.Controls.Add(this.buttonCreateDesignOptions);
            this.MaximumSize = new System.Drawing.Size(1024, 400);
            this.MinimumSize = new System.Drawing.Size(1024, 400);
            this.Name = "DesignOptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DESIGN OPTIONS MAKER";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownViewNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDesignOptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateDesignOptions;
        private System.Windows.Forms.Label labelDesignSetsNum;
        private System.Windows.Forms.NumericUpDown numericUpDownViewNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelTitleBlocks;
        private System.Windows.Forms.Label labelScales;
        private System.Windows.Forms.ComboBox comboBoxScales;
        private System.Windows.Forms.ComboBox comboBoxTitleBlocks;
        private System.Windows.Forms.DataGridView dataGridViewDesignOptions;
        private System.Windows.Forms.DataGridViewComboBoxColumn designSetName;
        private System.Windows.Forms.DataGridViewComboBoxColumn designOptionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewName;
        private System.Windows.Forms.DataGridViewComboBoxColumn level;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn sheetNames;
    }
}

