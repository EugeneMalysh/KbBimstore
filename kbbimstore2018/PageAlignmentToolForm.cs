using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    public partial class PageAlignmentToolForm : System.Windows.Forms.Form
    {
        Autodesk.Revit.UI.UIApplication uiapp;
        Autodesk.Revit.DB.Document doc;
        bool scaleResult = false;

        public PageAlignmentToolForm(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.doc = uiapp.ActiveUIDocument.Document;
            this.InitializeComponent();
            this.CenterToScreen();
            this.InitUI();

            this.DialogResult = DialogResult.None;
        }

        public System.Windows.Forms.ComboBox getScaleComboBox()
        {
            return this.comboBoxSelectScale;
        }

        public bool getScaleResult()
        {
            return this.scaleResult;
        }

        private void InitUI()
        {
            InitScales();
        }


        private void InitScales()
        {
            this.comboBoxSelectScale.Items.Clear();
            List<string> scalesNames = GetScalesNames();

            for (int t = 0; t < scalesNames.Count; t++)
            {
                this.comboBoxSelectScale.Items.Add(scalesNames.ElementAt(t));
            }

            this.comboBoxSelectScale.SelectedIndex = 0;
        }


        private List<string> GetScalesNames()
        {
            return (KbBimstoreConst.getScalesNames());
        }

        private List<Autodesk.Revit.DB.ViewSheet> getSheetsByViewScale(int selScale)
        {
            List<Autodesk.Revit.DB.ViewSheet> selSheets = new List<Autodesk.Revit.DB.ViewSheet>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc).OfClass(typeof(Autodesk.Revit.DB.ViewSheet));

            if (docFilter != null)
            {
                FilteredElementIterator docFilterIterator = docFilter.GetElementIterator();
                while (docFilterIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ViewSheet curViewSheet = docFilterIterator.Current as Autodesk.Revit.DB.ViewSheet;
                    if (curViewSheet != null)
                    {
                        bool sheetHasSelectedScale = false;

                        ISet<ElementId> curViewSheetViews = curViewSheet.GetAllPlacedViews();
                        foreach (ElementId curElementId in curViewSheetViews)
                        {
                            Autodesk.Revit.DB.View curView = doc.GetElement(curElementId) as Autodesk.Revit.DB.View;
                            if (curView != null)
                            {
                                if (curView.Scale == selScale)
                                {
                                    sheetHasSelectedScale = true;
                                    break;
                                }
                            }
                        }

                        if (sheetHasSelectedScale)
                        {
                            selSheets.Add(curViewSheet);
                        }
                    }
                }
            }

            return selSheets;
        }

        private void buttonSelectScale_Click(object sender, EventArgs e)
        {
            string scaleStr = this.comboBoxSelectScale.SelectedItem.ToString();
            int scaleInt = KbBimstoreConst.getScaleValue(scaleStr);

            List<Autodesk.Revit.DB.ViewSheet> curViewSheets = getSheetsByViewScale(scaleInt);

            if (curViewSheets.Count > 0)
            {
                uiapp.ActiveUIDocument.ActiveView = curViewSheets[0];
                this.DialogResult = DialogResult.OK;
                this.scaleResult = true;
                this.Close();
                this.Dispose();  
            }
            else
            {
                this.DialogResult = DialogResult.None;
                TaskDialog.Show("Info", ("There are no sheets with scale " + scaleStr));
            }

        }
    }
}
