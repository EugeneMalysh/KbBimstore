using System;
using System.Drawing;
using System.Windows.Forms;
using com.softwarekey.Client.Licensing;

namespace KbBimstore.KBRevitLicensing
{

    internal enum LicenseStatusIcon
    {
        None = -1,
        Ok = 0,
        Error = 1,
        Information = 2,
        Unavailable = 3,
        Warning = 4
    }

    public partial class AboutForm : Form
    {
        private LicenseUpdater m_licenseupdater = null;

        public AboutForm(LicenseUpdater licenseValidator)
        {
            m_licenseupdater = licenseValidator;
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            versionLabel.Text += LicenseConfiguration.ThisProductVersion;

            LoadStatus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activateOnlineButton_Click(object sender, EventArgs e)
        {
            using (OnlineActivationForm activationDialog = new OnlineActivationForm(m_licenseupdater, m_licenseupdater.License))
            {
                activationDialog.ShowDialog(this);
            }
            LoadStatus();
        }

        private void activateManuallyButton_Click(object sender, EventArgs e)
        {
            using (ManualActivationForm activationDialog = new ManualActivationForm(m_licenseupdater, m_licenseupdater.License))
            {
                activationDialog.ShowDialog(this);
            }
            LoadStatus();
        }

        private void deactivateButton_Click(object sender, EventArgs e)
        {
            if (m_licenseupdater.License.DeactivateOnline())
            {
                MessageBox.Show(this, "The license has been deactivated successfully.", "Deactivation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "The license was not deactivated.  Error: (" + m_licenseupdater.License.LastError.ErrorNumber + ")" + m_licenseupdater.License.LastError.ErrorString, "Deactivation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            m_licenseupdater.validate();
            LoadStatus();
        }

        private void refreshLicenseButton_Click(object sender, EventArgs e)
        {
            if (m_licenseupdater.License.RefreshLicense())
            {
                MessageBox.Show(this, "The license has been refreshed successfully.", "License Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "The license was not refreshed.  Error: (" + m_licenseupdater.License.LastError.ErrorNumber + ")" + m_licenseupdater.License.LastError.ErrorString, "License Refresh", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            m_licenseupdater.validate();
            LoadStatus();
        }

        private void LoadStatus()
        {
            registrationInfoLabel.Text = m_licenseupdater.LicenseRegistrationInfo;
            statusListView.Items.Clear();

            ListViewItem licenseItem = new ListViewItem("License");
            licenseItem.ImageIndex = (int)(m_licenseupdater.IsLicenseValid ? LicenseStatusIcon.Ok : LicenseStatusIcon.Error);
            licenseItem.SubItems.Add(m_licenseupdater.LicenseStatus);
            licenseItem.ToolTipText = m_licenseupdater.LicenseStatus;
            statusListView.Items.Add(licenseItem);

            registrationInfoLabel.Text = m_licenseupdater.LicensedTo;

            statusListView.Refresh();

            refreshLicenseButton.Visible = !string.IsNullOrEmpty(m_licenseupdater.License.InstallationID);
            deactivateButton.Visible = !string.IsNullOrEmpty(m_licenseupdater.License.InstallationID);

            if (m_licenseupdater.License.LastError.ErrorNumber == LicenseError.ERROR_PLUS_EVALUATION_INVALID)
            {
                activateOnlineButton.Enabled = false;
                activateManuallyButton.Enabled = false;
                refreshLicenseButton.Enabled = false;
            }
        }
    }
}