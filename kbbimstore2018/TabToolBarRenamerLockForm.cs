using System;
using System.Linq;
using System.Windows.Forms;

namespace KbBimstore
{
    public partial class TabToolBarRenamerLockForm : Form
    {
        public TabToolBarRenamerLockForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string pw = this.passwordTextbox.Text.Trim();

            if (!String.IsNullOrEmpty(pw) && !String.IsNullOrWhiteSpace(pw) && !pw.Contains(' ') && pw.Length >= 8)
            {
                KbBimstoreApp.MainTab.Locked = true;
                KbBimstoreApp.MainTab.hxp2 = SecurityUtils.GetPasswordHash(pw);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Password must follow the following rules:\n\n   Contain only letters, numbers and symbols\n    Must be at least 8 characters long\n    Must not contain any whitespaces\n", KbBimstoreApp.MainTab.Name);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
