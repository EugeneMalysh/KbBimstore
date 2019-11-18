using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KbBimstore
{
    public partial class ViewDepthOverrideForm : Form
    {
        public LineWeightSettings LineWeightSettings;
        public ViewDepthOverrideForm()
        {
            InitializeComponent();
            LineWeightSettings = new LineWeightSettings();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LineWeightSettings.ForegroundElementsSettings.LineWeight = (int)numForegroundWeight.Value;
            LineWeightSettings.ForegroundElementsSettings.IsProjection = radFroegroundProj.Checked;


            LineWeightSettings.Middle1ElementsSettings.LineWeight = (int)numMiddle1Weight.Value;
            LineWeightSettings.Middle1ElementsSettings.IsProjection = radMiddle1Proj.Checked;


            LineWeightSettings.Middle2ElementsSettings.LineWeight = (int)numMiddle2Weight.Value;
            LineWeightSettings.Middle2ElementsSettings.IsProjection = radMiddle2Proj.Checked;


            LineWeightSettings.Middle3ElementsSettings.LineWeight = (int)numMiddle3Weight.Value;
            LineWeightSettings.Middle3ElementsSettings.IsProjection = radMiddle3Proj.Checked;


            LineWeightSettings.BackgroundElementsSettings.LineWeight = (int)numBackgroundWeight.Value;
            LineWeightSettings.BackgroundElementsSettings.IsProjection = radBackgroundProj.Checked;

            Close();
        }
    }
}
