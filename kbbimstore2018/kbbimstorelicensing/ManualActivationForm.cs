using System;
using System.Windows.Forms;

namespace KbBimstore.KBRevitLicensing
{
    public partial class ManualActivationForm : Form
    {
        private LicenseUpdater m_licenseupdater;
        private SampleLicense m_License;

        public ManualActivationForm(LicenseUpdater licenseUpdater, SampleLicense license)
        {
            m_licenseupdater = licenseUpdater;
            m_License = license;
            InitializeComponent();
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            string lfContent = "";
            bool successful = false;

            activateButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            successful = m_License.ProcessActivateInstallationLicenseFileResponse(activationCodeTextBox.Text, ref lfContent);

            activateButton.Enabled = true;
            Cursor = Cursors.Default;

            if (!successful)
            {
                MessageBox.Show(this, "Activation Failed.  Reason: " + m_License.LastError.ErrorString, "Manual Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (m_License.SaveLicenseFile(lfContent))
            {
                MessageBox.Show(this, "Activation Successful!", "Manual Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_License.ResetSessionCode();
                m_licenseupdater.validate();
                Close();
            }
            else
            {
                MessageBox.Show(this, "Activation Failed.  Reason: " + m_License.LastError.ErrorString, "Manual Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void activationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            activateButton.Enabled = !string.IsNullOrEmpty(activationCodeTextBox.Text);
        }

        private void activationPageButton_Click(object sender, EventArgs e)
        {
            WebServiceHelper.OpenManualRequestUrl();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(activationRequestTextBox.Text, false, 100, 10);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void generateRequestButton_Click(object sender, EventArgs e)
        {
            Int32 licenseId = 0;
            string password = passwordTextBox.Text;
            string request = "";

            m_License.ResetSessionCode();
            //TODO: store the value of m_license.CurrentSessionCode in some hidden location

            if (string.IsNullOrEmpty(licenseIDTextBox.Text))
            {
                MessageBox.Show(this, "Please enter a License ID.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                licenseIDTextBox.Focus();
                return;
            }

            if (!Int32.TryParse(licenseIDTextBox.Text, out licenseId))
            {
                MessageBox.Show(this, "The License ID may only contain numbers.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                licenseIDTextBox.Focus();
                return;
            }

            if (!Int32.TryParse(licenseIDTextBox.Text, out licenseId))
            {
                MessageBox.Show(this, "The License ID may only contain numbers.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                licenseIDTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show(this, "Enter your password.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                passwordTextBox.Focus();
                return;
            }

            generateRequestButton.Enabled = false;
            Cursor = Cursors.WaitCursor;

            request = m_License.GetActivationInstallationLicenseFileRequest(licenseId, password);
            activationRequestTextBox.Text = request;

            generateRequestButton.Enabled = true;
            Cursor = Cursors.Default;

            copyButton.Enabled = true;
            pasteButton.Enabled = true;
            activationPageButton.Enabled = true;
            activationCodeTextBox.Enabled = true;
        }

        private void ManualActivationForm_Shown(object sender, EventArgs e)
        {
            if (m_License.LicenseID > 0)
            {
                licenseIDTextBox.Text = m_License.LicenseID.ToString();
                passwordTextBox.Focus();
            }
            else
            {
                licenseIDTextBox.Focus();
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            activationCodeTextBox.Text = Clipboard.GetText();
        }
    }
}
