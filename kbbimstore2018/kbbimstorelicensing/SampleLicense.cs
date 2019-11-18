using System;
using com.softwarekey.Client.Licensing;

namespace KbBimstore.KBRevitLicensing
{
    public interface SampleLicense
    {
        #region PLUSManaged Properties
        DateTime EffectiveEndDate { get; }
        DateTime EffectiveStartDate { get; }
        string InstallationID { get; set; }
        string InstallationName { get; set; }
        LicenseError LastError { get; set; }
        Int32 LicenseID { get; set; }
        #endregion

        #region Sample Properties
        bool IsEvaluation { get; }
        #endregion

        #region PLUSManaged Methods
        bool ActivateOnline(Int32 licenseId, string password);
        string GetActivationInstallationLicenseFileRequest(Int32 licenseId, string password);
        bool LoadFile(string path);
        bool ProcessActivateInstallationLicenseFileResponse(string response, ref string licenseContent);
        void ResetSessionCode();
        #endregion

        #region Sample Methods
        bool DeactivateOnline();
        bool RefreshLicense();
        bool SaveLicenseFile(string lfContent);
        bool Validate();
        #endregion
    }
}
