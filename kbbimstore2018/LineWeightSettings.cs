using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    public class LineWeightSettings
    {
        public LineWeightSettingItem ForegroundElementsSettings { get; set; }
        public LineWeightSettingItem Middle1ElementsSettings { get; set; }
        public LineWeightSettingItem Middle2ElementsSettings { get; set; }
        public LineWeightSettingItem Middle3ElementsSettings { get; set; }
        public LineWeightSettingItem BackgroundElementsSettings { get; set; }

        public LineWeightSettings()
        {
            ForegroundElementsSettings = new LineWeightSettingItem();
            Middle1ElementsSettings = new LineWeightSettingItem();
            Middle2ElementsSettings = new LineWeightSettingItem();
            Middle3ElementsSettings = new LineWeightSettingItem();
            BackgroundElementsSettings = new LineWeightSettingItem();
        }
    }
    public class LineWeightSettingItem
    {
        public int LineWeight { get; set; }
        public bool IsProjection { get; set; }
    }
}
