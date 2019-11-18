using System;
using System.Windows.Forms;
using com.softwarekey.Client.Licensing;

namespace KbBimstore.KBRevitLicensing
{
    public partial class OnlineActivationForm : Form
    {
        private LicenseUpdater m_licenseupdater;
        private SampleLicense m_license;

        public OnlineActivationForm(LicenseUpdater licenseUpdater, SampleLicense license)
        {
            m_licenseupdater = licenseUpdater;
            m_license = license;

            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void activateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 licenseId = 0;
                string password = passwordTextBox.Text;
                bool successful = false;

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

                m_license.InstallationName = installationNameTextBox.Text;

                successful = m_license.ActivateOnline(licenseId, password);

                if (successful)
                {
                    MessageBox.Show(this, "Activation Successful!", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_licenseupdater.validate();
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "Activation Failed." + Environment.NewLine + Environment.NewLine + m_license.LastError.ToString(), "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Activation Failed." + Environment.NewLine + Environment.NewLine + ex.ToString(), "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnlineActivationForm_Shown(object sender, EventArgs e)
        {
            if (m_license.LicenseID > 0)
            {
                licenseIDTextBox.Text = m_license.LicenseID.ToString();
                passwordTextBox.Focus();
            }
            else
            {
                licenseIDTextBox.Focus();
            }
        }
    }
}
