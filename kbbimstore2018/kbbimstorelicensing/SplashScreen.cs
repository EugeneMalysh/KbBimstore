using System;
using System.Text;
using System.Xml;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>SplashScreen class implementation</summary>
    public static class SplashScreen
    {
        #region Private Static Member Variables
        private static SplashScreenForm m_SplashForm = null;
        #endregion

        #region Public Static Methods
        /// <summary>Displays the SplashScreen</summary>
        public static void ShowSplashScreen()
        {
            if (m_SplashForm == null)
            {
                m_SplashForm = new SplashScreenForm();
                m_SplashForm.ShowSplashScreen();
            }
        }

        /// <summary>Closes the SplashScreen</summary>
        public static void CloseSplashScreen()
        {
            if (m_SplashForm != null)
            {
                m_SplashForm.CloseSplashScreen();
                m_SplashForm = null;
            }
        }

        /// <summary>Update text in default of success message</summary>
        /// <param name="Text">string - Message</param>
        public static void UdpateStatusText(string Text)
        {
            if (m_SplashForm != null)
                m_SplashForm.UdpateStatusText(Text);
        }

        /// <summary>Initializes application features</summary>
        /// <param name="features">Features - object</param>
        /// <param name="license">ReadOnlyLicense - license object</param>
        /// <param name="successful">Whether license is reloaded successfully or not</param>
        public static void InitializeFeatures(Features features, SampleReadOnlyLicense license, bool successful)
        {
            XmlNode optionsNode = null;

            features.ListFeatures.Clear();
            Enum e = LicenseFeatures.Copy;

            //Get the SimpleTextEditorOptions node from License custom data string
            if (!string.IsNullOrEmpty(license.LicenseCustomData))
            {
                optionsNode = GetCustomDataXml(license.LicenseCustomData);
            }

            //If LicenseCustomData does not contains settings for SimpleTextEditor Options then fall back to Product Option CustomData
            if (optionsNode == null && !string.IsNullOrEmpty(license.ProductOption.CustomData))
            {
                //Get the SimpleTextEditorOptions node from Product option custom data string
                optionsNode = GetCustomDataXml(license.ProductOption.CustomData);
            }

            foreach (object name in Enum.GetValues(e.GetType()))
            {
                features.AddFeature(new Feature(name.ToString(), !successful ? false : Features.GetFeatureEnabled(optionsNode, (LicenseFeatures)name), Features.GetDisplayName((LicenseFeatures)name)));
            }
        }

        #endregion

        #region Private Static Methods
        /// <summary>Loads the xml from custom data string</summary>
        /// <param name="customData">string, containing custom xml or string</param>
        /// <returns>XmlNode of SimpleTextEditor options</returns>
        private static XmlNode GetCustomDataXml(string customData)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode node = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                //Append a parent node to license custom data string to parse into Xml
                sb.Append("<LicenseCustomData>");
                sb.Append(customData);
                sb.Append("</LicenseCustomData>");

                sb = sb.Replace("&amp;", "&");
                sb = sb.Replace("&lt;", "<");
                sb = sb.Replace("&gt;", ">");

                //Load the Xml
                xml.LoadXml(sb.ToString());

                //Get Xml node
                node = xml.DocumentElement.SelectSingleNode("//SampleLicenseSettings/SimpleTextEditorOptions");
            }
            catch
            {
                node = null;
            }

            return node;
        }
        #endregion
    }
}