using System.Xml.Serialization;

namespace KbBimstore
{
    public class KbBimstoreToolbar
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Enabled")]
        public bool Enabled { get; set; }

        [XmlElement("TabIdentity")]
        public int TabIdentity { get; set; }

        public KbBimstoreToolbar()
        {

        }

        public static KbBimstoreToolbar CreateDefaultToolbar()
        {
            KbBimstoreToolbar toolbar = new KbBimstoreToolbar();
            toolbar.Name = "Default";
            toolbar.Enabled = true;
            toolbar.TabIdentity = 0;

            return toolbar;
        }
    }
}
