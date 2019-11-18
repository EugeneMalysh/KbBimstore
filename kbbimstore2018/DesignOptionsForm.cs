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
using Autodesk.Revit.Creation;

namespace KbBimstore
{
    public partial class DesignOptionsForm : System.Windows.Forms.Form
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.DB.Document doc;
        private int viewsAmount = 0;        
        private string designScale = "";
        private string designTitleBlocksName = "";

        public DesignOptionsForm(UIApplication uiapp)
        {
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
            InitScales();
            InitTitleBlocks();
            InitDesignOptions();
        }

        private void InitScales()
        {
            this.comboBoxScales.Items.Clear();
            List<string> scalesNames = GetScalesNames();

            for (int t = 0; t < scalesNames.Count; t++)
            {
                this.comboBoxScales.Items.Add(scalesNames.ElementAt(t));
            }

            this.comboBoxScales.SelectedIndex = 0;
        }

        private void InitTitleBlocks()
        {
            this.comboBoxTitleBlocks.Items.Clear();
            List<string> titleBlocksNames = GetTitleBlocksNames();

            for (int t = 0; t < titleBlocksNames.Count; t++)
            {
                this.comboBoxTitleBlocks.Items.Add(titleBlocksNames.ElementAt(t));
            }

            this.comboBoxTitleBlocks.SelectedIndex = 0;
        }

        private void InitDesignOptions()
        {

        }

        private List<string> GetScalesNames()
        {
            return (KbBimstoreConst.getScalesNames());
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

        private HashSet<string> GetExistingViewsNames()
        {
            HashSet<string> viewsNames = new HashSet<string>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator viewsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    if (curView != null)
                    {
                        viewsNames.Add(curView.Name);
                    }
                }
            }

            return viewsNames;
        }

        private HashSet<string> GetExistingSheetsNames()
        {
            HashSet<string> sheetsNames = new HashSet<string>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator sheetsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.ViewSheet)).GetElementIterator();

                while (sheetsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ViewSheet curViewSheet = sheetsIterator.Current as Autodesk.Revit.DB.ViewSheet;
                    if (curViewSheet != null)
                    {
                        sheetsNames.Add(curViewSheet.Name);
                    }
                }
            }

            return sheetsNames;
        }

        private HashSet<string> GetExistingSheetsNumbers()
        {
            HashSet<string> sheetsNumbers = new HashSet<string>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator sheetsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.ViewSheet)).GetElementIterator();

                while (sheetsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ViewSheet curViewSheet = sheetsIterator.Current as Autodesk.Revit.DB.ViewSheet;
                    if (curViewSheet != null)
                    {
                        sheetsNumbers.Add(curViewSheet.SheetNumber);
                    }
                }
            }

            return sheetsNumbers;
        }

        private List<string> GetExistingLevelsNames()
        {
            List<string> levelsNames = new List<string>();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator levelsIterator = docFilter.OfClass(typeof(Level)).GetElementIterator();

                while (levelsIterator.MoveNext())
                {
                    Level curLevel = levelsIterator.Current as Level;
                    if (curLevel != null)
                    {
                        levelsNames.Add(curLevel.Name);
                    }
                }
            }
            return levelsNames;
        }

        private void UpdateDesignGridViews()
        {
            List<string> levelsNames = GetExistingLevelsNames();
            SortedDictionary<string, List<string>> designOptionsDictionary = getExistingDesignOptionsDictionary();
            
            while (this.dataGridViewDesignOptions.Rows.Count > this.viewsAmount)
            {
                try
                {
                    int lastRowIndex = this.dataGridViewDesignOptions.Rows.Count - 1;
                    this.dataGridViewDesignOptions.Rows.RemoveAt(lastRowIndex);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Exception: ", ex.Message);
                }
            }

            while (this.dataGridViewDesignOptions.Rows.Count < this.viewsAmount)
            {
                DataGridViewRow norRow = new DataGridViewRow();
                this.dataGridViewDesignOptions.Rows.Add(norRow);

                int lastRowIndex = this.dataGridViewDesignOptions.Rows.Count - 1;

                DataGridViewComboBoxCell setNameCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[0] as DataGridViewComboBoxCell;
                setNameCell.Items.Clear();
                if (designOptionsDictionary.Count > 0)
                {
                    SortedDictionary<string, List<string>>.KeyCollection optionSetsCollection = designOptionsDictionary.Keys;
                    foreach (String curKey in optionSetsCollection)
                    {
                        setNameCell.Items.Add(curKey);
                    }
                    setNameCell.Value = setNameCell.Items[0];
                }


                DataGridViewComboBoxCell optionNameCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[1] as DataGridViewComboBoxCell;                
                optionNameCell.Items.Clear();
                if(designOptionsDictionary.ContainsKey(setNameCell.Value.ToString()))
                {
                    List<string> curOptionsList = new List<string>();
                    if (designOptionsDictionary.TryGetValue(setNameCell.Value.ToString(), out curOptionsList))
                    {
                        if (curOptionsList.Count > 0)
                        {
                            curOptionsList.Sort();
                            foreach (string curOption in curOptionsList)
                            {
                                optionNameCell.Items.Add(curOption);
                            }
                            optionNameCell.Value = optionNameCell.Items[0];
                        }
                    }
                }

                DataGridViewTextBoxCell optionViewNameCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[2] as DataGridViewTextBoxCell;
                optionViewNameCell.Value = "DesignViewName" + lastRowIndex.ToString();

                DataGridViewComboBoxCell levelsCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[3] as DataGridViewComboBoxCell;
                levelsCell.Items.Clear();
                if (levelsNames.Count > 0)
                {
                    for (int s = 0; s < levelsNames.Count; s++)
                    {
                        levelsCell.Items.Add(levelsNames[s]);
                    }

                    levelsCell.Value = levelsNames[0];
                }

                DataGridViewTextBoxCell sheetNumberCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[4] as DataGridViewTextBoxCell;
                sheetNumberCell.Value = "DesignSheetNumber" + lastRowIndex.ToString();

                DataGridViewTextBoxCell sheetNameCell = this.dataGridViewDesignOptions.Rows[lastRowIndex].Cells[5] as DataGridViewTextBoxCell;
                sheetNameCell.Value = "DesignSheetName" + lastRowIndex.ToString();
            }
        }

        private void numericUpDownDesignSetsNum_ValueChanged(object sender, EventArgs e)
        {
            this.viewsAmount = decimal.ToInt32(this.numericUpDownViewNum.Value);
            UpdateDesignGridViews();
        }

        private void comboBoxScales_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.designScale = this.comboBoxScales.SelectedItem.ToString();
        }

        private void comboBoxTitleBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.designTitleBlocksName = this.comboBoxTitleBlocks.SelectedItem.ToString();
        }

        private DesignOptionsRequestData CreateRequestData()
        {
            DesignOptionsRequestData requestData = new DesignOptionsRequestData();

            requestData.viewsAmount = decimal.ToInt32(this.numericUpDownViewNum.Value);
            requestData.scaleName = this.comboBoxScales.SelectedItem.ToString();
            requestData.titleBlockName = this.comboBoxTitleBlocks.SelectedItem.ToString();

            for (int r = 0; r < this.dataGridViewDesignOptions.Rows.Count; r++)
            {
                DataGridViewRow curRow = this.dataGridViewDesignOptions.Rows[r];
                string curSetName = curRow.Cells[0].Value.ToString();
                string curOptionName = curRow.Cells[1].Value.ToString();
                string curViewName = curRow.Cells[2].Value.ToString();
                string curLevelName = curRow.Cells[3].Value.ToString();
                string curSheetNumber = curRow.Cells[4].Value.ToString();
                string curSheetName = curRow.Cells[5].Value.ToString();

                requestData.optionsInfos.Add(new Tuple<string, string, string, string, string, string>(curSetName, curOptionName, curViewName, curLevelName, curSheetNumber, curSheetName));
            }

            return requestData;
        }

        private List<DesignOption> getExistingDesignOptions()
        {
            IEnumerable<DesignOption> designOptions = new FilteredElementCollector(doc).OfClass(typeof(DesignOption)).Cast<DesignOption>();

            return (new List<DesignOption>(designOptions));
        }

        private List<Tuple<string, string>> getExistingDesignOptionsInfos()
        {
            List<Tuple<string, string>> designOptionsInfos = new List<Tuple<string, string>>();

            List<DesignOption> designOptions = getExistingDesignOptions();
            foreach (DesignOption curDesignOption in designOptions)
            {
                string curDesignOptionName = curDesignOption.Name;
                string curDesignOptionSetName = "";

                foreach (Parameter curParameter in curDesignOption.Parameters)
                {
                    if (curParameter.Definition.Name == "Design Option Set Id")
                    {
                        ElementId curSetElementId = curParameter.AsElementId();
                        if (curSetElementId != null)
                        {
                            Element curSetElement = doc.GetElement(curSetElementId);
                            if (curSetElement != null)
                            {
                                curDesignOptionSetName = curSetElement.Name;
                            }
                        }
                    }
                }

                if ((curDesignOptionName != "") && (curDesignOptionSetName != ""))
                {
                    designOptionsInfos.Add(new Tuple<string, string>(curDesignOptionName, curDesignOptionSetName));
                }
            }

            return designOptionsInfos;
        }

        private SortedDictionary<string, List<string>> getExistingDesignOptionsDictionary()
        {
            List<string> levelsNames = GetExistingLevelsNames();
            List<Tuple<string, string>> designOptionsInfos = getExistingDesignOptionsInfos();

            SortedDictionary<string, List<string>> designOptionsDictionary = new SortedDictionary<string, List<string>>();
            foreach (Tuple<string, string> curInfo in designOptionsInfos)
            {
                if (designOptionsDictionary.ContainsKey(curInfo.Item2))
                {
                    designOptionsDictionary[curInfo.Item2].Add(curInfo.Item1);
                }
                else
                {
                    List<string> norList = new List<string>();
                    norList.Add(curInfo.Item1);
                    designOptionsDictionary.Add(curInfo.Item2, norList);
                }
            }

            return designOptionsDictionary;
        }

        private bool validateDesignOptionsData()
        {
            HashSet<string> existViewsNames = GetExistingViewsNames();
            HashSet<string> existSheetsNames = GetExistingSheetsNames();
            HashSet<string> existSheetsNumbers = GetExistingSheetsNumbers();

            HashSet<string> curViewsNames = new HashSet<string>();
            HashSet<string> curSheetsNames = new HashSet<string>();
            HashSet<string> curSheetsNumbers = new HashSet<string>();

            for (int r = 0; r < dataGridViewDesignOptions.Rows.Count; r++)
            {
                DataGridViewRow curRow = dataGridViewDesignOptions.Rows[r];

                string curViewName = curRow.Cells[2].Value.ToString();
                string curSheetName = curRow.Cells[5].Value.ToString();
                string curSheetNumber = curRow.Cells[4].Value.ToString();

                if (existViewsNames.Contains(curViewName))
                {
                    TaskDialog.Show("Error", ("View with name " + curViewName + " already exists, please choose another name"));
                    return false;
                }

                if (existSheetsNames.Contains(curSheetName))
                {
                    TaskDialog.Show("Error", ("Sheet with name " + curSheetName + " already exists, please choose another name"));
                    return false;
                }


                if (existSheetsNumbers.Contains(curSheetNumber))
                {
                    TaskDialog.Show("Error", ("Sheet with number " + curSheetNumber + " already exists, please choose another number"));
                    return false;
                }

                if (curViewsNames.Contains(curViewName))
                {
                    TaskDialog.Show("Error", ("View name " + curViewName + " is used more than once, please choose another name"));
                    return false;
                }
                else
                {
                    curViewsNames.Add(curViewName);
                }

                if (curSheetsNames.Contains(curSheetName))
                {
                    TaskDialog.Show("Error", ("Sheet name " + curSheetName + " is used more than once, please choose another name"));
                    return false;
                }
                else
                {
                    curSheetsNames.Add(curSheetName);
                }


                if (curSheetsNumbers.Contains(curSheetNumber))
                {
                    TaskDialog.Show("Error", ("Sheet number " + curSheetNumber + " already exists, please choose another number"));
                    return false;
                }
                else
                {
                    curSheetsNumbers.Add(curSheetNumber);
                }
            }

            return true;
        }

        private void buttonCreateDesignOptions_Click(object sender, EventArgs e)
        {
            if (validateDesignOptionsData())
            {

                try
                {
                    DesignOptionsRequestData requestData = CreateRequestData();

                    if (requestData != null)
                    {
                        DesignOptionsRequestHandler handler = new DesignOptionsRequestHandler(requestData);
                        ExternalEvent exEvent = ExternalEvent.Create(handler);
                        exEvent.Raise();

                        this.Close();
                        this.Dispose();

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void dataGridViewDesignOptions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridViewCell currentCell = dataGridViewDesignOptions.CurrentCell;

            if (currentCell.ColumnIndex == 0)
            {
                SortedDictionary<string, List<string>> curDesignOptionsDictionary = getExistingDesignOptionsDictionary();

                DataGridViewComboBoxCell curOptionSetCombo = dataGridViewDesignOptions.Rows[currentCell.RowIndex].Cells[0] as DataGridViewComboBoxCell;
                DataGridViewComboBoxCell curOptionNameCombo = dataGridViewDesignOptions.Rows[currentCell.RowIndex].Cells[1] as DataGridViewComboBoxCell;

                if ((curOptionNameCombo != null) && (curOptionSetCombo != null))
                {
                    curOptionNameCombo.Items.Clear();
                    string curOptionSet = curOptionSetCombo.Value.ToString();

                    if (curDesignOptionsDictionary.ContainsKey(curOptionSet))
                    {
                        List<string> curOptionsNames = new List<string>();
                        if (curDesignOptionsDictionary.TryGetValue(curOptionSet, out curOptionsNames))
                        {
                            foreach (string curOptionName in curOptionsNames)
                            {
                                curOptionNameCombo.Items.Add(curOptionName);
                            }
                            curOptionNameCombo.Value = curOptionNameCombo.Items[0];
                        }
                    }
                }
            }
        }
    }
}
