using System;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.Revit.UI;

namespace KbBimstore.ToolbarManager.Forms
{
    public partial class ToolbarManagerForm : Form
    {

        public Dictionary<string, List<string>> tabItems = new Dictionary<string, List<string>>();

        public ToolbarManagerForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            FillTabItems();

            DataGridViewComboBoxColumn toolbarColumn = this.dataGridView1.Columns[1] as DataGridViewComboBoxColumn;
            toolbarColumn.DataSource = tabItems.Keys.ToList();

            foreach (string toolbarName in tabItems.Keys)
            {
                foreach (string buttonName in tabItems[toolbarName])
                {
                    int rowIndex = this.dataGridView1.Rows.Add();

                    var row = this.dataGridView1.Rows[rowIndex];
                    row.Cells[0].Value = buttonName;

                    int indexOfToolbar = GetIndexOfToolbar(toolbarName);
                    (row.Cells[1] as DataGridViewComboBoxCell).Value = (row.Cells[1] as DataGridViewComboBoxCell).Items[indexOfToolbar];
                }
            }

            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private int GetIndexOfToolbar(string toolbarName)
        {
            for (int i = 0; i < tabItems.Keys.ToList().Count; i++)
            {
                string currentToolbar = tabItems.Keys.ToList()[i];

                if (currentToolbar == toolbarName)
                {
                    return i;
                }
            }

            return 0;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

            string currentRowButtonName = (string) row.Cells[0].Value;
            string currentRowToolbarName = (string) row.Cells[1].Value;

            if (e.ColumnIndex == 1)
            {
                HandleAssociatedToolbarChanged(currentRowButtonName, currentRowToolbarName);
            }
        }

        private void HandleAssociatedToolbarChanged(string currentRowButtonName, string currentRowToolbarName)
        {
            foreach (string tn in tabItems.Keys)
            {
                if (tabItems[tn].Contains(currentRowButtonName))
                {
                    tabItems[tn].Remove(currentRowButtonName);
                    break;
                }
            }

            tabItems[currentRowToolbarName].Add(currentRowButtonName);
        }

        private void FillTabItems()
        {
            List<RibbonPanel> ribbonPanels = KbBimstoreApp.activeUiContApp.GetRibbonPanels(KbBimstoreApp.TAB_NAME);

            foreach (RibbonPanel rp in ribbonPanels)
            {
                string toolbarName = rp.Name.Trim();

                tabItems[toolbarName] = new List<string>();

                foreach (var button in rp.GetItems())
                {
                    if (button.ItemText != null)
                        tabItems[toolbarName].Add(button.ItemText.Trim());
                }
            }
        }

        private void createToolbarButton_Click(object sender, EventArgs e)
        {
            using (NewToolbarForm ntf = new NewToolbarForm())
            {
                DialogResult dr = ntf.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    tabItems[ntf.newToolbarName] = new List<string>();
                    DataGridViewComboBoxColumn toolbarColumn = this.dataGridView1.Columns[1] as DataGridViewComboBoxColumn;
                    toolbarColumn.DataSource = tabItems.Keys.ToList();
                }
            }
        }

        private void applySettingsButton_Click(object sender, EventArgs e)
        {
            XElement xElement = new XElement("root",
                tabItems.Select(kv => new XElement(kv.Key.Replace(' ', '-'), string.Join(",", kv.Value))));

            System.IO.FileInfo fi = new System.IO.FileInfo(KbBimstoreApp.ToolbarManagerFilePath);

            if (!fi.Directory.Exists)
                System.IO.Directory.CreateDirectory(fi.Directory.FullName);

            xElement.Save(fi.FullName);
            FillTabItems();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
