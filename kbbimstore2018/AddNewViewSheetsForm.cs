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
    public partial class AddNewViewSheetsForm : System.Windows.Forms.Form
    {
        private string newCellValue;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.DB.Document doc;

        public AddNewViewSheetsForm(UIApplication uiapp)
        {

            if (uiapp != null)
            {
                this.uiapp = uiapp;
                this.doc = uiapp.ActiveUIDocument.Document;
                this.InitializeComponent();
                this.CenterToScreen();

                //TaskDialog.Show("Info", "Existing Sheets Info:\r\n" + GetExistingViewSheetsInfosStr());
            }
        }

        private List<Level> GetExistingLevels()
        {
            List<Level> levels = new List<Level>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator levelsIterator = docFilter.OfClass(typeof(Level)).GetElementIterator();

                while (levelsIterator.MoveNext())
                {
                    Level curLevel = levelsIterator.Current as Level;
                    if (curLevel != null)
                    {
                        levels.Add(curLevel);
                    }
                }
            }
            return levels;
        }

        private List<Tuple<string, string, string>> GetExistingViewSheetsInfos()
        {
            List<Tuple<string, string, string>> infos = new List<Tuple<string, string, string>>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator viewsheetsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.ViewSheet)).GetElementIterator();

                while (viewsheetsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ViewSheet curViewSheet = viewsheetsIterator.Current as Autodesk.Revit.DB.ViewSheet;
                    if (curViewSheet != null)
                    {
                        string attachedViewName = "";
                        ISet<ElementId> subViews = curViewSheet.GetAllPlacedViews();
                        if (subViews.Count() > 0)
                        {
                            ElementId curSubViewId = subViews.ElementAt(0);
                            if (curSubViewId != null)
                            {
                                Autodesk.Revit.DB.View attachedView = doc.GetElement(curSubViewId) as Autodesk.Revit.DB.View;
                                if (attachedView != null)
                                {
                                    attachedViewName = attachedView.Name;
                                }
                            }
                        }

                        Tuple<string, string, string> norInfo = new Tuple<string, string, string>(curViewSheet.SheetNumber, curViewSheet.Name, attachedViewName);
                        infos.Add(norInfo);
                    }
                }
            }

            return infos;
        }

        private string GetExistingViewSheetsInfosStr()
        {
            StringBuilder strBld = new StringBuilder();

            List<Tuple<string, string, string>> infos = GetExistingViewSheetsInfos();

            foreach (Tuple<string, string, string> curInfo in infos)
            {
                strBld.AppendLine("number:" + curInfo.Item1 + " name:" + curInfo.Item2 + " viewId:" + curInfo.Item3);
            }

            return strBld.ToString();
        }

        private List<string> GetTitleBlocksNames()
        {
            List<string> names = new List<string>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator titleBlocksIterator = docFilter.OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks).GetElementIterator();

                while (titleBlocksIterator.MoveNext())
                {
                    Element titleBlock = titleBlocksIterator.Current;
                    if (titleBlock != null)
                    {
                        names.Add(titleBlock.Name);
                    }
                }
            }

            return names;
        }

        private List<string> GetScalesNames()
        {
            return (KbBimstoreConst.getScalesNames());
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

        private void dataGridViewMainSheet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.dataGridViewSheets.CurrentCell.IsInEditMode)
            {
                if (this.dataGridViewSheets.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.dataGridViewSheets.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if ((cell.ColumnIndex == 3) && (!cell.Items.Contains(e.FormattedValue)))
                    {
                        cell.Value = e.FormattedValue;
                        string testValue = e.FormattedValue.ToString();

                        if (testValue.All(c => Char.IsLetterOrDigit(c) || c == '_'))
                        {
                            cell.Items.Add(e.FormattedValue);
                            newCellValue = testValue;
                        }
                        else
                        {
                            newCellValue = "";
                            TaskDialog.Show("Error", "Please use only letters, numbers and underscores");
                        }
                    }
                    else
                    {
                        cell.Value = e.FormattedValue;
                        newCellValue = "";
                    }
                }
            }

        }

        private void dataGridViewMainSheet_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (newCellValue != "")
            {
                if (this.dataGridViewSheets.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewCell cell = this.dataGridViewSheets.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = newCellValue.Clone();
                }

                newCellValue = "";
            }

        }

        private void dataGridViewMainSheet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control.GetType() ==
                typeof(DataGridViewComboBoxEditingControl))
                {
                    ((System.Windows.Forms.ComboBox)e.Control).DropDownStyle =
                    ComboBoxStyle.DropDown;
                    ((System.Windows.Forms.ComboBox)e.Control).PreviewKeyDown -= new
                    PreviewKeyDownEventHandler(dataGridViewMainSheet_PreviewKeyDown);
                    ((System.Windows.Forms.ComboBox)e.Control).PreviewKeyDown += new
                    PreviewKeyDownEventHandler(dataGridViewMainSheet_PreviewKeyDown);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewMainSheet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (newCellValue != "")
                {
                    this.dataGridViewSheets.Rows.RemoveAt(this.dataGridViewSheets.Rows.Add());
                }
            }
        }

        private void dataGridViewMainSheet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private AddNewViewSheetsRequestData CreateRequestData()
        {
            AddNewViewSheetsRequestData requestData = new AddNewViewSheetsRequestData();


            for (int r = 0; r < this.dataGridViewSheets.Rows.Count; r++)
            {
                DataGridViewRow curViewRow = this.dataGridViewSheets.Rows[r];

                string[] mainItems = new string[5];
                for (int c = 0; c < 5; c++)
                {
                    DataGridViewCell curCell = curViewRow.Cells[c] as DataGridViewCell;
                    if (curCell.Value != null)
                    {
                        mainItems[c] = curCell.Value.ToString();
                    }
                    else
                    {
                        TaskDialog.Show("Error", "Value is null for cell: " + c.ToString());
                        mainItems[c] = "";
                    }
                }

                requestData.viewSheetsInfo.Add(new Tuple<string, string, string, string, string>(mainItems[0], mainItems[1], mainItems[2], mainItems[3], mainItems[4]));
            }


            if (requestData != null)
            {
                HashSet<string> allSheetsNumbers = new HashSet<string>();
                HashSet<string> allSheetsNames = new HashSet<string>();
                HashSet<string> allViewsNames = new HashSet<string>();

                for (int m = 0; m < requestData.viewSheetsInfo.Count; m++)
                {
                    Tuple<string, string, string, string, string> curViewSheetInfo = requestData.viewSheetsInfo[m];

                    if (allSheetsNames.Contains(curViewSheetInfo.Item1))
                    {
                        TaskDialog.Show("Error", "More than one sheet has number: " + curViewSheetInfo.Item1 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curViewSheetInfo.Item1);
                    }

                    if (allSheetsNames.Contains(curViewSheetInfo.Item2))
                    {
                        TaskDialog.Show("Error", "More than one sheet has name: " + curViewSheetInfo.Item2 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curViewSheetInfo.Item2);
                    }

                }
            }

            return requestData;
        }

        private void buttonAddViews_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewViewSheetsRequestData requestData = CreateRequestData();

                if (requestData != null)
                {
                    AddNewViewSheetsRequestHandler handler = new AddNewViewSheetsRequestHandler(requestData);
                    ExternalEvent exEvent = ExternalEvent.Create(handler);
                    exEvent.Raise();


                    this.Close();
                    this.Dispose();
                }
                else
                {
                    TaskDialog.Show("Info", "requestData == null");
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void addView_Click(object sender, EventArgs e)
        {
            addRow();
        }

        private void deleteView_Click(object sender, EventArgs e)
        {
            int lastRowIndex = this.dataGridViewSheets.Rows.Count - 1;

            if (lastRowIndex > 0)
            {
                this.dataGridViewSheets.Rows.RemoveAt(lastRowIndex);
            }
        }

        private void addRow()
        {
            DataGridViewRow norRow = new DataGridViewRow();
            this.dataGridViewSheets.Rows.Add(norRow);
            int lastRowIndex = this.dataGridViewSheets.Rows.Count - 1;

            DataGridViewTextBoxCell numberCell = this.dataGridViewSheets.Rows[lastRowIndex].Cells[0] as DataGridViewTextBoxCell;
            numberCell.Value = "SheetNumber" + lastRowIndex.ToString();

            DataGridViewTextBoxCell nameCell = this.dataGridViewSheets.Rows[lastRowIndex].Cells[1] as DataGridViewTextBoxCell;
            nameCell.Value = "SheetName" + lastRowIndex.ToString();

            DataGridViewComboBoxCell scaleCell = this.dataGridViewSheets.Rows[lastRowIndex].Cells[2] as DataGridViewComboBoxCell;
            scaleCell.Items.Clear();
            List<string> scalesNames = GetScalesNames();
            if (scalesNames.Count > 0)
            {
                for (int s = 0; s < scalesNames.Count; s++)
                {
                    scaleCell.Items.Add(scalesNames[s]);
                }

                scaleCell.Value = scalesNames[0];
            }

            DataGridViewComboBoxCell templateCell = this.dataGridViewSheets.Rows[lastRowIndex].Cells[3] as DataGridViewComboBoxCell;
            templateCell.Items.Clear();
            List<string> templatesNames = GetDraftAndPlanViewsNames();
            if (templatesNames.Count > 0)
            {
                for (int t = 0; t < templatesNames.Count; t++)
                {
                    templateCell.Items.Add(templatesNames[t]);
                }

                templateCell.Value = templatesNames[0];
            }

            DataGridViewComboBoxCell titleblockCell = this.dataGridViewSheets.Rows[lastRowIndex].Cells[4] as DataGridViewComboBoxCell;
            titleblockCell.Items.Clear();
            List<string> titleblocksNames = GetTitleBlocksNames();
            if (titleblocksNames.Count > 0)
            {
                for (int t = 0; t < titleblocksNames.Count; t++)
                {
                    titleblockCell.Items.Add(titleblocksNames[t]);
                }

                titleblockCell.Value = titleblocksNames[0];
            }
        }
    }
}
