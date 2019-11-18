using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using com.softwarekey.Client.Licensing;
using com.softwarekey.Client.Utils;
using com.softwarekey.Client.WebService.XmlActivationService;
using com.softwarekey.Client.WebService.XmlLicenseFileService;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>A sample <see cref="License"/>
    /// <see href="http://www.softwarekey.com/help/plus5/#StartTopic=Content/PLUSManaged_License_Implementation.htm">implementation class</see>,
    /// which uses read-only licenses.</summary>
    public class SampleReadOnlyLicense : License, SampleLicense
    {
        #region Member Variables
        private static WebServiceHelper m_WebServiceHelper = new WebServiceHelper();
        #endregion

        #region Constructors
        /// <summary>Creates a new <see cref="SampleReadOnlyLicense"/> object.</summary>
        public SampleReadOnlyLicense()
            : base(LicenseConfiguration.EncryptionKey, true, true, LicenseConfiguration.ThisProductID, LicenseConfiguration.ThisProductVersion, LicenseConfiguration.SystemIdentifierAlgorithms)
        {
        }
        #endregion

        #region Private Properties
        /// <summary>Gets whether a license refresh attempt is due.</summary>
        private bool IsRefreshLicenseAttemptDue
        {
            get
            {
                //Calculate the date difference between signature date and the current date
                TimeSpan dateDiff = DateTime.UtcNow.Subtract(this.SignatureDate);

                if (LicenseConfiguration.RefreshLicenseAlwaysRequired ||
                    (LicenseConfiguration.RefreshLicenseAttemptFrequency > 0 &&
                    (dateDiff.TotalDays > LicenseConfiguration.RefreshLicenseAttemptFrequency ||
                    (LicenseConfiguration.RefreshLicenseRequireFrequency > 0 && dateDiff.TotalDays > LicenseConfiguration.RefreshLicenseRequireFrequency))))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>Gets whether a license refresh is required.</summary>
        private bool IsRefreshLicenseRequired
        {
            get
            {
                //Calculate the date difference between signature date and the current date
                TimeSpan dateDiff = DateTime.UtcNow.Subtract(this.SignatureDate);

                if (LicenseConfiguration.RefreshLicenseAlwaysRequired || (LicenseConfiguration.RefreshLicenseRequireFrequency > 0 && dateDiff.TotalDays > LicenseConfiguration.RefreshLicenseRequireFrequency))
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Public Properties
        /// <summary>Gets whether or not this is an evaluation license.</summary>
        public bool IsEvaluation
        {
            get { return false; }
        }
        #endregion

        #region Public Methods
        /// <summary>Activates the license online.</summary>
        /// <param name="licenseId">The License ID issued by SOLO Server.</param>
        /// <param name="password">The customer password.</param>
        /// <returns>Returns true if the system was activated successfully. If false is returned, evaluate the <see cref="License.LastError"/> property to determine the reason why activation failed.</returns>
        public bool ActivateOnline(Int32 licenseId, string password)
        {
            string licenseContent = "";

            //initialize the object used for calling the web service method
            XmlActivationService ws = m_WebServiceHelper.CreateXmlActivationServiceObject();
            if (null == ws)
            {
                this.LastError = m_WebServiceHelper.LastError;
                return false;
            }

            //activate online using our endpoint configuration from app.config
            if (!this.ActivateInstallationLicenseFile(licenseId, password, ws, ref licenseContent))
                return false;

            return this.SaveLicenseFile(licenseContent);
        }

        /// <summary>Deactivates the installation online.</summary>
        /// <returns>Returns true if the system was deactivated successfully. If false is returned, evaluate the <see cref="License.LastError"/> property to determine the reason why deactivation failed.</returns>
        public bool DeactivateOnline()
        {
            //initialize the object used for calling the web service method
            XmlActivationService ws = m_WebServiceHelper.CreateXmlActivationServiceObject();
            if (null == ws)
            {
                this.LastError = m_WebServiceHelper.LastError;
                return false;
            }

            if (this.DeactivateInstallation(ws) || this.LastError.ExtendedErrorNumber == 5010 || this.LastError.ExtendedErrorNumber == 5015 || this.LastError.ExtendedErrorNumber == 5016 || this.LastError.ExtendedErrorNumber == 5017)
            {
                File.Delete(LicenseConfiguration.LicenseFilePath);
                return true;
            }

            return false;
        }

        /// <summary>Refreshes the license file with the latest data from SOLO Server.</summary>
        /// <returns>Returns true if the license file was refreshed successfully. If false is returned, evaluate the <see cref="License.LastError"/> property to determine the reason why the refresh failed.</returns>
        public bool RefreshLicense()
        {
            //initialize the object used for calling the web service method
            XmlLicenseFileService ws = m_WebServiceHelper.CreateXmlLicenseFileServiceObject();
            if (null == ws)
            {
                this.LastError = m_WebServiceHelper.LastError;
                return false;
            }

            string licenseContent = "";
            if (!base.RefreshLicense(ws, ref licenseContent))
            {
                if (this.LastError.ExtendedErrorNumber == 5010 || this.LastError.ExtendedErrorNumber == 5015 || this.LastError.ExtendedErrorNumber == 5016 || this.LastError.ExtendedErrorNumber == 5017)
                {
                    File.Delete(LicenseConfiguration.LicenseFilePath);
                    return true;
                }
                return false;
            }

            //try to save the license file to the file system
            if (!SaveLicenseFile(licenseContent))
                return false;

            return true;
        }

        /// <summary>Saves a new license file to the file system. (Used by RefreshLicense and <see cref="ManualActivationForm"/>.)</summary>
        /// <param name="lfContent">The new content to write to the license file.</param>
        /// <returns>Returns true if the license file was saved successfully. If false is returned, evaluate the <see cref="License.LastError"/> property to determine the reason why the file could not be saved.</returns>
        public bool SaveLicenseFile(string lfContent)
        {
            //try to save the license file to the file system
            try
            {
                File.WriteAllText(LicenseConfiguration.LicenseFilePath, lfContent);
            }
            catch (Exception ex)
            {
                this.LastError = new LicenseError(LicenseError.ERROR_COULD_NOT_SAVE_LICENSE, ex);
                return false;
            }

            return true;
        }

        /// <summary>Validates the license.</summary>
        /// <returns>Returns true if the license is valid.  If the license is not valid, evaluate the <see cref="License.LastError"/> property to determine the reason why validation failed.</returns>
        public bool Validate()
        {
            //Refresh the license if date difference is greater than RefreshLicenseAttemptFrequency (days).
            if (IsRefreshLicenseAttemptDue)
            {
                //If refresh license fails and date difference is greater than RefreshLicenseRequireFrequency (days) set license as invalid.
                if (!RefreshLicense() && IsRefreshLicenseRequired)
                {
                    return false;
                }
            }

            if (!ValidateVersion())
                return false;

            //Create a list of validations to perform.
            List<SystemValidation> validations = new List<SystemValidation>();

            //Add a validation to make sure there is no active system clock tampering taking place.
            validations.Add(new SystemClockValidation());

            //Validate the Product ID authorized in the license to make sure the license file was issued for this application.
            //validations.Add(new LicenseProductValidation(this, ThisProductID));

            //Add a validation to make sure this system is authorized to use the activated license.  (This implements copy-protection.)
            validations.Add(new SystemIdentifierValidation(AuthorizedIdentifiers, CurrentIdentifiers, SystemIdentifierValidation.REQUIRE_EXACT_MATCH));

            //Run all of the validations (in the order they were added), and make sure all of them succeed.
            foreach (SystemValidation validation in validations)
            {
                if (!validation.Validate())
                {
                    LastError = validation.LastError;
                    return false;
                }
            }

            //If we got this far, all validations were successful, so return true to indicate success and a valid license.
            return true;
        }

        private bool ValidateVersion()
        {
            List<int> productVersions = new List<int>() { 362368, 362392, 358384 };
            LicenseError err = null;

            //Tries to validate the 3 different versions, if the product id of the license does not equal any of the versions available then return false
            foreach (int productVersion in productVersions)
            {
                LicenseProductValidation validation = new LicenseProductValidation(this, productVersion);

                if (validation.Validate())
                {
                    ThisProductID = productVersion;
                    return true;
                }
                else
                {
                    err = validation.LastError;
                }
            }

            LastError = err;

            return false;
        }
        #endregion
    }
}
