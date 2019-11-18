namespace KbBimstore
{
    partial class AllDetailsImportsForm
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
            this.dataGridViewDocViews = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.View = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.panelPreview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDocViews)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDocViews
            // 
            this.dataGridViewDocViews.AllowUserToAddRows = false;
            this.dataGridViewDocViews.AllowUserToDeleteRows = false;
            this.dataGridViewDocViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDocViews.ColumnHeadersVisible = false;
            this.dataGridViewDocViews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.View});
            this.dataGridViewDocViews.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewDocViews.Name = "dataGridViewDocViews";
            this.dataGridViewDocViews.RowHeadersVisible = false;
            this.dataGridViewDocViews.Size = new System.Drawing.Size(447, 460);
            this.dataGridViewDocViews.TabIndex = 0;
            this.dataGridViewDocViews.SelectionChanged += new System.EventHandler(this.dataGridViewDocViews_SelectionChanged);
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 48;
            // 
            // View
            // 
            this.View.HeaderText = "View";
            this.View.Name = "View";
            this.View.ReadOnly = true;
            this.View.Width = 395;
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(385, 486);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 1;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(473, 12);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(500, 500);
            this.panelPreview.TabIndex = 2;
            // 
            // AllDetailsImportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 521);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.dataGridViewDocViews);
            this.MaximumSize = new System.Drawing.Size(1000, 560);
            this.MinimumSize = new System.Drawing.Size(1000, 560);
            this.Name = "AllDetailsImportsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDocViews)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDocViews;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn View;
        private System.Windows.Forms.Panel panelPreview;
    }
}