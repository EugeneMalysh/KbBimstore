namespace KbBimstore
{
    partial class SuperFilterForm
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
            this.treeViewFilter = new System.Windows.Forms.TreeView();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewFilter
            // 
            this.treeViewFilter.CheckBoxes = true;
            this.treeViewFilter.Location = new System.Drawing.Point(12, 12);
            this.treeViewFilter.Name = "treeViewFilter";
            this.treeViewFilter.Size = new System.Drawing.Size(328, 340);
            this.treeViewFilter.TabIndex = 0;
            this.treeViewFilter.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFilter_BeforeCheck);
            this.treeViewFilter.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFilter_AfterCheck);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(265, 358);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonFilter.TabIndex = 1;
            this.buttonFilter.Text = "FILTER";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // SuperFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 393);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.treeViewFilter);
            this.MaximumSize = new System.Drawing.Size(360, 420);
            this.MinimumSize = new System.Drawing.Size(360, 420);
            this.Name = "SuperFilterForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SUPER FILTER";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewFilter;
        private System.Windows.Forms.Button buttonFilter;
    }
}