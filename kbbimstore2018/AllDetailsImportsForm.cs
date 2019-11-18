using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    public partial class AllDetailsImportsForm : System.Windows.Forms.Form
    {
        

        private Document doc;
        private UIDocument uidoc;
        private UIApplication uiapp;
        private Document importedDoc;
        private string detailsName = "";
        private string selectedFilePath = "";

        private string BaseAndTransitionDetailsTemplatePath = "";
        private string CeilingDetailsTemplatePath = "";
        private string DoorAndWindowDetailsTemplatePath = "";
        private string MillworkTemplatePath = "";
        private string PartitionTemplatePath = "";
        private PreviewControl prevControl;
                

        public AllDetailsImportsForm(UIApplication uiapp, string detailsName)
        {
            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;
            this.detailsName = detailsName;

            InitializeComponent();

            LoadSettings();

            if (detailsName == "BaseAndTransitionDetails")
            {
                selectedFilePath = BaseAndTransitionDetailsTemplatePath;
            }

            if (detailsName == "CeilingDetails")
            {
                selectedFilePath = CeilingDetailsTemplatePath;
            }

            if (detailsName == "DoorAndWindowDetails")
            {
                selectedFilePath = DoorAndWindowDetailsTemplatePath;
            }

            if (detailsName == "Millwork")
            {
                selectedFilePath = MillworkTemplatePath;
            }

            if (detailsName == "Partition")
            {
                selectedFilePath = PartitionTemplatePath;
            }

            if (File.Exists(selectedFilePath))
            {
                if (selectedFilePath.EndsWith(".rvt", true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    importedDoc = this.uiapp.Application.OpenDocumentFile(selectedFilePath);

                }
                else
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Details", "Import Path is not a Revit File, please correct settings");
                    return;
                }

                InitUI();


                this.ShowDialog();
            }
            else
            {
                Autodesk.Revit.UI.TaskDialog.Show("Details", "Import file: " + selectedFilePath + " does not exist, please correct settings");
            }
        }

        private void InitUI()
        {
            this.dataGridViewDocViews.Rows.Clear();

            if (importedDoc != null)
            {
                List<string> viewsNames = GetDraftAndPlanViewsNames(importedDoc);

                for (int v = 0; v < viewsNames.Count; v++)
                {
                    DataGridViewRow norRow = new DataGridViewRow();
                    this.dataGridViewDocViews.Rows.Add(norRow);

                    int lastRowIndex = this.dataGridViewDocViews.Rows.Count - 1;

                    DataGridViewTextBoxCell nameCell = this.dataGridViewDocViews.Rows[lastRowIndex].Cells[1] as DataGridViewTextBoxCell;
                    nameCell.Value = viewsNames[v];
                }
            }
        }


        private int LoadSettings()
        {
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));

                if (File.Exists(KbBimstoreApp.ImporSsettingsFilePath))
                {
                    TextReader reader = new StreamReader(KbBimstoreApp.ImporSsettingsFilePath);
                    DataSet myDataSet = (DataSet)xmlSer.Deserialize(reader);
                    reader.Close();

                    if (!myDataSet.Tables.Contains("Settings"))
                    {
                        return -4;
                    }

                    DataTable settingsTable = myDataSet.Tables["Settings"];

                    if (settingsTable.Columns.Count < 2)
                    {
                        return -5;
                    }

                    if (settingsTable.Rows.Count < 5)
                    {
                        return -6;
                    }

                    DataRow BaseAndTransitionDataRow = settingsTable.Rows[0];
                    if (BaseAndTransitionDataRow[0].ToString() == "BaseAndTransitionData")
                    {
                        this.BaseAndTransitionDetailsTemplatePath = BaseAndTransitionDataRow[1].ToString();
                    }

                    DataRow CeilingDataRow = settingsTable.Rows[1];
                    if (CeilingDataRow[0].ToString() == "Ceiling")
                    {
                        this.CeilingDetailsTemplatePath = CeilingDataRow[1].ToString();
                    }

                    DataRow DoorAndWindowDataRow = settingsTable.Rows[2];
                    if (DoorAndWindowDataRow[0].ToString() == "DoorAndWindow")
                    {
                        this.DoorAndWindowDetailsTemplatePath = DoorAndWindowDataRow[1].ToString();
                    }

                    DataRow MillworkDataRow = settingsTable.Rows[3];
                    if (MillworkDataRow[0].ToString() == "Millwork")
                    {
                        this.MillworkTemplatePath = MillworkDataRow[1].ToString();
                    }

                    DataRow PartitionDataRow = settingsTable.Rows[4];
                    if (PartitionDataRow[0].ToString() == "Partition")
                    {
                        this.PartitionTemplatePath = PartitionDataRow[1].ToString();
                    }

                    return 1;
                }
                else
                {
                    return -3;
                }
            }
            catch (Exception ex)
            {
                return -2;
            }

            return -1;
        }

        private List<string> GetDraftAndPlanViewsNames(Document vdoc)
        {
            List<string> names = new List<string>();

            FilteredElementCollector viewFilter = new FilteredElementCollector(vdoc);
            if (viewFilter != null)
            {
                FilteredElementIterator viewsIterator = viewFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan") || (curViewTypeName == "ViewSchedule"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = vdoc.GetElement(curElementId) as ElementType;

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

        private Autodesk.Revit.DB.View GetViewByName(string name, Document vdoc)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(vdoc);

            if (docFilter != null)
            {
                FilteredElementIterator viewsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan") || (curViewTypeName == "ViewSheet") || (curViewTypeName == "ViewSchedule"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = vdoc.GetElement(curElementId) as ElementType;

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

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            List<Autodesk.Revit.DB.View> selectedViews = new List<Autodesk.Revit.DB.View>();

            foreach (DataGridViewRow curRow in this.dataGridViewDocViews.Rows)
            {
                if (curRow.Cells.Count > 1)
                {
                    DataGridViewCheckBoxCell curCheckbox = curRow.Cells[0] as DataGridViewCheckBoxCell;
                    if (curCheckbox != null)
                    {
                        if ((curCheckbox.Value != null) && ((bool)curCheckbox.Value))
                        {
                            DataGridViewTextBoxCell curTextBox = curRow.Cells[1] as DataGridViewTextBoxCell;
                            string curViewName = curTextBox.Value.ToString();

                            Autodesk.Revit.DB.View curView = GetViewByName(curViewName, importedDoc);
                            if (curView != null)
                            {
                                selectedViews.Add(curView);
                            }
                        }
                    }
                }
            }

            AllDetailsImportsHandler handler = new AllDetailsImportsHandler(this.uiapp, this.doc, this.importedDoc, selectedViews);
            handler.Execute(this.uiapp);

            this.Close();
        }

        private void dataGridViewDocViews_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    if (importedDoc != null)
                    {
                        if (sender is DataGridView)
                        {
                            DataGridView curDataGridView = sender as DataGridView;
                            if (curDataGridView != null)
                            {
                                DataGridViewCell curCell = curDataGridView.CurrentCell;
                                if (curCell != null)
                                {
                                    DataGridViewRow curRow = this.dataGridViewDocViews.Rows[curCell.RowIndex];
                                    DataGridViewTextBoxCell curTextBox = curRow.Cells[1] as DataGridViewTextBoxCell;
                                    if (curTextBox != null)
                                    {
                                        string curViewName = curTextBox.Value.ToString(); 
                                        Autodesk.Revit.DB.View curView = GetViewByName(curViewName, importedDoc);
                                        
                                        this.panelPreview.Controls.Clear();
                                        if (curView != null)
                                        {
                                            if (prevControl != null)
                                            {
                                                prevControl.Dispose();
                                            }

                                            prevControl = new PreviewControl(importedDoc, curView.Id);
                                            if (prevControl != null)
                                            {
                                                ElementHost prevHost = new ElementHost();
                                                prevHost.Child = prevControl;
                                                prevHost.Dock = DockStyle.Fill;
                                                this.panelPreview.Controls.Add(prevHost);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
