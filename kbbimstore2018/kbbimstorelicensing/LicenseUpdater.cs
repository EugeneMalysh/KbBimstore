using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using com.softwarekey.Client.Licensing;

namespace KbBimstore.KBRevitLicensing
{
    public class LicenseUpdater
    {
        private SampleReadOnlyLicense m_License = null;
        private AboutForm abForm = null;
        private bool m_IsLicenseValid = false;
        private bool m_IsEvaluation = false;
        private string m_LicensedTo = "";
        private string m_LicenseStatus = "";

        private SplashScreenForm splashForm = null;

        public LicenseUpdater()
        {

        }

        public bool IsEvaluation
        {
            get { return m_IsEvaluation; }
        }

        public string LicenseStatus
        {
            get { return m_LicenseStatus; }
        }

        public string LicensedTo
        {
            get { return m_LicensedTo; }
        }

        public bool IsLicenseValid
        {
            get { return m_IsLicenseValid; }
        }

        public SampleReadOnlyLicense License
        {
            get { return m_License; }
        }

        public string LicenseRegistrationInfo
        {
            get
            {
                StringBuilder registrationInfo = new StringBuilder();

                if (m_License != null)
                {
                    if (m_License.Customer.Unregistered ||
                        (!string.IsNullOrEmpty(m_License.Customer.FirstName) && m_License.Customer.FirstName.ToUpperInvariant() == "UNREGISTERED") ||
                        (!string.IsNullOrEmpty(m_License.Customer.LastName) && m_License.Customer.LastName.ToUpperInvariant() == "UNREGISTERED") ||
                        (!string.IsNullOrEmpty(m_License.Customer.CompanyName) && m_License.Customer.CompanyName.ToUpperInvariant() == "UNREGISTERED"))
                    {
                        registrationInfo.Append("Unregistered");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(m_License.Customer.FirstName))
                            registrationInfo.Append(m_License.Customer.FirstName + " ");

                        if (!string.IsNullOrEmpty(m_License.Customer.LastName))
                            registrationInfo.Append(m_License.Customer.LastName + Environment.NewLine);

                        if (!string.IsNullOrEmpty(m_License.Customer.CompanyName))
                            registrationInfo.Append(m_License.Customer.CompanyName);
                    }

                    if (registrationInfo.Length == 0)
                        registrationInfo.Append("Unregistered");
                }
                return registrationInfo.ToString();
            }
        }

        public void UpdateLicense()
        {
            if (abForm != null)
            {
                abForm.Close();
                abForm.Dispose();
            }

            validate();

            abForm = new AboutForm(this);
            DialogResult abFormResult = abForm.ShowDialog();
            if (abFormResult != DialogResult.None)
            {
                validate();
            }
        }

        public void validate()
        {
            m_License = new SampleReadOnlyLicense();
            if (File.Exists(LicenseConfiguration.LicenseFilePath))
            {
                bool m_License_Successful = m_License.LoadFile(LicenseConfiguration.LicenseFilePath);
            }

            if (m_License.Validate())
            {
                DateTime currentDateTime = DateTime.Now;
                DateTime expirationDateTime = m_License.EffectiveEndDate;

                if (expirationDateTime != null)
                {
                    if (expirationDateTime > currentDateTime)
                    {
                        m_IsLicenseValid = true;
                        TimeSpan evaluationTimeSpan = expirationDateTime - currentDateTime;
                        int daysToExpire = evaluationTimeSpan.Days;
                        if (daysToExpire <= 180)
                        {
                            m_LicenseStatus = "Evaluation License Will Expire in " + daysToExpire.ToString() + " days";
                        }
                        else
                        {
                            m_LicenseStatus = "Valid License";
                        }

                        LicenseCustomer curCustomer = m_License.Customer;
                        if (curCustomer != null)
                        {
                            StringBuilder strBld = new StringBuilder();
                            strBld.Append(curCustomer.FirstName + " ");
                            strBld.Append(curCustomer.LastName + ", ");
                            strBld.Append(curCustomer.CompanyName + ", ");
                            strBld.Append(curCustomer.Email + " ");
                            m_LicensedTo = strBld.ToString();
                        }
                        else
                        {
                            m_LicensedTo = "";
                        }
                    }
                    else
                    {
                        m_LicensedTo = "";
                        m_IsLicenseValid = false;
                        m_LicenseStatus = "License Expired";
                    }
                }

            }
            else
            {
                m_LicensedTo = "";
                m_IsLicenseValid = false;
                m_LicenseStatus = "Invalid License";
            }

            if (m_IsLicenseValid)
            {
                var curValidationResult = 2;

                //for different product codes
                if (m_License.ThisProductID == 362368)
                    curValidationResult = 2;
                else if (m_License.ThisProductID == 362392)
                    curValidationResult = 3;
                else if (m_License.ThisProductID == 358384)
                    curValidationResult = 4;

                KbBimstoreApp.setLicenseState(curValidationResult);
            }
            else
            {
                int trialValidationResult = KbBimStoreTrialLicensing.TrialWebServiceInterface.doOperation("validate");

                if (trialValidationResult >= 0)
                {
                    MessageBox.Show("Trial Period of BIMeta Plugin Will Expire in " + trialValidationResult + " Days");
                    KbBimstoreApp.setLicenseState(1);
                }
                else
                {
                    if ((trialValidationResult + 36600) > 0)
                    {
                        MessageBox.Show("Trial Period of BIMeta Plugin Expired. Please Buy BIMeta Plugin");
                    }

                    KbBimstoreApp.setLicenseState(-1);
                }
            }
        }
    }
}
