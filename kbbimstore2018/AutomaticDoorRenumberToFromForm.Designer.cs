namespace KbBimstore
{
    partial class AutomaticDoorRenumberToFromForm
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
            this.labelToRoom = new System.Windows.Forms.Label();
            this.labelFromRoom = new System.Windows.Forms.Label();
            this.radioButtonToRoom = new System.Windows.Forms.RadioButton();
            this.radioButtonFromRoom = new System.Windows.Forms.RadioButton();
            this.buttonRenumber = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelToRoom
            // 
            this.labelToRoom.AutoSize = true;
            this.labelToRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelToRoom.Location = new System.Drawing.Point(32, 15);
            this.labelToRoom.Name = "labelToRoom";
            this.labelToRoom.Size = new System.Drawing.Size(58, 13);
            this.labelToRoom.TabIndex = 0;
            this.labelToRoom.Text = "To Room";
            // 
            // labelFromRoom
            // 
            this.labelFromRoom.AutoSize = true;
            this.labelFromRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFromRoom.Location = new System.Drawing.Point(32, 40);
            this.labelFromRoom.Name = "labelFromRoom";
            this.labelFromRoom.Size = new System.Drawing.Size(70, 13);
            this.labelFromRoom.TabIndex = 1;
            this.labelFromRoom.Text = "From Room";
            // 
            // radioButtonToRoom
            // 
            this.radioButtonToRoom.AutoSize = true;
            this.radioButtonToRoom.Location = new System.Drawing.Point(12, 15);
            this.radioButtonToRoom.Name = "radioButtonToRoom";
            this.radioButtonToRoom.Size = new System.Drawing.Size(14, 13);
            this.radioButtonToRoom.TabIndex = 2;
            this.radioButtonToRoom.UseVisualStyleBackColor = true;
            // 
            // radioButtonFromRoom
            // 
            this.radioButtonFromRoom.AutoSize = true;
            this.radioButtonFromRoom.Checked = true;
            this.radioButtonFromRoom.Location = new System.Drawing.Point(12, 40);
            this.radioButtonFromRoom.Name = "radioButtonFromRoom";
            this.radioButtonFromRoom.Size = new System.Drawing.Size(14, 13);
            this.radioButtonFromRoom.TabIndex = 3;
            this.radioButtonFromRoom.TabStop = true;
            this.radioButtonFromRoom.UseVisualStyleBackColor = true;
            // 
            // buttonRenumber
            // 
            this.buttonRenumber.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonRenumber.Location = new System.Drawing.Point(35, 68);
            this.buttonRenumber.Name = "buttonRenumber";
            this.buttonRenumber.Size = new System.Drawing.Size(75, 23);
            this.buttonRenumber.TabIndex = 4;
            this.buttonRenumber.Text = "Renumber";
            this.buttonRenumber.UseVisualStyleBackColor = true;
            // 
            // AutomaticDoorRenumberToFromForm
            // 
            this.AcceptButton = this.buttonRenumber;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 101);
            this.Controls.Add(this.buttonRenumber);
            this.Controls.Add(this.radioButtonFromRoom);
            this.Controls.Add(this.radioButtonToRoom);
            this.Controls.Add(this.labelFromRoom);
            this.Controls.Add(this.labelToRoom);
            this.MaximumSize = new System.Drawing.Size(130, 140);
            this.MinimumSize = new System.Drawing.Size(130, 140);
            this.Name = "AutomaticDoorRenumberToFromForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "AutomaticDoorRenumberToFromForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelToRoom;
        private System.Windows.Forms.Label labelFromRoom;
        private System.Windows.Forms.RadioButton radioButtonToRoom;
        private System.Windows.Forms.RadioButton radioButtonFromRoom;
        private System.Windows.Forms.Button buttonRenumber;
    }
}