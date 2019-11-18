using System;
using System.Net;
using System.Windows.Forms;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>Proxy server authentication credentials form</summary>
    public partial class ProxyCredentialsForm : Form
    {
        #region Private Member Variables
        private string m_Username = "";
        private string m_Password = "";
        #endregion

        #region Public Methods
        /// <summary>The proxy authentication credentials entered by the user.</summary>
        public NetworkCredential Credentials
        {
            get { return new NetworkCredential(this.m_Username, this.m_Password); }
        }
        #endregion

        #region Constructors
        /// <summary>ProxyCredentialsForm default constructor</summary>
        public ProxyCredentialsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>Ok button click event handler</summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                MessageBox.Show(this, "Please enter your username.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.usernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                MessageBox.Show(this, "Please enter your password.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.passwordTextBox.Focus();
                return;
            }

            this.m_Username = this.usernameTextBox.Text;
            this.m_Password = this.passwordTextBox.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        /// <summary>Cancel button click event handler</summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
