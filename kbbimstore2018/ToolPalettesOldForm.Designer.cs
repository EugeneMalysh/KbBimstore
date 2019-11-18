namespace KbBimstore
{
    partial class ToolPalettesOldForm
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
            this.treeViewTool = new System.Windows.Forms.TreeView();
            this.pictureBoxPalette = new System.Windows.Forms.PictureBox();
            this.buttonElectrical = new System.Windows.Forms.Button();
            this.buttonPlumbing = new System.Windows.Forms.Button();
            this.buttonMechanical = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalette)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewTool
            // 
            this.treeViewTool.Location = new System.Drawing.Point(12, 41);
            this.treeViewTool.Name = "treeViewTool";
            this.treeViewTool.Size = new System.Drawing.Size(327, 420);
            this.treeViewTool.TabIndex = 0;
            this.treeViewTool.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTool_AfterSelect);
            this.treeViewTool.DoubleClick += new System.EventHandler(this.treeViewTool_DoubleClick);
            // 
            // pictureBoxPalette
            // 
            this.pictureBoxPalette.Location = new System.Drawing.Point(360, 41);
            this.pictureBoxPalette.Name = "pictureBoxPalette";
            this.pictureBoxPalette.Padding = new System.Windows.Forms.Padding(5);
            this.pictureBoxPalette.Size = new System.Drawing.Size(420, 420);
            this.pictureBoxPalette.TabIndex = 2;
            this.pictureBoxPalette.TabStop = false;
            // 
            // buttonElectrical
            // 
            this.buttonElectrical.Location = new System.Drawing.Point(540, 12);
            this.buttonElectrical.Name = "buttonElectrical";
            this.buttonElectrical.Size = new System.Drawing.Size(240, 23);
            this.buttonElectrical.TabIndex = 3;
            this.buttonElectrical.Text = "Electrical Fixtures";
            this.buttonElectrical.UseVisualStyleBackColor = true;
            this.buttonElectrical.Click += new System.EventHandler(this.buttonElectrical_Click);
            // 
            // buttonPlumbing
            // 
            this.buttonPlumbing.Enabled = false;
            this.buttonPlumbing.Location = new System.Drawing.Point(277, 12);
            this.buttonPlumbing.Name = "buttonPlumbing";
            this.buttonPlumbing.Size = new System.Drawing.Size(240, 23);
            this.buttonPlumbing.TabIndex = 4;
            this.buttonPlumbing.Text = "Plumbing Fixtures";
            this.buttonPlumbing.UseVisualStyleBackColor = true;
            this.buttonPlumbing.Click += new System.EventHandler(this.buttonPlumbing_Click);
            // 
            // buttonMechanical
            // 
            this.buttonMechanical.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMechanical.Location = new System.Drawing.Point(12, 12);
            this.buttonMechanical.Name = "buttonMechanical";
            this.buttonMechanical.Size = new System.Drawing.Size(240, 23);
            this.buttonMechanical.TabIndex = 1;
            this.buttonMechanical.Text = "Mechanical Fixtures";
            this.buttonMechanical.UseVisualStyleBackColor = true;
            this.buttonMechanical.Click += new System.EventHandler(this.buttonMechanical_Click);
            // 
            // ToolPalettesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.buttonPlumbing);
            this.Controls.Add(this.buttonElectrical);
            this.Controls.Add(this.pictureBoxPalette);
            this.Controls.Add(this.buttonMechanical);
            this.Controls.Add(this.treeViewTool);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "ToolPalettesForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ToolPalettesForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalette)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewTool;
        private System.Windows.Forms.PictureBox pictureBoxPalette;
        private System.Windows.Forms.Button buttonElectrical;
        private System.Windows.Forms.Button buttonPlumbing;
        private System.Windows.Forms.Button buttonMechanical;
    }
}