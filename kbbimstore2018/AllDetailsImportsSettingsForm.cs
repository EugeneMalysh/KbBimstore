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
    public partial class AllDetailsImportsSettingsForm : System.Windows.Forms.Form
    {
        static string settingsFilePath = KbBimstoreApp.ImporSsettingsFilePath;

        private Document doc;
        private UIDocument uidoc;
        private UIApplication uiapp;


        public AllDetailsImportsSettingsForm(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;

            InitializeComponent();

            int loadResult = LoadSettings();

            this.CenterToScreen();
            this.ShowDialog();
        }

        private void InitUI()
        {

        }

        private int LoadSettings()
        {
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));

                if (File.Exists(settingsFilePath))
                {
                    TextReader reader = new StreamReader(settingsFilePath);
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
                        this.textBoxBaseAndTransition.Text = BaseAndTransitionDataRow[1].ToString();
                    }
                    else
                    {
                        return -7;
                    }

                    DataRow CeilingDataRow = settingsTable.Rows[1];
                    if (CeilingDataRow[0].ToString() == "Ceiling")
                    {
                        this.textBoxCeiling.Text = CeilingDataRow[1].ToString();
                    }
                    else
                    {
                        return -8;
                    }

                    DataRow DoorAndWindowDataRow = settingsTable.Rows[2];
                    if (DoorAndWindowDataRow[0].ToString() == "DoorAndWindow")
                    {
                        this.textBoxDoorAndWindow.Text = DoorAndWindowDataRow[1].ToString();
                    }
                    else
                    {
                        return -9;
                    }

                    DataRow MillworkDataRow = settingsTable.Rows[3];
                    if (MillworkDataRow[0].ToString() == "Millwork")
                    {
                        this.textBoxMillwork.Text = MillworkDataRow[1].ToString();
                    }
                    else
                    {
                        return -10;
                    }

                    DataRow PartitionDataRow = settingsTable.Rows[4];
                    if (PartitionDataRow[0].ToString() == "Partition")
                    {
                        this.textBoxPartition.Text = PartitionDataRow[1].ToString();
                    }
                    else
                    {
                        return -11;
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

        private int SaveSettings()
        {

            try
            {
                if (!File.Exists(this.textBoxBaseAndTransition.Text))
                {
                    TaskDialog.Show("Error", "Please Correct Base And Transition Details Import Path");
                    return -1;
                }
                else if (!File.Exists(this.textBoxCeiling.Text))
                {
                    TaskDialog.Show("Error", "Please Correct Ceiling Details Import Path");
                    return -1;
                }
                else if (!File.Exists(this.textBoxDoorAndWindow.Text))
                {
                    TaskDialog.Show("Error", "Please Correct Door And Window Details Import Path");
                }
                else if (!File.Exists(this.textBoxMillwork.Text))
                {
                    TaskDialog.Show("Error", "Please Correct Millwork Import Path");
                }
                else if (!File.Exists(this.textBoxPartition.Text))
                {
                    TaskDialog.Show("Error", "Please Correct Partition Import Path");
                }
                else
                {
                    DataSet myDataSet = new DataSet("DataSet");

                    DataTable settingsTable = new DataTable("Settings");
                    DataColumn settingsColumnName = new DataColumn("Name", System.Type.GetType("System.String"));
                    DataColumn settingsColumnValue = new DataColumn("Value", System.Type.GetType("System.String"));
                    settingsTable.Columns.Add(settingsColumnName);
                    settingsTable.Columns.Add(settingsColumnValue);
                    myDataSet.Tables.Add(settingsTable);

                    DataRow BaseAndTransitionDataRow = settingsTable.NewRow();
                    BaseAndTransitionDataRow[0] = "BaseAndTransitionData";
                    BaseAndTransitionDataRow[1] = this.textBoxBaseAndTransition.Text;
                    settingsTable.Rows.Add(BaseAndTransitionDataRow);

                    DataRow CeilingDataRow = settingsTable.NewRow();
                    CeilingDataRow[0] = "Ceiling";
                    CeilingDataRow[1] = this.textBoxCeiling.Text;
                    settingsTable.Rows.Add(CeilingDataRow);

                    DataRow DoorAndWindowDataRow = settingsTable.NewRow();
                    DoorAndWindowDataRow[0] = "DoorAndWindow";
                    DoorAndWindowDataRow[1] = this.textBoxDoorAndWindow.Text;
                    settingsTable.Rows.Add(DoorAndWindowDataRow);

                    DataRow MillworkDataRow = settingsTable.NewRow();
                    MillworkDataRow[0] = "Millwork";
                    MillworkDataRow[1] = this.textBoxMillwork.Text;
                    settingsTable.Rows.Add(MillworkDataRow);

                    DataRow PartitionDataRow = settingsTable.NewRow();
                    PartitionDataRow[0] = "Partition";
                    PartitionDataRow[1] = this.textBoxPartition.Text;
                    settingsTable.Rows.Add(PartitionDataRow);

                    XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));
                    TextWriter writer = new StreamWriter(settingsFilePath);
                    xmlSer.Serialize(writer, myDataSet);
                    writer.Close();

                    TaskDialog.Show("Info", "Details Import Configuration was saved");

                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }

            return -1;
        }

        private void buttonBaseAndTransition_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxBaseAndTransition.Text = this.openFileDialog.FileName;
            }
        }

        private void buttonCeiling_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxCeiling.Text = this.openFileDialog.FileName;
            }
        }

        private void buttonDoorAndWindow_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxDoorAndWindow.Text = this.openFileDialog.FileName;
            }
        }

        private void buttonMillwork_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxMillwork.Text = this.openFileDialog.FileName;
            }
        }

        private void buttonPartition_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBoxPartition.Text = this.openFileDialog.FileName;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (0 < SaveSettings())
            {
                this.Close();
                this.Dispose();
            }
        }

    }

}
