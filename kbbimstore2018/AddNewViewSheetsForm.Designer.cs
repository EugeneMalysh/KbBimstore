namespace KbBimstore
{
    partial class AddNewViewSheetsForm
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
            this.buttonCreateProj = new System.Windows.Forms.Button();
            this.dataGridViewSheets = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chooseScale = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.chooseTemplate = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.chooseTitleBlock = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.aDDSHEETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETESHEETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSheets)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreateProj
            // 
            this.buttonCreateProj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateProj.Location = new System.Drawing.Point(905, 260);
            this.buttonCreateProj.Name = "buttonCreateProj";
            this.buttonCreateProj.Size = new System.Drawing.Size(101, 30);
            this.buttonCreateProj.TabIndex = 1;
            this.buttonCreateProj.Text = "CREATE";
            this.buttonCreateProj.UseVisualStyleBackColor = true;
            this.buttonCreateProj.Click += new System.EventHandler(this.buttonAddViews_Click);
            // 
            // dataGridViewSheets
            // 
            this.dataGridViewSheets.AllowUserToAddRows = false;
            this.dataGridViewSheets.AllowUserToDeleteRows = false;
            this.dataGridViewSheets.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSheets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.chooseScale,
            this.chooseTemplate,
            this.chooseTitleBlock});
            this.dataGridViewSheets.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridViewSheets.Location = new System.Drawing.Point(14, 14);
            this.dataGridViewSheets.Name = "dataGridViewSheets";
            this.dataGridViewSheets.RowHeadersVisible = false;
            this.dataGridViewSheets.Size = new System.Drawing.Size(992, 240);
            this.dataGridViewSheets.TabIndex = 16;
            this.dataGridViewSheets.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMainSheet_CellValidated);
            this.dataGridViewSheets.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewMainSheet_CellValidating);
            this.dataGridViewSheets.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewMainSheet_DataError);
            this.dataGridViewSheets.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewMainSheet_EditingControlShowing);
            this.dataGridViewSheets.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridViewMainSheet_PreviewKeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "SHEET NUMBER";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "SHEET NAME";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // chooseScale
            // 
            this.chooseScale.HeaderText = "CHOOSE SCALE";
            this.chooseScale.Name = "chooseScale";
            // 
            // chooseTemplate
            // 
            this.chooseTemplate.AutoComplete = false;
            this.chooseTemplate.HeaderText = "CHOOSE TEMPLATE";
            this.chooseTemplate.Name = "chooseTemplate";
            this.chooseTemplate.Width = 285;
            // 
            // chooseTitleBlock
            // 
            this.chooseTitleBlock.HeaderText = "CHOOSE TITLE BLOCK";
            this.chooseTitleBlock.Name = "chooseTitleBlock";
            this.chooseTitleBlock.Width = 285;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDSHEETToolStripMenuItem,
            this.dELETESHEETToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(151, 48);
            // 
            // aDDSHEETToolStripMenuItem
            // 
            this.aDDSHEETToolStripMenuItem.Name = "aDDSHEETToolStripMenuItem";
            this.aDDSHEETToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.aDDSHEETToolStripMenuItem.Text = "ADD SHEET";
            this.aDDSHEETToolStripMenuItem.Click += new System.EventHandler(this.addView_Click);
            // 
            // dELETESHEETToolStripMenuItem
            // 
            this.dELETESHEETToolStripMenuItem.Name = "dELETESHEETToolStripMenuItem";
            this.dELETESHEETToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.dELETESHEETToolStripMenuItem.Text = "DELETE SHEET";
            this.dELETESHEETToolStripMenuItem.Click += new System.EventHandler(this.deleteView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Right Click To Add Or Delete Sheets";
            // 
            // AddNewViewSheetsForm
            // 
            this.AcceptButton = this.buttonCreateProj;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 291);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewSheets);
            this.Controls.Add(this.buttonCreateProj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 330);
            this.Name = "AddNewViewSheetsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ADD NEW SHEETS AND VIEWS";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSheets)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateProj;
        private System.Windows.Forms.DataGridView dataGridViewSheets;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem aDDSHEETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETESHEETToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn chooseScale;
        private System.Windows.Forms.DataGridViewComboBoxColumn chooseTemplate;
        private System.Windows.Forms.DataGridViewComboBoxColumn chooseTitleBlock;
        private System.Windows.Forms.Label label1;
    }
}