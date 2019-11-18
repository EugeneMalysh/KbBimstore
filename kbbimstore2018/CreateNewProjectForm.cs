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

using MSExcel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace KbBimstore
{
    public partial class CreateNewProjectForm : System.Windows.Forms.Form
    {
        private CreateNewProjectModelMain mainSheetsModel = new CreateNewProjectModelMain();

        private string tempFilePath;
        private string settingsFilePath;
        private string templateFilePath;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.DB.Document doc;
        private string newCellValue;

        private int titleBlockIndex = 0;
        private double levelsDistance = 0.0d;
        private int levelsAmount = 0;
        private int frontSheetsAmount = 0;
        private int mainSheetsAmount = 0;
        private int mainSheetsScalesIndex = 0;

        private string previousExcelFilePath = "";
        private List<string> sheetNamePrefixes = new List<string>();

        public CreateNewProjectForm(UIApplication uiapp)
        {
            this.settingsFilePath = Path.Combine(KbBimstoreApp.DataFolderPath, "settings.xml");
            this.templateFilePath = Path.Combine(KbBimstoreApp.TemplatesFolderPath, "template.rvt");

            if (uiapp != null)
            {
                this.uiapp = uiapp;
                this.InitializeComponent();
                this.CenterToScreen();

                this.excelFilePathComboBox.Items.Add("None"); //blank item for no excel file
                this.excelFilePathComboBox.SelectedIndex = 0;

                InitUI();
            }
        }

        private void ClearForm()
        {
            this.textBoxDistBetweenLavels.Text = "0.0";
            this.numericUpDownAmountOfLevels.Value = 0;
            this.numericUpDownFrontSheetAmount.Value = 0;
            this.numericUpDownMainSheetAmount.Value = 0;

            this.dataGridViewFrontSheet.Rows.Clear();
            this.dataGridViewMainSheet.Rows.Clear();
        }

        private void InitUI()
        {
            ClearForm();

            ClearTempFolder();

            this.tempFilePath = CreateTempFile();

            SaveAsOptions sao = new SaveAsOptions();
            sao.OverwriteExistingFile = true;

            if (File.Exists(templateFilePath))
            {
                try
                {
                    this.uiapp.OpenAndActivateDocument(templateFilePath);
                    this.doc = this.uiapp.ActiveUIDocument.Document;
                    
                    if (File.Exists(tempFilePath))
                    {
                        try
                        {
                            File.Delete(tempFilePath);
                        }
                        catch
                        {

                        }
                    }
                    
                    InitTitleBlocks();
                }
                catch (Exception exc)
                {
                    //TaskDialog.Show("Exception: ", exc.Message);
                }
            }
            else
            {
                TaskDialog.Show("Template file does not exits: ", templateFilePath);
            }

            List<string> scalesNames = GetScalesNames();
            this.comboBoxChooseScale.Items.Clear();
            for (int i = 0; i < scalesNames.Count; i++)
            {
                this.comboBoxChooseScale.Items.Add(scalesNames[i]);
            }
            if (this.comboBoxChooseScale.Items.Count > 0)
            {
                this.comboBoxChooseScale.SelectedIndex = 0;
            }
        }

        private void InitTitleBlocks()
        {
            this.comboBoxSelectTitleBlock.Items.Clear();
            List<string> titleBlocksNames = GetTitleBlocksNames();

            for (int t = 0; t < titleBlocksNames.Count; t++)
            {
                this.comboBoxSelectTitleBlock.Items.Add(titleBlocksNames.ElementAt(t));
            }

            this.comboBoxSelectTitleBlock.SelectedIndex = 0;
            this.titleBlockIndex = this.comboBoxSelectTitleBlock.SelectedIndex;
        }

        private void ClearTempFolder()
        {
            if (Directory.Exists(KbBimstoreApp.TempFolderPath))
            {
                IEnumerable<string> filesNames = Directory.EnumerateFiles(KbBimstoreApp.TempFolderPath);
                foreach (string fileName in filesNames)
                {
                    string filePath = Path.Combine(KbBimstoreApp.TempFolderPath, fileName);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }

        private string CreateTempFile()
        {
            StringBuilder curTempFileBuilder = new StringBuilder();
            curTempFileBuilder.Append("temp");

            DateTime curDateTime = DateTime.Now;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            curTempFileBuilder.Append(Convert.ToInt64((curDateTime.ToUniversalTime() - epoch).TotalSeconds));
            curTempFileBuilder.Append(".rvt");

            string tfPath = Path.Combine(KbBimstoreApp.TempFolderPath, curTempFileBuilder.ToString());
            return tfPath;
        }

        private void OnLevelsDistanceChanged(object sender, EventArgs e)
        {
            try
            {
                this.levelsDistance = Convert.ToDouble(textBoxDistBetweenLavels.Text);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", "Distance between levels must be a positive number");
                textBoxDistBetweenLavels.Text = this.levelsDistance.ToString();
            }
        }

        private void OnLevelsAmountChanged(object sender, EventArgs e)
        {
            this.levelsAmount = decimal.ToInt32(numericUpDownAmountOfLevels.Value);

            if (this.levelsAmount > mainSheetsModel.getLevelsNumber())
            {
                int norLevelsNumber = this.levelsAmount - mainSheetsModel.getLevelsNumber();
                mainSheetsModel.addLevels(norLevelsNumber, decimal.ToInt32(this.numericUpDownStartingNumber.Value));
            }
            else if (this.levelsAmount < mainSheetsModel.getLevelsNumber())
            {
                int delLevelsNumber = mainSheetsModel.getLevelsNumber() - this.levelsAmount;
                mainSheetsModel.deleteLevels(delLevelsNumber);
            }

            UpdateMainSheets();
        }

        private void OnFrontSheetAmountChanged(object sender, EventArgs e)
        {
            this.frontSheetsAmount = decimal.ToInt32(numericUpDownFrontSheetAmount.Value);

            while (dataGridViewFrontSheet.Rows.Count > frontSheetsAmount)
            {
                try
                {
                    int lastRowIndex = dataGridViewFrontSheet.Rows.Count - 1;
                    dataGridViewFrontSheet.Rows.RemoveAt(lastRowIndex);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Exception: ", ex.Message);
                }
            }

            if (dataGridViewFrontSheet.Rows.Count < frontSheetsAmount)
            {
                List<string> drviewsNames = GetDraftAndPlanViewsNames();

                while (dataGridViewFrontSheet.Rows.Count < frontSheetsAmount)
                {
                    DataGridViewRow norRow = new DataGridViewRow();
                    dataGridViewFrontSheet.Rows.Add(norRow);

                    int lastRowIndex = dataGridViewFrontSheet.Rows.Count - 1;

                    DataGridViewTextBoxCell numberCell = dataGridViewFrontSheet.Rows[lastRowIndex].Cells[0] as DataGridViewTextBoxCell;
                    numberCell.Value = "FrontSheetNumber" + lastRowIndex.ToString();

                    DataGridViewTextBoxCell nameCell = dataGridViewFrontSheet.Rows[lastRowIndex].Cells[1] as DataGridViewTextBoxCell;
                    nameCell.Value = "FrontSheetName" + lastRowIndex.ToString();

                    DataGridViewComboBoxCell drviewCell = dataGridViewFrontSheet.Rows[lastRowIndex].Cells[2] as DataGridViewComboBoxCell;
                    drviewCell.Items.Clear();
                    if (drviewsNames.Count > 0)
                    {
                        for (int d = 0; d < drviewsNames.Count; d++)
                        {
                            drviewCell.Items.Add(drviewsNames[d]);
                        }

                        drviewCell.Value = drviewsNames[0];
                    }
                }
            }
        }

        private void OnMainSheetAmountChanged(object sender, EventArgs e)
        {
            this.mainSheetsAmount = decimal.ToInt32(numericUpDownMainSheetAmount.Value);

            if (this.mainSheetsAmount > mainSheetsModel.getSheetsNumber())
            {
                int norSheetNumber = this.mainSheetsAmount - mainSheetsModel.getSheetsNumber();
                mainSheetsModel.addSheets(norSheetNumber, decimal.ToInt32(this.numericUpDownStartingNumber.Value));
            }
            else if (this.mainSheetsAmount < mainSheetsModel.getSheetsNumber())
            {
                int delSheetNumber = mainSheetsModel.getSheetsNumber() - this.mainSheetsAmount;
                mainSheetsModel.deleteSheets(delSheetNumber);
            }

            UpdateMainSheets(true);
        }

        private void UpdateMainSheets(bool addSheets = false)
        {
            List<string> templatesNames = GetViewTemplates();

            string selectedExcelFilePath = Convert.ToString(this.excelFilePathComboBox.SelectedItem);

            if (selectedExcelFilePath != null && this.excelFilePathComboBox.SelectedIndex != 0 && File.Exists(selectedExcelFilePath) && selectedExcelFilePath != this.previousExcelFilePath)
            {
                this.sheetNamePrefixes = ParseExcelFile(selectedExcelFilePath);
                this.previousExcelFilePath = selectedExcelFilePath;
            }
            else if (this.excelFilePathComboBox.SelectedIndex == 0)
                this.sheetNamePrefixes = new List<string>();

            int showLevelsNumber = Math.Min(this.levelsAmount, this.mainSheetsModel.getLevelsNumber());
            int showSheetsNumber = Math.Min(this.mainSheetsAmount, this.mainSheetsModel.getSheetsNumber());

            List<int> defaultNumbers = new List<int>();
            if (dataGridViewMainSheet.Rows.Count > 0)
            {
                int existingSheetsPerLevel = showSheetsNumber;
                if (addSheets) existingSheetsPerLevel--; //if we added sheets then we know n-1 values, not n

                for (int i = 0; i < existingSheetsPerLevel; i++) //only take the first set 
                {
                    var cell = Convert.ToString(this.dataGridViewMainSheet.Rows[i].Cells[0].Value);
                    if (cell != null)
                    {
                        int value;

                        char firstIntInText = cell.FirstOrDefault(x => Char.IsDigit(x));
                        if (firstIntInText != default(Char) && int.TryParse(Convert.ToString(firstIntInText), out value))
                        {
                            defaultNumbers.Add(value);
                        }
                        else
                        {
                            defaultNumbers.Add(i);
                        }
                    }
                    else
                    {
                        defaultNumbers.Add(i);
                    }
                }

            }

            dataGridViewMainSheet.Rows.Clear();

            for (int l = 0; l < showLevelsNumber; l++)
            {
                for (int s = 0; s < showSheetsNumber; s++)
                {
                    int lastRowNumber = s + (l * showSheetsNumber);

                    DataGridViewRow norRow = new DataGridViewRow();
                    dataGridViewMainSheet.Rows.Add(norRow);

                    DataGridViewTextBoxCell numberCell = dataGridViewMainSheet.Rows[lastRowNumber].Cells[0] as DataGridViewTextBoxCell;
                    int numberVal = l + decimal.ToInt32(this.numericUpDownStartingNumber.Value);
                    if (numberVal < 10)
                    {
                        numberCell.Value = this.textBoxPrefix.Text + (defaultNumbers.Count > s ? defaultNumbers[s] : s).ToString() + this.textBoxBetween.Text + numberVal.ToString() + this.textBoxSuffix.Text;
                    }
                    else
                    {
                        numberCell.Value = this.textBoxPrefix.Text + (defaultNumbers.Count > s ? defaultNumbers[s] : s).ToString() + numberVal.ToString() + this.textBoxSuffix.Text;
                    }

                    if (l > 0)
                    {
                        int modelLevel = l - 1;

                        DataGridViewTextBoxCell nameCell = dataGridViewMainSheet.Rows[lastRowNumber].Cells[1] as DataGridViewTextBoxCell;
                        string sheetName = this.mainSheetsModel.otherLevels[modelLevel].sheets[s].Item1;

                        if (this.sheetNamePrefixes.Count > numberVal && !string.IsNullOrEmpty(sheetName) && sheetName.LastIndexOf('(') != -1)
                        {
                            nameCell.Value = this.sheetNamePrefixes[numberVal] + string.Join("", sheetName.Take(sheetName.LastIndexOf('(')));
                        }
                        else
                            nameCell.Value = (this.sheetNamePrefixes.Count > numberVal ? this.sheetNamePrefixes[numberVal] : "") + sheetName;

                        int curSelectionIndex = this.mainSheetsModel.otherLevels[modelLevel].sheets[s].Item2;
                        DataGridViewComboBoxCell templateCell = dataGridViewMainSheet.Rows[lastRowNumber].Cells[2] as DataGridViewComboBoxCell;
                        templateCell.Items.Clear();
                        if (templatesNames.Count > 0)
                        {
                            for (int t = 0; t < templatesNames.Count; t++)
                            {
                                templateCell.Items.Add(templatesNames[t]);
                            }

                            if (templatesNames.Count > curSelectionIndex)
                            {
                                templateCell.Value = templatesNames[curSelectionIndex];
                            }
                        }
                    }
                    else
                    {
                        DataGridViewTextBoxCell nameCell = dataGridViewMainSheet.Rows[lastRowNumber].Cells[1] as DataGridViewTextBoxCell;
                        string sheetName = this.mainSheetsModel.firstLevel.sheets[s].Item1;

                        if (this.sheetNamePrefixes.Count > numberVal && !string.IsNullOrEmpty(sheetName) && sheetName.LastIndexOf('(') != -1)
                        {
                            nameCell.Value = this.sheetNamePrefixes[numberVal] + string.Join("", sheetName.Take(sheetName.LastIndexOf('(')));
                        }
                        else
                            nameCell.Value = (this.sheetNamePrefixes.Count > numberVal ? this.sheetNamePrefixes[numberVal] : "") + sheetName;

                        int curSelectionIndex = this.mainSheetsModel.firstLevel.sheets[s].Item2;
                        DataGridViewComboBoxCell templateCell = dataGridViewMainSheet.Rows[lastRowNumber].Cells[2] as DataGridViewComboBoxCell;
                        templateCell.Items.Clear();
                        if (templatesNames.Count > 0)
                        {
                            for (int t = 0; t < templatesNames.Count; t++)
                            {
                                templateCell.Items.Add(templatesNames[t]);
                            }

                            if (templatesNames.Count > curSelectionIndex)
                            {
                                templateCell.Value = templatesNames[curSelectionIndex];
                            }
                        }
                    }

                }
            }

        }

        private void OnSaveConfiguration(object sender, EventArgs e)
        {
            SaveSettings(settingsFilePath, "saved");
        }

        private void OnLoadConfiguration(object sender, EventArgs e)
        {
            LoadSettings(settingsFilePath, "loaded");
        }

        private void OnCreateProject(object sender, EventArgs e)
        {
            try
            {
                double testDouble = Convert.ToDouble(this.textBoxDistBetweenLavels.Text);

                if (testDouble > 0)
                {
                    CreateNewProjectRequestData requestData = CreateRequestData();

                    if (requestData != null)
                    {
                        CreateNewProjectRequestHandler handler = new CreateNewProjectRequestHandler(requestData);
                        ExternalEvent exEvent = ExternalEvent.Create(handler);
                        exEvent.Raise();

                        this.Close();
                        this.Dispose();
                    }
                }
                else
                {
                    TaskDialog.Show("Error", "Distance between levels must be a positive number");
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", "Some required parameters are missing. Ensure you have the following parameters specified:\n TitleBlock\nLevel Distance\nNumber of Levels\nNumber of Front Sheets\nNumber of Main Sheets\nSheet Scale");
            }
        }

        private CreateNewProjectRequestData CreateRequestData()
        {
            CreateNewProjectRequestData requestData = new CreateNewProjectRequestData();

            requestData.titleBlockName = Convert.ToString(comboBoxSelectTitleBlock.SelectedItem); //will make it null if none is selected which then translates to -1 titleblock id when creating sheets

            if (Convert.ToDouble(this.textBoxDistBetweenLavels.Text) > 0)
                requestData.levelsDistance = Convert.ToDouble(this.textBoxDistBetweenLavels.Text);
            else
            {
                TaskDialog.Show("Error", "Distance between levels must be a positive number");
                return null;
            }

            if (this.comboBoxChooseScale.SelectedItem != null)
                requestData.mainSheetsScale = comboBoxChooseScale.SelectedItem.ToString();
            else
            {
                TaskDialog.Show("Error", "Please choose a scale");
                return null;
            }

            requestData.levelsAmount = decimal.ToInt32(this.numericUpDownAmountOfLevels.Value);
            requestData.frontSheetsAmount = decimal.ToInt32(this.numericUpDownFrontSheetAmount.Value);
            requestData.mainSheetsAmount = decimal.ToInt32(this.numericUpDownMainSheetAmount.Value);

            for (int r = 0; r < this.dataGridViewFrontSheet.Rows.Count; r++)
            {
                DataGridViewRow curViewRow = this.dataGridViewFrontSheet.Rows[r];

                string[] frontItems = new string[3];
                for (int c = 0; c < 3; c++)
                {
                    DataGridViewCell curCell = curViewRow.Cells[c] as DataGridViewCell;
                    if (curCell.Value != null)
                    {
                        frontItems[c] = curCell.Value.ToString();
                    }
                    else
                    {
                        frontItems[c] = "";
                    }
                }

                requestData.frontSheetsInfo.Add(new Tuple<string, string, string>(frontItems[0], frontItems[1], frontItems[2]));
            }

            for (int r = 0; r < this.dataGridViewMainSheet.Rows.Count; r++)
            {
                DataGridViewRow curViewRow = this.dataGridViewMainSheet.Rows[r];

                string[] mainItems = new string[3];
                for (int c = 0; c < 3; c++)
                {
                    DataGridViewCell curCell = curViewRow.Cells[c] as DataGridViewCell;
                    if (curCell.Value != null)
                    {
                        mainItems[c] = curCell.Value.ToString();
                    }
                    else
                    {
                        mainItems[c] = "";
                    }
                }

                requestData.mainSheetsInfo.Add(new Tuple<string, string, string>(mainItems[0], mainItems[1], mainItems[2]));
            }

            if (requestData != null) //it would never be null
            {
                HashSet<string> allSheetsNumbers = new HashSet<string>();
                HashSet<string> allSheetsNames = new HashSet<string>();
                HashSet<string> allViewsNames = new HashSet<string>();

                for (int f = 0; f < requestData.frontSheetsInfo.Count; f++)
                {
                    Tuple<string, string, string> curFrontSheetInfo = requestData.frontSheetsInfo[f];

                    if (allSheetsNames.Contains(curFrontSheetInfo.Item1))
                    {
                        TaskDialog.Show("Error", "More than one sheet has number: " + curFrontSheetInfo.Item1 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curFrontSheetInfo.Item1);
                    }

                    if (allSheetsNames.Contains(curFrontSheetInfo.Item2))
                    {
                        TaskDialog.Show("Error", "More than one sheet has name: " + curFrontSheetInfo.Item2 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curFrontSheetInfo.Item2);
                    }
                }

                for (int m = 0; m < requestData.mainSheetsInfo.Count; m++) //checks for duplicates
                {
                    Tuple<string, string, string> curMainSheetInfo = requestData.mainSheetsInfo[m];

                    if (allSheetsNames.Contains(curMainSheetInfo.Item1))
                    {
                        TaskDialog.Show("Error", "More than one sheet has number: " + curMainSheetInfo.Item1 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curMainSheetInfo.Item1);
                    }

                    if (allSheetsNames.Contains(curMainSheetInfo.Item2))
                    {
                        TaskDialog.Show("Error", "More than one sheet has name: " + curMainSheetInfo.Item2 + " please rename");
                        return null;
                    }
                    else
                    {
                        allSheetsNames.Add(curMainSheetInfo.Item2);
                    }
                }
            }

            return requestData;
        }

        private void SaveSettings(string filePath, string usagename)
        {
            try
            {
                DataSet myDataSet = new DataSet("DataSet");

                #region
                DataTable parametersTable = new DataTable("ParametersTable");
                DataColumn titleBlockIndexColumn = new DataColumn("TitleBlockIndex", System.Type.GetType("System.Int32"));
                DataColumn levelsDistanceColumn = new DataColumn("LevelsDistance", System.Type.GetType("System.Double"));
                DataColumn levelsAmountColumn = new DataColumn("LevelsAmount", System.Type.GetType("System.Int32"));
                DataColumn frontSheetsAmountColumn = new DataColumn("FrontSheetsAmount", System.Type.GetType("System.Int32"));
                DataColumn mainSheetsAmountColumn = new DataColumn("MainSheetsAmount", System.Type.GetType("System.Int32"));
                DataColumn mainSheetsScalesColumn = new DataColumn("MainSheetsScales", System.Type.GetType("System.Int32"));

                parametersTable.Columns.Add(titleBlockIndexColumn);
                parametersTable.Columns.Add(levelsDistanceColumn);
                parametersTable.Columns.Add(levelsAmountColumn);
                parametersTable.Columns.Add(frontSheetsAmountColumn);
                parametersTable.Columns.Add(mainSheetsAmountColumn);
                parametersTable.Columns.Add(mainSheetsScalesColumn);
                myDataSet.Tables.Add(parametersTable);

                DataRow paramsDataRow = parametersTable.NewRow();
                paramsDataRow[0] = this.comboBoxSelectTitleBlock.SelectedIndex;
                paramsDataRow[1] = Convert.ToDouble(this.textBoxDistBetweenLavels.Text);
                paramsDataRow[2] = decimal.ToInt32(this.numericUpDownAmountOfLevels.Value);
                paramsDataRow[3] = decimal.ToInt32(this.numericUpDownFrontSheetAmount.Value);
                paramsDataRow[4] = decimal.ToInt32(this.numericUpDownMainSheetAmount.Value);
                paramsDataRow[5] = this.comboBoxChooseScale.SelectedIndex;
                parametersTable.Rows.Add(paramsDataRow);
                #endregion

                #region
                DataTable frontSheetDataTable = new DataTable("FrontSheetDataTable");
                DataColumn frontSheetColumnNumber = new DataColumn("FrontSheetNumber", System.Type.GetType("System.String"));
                DataColumn frontSheetColumnName = new DataColumn("FrontSheetName", System.Type.GetType("System.String"));
                DataColumn frontSheetColumnDraftingView = new DataColumn("FrontSheetDraftingView", System.Type.GetType("System.String"));
                frontSheetDataTable.Columns.Add(frontSheetColumnNumber);
                frontSheetDataTable.Columns.Add(frontSheetColumnName);
                frontSheetDataTable.Columns.Add(frontSheetColumnDraftingView);
                myDataSet.Tables.Add(frontSheetDataTable);

                for (int r = 0; r < this.dataGridViewFrontSheet.Rows.Count; r++)
                {
                    DataRow frontSheetDataRow = frontSheetDataTable.NewRow();
                    DataGridViewRow curViewRow = this.dataGridViewFrontSheet.Rows[r];

                    for (int c = 0; c < 3; c++)
                    {
                        DataGridViewCell curCell = curViewRow.Cells[c] as DataGridViewCell;
                        if (curCell.Value != null)
                        {
                            frontSheetDataRow[c] = curCell.Value.ToString();
                        }
                        else
                        {
                            frontSheetDataRow[c] = "";
                        }
                    }

                    frontSheetDataTable.Rows.Add(frontSheetDataRow);
                }
                #endregion

                #region
                DataTable mainSheetDataTable = new DataTable("MainSheetDataTable");
                DataColumn mainSheetColumnNumber = new DataColumn("MainSheetNumber", System.Type.GetType("System.String"));
                DataColumn mainSheetColumnName = new DataColumn("MainSheetName", System.Type.GetType("System.String"));
                DataColumn mainSheetColumnTemplate = new DataColumn("MainSheetTemplate", System.Type.GetType("System.String"));
                mainSheetDataTable.Columns.Add(mainSheetColumnNumber);
                mainSheetDataTable.Columns.Add(mainSheetColumnName);
                mainSheetDataTable.Columns.Add(mainSheetColumnTemplate);
                myDataSet.Tables.Add(mainSheetDataTable);

                for (int r = 0; r < this.dataGridViewMainSheet.Rows.Count; r++)
                {
                    DataRow mainSheetDataRow = mainSheetDataTable.NewRow();
                    DataGridViewRow curViewRow = this.dataGridViewMainSheet.Rows[r];

                    for (int c = 0; c < 3; c++)
                    {
                        DataGridViewCell curCell = curViewRow.Cells[c] as DataGridViewCell;
                        if (curCell.Value != null)
                        {
                            mainSheetDataRow[c] = curCell.Value.ToString();
                        }
                        else
                        {
                            mainSheetDataRow[c] = "";
                        }
                    }

                    mainSheetDataTable.Rows.Add(mainSheetDataRow);
                }
                #endregion

                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));
                TextWriter writer = new StreamWriter(filePath);
                xmlSer.Serialize(writer, myDataSet);
                writer.Close();

                TaskDialog.Show("Info", "Configuration was " + usagename);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }
        }

        private void LoadSettings(string filePath, string usagename)
        {
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));

                if (File.Exists(filePath))
                {
                    TextReader reader = new StreamReader(filePath);
                    DataSet myDataSet = (DataSet)xmlSer.Deserialize(reader);
                    reader.Close();

                    if ((!myDataSet.Tables.Contains("ParametersTable")) || (!myDataSet.Tables.Contains("FrontSheetDataTable")) || (!myDataSet.Tables.Contains("MainSheetDataTable")))
                    {
                        return;
                    }

                    DataTable parametersTable = myDataSet.Tables["ParametersTable"];
                    if (parametersTable.Rows.Count > 0)
                    {
                        if (parametersTable.Columns.Count > 4)
                        {
                            List<string> drviewsNames = GetDraftAndPlanViewsNames();
                            List<string> templatesNames = GetViewTemplates();

                            DataRow paramsDataRow = parametersTable.Rows[0];

                            this.titleBlockIndex = paramsDataRow.Field<Int32>(0);
                            this.levelsDistance = paramsDataRow.Field<Double>(1);
                            this.levelsAmount = paramsDataRow.Field<Int32>(2);
                            this.frontSheetsAmount = paramsDataRow.Field<Int32>(3);
                            this.mainSheetsAmount = paramsDataRow.Field<Int32>(4);
                            this.mainSheetsScalesIndex = paramsDataRow.Field<Int32>(5);

                            this.comboBoxSelectTitleBlock.SelectedIndex = this.titleBlockIndex;
                            this.textBoxDistBetweenLavels.Text = this.levelsDistance.ToString();
                            this.numericUpDownAmountOfLevels.Value = this.levelsAmount;
                            this.numericUpDownFrontSheetAmount.Value = this.frontSheetsAmount;
                            this.numericUpDownMainSheetAmount.Value = this.mainSheetsAmount;
                            this.comboBoxChooseScale.SelectedIndex = this.mainSheetsScalesIndex;

                            DataTable frontSheetDataTable = myDataSet.Tables["FrontSheetDataTable"];
                            if (frontSheetDataTable.Rows.Count == this.frontSheetsAmount)
                            {
                                this.dataGridViewFrontSheet.Rows.Clear();
                                for (int r = 0; r < frontSheetDataTable.Rows.Count; r++)
                                {
                                    DataGridViewRow norRow = new DataGridViewRow();
                                    dataGridViewFrontSheet.Rows.Add(norRow);
                                    int lastRowIndex = dataGridViewFrontSheet.Rows.Count - 1;

                                    DataRow frontSheetTableRow = frontSheetDataTable.Rows[r];

                                    for (int c = 0; c < 2; c++)
                                    {
                                        dataGridViewFrontSheet.Rows[lastRowIndex].Cells[c].Value = frontSheetTableRow.Field<string>(c);
                                    }

                                    DataGridViewComboBoxCell drviewCell = dataGridViewFrontSheet.Rows[lastRowIndex].Cells[2] as DataGridViewComboBoxCell;
                                    drviewCell.Items.Clear();
                                    for (int d = 0; d < drviewsNames.Count; d++)
                                    {
                                        drviewCell.Items.Add(drviewsNames[d]);
                                    }

                                    string curDrawName = frontSheetTableRow.Field<string>(2);
                                    if (!templatesNames.Contains(curDrawName))
                                    {
                                        drviewCell.Items.Add(curDrawName);
                                    }
                                    dataGridViewFrontSheet.Rows[lastRowIndex].Cells[2].Value = curDrawName;
                                }
                            }

                            DataTable mainSheetDataTable = myDataSet.Tables["MainSheetDataTable"];
                            if (mainSheetDataTable.Rows.Count == (this.levelsAmount * this.mainSheetsAmount))
                            {
                                this.dataGridViewMainSheet.Rows.Clear();
                                for (int r = 0; r < mainSheetDataTable.Rows.Count; r++)
                                {
                                    DataGridViewRow norRow = new DataGridViewRow();
                                    dataGridViewMainSheet.Rows.Add(norRow);
                                    int lastRowIndex = dataGridViewMainSheet.Rows.Count - 1;

                                    DataRow mainSheetTableRow = mainSheetDataTable.Rows[r];

                                    for (int c = 0; c < 2; c++)
                                    {
                                        dataGridViewMainSheet.Rows[lastRowIndex].Cells[c].Value = mainSheetTableRow.Field<string>(c);
                                    }

                                    DataGridViewComboBoxCell templateCell = dataGridViewMainSheet.Rows[lastRowIndex].Cells[2] as DataGridViewComboBoxCell;
                                    templateCell.Items.Clear();
                                    if (templatesNames.Count > 0)
                                    {
                                        for (int t = 0; t < templatesNames.Count; t++)
                                        {
                                            templateCell.Items.Add(templatesNames[t]);
                                        }
                                    }

                                    string curTemplateName = mainSheetTableRow.Field<string>(2);
                                    if (!templatesNames.Contains(curTemplateName))
                                    {
                                        templateCell.Items.Add(curTemplateName);
                                    }
                                    dataGridViewMainSheet.Rows[lastRowIndex].Cells[2].Value = curTemplateName;
                                }
                            }

                            TaskDialog.Show("Info", "Configuration was " + usagename);
                        }
                    }

                    validateAllCells();
                }
                else
                {
                    TaskDialog.Show("Exception", "Configuration file doesn't exist");
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
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

        private List<string> GetViewTemplates()
        {
            List<string> names = new List<string>();

            if (doc != null)
            {
                FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(Autodesk.Revit.DB.View));

                foreach (Autodesk.Revit.DB.View view in collector.ToElements())
                {
                    if (view.IsTemplate)
                        names.Add(view.Name);
                }
            }

            return names;
        }

        private List<string> GetDraftAndPlanViewsNames()
        {
            List<string> names = new List<string>();

            if (doc != null)
            {
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
            }

            names.Sort();

            return names;
        }

        private void SelectReferenceFile_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openRvtFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFileName = openRvtFileDialog.FileName;

                if (File.Exists(selectedFileName))
                {
                    try
                    {
                        File.Copy(selectedFileName, templateFilePath, true);

                        File.Delete(this.settingsFilePath);

                        InitUI();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void comboBoxSelectTitleBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.titleBlockIndex = this.comboBoxSelectTitleBlock.SelectedIndex;
        }

        private void dataGridViewMainSheet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.dataGridViewMainSheet.CurrentCell.IsInEditMode)
            {
                if (this.dataGridViewMainSheet.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.dataGridViewMainSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];

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
            validateCell(e.ColumnIndex, e.RowIndex);
        }

        private void validateAllCells()
        {
            for (int r = 0; r < this.dataGridViewMainSheet.RowCount; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    validateCell(c, r);
                }
            }
        }

        private void validateCell(int columnIndex, int rowIndex)
        {
            if (columnIndex > 0)
            {
                if ((this.mainSheetsAmount > 0) && (this.levelsAmount > 0))
                {
                    int selectedLevelNumber = (int)Math.Floor((double)rowIndex / this.mainSheetsAmount);
                    int selectedSheetNumber = rowIndex - (selectedLevelNumber * this.mainSheetsAmount);

                    //MessageBox.Show("level=" + selectedLevelNumber.ToString() + " sheet=" + selectedSheetNumber.ToString());

                    try
                    {
                        if ((selectedLevelNumber >= 0) && (selectedSheetNumber >= 0))
                        {
                            if ((selectedLevelNumber < this.mainSheetsModel.getLevelsNumber()) && (selectedSheetNumber < this.mainSheetsModel.getSheetsNumber()))
                            {
                                if (columnIndex == 1)
                                {
                                    DataGridViewTextBoxCell nameCell = dataGridViewMainSheet.Rows[rowIndex].Cells[1] as DataGridViewTextBoxCell;
                                    String curItem1 = nameCell.Value.ToString();

                                    if (selectedLevelNumber == 0)
                                    {
                                        String oldItem1 = this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber].Item1;
                                        int oldItem2 = this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber].Item2;

                                        this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber] = new Tuple<string, int>(curItem1, oldItem2);
                                    }
                                    else
                                    {
                                        int otherLevelNumber = selectedLevelNumber - 1;

                                        String oldItem1 = this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber].Item1;
                                        int oldItem2 = this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber].Item2;

                                        this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber] = new Tuple<string, int>(curItem1, oldItem2);
                                    }
                                }
                                else if (columnIndex == 2)
                                {
                                    DataGridViewComboBoxCell templateCell = dataGridViewMainSheet.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell;
                                    int curItem2 = 0;

                                    object curItem2Object = templateCell.Value;
                                    int indexOfItem2 = templateCell.Items.IndexOf(curItem2Object);
                                    if (indexOfItem2 >= 0)
                                    {
                                        curItem2 = indexOfItem2;
                                    }

                                    if (selectedLevelNumber == 0)
                                    {
                                        String oldItem1 = this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber].Item1;
                                        int oldItem2 = this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber].Item2;

                                        this.mainSheetsModel.firstLevel.sheets[selectedSheetNumber] = new Tuple<string, int>(oldItem1, curItem2);

                                    }
                                    else
                                    {
                                        int otherLevelNumber = selectedLevelNumber - 1;

                                        String oldItem1 = this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber].Item1;
                                        int oldItem2 = this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber].Item2;

                                        this.mainSheetsModel.otherLevels[otherLevelNumber].sheets[selectedSheetNumber] = new Tuple<string, int>(oldItem1, curItem2);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Exception: " + ex.ToString());
                    }
                }
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
                    this.dataGridViewMainSheet.Rows.RemoveAt(this.dataGridViewMainSheet.Rows.Add());
                }
            }
        }

        private void dataGridViewMainSheet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridViewFrontSheet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.dataGridViewFrontSheet.CurrentCell.IsInEditMode)
            {
                if (this.dataGridViewFrontSheet.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.dataGridViewFrontSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if ((cell.ColumnIndex == 2) && (!cell.Items.Contains(e.FormattedValue)))
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

        private void dataGridViewFrontSheet_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (newCellValue != "")
            {
                if (this.dataGridViewFrontSheet.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewCell cell = this.dataGridViewFrontSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = newCellValue.Clone();
                }

                newCellValue = "";
            }

        }

        private void dataGridViewFrontSheet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control.GetType() ==
                typeof(DataGridViewComboBoxEditingControl))
                {
                    ((System.Windows.Forms.ComboBox)e.Control).DropDownStyle =
                    ComboBoxStyle.DropDown;
                    ((System.Windows.Forms.ComboBox)e.Control).PreviewKeyDown -= new
                    PreviewKeyDownEventHandler(dataGridViewFrontSheet_PreviewKeyDown);
                    ((System.Windows.Forms.ComboBox)e.Control).PreviewKeyDown += new
                    PreviewKeyDownEventHandler(dataGridViewFrontSheet_PreviewKeyDown);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewFrontSheet_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (newCellValue != "")
                {
                    this.dataGridViewFrontSheet.Rows.RemoveAt(this.dataGridViewFrontSheet.Rows.Add());
                }
            }
        }

        private void dataGridViewFrontSheet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void buttonExportConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = this.saveXmlFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveSettings(this.saveXmlFileDialog.FileName, "exported");
            }
        }

        private void buttonImportConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openXmlFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (this.openXmlFileDialog.FileName.EndsWith(".xml", true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    LoadSettings(this.openXmlFileDialog.FileName, "imported");
                }
            }
        }

        private void numericUpDownStartingNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateMainSheets();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectExcelFileButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openExcelFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.excelFilePathComboBox.Items.Add(this.openExcelFileDialog.FileName);
                this.excelFilePathComboBox.SelectedIndex = this.excelFilePathComboBox.Items.Count - 1;
            }
        }

        private Tuple<string, string> GetRangeFromPrintArea(string printArea)
        {
            string first = string.Join("", printArea.TakeWhile(x => x != ':')).Replace("$", "");
            string second = printArea.Substring(printArea.IndexOf(":") + 1).Replace("$", "");

            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
                first = second = null;

            return new Tuple<string, string>(first, second);
        }

        private List<string> ParseExcelFile(string path)
        {
            List<string> sheetNamePrefixes = new List<string>();

            MSExcel.Application xlApp = new MSExcel.Application();
            MSExcel.Workbooks xlWorkbooks = xlApp.Workbooks;

            MSExcel.Workbook xlWorkbook = xlWorkbooks.Open(path);
            MSExcel._Worksheet xlWorksheet = (MSExcel._Worksheet)xlWorkbook.Sheets[1];
            MSExcel.Range xlRange = null;
            MSExcel.PageSetup pageSetup = xlWorksheet.PageSetup;

            if (!string.IsNullOrEmpty(pageSetup.PrintArea))
            {
                var rangeString = GetRangeFromPrintArea(pageSetup.PrintArea);

                if (string.IsNullOrEmpty(rangeString.Item1) || string.IsNullOrEmpty(rangeString.Item2))
                    xlRange = xlWorksheet.UsedRange;
                else
                    xlRange = xlWorksheet.get_Range(rangeString.Item1, rangeString.Item2) as MSExcel.Range;
            }
            else
                xlRange = xlWorksheet.UsedRange;

            Marshal.ReleaseComObject(pageSetup);
            pageSetup = null;

            for (int i = 1; i <= xlRange.Rows.Count; i++)
            {
                var cell = xlRange.Cells[i, 1] as MSExcel.Range; //first column of every row only

                sheetNamePrefixes.Add(Convert.ToString(cell.Value2));

                Marshal.ReleaseComObject(cell); //always important
                cell = null;
            }

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlRange = null;
            xlWorksheet = null;

            xlWorkbook.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(xlWorkbook);
            xlWorkbook = null;

            xlWorkbooks.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkbooks);
            Marshal.ReleaseComObject(xlApp);

            xlWorkbooks = null;
            xlApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return sheetNamePrefixes;
        }

        private void excelFilePathComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainSheets();
        }

        private void textBoxBetween_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewMainSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
