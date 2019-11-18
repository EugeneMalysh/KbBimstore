using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KbBimstore
{
    public partial class TabToolBarRenamerUnlockForm : Form
    {
        public TabToolBarRenamerUnlockForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string pw = this.passwordTextbox.Text.Trim();

            if (SecurityUtils.ValidatePassword(pw))
            {
                KbBimstoreApp.MainTab.Locked = false;
                this.DialogResult = DialogResult.OK;

                MessageBox.Show("Successfully unlocked settings!", KbBimstoreApp.MainTab.Name);
            }
            else
            {
                MessageBox.Show("Password does not match. Please try again.", KbBimstoreApp.MainTab.Name);
            }
        }
    }
}
