namespace KbBimstore.KBRevitLicensing
{
    /// <summary>Business class implementation for feature</summary>
    public class Feature
    {
        #region Private Member Variables
        private string m_Name = "";
        private string m_DisplayName = "";
        private bool m_Enabled = false;
        #endregion

        #region Public Constructor
        /// <summary>Creation contructor</summary>
        /// <param name="name">string - Name of the feature</param>
        /// <param name="enabled">bool - whether feature is enabled or not</param>
        public Feature(string name, bool enabled)
        {
            m_Name = name;
            m_Enabled = enabled;
            m_DisplayName = name;
        }

        /// <summary>Creation contructor with display name</summary>
        /// <param name="name">string - Name of the feature</param>
        /// <param name="enabled">bool - whether feature is enabled or not</param>
        /// <param name="displayName">string - display name</param>
        public Feature(string name, bool enabled, string displayName)
        {
            m_Name = name;
            m_DisplayName = displayName;
            m_Enabled = enabled;
        }
        #endregion

        #region Public Properties
        /// <summary>Name</summary>
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>Display name</summary>
        public string DisplayName
        {
            get { return m_DisplayName; }
            set { m_DisplayName = value; }
        }

        /// <summary>Enabled</summary>
        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }
        #endregion
    }
}