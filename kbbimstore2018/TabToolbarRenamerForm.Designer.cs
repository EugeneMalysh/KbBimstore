namespace KbBimstore
{
    partial class TabToolbarRenamerForm
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
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.removeTabToolbarButton = new System.Windows.Forms.Button();
            this.unlockSettingsButton = new System.Windows.Forms.Button();
            this.exportSettingsButton = new System.Windows.Forms.Button();
            this.importSettingsButton = new System.Windows.Forms.Button();
            this.lockSettingsButton = new System.Windows.Forms.Button();
            this.addTabToolbarButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(414, 202);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.FillWeight = 180F;
            this.Column1.HeaderText = "Rename:";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 180;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tab / Toolbar:";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column3.HeaderText = "Enabled:";
            this.Column3.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 131;
            // 
            // removeTabToolbarButton
            // 
            this.removeTabToolbarButton.Enabled = false;
            this.removeTabToolbarButton.Location = new System.Drawing.Point(11, 256);
            this.removeTabToolbarButton.Name = "removeTabToolbarButton";
            this.removeTabToolbarButton.Size = new System.Drawing.Size(118, 23);
            this.removeTabToolbarButton.TabIndex = 1;
            this.removeTabToolbarButton.Text = "Remove Tab";
            this.removeTabToolbarButton.UseVisualStyleBackColor = true;
            // 
            // unlockSettingsButton
            // 
            this.unlockSettingsButton.Location = new System.Drawing.Point(165, 256);
            this.unlockSettingsButton.Name = "unlockSettingsButton";
            this.unlockSettingsButton.Size = new System.Drawing.Size(118, 23);
            this.unlockSettingsButton.TabIndex = 2;
            this.unlockSettingsButton.Text = "Unlock Settings";
            this.unlockSettingsButton.UseVisualStyleBackColor = true;
            this.unlockSettingsButton.Click += new System.EventHandler(this.unlockSettingsButton_Click);
            // 
            // exportSettingsButton
            // 
            this.exportSettingsButton.Location = new System.Drawing.Point(313, 256);
            this.exportSettingsButton.Name = "exportSettingsButton";
            this.exportSettingsButton.Size = new System.Drawing.Size(118, 23);
            this.exportSettingsButton.TabIndex = 3;
            this.exportSettingsButton.Text = "Export";
            this.exportSettingsButton.UseVisualStyleBackColor = true;
            this.exportSettingsButton.Click += new System.EventHandler(this.exportSettingsButton_Click);
            // 
            // importSettingsButton
            // 
            this.importSettingsButton.Location = new System.Drawing.Point(313, 285);
            this.importSettingsButton.Name = "importSettingsButton";
            this.importSettingsButton.Size = new System.Drawing.Size(118, 23);
            this.importSettingsButton.TabIndex = 6;
            this.importSettingsButton.Text = "Import";
            this.importSettingsButton.UseVisualStyleBackColor = true;
            this.importSettingsButton.Click += new System.EventHandler(this.importSettingsButton_Click);
            // 
            // lockSettingsButton
            // 
            this.lockSettingsButton.Location = new System.Drawing.Point(165, 285);
            this.lockSettingsButton.Name = "lockSettingsButton";
            this.lockSettingsButton.Size = new System.Drawing.Size(118, 23);
            this.lockSettingsButton.TabIndex = 5;
            this.lockSettingsButton.Text = "Lock Settings";
            this.lockSettingsButton.UseVisualStyleBackColor = true;
            this.lockSettingsButton.Click += new System.EventHandler(this.lockSettingsButton_Click);
            // 
            // addTabToolbarButton
            // 
            this.addTabToolbarButton.Enabled = false;
            this.addTabToolbarButton.Location = new System.Drawing.Point(11, 285);
            this.addTabToolbarButton.Name = "addTabToolbarButton";
            this.addTabToolbarButton.Size = new System.Drawing.Size(118, 23);
            this.addTabToolbarButton.TabIndex = 4;
            this.addTabToolbarButton.Text = "Add Tab";
            this.addTabToolbarButton.UseVisualStyleBackColor = true;
            this.addTabToolbarButton.Click += new System.EventHandler(this.addTabToolbarButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.moveDownButton);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.moveUpButton);
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 238);
            this.panel1.TabIndex = 7;
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(302, 209);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(115, 23);
            this.moveDownButton.TabIndex = 9;
            this.moveDownButton.Text = "Move Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(3, 209);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(115, 23);
            this.moveUpButton.TabIndex = 8;
            this.moveUpButton.Text = "Move Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TabToolbarRenamerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 317);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.importSettingsButton);
            this.Controls.Add(this.lockSettingsButton);
            this.Controls.Add(this.addTabToolbarButton);
            this.Controls.Add(this.exportSettingsButton);
            this.Controls.Add(this.unlockSettingsButton);
            this.Controls.Add(this.removeTabToolbarButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TabToolbarRenamerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Toolbar / Tab Renamer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button removeTabToolbarButton;
        private System.Windows.Forms.Button unlockSettingsButton;
        private System.Windows.Forms.Button exportSettingsButton;
        private System.Windows.Forms.Button importSettingsButton;
        private System.Windows.Forms.Button lockSettingsButton;
        private System.Windows.Forms.Button addTabToolbarButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}