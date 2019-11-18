using System;
using System.IO;

using KbBimstore.KbBimStoreTrialLicensing;

namespace KbBimstore.KBRevitLicensing
{
    public class LicenseStarter
    {
        private static bool isFirstTime = true;
        private static int trialValidationResult = -90000009;

        public LicenseStarter()
        {

        }

        public void startlicensecheck()
        {
            SplashScreenForm splashForm = new SplashScreenForm();
            splashForm.Show();

            SampleReadOnlyLicense m_License = new SampleReadOnlyLicense();
            if (File.Exists(LicenseConfiguration.LicenseFilePath))
            {
                bool m_License_Successful = m_License.LoadFile(LicenseConfiguration.LicenseFilePath);
            }

            splashForm.Close();
            splashForm.Dispose();

            int curValidationResult = -1;
            if (m_License.Validate())
            {
                DateTime currentDateTime = DateTime.Now;
                DateTime expirationDateTime = m_License.EffectiveEndDate;

                if (expirationDateTime != null)
                {
                    if (expirationDateTime > currentDateTime)
                    {
                        //for different product codes
                        if (m_License.ThisProductID == 362368)
                            curValidationResult = 2;
                        else if (m_License.ThisProductID == 362392)
                            curValidationResult = 3;
                        else if (m_License.ThisProductID == 358384)
                            curValidationResult = 4;
                    }
                }
            }

            if (curValidationResult < 0)
            {

                trialValidationResult = TrialWebServiceInterface.doOperation("validate");

                if (trialValidationResult >= 0)
                {
                    curValidationResult = 1;
                    if (isFirstTime)
                    {
                        AutoClosingMessageBox.Show("Trial Period of BIMeta Plugin Will Expire in " + trialValidationResult + " Days", "BIMeta", 3000);
                        isFirstTime = false;
                    }
                }
                else
                {
                    curValidationResult = -1;
                    if ((trialValidationResult + 36600) > 0)
                    {
                        if (isFirstTime)
                        {
                            AutoClosingMessageBox.Show("Trial Period of BIMeta Plugin Expired. Please Buy BIMeta Plugin", "BIMeta", 3000);
                            isFirstTime = false;
                        }
                    }
                }
            }

            KbBimstoreApp.setLicenseState(curValidationResult);
        }
    }
}
