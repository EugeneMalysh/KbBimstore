using System.Diagnostics;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using com.softwarekey.Client.Compatibility.ProtectionPLUS4;
using com.softwarekey.Client.Licensing;
using com.softwarekey.Client.Utils;
using com.softwarekey.Client.WebService;
using com.softwarekey.Client.WebService.XmlActivationService;
using com.softwarekey.Client.WebService.XmlLicenseFileService;
using com.softwarekey.Client.WebService.XmlLicenseService;
using com.softwarekey.Client.WebService.XmlNetworkFloatingService;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>Web service helper class, which helps with setting endpoint URLs, proxy server settings, etc...</summary>
    internal class WebServiceHelper
    {
        #region Private Member Variables
        private InternetConnectionInformation m_ConnectionInformation = null;
        private NetworkCredential m_ProxyAuthenticationCredentials = null;
        private LicenseError m_LastError = new LicenseError(LicenseError.ERROR_NONE);
        #endregion

        #region Private Constants
        //TODO: If you are using your own domain name (instead of secure.softwarekey.com) for SOLO Server, you should update the values for these URLs.
        private const string m_ManualRequestUrl = "https://secure.softwarekey.com/solo/customers/ManualRequest.aspx";
        //SOLO Server web services
        private const string m_XmlActivationServiceUrl = "https://secure.softwarekey.com/solo/webservices/XmlActivationService.asmx";
        private const string m_XmlLicenseFileServiceUrl = "https://secure.softwarekey.com/solo/webservices/XmlLicenseFileService.asmx";
        private const string m_XmlLicenseServiceUrl = "https://secure.softwarekey.com/solo/webservices/XmlLicenseService.asmx";
        private const string m_XmlNetworkFloatingServiceUrl = "https://secure.softwarekey.com/solo/webservices/XmlNetworkFloatingService.asmx";
        //Automation Client URLs:
        private const string m_PostEvalDataUrl = "https://secure.softwarekey.com/solo/products/trialsignup.asp"; //This feature is not presently supported in SOLO Server Lite.
        //Automation Client URLs for Instant SOLO Server:
        private const string m_GetLicenseStatusUrl = "https://secure.softwarekey.com/solo/customers/getlicensestatus.asp";
        private const string m_GetRegDataUrl = "https://secure.softwarekey.com/solo/customers/getregdata.asp";
        private const string m_GetTcDataUrl = "https://secure.softwarekey.com/solo/unlock/getcode.asp";
        private const string m_PostRegDataUrl = "https://secure.softwarekey.com/solo/postings/postregdata.asp";
        //Automation Client SOLO Server Lite URLs:
        //private const string m_GetLicenseStatusUrl = "http://sl.softwarekey.com/getregdata.asp";
        //private const string m_GetRegDataUrl = "http://sl.softwarekey.com/getregdata.asp";
        //private const string m_GetTcDataUrl = "http://sl.softwarekey.com/getcode.asp";
        //private const string m_PostRegDataUrl = "http://sl.softwarekey.com/postregdata.asp";
        #endregion

        #region Constructors
        /// <summary>Creates a new <see cref="WebServiceHelper"/> object.</summary>
        public WebServiceHelper()
        {
            this.Initialize();
        }
        #endregion

        #region Private Methods
        /// <summary>Initializes the connection information object</summary>
        private void Initialize()
        {
            //TODO: if you wish to test the connection with your own web site/server, change the timeout
            //      for the test (which is 10 seconds by default), and/or specify a proxy server to use
            //      for the test (none or the system default is used by default), then you should call
            //      the appropriate overload below.
            this.m_ConnectionInformation = new InternetConnectionInformation(m_XmlActivationServiceUrl);
        }

        /// <summary>Initializes proxy authentication credentials only when they are not already initialized.</summary>
        /// <returns>Returns true if we have proxy authentication credentials to use.</returns>
        private bool InitializeProxyAuthenticationCredentials()
        {
            if (null != this.m_ProxyAuthenticationCredentials)
            {
                return true;
            }

            return this.ShowProxyAuthenticationCredentialsDialog();
        }

        /// <summary>Shows the proxy authentication credentials dialog to obtain the credentials from the user.</summary>
        /// <returns>Returns true if we got data from the proxy authentication credentials dialog.</returns>
        private bool ShowProxyAuthenticationCredentialsDialog()
        {
            //we don't have any credentials, so let's ask for them...
            using (ProxyCredentialsForm credentialForm = new ProxyCredentialsForm())
            {
                if (DialogResult.OK == credentialForm.ShowDialog())
                {
                    this.m_ProxyAuthenticationCredentials = credentialForm.Credentials;
                    return true;
                }
            }

            return false;
        }

        /// <summary>Initializes a web service object.</summary>
        /// <param name="ws">The object which will be used to call web service methods.</param>
        /// <returns>Returns true if the object was initialized and is considered usable.</returns>
        private bool InitializeWebServiceObject(ref SoapHttpClientProtocol ws)
        {
            IWebProxy proxy = null;

            if (!InitializeProxyObject(out proxy))
                return false;

            if (null != proxy)
                ws.Proxy = proxy;

            return true;
        }

        /// <summary>Initializes a <see cref="WebFormCall"/> object.</summary>
        /// <param name="wfc">The object which will be used to call a web form.</param>
        /// <returns>Returns true if the object was initialized and is considered usable.</returns>
        private bool InitializeWebFormCall(ref WebFormCall wfc)
        {
            IWebProxy proxy = null;

            if (!InitializeProxyObject(out proxy))
                return false;

            if (null != proxy)
                wfc.Proxy = proxy;

            return true;
        }

        /// <summary>Initializes a proxy object.</summary>
        /// <param name="proxy">The <see cref="IWebProxy"/> reference which will be initialized.  This may be set to null if no proxy is being used.</param>
        /// <returns>Returns true if the proxy object was initialized successfully.</returns>
        private bool InitializeProxyObject(out IWebProxy proxy)
        {
            proxy = null;

            if (m_ConnectionInformation.ProxyAuthenticationRequired)
            {
                if (null == m_ProxyAuthenticationCredentials)
                {
                    //no proxy authentication credentials have been entered yet, so let's ask for them...
                    bool done = false;
                    bool dialogResult = true;

                    do
                    {
                        //clear any credentials entered earlier and ask the user for new credentials
                        this.ResetProxyAuthenticationCredentials();
                        dialogResult = this.ShowProxyAuthenticationCredentialsDialog();
                        if (!dialogResult)
                        {
                            //the user clicked cancel on the dialog
                            return false;
                        }

                        //cache this info in our proxy object
                        this.m_ConnectionInformation.Proxy.Credentials = this.m_ProxyAuthenticationCredentials;

                        //test the proxy authentication info -- if it fails, we should ask the user for credentials again
                        done = this.m_ConnectionInformation.RunTestRequest();
                    } while (!done);
                }

                proxy = m_ConnectionInformation.Proxy;
            }

            return true;
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>The <see cref="InternetConnectionInformation"/> object, which contains data about the application's Internet connectivity.</summary>
        public InternetConnectionInformation ConnectionInformation
        {
            get { return this.m_ConnectionInformation; }
        }

        /// <summary>The <see cref="LicenseError"/> object, containing details about the last error that occurred.</summary>
        public LicenseError LastError
        {
            get { return this.m_LastError; }
        }
        #endregion

        #region Public Properties
        /// <summary>Contains the username and password credentials used for authenticating with a proxy server (may be null if unused).</summary>
        public NetworkCredential ProxyAuthenticationCredentials
        {
            get { return this.m_ProxyAuthenticationCredentials; }
            set { this.m_ProxyAuthenticationCredentials = value; }
        }
        #endregion

        #region Public Static Methods
        /// <summary>Opens the manual request URL in the default web browser.</summary>
        public static void OpenManualRequestUrl()
        {
            Process.Start(m_ManualRequestUrl);
        }
        #endregion

        #region Public Methods
        /// <summary>Resets/disregards any prior proxy authentication credentials that may have been entered.</summary>
        public void ResetProxyAuthenticationCredentials()
        {
            this.m_ProxyAuthenticationCredentials = null;
        }

        #region Web Service Object Methods
        /// <summary>Creates a new <see cref="XmlActivationService"/> object, which may be used for processing web service methods centered around activation.</summary>
        /// <returns>Returns a new <see cref="XmlActivationService"/> object.</returns>
        public XmlActivationService CreateXmlActivationServiceObject()
        {
            //create the new web service object with our URL
            SoapHttpClientProtocol ws = new XmlActivationService();
            ws.Url = m_XmlActivationServiceUrl;

            if (!this.InitializeWebServiceObject(ref ws))
                return null;

            //return the web service object
            return (XmlActivationService)ws;
        }

        /// <summary>Creates a new <see cref="XmlLicenseFileService"/> object, which may be used for processing web service methods centered around XML license file calls to SOLO Server.</summary>
        /// <returns>Returns a new <see cref="XmlLicenseFileService"/> object.</returns>
        public XmlLicenseFileService CreateXmlLicenseFileServiceObject()
        {
            //create the new web service object with our URL
            SoapHttpClientProtocol ws = new XmlLicenseFileService();
            ws.Url = m_XmlLicenseFileServiceUrl;

            if (!this.InitializeWebServiceObject(ref ws))
                return null;

            //return the web service object
            return (XmlLicenseFileService)ws;
        }

        /// <summary>Creates a new <see cref="XmlLicenseService"/> object, which may be used for processing web service methods centered around obtaining and update license information in SOLO Server.</summary>
        /// <returns>Returns a new <see cref="XmlLicenseService"/> object.</returns>
        public XmlLicenseService CreateXmlLicenseServiceObject()
        {
            //create the new web service object with our URL
            SoapHttpClientProtocol ws = new XmlLicenseService();
            ws.Url = m_XmlLicenseServiceUrl;

            if (!this.InitializeWebServiceObject(ref ws))
                return null;

            //return the web service object
            return (XmlLicenseService)ws;
        }

        /// <summary>Gets a new <see cref="XmlNetworkFloatingService"/> object, which may be used for processing web service methods centered around network floating licensing via SOLO Server.</summary>
        /// <returns>Returns a new <see cref="XmlNetworkFloatingService"/> object.</returns>
        public XmlNetworkFloatingService CreateXmlNetworkFloatingServiceObject()
        {
            //create the new web service object with our URL
            SoapHttpClientProtocol ws = new XmlNetworkFloatingService();
            ws.Url = m_XmlNetworkFloatingServiceUrl;

            if (!this.InitializeWebServiceObject(ref ws))
                return null;

            //return the web service object
            return (XmlNetworkFloatingService)ws;
        }
        #endregion

        #region Automation Client Object Methods
        /// <summary>Creates a new <see cref="GetLicenseStatus"/> object, which is typically used to query a license's status with SOLO Server Lite.</summary>
        /// <returns>Returns a <see cref="GetLicenseStatus"/> object.</returns>
        public GetLicenseStatus CreateGetLicenseStatusObject()
        {
            WebFormCall wfc = new GetLicenseStatus(m_GetLicenseStatusUrl);

            if (!InitializeWebFormCall(ref wfc))
                return null;

            return (GetLicenseStatus)wfc;
        }

        /// <summary>Creates a new <see cref="GetRegData"/> object, which is typically used to query a customer's registration data/contact information from SOLO Server Lite.</summary>
        /// <returns>Returns a <see cref="GetRegData"/> object.</returns>
        public GetRegData CreateGetRegDataObject()
        {
            WebFormCall wfc = new GetRegData(m_GetRegDataUrl);

            if (!InitializeWebFormCall(ref wfc))
                return null;

            return (GetRegData)wfc;
        }

        /// <summary>Creates a new <see cref="GetTcData"/> object, which is typically used to query a activate a system using SOLO Server Lite.</summary>
        /// <returns>Returns a <see cref="GetTcData"/> object.</returns>
        public GetTcData CreateGetTcDataObject()
        {
            WebFormCall wfc = new GetTcData(m_GetTcDataUrl);

            if (!InitializeWebFormCall(ref wfc))
                return null;

            return (GetTcData)wfc;
        }

        /// <summary>Creates a new <see cref="PostEvalData"/> object, which is typically used to update a prospective customer's evaluation/trial contact information in SOLO Server.</summary>
        /// <returns>Returns a <see cref="PostEvalData"/> object.</returns>
        public PostEvalData CreatePostEvalDataObject()
        {
            WebFormCall wfc = new PostEvalData(m_PostEvalDataUrl);

            if (!InitializeWebFormCall(ref wfc))
                return null;

            return (PostEvalData)wfc;
        }

        /// <summary>Creates a new <see cref="PostRegData"/> object, which is typically used to update a customer's registration data/contact information in SOLO Server Lite.</summary>
        /// <returns>Returns a <see cref="PostRegData"/> object.</returns>
        public PostRegData CreatePostRegDataObject()
        {
            WebFormCall wfc = new PostRegData(m_PostRegDataUrl);

            if (!InitializeWebFormCall(ref wfc))
                return null;

            return (PostRegData)wfc;
        }
        #endregion
        #endregion
    }
}
