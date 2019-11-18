using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace KbBimstore
{
    public class KbBimstoreTab
    {
        public static readonly string[] defaultPanels = new string[8] { "Details", "Settings and Converter", "Website", "Auto Synchronize", "Renumber", "Window Tile", "Additional Plugins", "Revit City" };
        private static readonly string defaultTabName = "KB-BimStore";

        [XmlElement("ToolBars")]
        public List<KbBimstoreToolbar> ToolBars { get; set; }

        [XmlElement("Locked")]
        public bool Locked { get; set; }

        [XmlElement("Enabled")]
        public bool Enabled { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("hxp2")]
        public string hxp2 { get; set; }

        public KbBimstoreTab()
        {

        }

        public void Save()
        {
            ExportToXML(KbBimstoreApp.TabToolbarRenamerSettingsFilePath);
        }

        public static KbBimstoreTab CreateBimStoreFromSettings(string path)
        {
            if (!File.Exists(path))
                CreateDefaultTabSettings(path);

            try
            {
                KbBimstoreTab tab;

                XmlSerializer deserializer = new XmlSerializer(typeof(KbBimstoreTab));

                using (Stream reader = File.OpenRead(path))
                {
                    tab = deserializer.Deserialize(reader) as KbBimstoreTab;
                }

                return tab;
            }
            catch
            {
                return null;
            }
        }

        private static void CreateDefaultTabSettings(string path)
        {
            KbBimstoreTab defaultTab = new KbBimstoreTab();
            defaultTab.Name = defaultTabName;
            defaultTab.Locked = false;
            defaultTab.Enabled = true;
            defaultTab.ToolBars = new List<KbBimstoreToolbar>();
            
            for (int i = 0; i < defaultPanels.Length; i++)
            {
                string panel = defaultPanels[i];

                KbBimstoreToolbar toolbar = new KbBimstoreToolbar();
                toolbar.Name = panel;
                toolbar.Enabled = true;
                toolbar.TabIdentity = i;

                defaultTab.ToolBars.Add(toolbar);
            }

            File.WriteAllText(path, GetSettingsXML(defaultTab));
        }

        public void ExportToXML(string path)
        {
            string xml = GetSettingsXML(this);

            try
            {
                File.WriteAllText(path, xml);
            }
            catch
            {

            }
        }

        public static string GetSettingsXML(KbBimstoreTab tab)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(KbBimstoreTab));

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    xsSubmit.Serialize(xmlWriter, tab);
                    string xml = stringWriter.ToString();

                    return xml;
                }
            }
        }
    }
}
