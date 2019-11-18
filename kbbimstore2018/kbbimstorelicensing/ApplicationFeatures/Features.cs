using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

namespace KbBimstore.KBRevitLicensing
{
    #region Public Enumerations
    /// <summary>LicenseFeatures Enumeration</summary>
    public enum LicenseFeatures
    {
        /// <summary>New feature</summary>
        New = 1,
        /// <summary>Open feature</summary>
        Open = 2,
        /// <summary>Save feature</summary>
        Save = 4,
        /// <summary>Save As feature</summary>
        [Description("Save As")]
        SaveAs = 8,
        /// <summary>Print feature</summary>
        Print = 16,
        /// <summary>Select All feature</summary>
        [Description("Select All")]
        SelectAll = 32,
        /// <summary>Cut feature</summary>
        Cut = 64,
        /// <summary>Copy feature</summary>
        Copy = 128,
        /// <summary>Paste feature</summary>
        Paste = 256,
        /// <summary>Find feature</summary>
        Find = 512,
        /// <summary>Replace feature</summary>
        Replace = 1024
    }
    #endregion

    /// <summary>Business class implementation for features</summary>
    public class Features
    {
        #region Private Member Variables
        private Dictionary<string, Feature> m_Features = new Dictionary<string, Feature>();
        #endregion

        #region Public Constructors
        /// <summary>Default constructor</summary>
        public Features() { }
        #endregion

        #region Public Properties
        /// <summary>List of features</summary>
        public Dictionary<string, Feature> ListFeatures
        {
            get { return m_Features; }
        }
        #endregion

        #region Public Methods
        /// <summary>Check the status of the feature whether enabled or disabled</summary>
        /// <param name="featureName">string - name of the feature</param>
        /// <returns>Returns true if the feature is enabled, or false if it is disabled.</returns>
        public bool CheckStatus(string featureName)
        {
            if (m_Features.ContainsKey(featureName))
            {
                return m_Features[featureName].Enabled;
            }
            return false;
        }

        /// <summary>Adds feature to the list object</summary>
        /// <param name="feature">Feature-object of Feature</param>
        public void AddFeature(Feature feature)
        {
            m_Features.Add(feature.Name, feature);
        }
        #endregion

        #region Public Static Methods
        /// <summary>Gets the display name to show for an enumeration's attribute.</summary>
        /// <param name="value">The enumeration value.</param>
        /// <returns>Returns a string containing the enumeration value's name, or the value in its <see cref="DescriptionAttribute"/> (if present).</returns>
        internal static string GetDisplayName(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>Determines whether or not access to a feature is allowed by this license.</summary>
        /// <param name="node">XmlNode, SimpleTextEditorOptions xml node containing all license feature/options as a child node</param>
        /// <param name="feature">The feature which will be evaluated</param>
        /// <returns>Returns true if access to the specified feature is enabled, or false if it is disabled.</returns>
        internal static bool GetFeatureEnabled(XmlNode node, LicenseFeatures feature)
        {
            bool result = false;

            //Check for SimpleTextEditorOptions node
            if (node == null || string.IsNullOrEmpty(node.InnerXml))
            {
                return false;
            }

            //get the option node
            XmlNode optionNode = node.SelectSingleNode("LicenseOption" + feature.ToString());
            if (optionNode != null && !bool.TryParse(optionNode.InnerText, out result))
            {
                return false;
            }

            return result;
        }
        #endregion
    }
}