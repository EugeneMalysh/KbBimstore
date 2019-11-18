using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    public partial class CadDetailConverterSelectForm : System.Windows.Forms.Form
    {

        private string settingsFilePath;
        private string templateFilePath;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.DB.Document doc;


        public CadDetailConverterSelectForm(UIApplication uiapp)
        {
            this.settingsFilePath = Path.Combine(KbBimstoreApp.DataFolderPath, "settings.xml");
            this.templateFilePath = Path.Combine(KbBimstoreApp.TemplatesFolderPath, "template.rvt");

            if (uiapp != null)
            {
                this.uiapp = uiapp;
                this.doc = uiapp.ActiveUIDocument.Document;

                this.InitializeComponent();
                this.CenterToScreen();

                InitUI();
            }
        }

        private void InitUI()
        {
            List<string> viewsNames = GetDraftAndPlanViewsNames();

            this.comboBoxImportView.Items.Clear();
            this.comboBoxImportView.Items.AddRange(viewsNames.ToArray());
            this.comboBoxImportView.SelectedIndex = 0;
        }

        private void resetActiveView()
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator viewPlansIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.ViewPlan)).GetElementIterator();

                while (viewPlansIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ViewPlan curViewPlan = viewPlansIterator.Current as Autodesk.Revit.DB.ViewPlan;

                    if (curViewPlan != null)
                    {
                        uiapp.ActiveUIDocument.ActiveView = curViewPlan;
                        break;
                    }
                }
            }
        }

        private void SelectAutoCADFile_Click(object sender, EventArgs e)
        {
            resetActiveView();

            DialogResult result = this.openAutoCADFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFileName = this.openAutoCADFileDialog.FileName;

                if (File.Exists(selectedFileName))
                {
                    try
                    {
                        Autodesk.Revit.DB.View curView = GetViewByName(this.comboBoxImportView.SelectedItem.ToString());
                        if (curView != null)
                        {
                            uiapp.ActiveUIDocument.ActiveView = curView;
                            CadDetailConverterHandler handler = new CadDetailConverterHandler(curView, selectedFileName);
                            ExternalEvent exEvent = ExternalEvent.Create(handler);
                            exEvent.Raise();

                            this.Close();
                            this.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("Exception", ex.Message);
                    }
                }
            }
        }

        private List<string> GetDraftAndPlanViewsNames()
        {
            List<string> names = new List<string>();

            FilteredElementCollector viewFilter = new FilteredElementCollector(doc);
            if (viewFilter != null)
            {
                FilteredElementIterator viewsIterator = viewFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = doc.GetElement(curElementId) as ElementType;

                        if (curElementType != null)
                        {
                            if (curElementType.GetType().Name == "ViewFamilyType")
                            {
                                Autodesk.Revit.DB.ViewFamilyType curViewFamilyType = (ViewFamilyType)curElementType;

                                if (curViewFamilyType != null)
                                {
                                    string curName = curViewFamilyType.Name + ": " + curView.Name;
                                    names.Add(curName);
                                }
                            }
                        }
                    }
                }
            }

            names.Sort();

            return names;
        }

        private void comboBoxImportView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedViewName = this.comboBoxImportView.SelectedItem.ToString();

            Autodesk.Revit.DB.View selectedView = GetViewByName(selectedViewName);
            if (selectedView != null)
            {
                uiapp.ActiveUIDocument.ActiveView = selectedView;
            }
        }

        private Autodesk.Revit.DB.View GetViewByName(string name)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator viewsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = doc.GetElement(curElementId) as ElementType;

                        if (curElementType != null)
                        {
                            if (curElementType.GetType().Name == "ViewFamilyType")
                            {
                                Autodesk.Revit.DB.ViewFamilyType curViewFamilyType = (ViewFamilyType)curElementType;

                                if (curViewFamilyType != null)
                                {
                                    string curViewName = curViewFamilyType.Name + ": " + curView.Name;

                                    if (curViewName == name)
                                    {
                                        return curView;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

    }
}
