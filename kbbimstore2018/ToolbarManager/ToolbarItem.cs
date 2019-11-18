using System;
using System.Xml.Serialization;

using Autodesk.Revit.UI;

namespace KbBimstore.ToolbarManager
{
    [Serializable]
    [XmlRoot("RootNode")]
    public class ToolbarItem
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Text")]
        public string Text { get; set; }

        [XmlElement("ClassName")]
        public string ClassName { get; set; }

        [XmlElement("ImageLocation")]
        public string ImageLocation { get; set; }

        [XmlElement("LargeImageLocation")]
        public string LargeImageLocation { get; set; }

        [XmlElement("Tooltip")]
        public string Tooltip { get; set; }

        [XmlElement("ContextualHelpUrl")]
        public string ContextualHelpUrl { get; set; }

        public ToolbarItem()
        {

        }

        public ToolbarItem(RibbonItem ri, string className, string imageLocation, string largeImageLocation)
        {
            this.Name = ri.Name;
            this.Text = ri.ItemText;
            this.ClassName = "KbBimstore." + this.Name + "Command";
            this.ImageLocation = imageLocation;
            this.LargeImageLocation = largeImageLocation;
            this.Tooltip = ri.ToolTip;

            if (ri.GetContextualHelp() != null)
                this.ContextualHelpUrl = ri.GetContextualHelp().HelpTopicUrl;
            else
                this.ContextualHelpUrl = string.Empty;
        }
    }
}
