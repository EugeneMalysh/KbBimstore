using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace KbBimstore
{
    [Serializable]
    [XmlRoot("RootNode")]
    public class AutoSyncData
    {
        [XmlElement("AutoSyncInterval")]
        public int AutoSyncInterval { get; set; }

        [XmlElement("AutoSync")]
        public bool AutoSync { get; set; }

        [XmlElement("AutoSaveInterval")]
        public int AutoSaveInterval { get; set; }

        [XmlElement("AutoSave")]
        public bool AutoSave { get; set; }

        public AutoSyncData()
        {

        }

        public AutoSyncData(int interval)
        {
            this.AutoSaveInterval = interval;
            this.AutoSyncInterval = interval;
            this.AutoSave = false;
            this.AutoSync = false;
        }

        public void Serialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AutoSyncData));

            using (TextWriter tw = new StreamWriter(path))
            {
                serializer.Serialize(tw, this);
            }
        }

        public static AutoSyncData Deserialize(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AutoSyncData));
            AutoSyncData data = null;

            if (File.Exists(path))
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    data = (AutoSyncData) serializer.Deserialize(fs);
                }
                    
            }

            return data;
        }

    }
}
