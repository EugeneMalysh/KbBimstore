using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using com.softwarekey.Client.Licensing;
using com.softwarekey.Client.Utils;
using Microsoft.Win32;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>Contains data used for PLUSManaged samples.</summary>
    /// <remarks><note type="caution"><para>If you copy this code into your application, it is VERY IMPORTANT that you UPDATE THE CONFIGURATION PROPERTIES BELOW!!!</para></note></remarks>
    internal static partial class LicenseConfiguration
    {
        // TODO: update this constant to a registry location of your choosing (located under HKEY_CURRENT_USER).
        #region Private Constant Variables
        private const string PATH_REGISTRY_LOCATION = "Software\\KBBimstore\\License";
        #endregion

        #region Private Static Variables
        private static string _LicenseFilePath = "";
        private static int productId = 0;
        #endregion

        //TODO: IMPORTANT: If you copy this code into your application, update the configuration settings in the regions below!!!
        #region Encryption Settings
        /// <summary>Gets the encryption key data used to read the license and communicate with SOLO Server.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: Update this to use your SOLO Server account's Encryption Key data.
        /// If you only use License Manager (without SOLO Server), contact us (http://www.softwarekey.com/contact/) for an envelope.
        /// NEVER COPY THE ENVELOPE DIRECTLY FROM LICENSE MANAGER, as this contains key data that should never be known by your applications.</para></note></remarks>
        internal static AuthorEncryptionKey EncryptionKey
        {
            get
            {
                return new AuthorEncryptionKey("XnyuoAnP4fzSLoO04upmMQpPegXxfWsIF36B65xZL8clR7T/+loeX//6SXujzvEF", // The string before this comment is the Envelope Key
                   "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", //The string before this comment is the Envelope.
                   false);
                //IMPORTANT: Passing false in the last argument above will cause PLUSManaged to use the User Key Store
                //           for encryption.  This is recommended for most desktop or Dialog/Form applications.  If
                //           you are protecting a web application, or an application that runs as a service, you
                //           may need to pass true for this argument to use the Machine Key Store.  Note that using
                //           the Machine Key Store typically requires permissions that may not be sufficient with
                //           the typical/default user configuration (which includes application pool identities and
                //           impersonated identities).
            }
        }

        /// <summary>Gets the initialization vector used when encrypting and decrypting manual action session-states.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: Create a new, random, unique, initialization vector for each of your applications!</para></note>
        /// <example>This example shows how to generate a new initialization vector (IV) and key for manual requests:
        /// <code language="cs">
        /// using (System.Security.Cryptography.RijndaelManaged alg = new System.Security.Cryptography.RijndaelManaged())
        /// {
        ///     alg.KeySize = 256;
        ///     alg.GenerateIV();
        ///     alg.GenerateKey();
        /// 
        ///     string iv = Convert.ToBase64String(alg.IV);
        ///     string key = Convert.ToBase64String(alg.Key);
        ///     //TODO: Save your IV and key.  You can just put a breakpoint on the next line and inspect the values of iv and key while debugging.
        /// }
        /// </code>
        /// </example>
        /// </remarks>
        internal static string ManualActionIV
        {
            get { return "IZWGSERAx1h9zzpCGBmRXg=="; }
        }

        /// <summary>Gets the encryption key used when encrypting and decrypting manual action session-states.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: Create a new, random, unique, encryption key for each of your applications!</para></note></remarks>
        internal static string ManualActionKey
        {
            get { return "ajOsfy2RCC66qD5D9bCiJIK5zJVYUbJwLXwwC6fW4SI="; }
        }

        /// <summary>Gets the seed value used to decode \"Activation Code 2\" values when activating manually.  The decoded value will contain up to 14 bits of additional numeric data.</summary>
        /// <remarks><para>TODO: IMPORTANT: Each application should have its own, unique value for RegKey2Seed.  This MUST be a value between 1 and 255.</para></remarks>
        internal static Int32 RegKey2Seed
        {
            get { return 145; }
        }

        /// <summary>Gets the seed value used for validating \"Activation Code 1\" values when activating manually.</summary>
        /// <remarks><para>TODO: IMPORTANT: Each application should have its own, unique value for TriggerCodeSeed.  This MUST be a value between 1 and 65535.</para></remarks>
        internal static Int32 TriggerCodeSeed
        {
            get { return 1753; }
        }
        #endregion

        #region Application and Product Settings
        /// <summary>Gets the absolute path to the directory in which the application (executable file) is located.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: You may need to update this code for your application depending on where it needs to store the license file.</para></note></remarks>
        internal static string ApplicationDirectory
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        /// <summary>Gets this application's Product ID (typically determined by SOLO Server).  Make your own unique value if you are using License Manager only.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: Update this to use your Product ID</para></note></remarks>
        internal static int ThisProductID
        {
            get
            {
                return productId;
            }

            set
            {
                productId = value;
            }
        }

        /// <summary>Gets this application's product version number.</summary>
        /// <remarks>
        /// <note type="caution">
        /// <para>
        /// TODO: IMPORTANT: You may need to update this code for your application, especially if it is not a ".exe" file.
        /// You may also hard-code this data.
        /// </para>
        /// </note>
        /// </remarks>
        internal static string ThisProductVersion
        {
            get { return IOHelper.GetAssemblyFileVersion(Assembly.GetExecutingAssembly()); }
        }
        #endregion

        #region License File and Alias Settings
        /// <summary>Gets a list of aliases used with any samples that use writable license files.</summary>
        /// <remarks>
        /// <note type="caution"><para>TODO: IMPORTANT: You need to update this code so the alias locations are unique to your application!!!  Failing to do so could cause conflicts
        /// with the samples and other applications.</para></note>
        /// </remarks>
        internal static List<LicenseAlias> Aliases
        {
            get
            {
                return new List<LicenseAlias>(
                    new LicenseAlias[] {
                        new LicenseFileSystemAlias(Path.Combine(ApplicationDirectory, "kbbimstorelicense1.lfx"), EncryptionKey, true),
                        new LicenseFileSystemAlias(Path.Combine(ApplicationDirectory, "kbbimstorelicense2.lfx"), EncryptionKey, true)/*,
                        new LicenseWindowsRegistryAlias("Software\\Concept Software\\Protection PLUS\\Samples\\License", EncryptionKey, true, RegistryHive.CurrentUser, "LicenseAlias3"),
                        new LicenseWindowsRegistryAlias("Software\\Concept Software\\Protection PLUS\\Samples\\License", EncryptionKey, true, RegistryHive.CurrentUser, "LicenseAlias4")*/ });
            }
        }

        /// <summary>Gets or sets the absolute path to the application's license file.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: You may need to update this code for your application depending on where it needs to store the license file.</para></note></remarks>
        internal static string LicenseFilePath
        {
            set { _LicenseFilePath = Path.Combine(value, "kbbimstorelicense.lfx"); }
            get
            {
                if(string.IsNullOrEmpty(_LicenseFilePath))
                    return Path.Combine(ApplicationDirectory, "kbbimstorelicense.lfx");
                return _LicenseFilePath;
            }
        }

        /// <summary>Gets the absolute path to the manual action session state file.</summary>
        internal static string ManualActionSessionStateFilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location) + "ManualAction.xml");
            }
        }

        /// <summary>Gets or Sets/Creates the File Path in the Registry Path specified.</summary>
        internal static string PathRegistryValue
        {
            get
            {
                RegistryKey pathKey = Registry.CurrentUser.OpenSubKey(PATH_REGISTRY_LOCATION);
                return (string)pathKey.GetValue("LicensePath");
            }
            set
            {
                RegistryKey pathKey = Registry.CurrentUser.OpenSubKey(PATH_REGISTRY_LOCATION, true);

                if (pathKey == null)
                {
                    pathKey = Registry.CurrentUser.CreateSubKey(PATH_REGISTRY_LOCATION);
                }

                pathKey.SetValue("LicensePath", value);
            }
        }

        /// <summary>Gets a string name to use as the file name for the Network Semaphore Files</summary>
        internal static string NetworkSemaphorePrefix
        {
            get { return "sema"; }
        }
        #endregion

        #region Licensing Restrictions and Settings
        /// <summary>Gets the number of days to allow unlicensed users to evaluate the product.</summary>
        /// <remarks>
        /// <para>
        /// This setting is only applicable for samples which use a writable license, as it is not possible
        /// for an application to issue an evaluation license automatically using read-only licenses.
        /// </para>
        /// <note type="implementnotes"><para>TODO: Set this value per your licensing requirements!</para></note>
        /// </remarks>
        internal static int FreshEvaluationDuration
        {
            get { return 30; }
        }

        /// <summary>Gets whether or not a license file refresh will be required every time the application runs.</summary>
        /// <remarks><note type="implementnotes"><para>TODO: Set this value per your licensing requirements!</para></note></remarks>
        internal static bool RefreshLicenseAlwaysRequired
        {
            get { return false; }
        }

        /// <summary>Gets the number of days to wait before attempts to refresh and validate the license against SOLO Server begin. (Only applicable if <see cref="RefreshLicenseAlwaysRequired"/> is false.)</summary>
        /// <remarks><note type="implementnotes"><para>TODO: Set this value per your licensing requirements!</para></note></remarks>
        /// <note type="implementnotes"><para>Setting this to zero when <see cref="RefreshLicenseAlwaysRequired"/> is false means refreshing
        /// successfully is never attempted unless it is eventually required in the <see cref="RefreshLicenseRequireFrequency"/> property.</para></note>
        internal static int RefreshLicenseAttemptFrequency
        {
            get { return 7; }
        }

        /// <summary>Gets whether or not license refreshing is enabled at all.</summary>
        internal static bool RefreshLicenseEnabled
        {
            get { return (RefreshLicenseAlwaysRequired || RefreshLicenseAttemptFrequency != 0 || RefreshLicenseRequireFrequency != 0); }
        }

        /// <summary>Gets the number of days to wait before attempts to refresh and validate the license against SOLO Server are required.  (Only applicable when <see cref="RefreshLicenseAlwaysRequired"/> is false.)</summary>
        /// <remarks>
        /// <note type="implementnotes"><para>TODO: Set this value per your licensing requirements!</para></note>
        /// <note type="implementnotes"><para>If <see cref="RefreshLicenseAttemptFrequency"/> is used, then this property's value should be larger.</para></note>
        /// <note type="implementnotes"><para>Setting this to zero when <see cref="RefreshLicenseAlwaysRequired"/> is false means refreshing
        /// successfully is never required (though it may still be attempted depending on <see cref="RefreshLicenseAttemptFrequency"/>).</para></note>
        /// <para>An example of how this can be used would be to set <see cref="RefreshLicenseAttemptFrequency"/> to 7 days to start trying
        /// to phone-home after a week, while also setting <see cref="RefreshLicenseRequireFrequency"/> to 14 to require those attempts to
        /// succeed after two weeks.</para>
        /// </remarks>
        internal static int RefreshLicenseRequireFrequency
        {
            get { return 0; }
        }

        /// <summary>The amount of time (in seconds) to allow the system clock to be back-dated during run-time.</summary>
        /// <remarks>
        /// <para>This is used by SampleLicense.IsClockBackdatedAtRuntime to avoid raising "false alarms" when a system synchronizes its
        /// clock with Internet time (NTP).</para>
        /// <para>TODO: If you find your users run into issues where the back-date check is tripped after NTP synchronization occurs, you may relax the value here
        /// further.  The default value provided with the samples (300 seconds, or 5 minutes) is based on feedback.</para>
        /// </remarks>
        internal static int RuntimeBackdateThresholdSeconds
        {
            get { return 300; }
        }

        /// <summary>Gets the SystemIdentifierAlgorithms to use in the licensed application.</summary>
        /// <remarks><note type="implementnotes">TODO: Change or adjust the SystemIdentifierAlgorithm implementations you want to use to authorize licenses for a system.</note></remarks>
        internal static List<SystemIdentifierAlgorithm> SystemIdentifierAlgorithms
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    //Algorithms for Windows:
                    return new List<SystemIdentifierAlgorithm>(
                        new SystemIdentifierAlgorithm[] {
                            new NicIdentifierAlgorithm(),
                            new HardDiskVolumeSerialIdentifierAlgorithm(HardDiskVolumeSerialFilterType.OperatingSystemRootVolume),
                            new ComputerNameIdentifierAlgorithm(),
                            new BiosUuidIdentifierAlgorithm(),
                            new ProcessorIdentifierAlgorithm(new ProcessorIdentifierAlgorithmTypes[] { ProcessorIdentifierAlgorithmTypes.ProcessorName, ProcessorIdentifierAlgorithmTypes.ProcessorVendor, ProcessorIdentifierAlgorithmTypes.ProcessorVersion }) });
                }

                //Algorithms for other platforms:
                return new List<SystemIdentifierAlgorithm>(
                    new SystemIdentifierAlgorithm[] {
                        new HardDiskVolumeSerialIdentifierAlgorithm(HardDiskVolumeSerialFilterType.OperatingSystemRootVolume),
                        new NicIdentifierAlgorithm(),
                        new ComputerNameIdentifierAlgorithm() });
            }
        }

        /// <summary>Gets the number of days left on a time-limited license when the user should be warned about the limited amount of time left.</summary>
        /// <remarks><para>If this is set to 7, for example, this the user will be warned that the time-limited license will expire soon during the last week (7 days) in which the license is valid.</para></remarks>
        internal static int TimeLimitedWarningDays
        {
            get { return 7; }
        }
        #endregion

        #region Volume and Downloadable License File Settings
        //* Volume licenses are comprised of a read-only license file, which contains data necessary to uniquely identify a license. However, these licenses do not contain any data that
        //  uniquely identifies a licensed system. The benefit is that your customers can freely copy and use the volume license file to use your application without the need to activate.
        //* Downloadable licenses are very similar to volume licenses, in that they are read-only license files that only contain data capable of uniquely identifying the license. The
        //  difference, however, is that these licenses require trigger code validation to activate a separate, writable license file on each system on which the application is run.

        /// <summary>Gets whether or not application users are allowed to download an updated license file from the customer service portal and manually overwrite the license file used by the application.</summary>
        /// <remarks><note type="caution"><para>Enabling this feature can cause data previously stored in the writable license file copy to get overwritten.  You should evaluate the
        /// LicenseConfiguration.InitializeVolumeLicense method and update it as-needed to prevent this from being a problem for your application.</para></note></remarks>
        internal static bool DownloadableLicenseOverwriteWithNewerAllowed
        {
            get { return true; }
        }

        /// <summary>Gets whether or not application users can restore a previously download license file and restore it over the volume license file used by the application.</summary>
        /// <remarks><note type="caution"><para>Enabling this feature can cause data previously stored in the writable license file copy to get overwritten.  You should evaluate the
        /// LicenseConfiguration.InitializeVolumeLicense method and update it as-needed to prevent this from being a problem for your application.</para></note></remarks>
        internal static bool DownloadableLicenseOverwriteWithOlderAllowed
        {
            get { return true; }
        }

        /// <summary>Gets whether or not overwriting a downloaded license file with an older version of the file will require activation. (Only applicable when <see cref="DownloadableLicenseOverwriteWithNewerAllowed"/> is true.)</summary>
        internal static bool DownloadableLicenseOverwriteWithNewerRequiresActivation
        {
            get { return false; }
        }

        /// <summary>Gets whether or not overwriting a downloaded license file with an older version of the file will require activation. (Only applicable when <see cref="DownloadableLicenseOverwriteWithOlderAllowed"/> is true.)</summary>
        internal static bool DownloadableLicenseOverwriteWithOlderRequiresActivation
        {
            get { return true; }
        }

        /// <summary>Gets the absolute path to the application's downloadable/volume license file.</summary>
        /// <remarks><note type="caution"><para>TODO: IMPORTANT: You may need to update this code for your application depending on where it needs to store the license file.</para></note></remarks>
        internal static string VolumeLicenseFilePath
        {
            get
            {
                //TODO: Update your downloadable/volume license file name or path if necessary.
                return Path.Combine(ApplicationDirectory, "kbbimstorelicense.lfx");
            }
        }

        /// <summary>Gets the <see cref="SystemIdentifierAlgorithm"/> objects to use when validating a downloadable/volume license.</summary>
        internal static List<SystemIdentifierAlgorithm> VolumeSystemIdentifierAlgorithms
        {
            get
            {
                return new List<SystemIdentifierAlgorithm>(
                    new SystemIdentifierAlgorithm[] {
                        new LicenseIDIdentifierAlgorithm() });
            }
        }
        #endregion
    }
}
