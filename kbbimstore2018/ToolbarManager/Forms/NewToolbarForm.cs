using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KbBimstore.ToolbarManager.Forms
{
    public partial class NewToolbarForm : Form
    {

        public string newToolbarName = null;

        public NewToolbarForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string toolbarName = this.toolbarNameTextbox.Text.Trim();

            if (!string.IsNullOrEmpty(toolbarName) && !string.IsNullOrWhiteSpace(toolbarName) && toolbarName.Length < 40)
            {
                newToolbarName = toolbarName;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter a valid toolbar name.", KbBimstoreApp.TAB_NAME);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
