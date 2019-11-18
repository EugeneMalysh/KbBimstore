namespace KbBimstore.ToolbarManager.Forms
{
    partial class ToolbarManagerForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.createToolbarButton = new System.Windows.Forms.Button();
            this.applySettingsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.ButtonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolbarColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ButtonColumn,
            this.ToolbarColumn});
            this.dataGridView1.Location = new System.Drawing.Point(12, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(403, 241);
            this.dataGridView1.TabIndex = 0;
            // 
            // createToolbarButton
            // 
            this.createToolbarButton.Location = new System.Drawing.Point(12, 292);
            this.createToolbarButton.Name = "createToolbarButton";
            this.createToolbarButton.Size = new System.Drawing.Size(106, 41);
            this.createToolbarButton.TabIndex = 1;
            this.createToolbarButton.Text = "Create Toolbar";
            this.createToolbarButton.UseVisualStyleBackColor = true;
            this.createToolbarButton.Click += new System.EventHandler(this.createToolbarButton_Click);
            // 
            // applySettingsButton
            // 
            this.applySettingsButton.Location = new System.Drawing.Point(156, 292);
            this.applySettingsButton.Name = "applySettingsButton";
            this.applySettingsButton.Size = new System.Drawing.Size(106, 41);
            this.applySettingsButton.TabIndex = 2;
            this.applySettingsButton.Text = "Apply";
            this.applySettingsButton.UseVisualStyleBackColor = true;
            this.applySettingsButton.Click += new System.EventHandler(this.applySettingsButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(309, 292);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(106, 41);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ButtonColumn
            // 
            this.ButtonColumn.HeaderText = "Button";
            this.ButtonColumn.Name = "ButtonColumn";
            this.ButtonColumn.ReadOnly = true;
            this.ButtonColumn.Width = 63;
            // 
            // ToolbarColumn
            // 
            this.ToolbarColumn.HeaderText = "Toolbar";
            this.ToolbarColumn.Name = "ToolbarColumn";
            this.ToolbarColumn.Width = 49;
            // 
            // ToolbarManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 347);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applySettingsButton);
            this.Controls.Add(this.createToolbarButton);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolbarManagerForm";
            this.Text = "Manage Toolbars";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button createToolbarButton;
        private System.Windows.Forms.Button applySettingsButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ButtonColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ToolbarColumn;
    }
}