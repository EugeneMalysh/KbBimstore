using System;
using System.Windows.Forms;

namespace KbBimstore
{
    public partial class TabToolbarRenamerForm : Form
    {
        public TabToolbarRenamerForm()
        {
            InitializeComponent();
            AddRowsToForm();
            HandleLockedSettings();

            this.dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (e.RowIndex != 0)
            {
                if (e.ColumnIndex == 0)
                {
                    string cellValue = Convert.ToString(cell.Value).Trim();

                    if (!String.IsNullOrEmpty(cellValue) && !String.IsNullOrWhiteSpace(cellValue))
                    {
                        KbBimstoreApp.MainTab.ToolBars[e.RowIndex - 1].Name = cellValue;
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    bool enabled = Convert.ToString(cell.Value) == "Enabled" ? true : false;
                    KbBimstoreApp.MainTab.ToolBars[e.RowIndex - 1].Enabled = enabled;
                }
            }
            else if (e.ColumnIndex == 2)
            {
                bool enabled = Convert.ToString(cell.Value) == "Enabled" ? true : false;
                KbBimstoreApp.MainTab.Enabled = enabled;
            }
        }

        private void HandleLockedSettings()
        {
            if (KbBimstoreApp.MainTab.Locked)
            {
                this.dataGridView1.Enabled = false;
                this.lockSettingsButton.Enabled = false;

                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    this.dataGridView1.Rows[i].Selected = false;
                }
            }
        }

        private void AddRowsToForm()
        {
            for (int i = 0; i < KbBimstoreTab.defaultPanels.Length + 1; i++)
            {
                this.dataGridView1.Rows.Add();
            }

            DataGridViewRow tabRow = this.dataGridView1.Rows[0];
            tabRow.Cells[0].Value = KbBimstoreApp.MainTab.Name;
            tabRow.Cells[1].Value = "Tab";
            ((DataGridViewComboBoxCell)tabRow.Cells[2]).Value = KbBimstoreApp.MainTab.Enabled ? "Enabled" : "Disabled";

            for (int i = 0; i < KbBimstoreApp.MainTab.ToolBars.Count; i++)
            {
                var toolBar = KbBimstoreApp.MainTab.ToolBars[i];

                DataGridViewRow row = this.dataGridView1.Rows[i + 1];
                row.Cells[0].Value = toolBar.Name;
                row.Cells[1].Value = "Toolbar";
                ((DataGridViewComboBoxCell)row.Cells[2]).Value = toolBar.Enabled ? "Enabled" : "Disabled";
            }
        }

        private void exportSettingsButton_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult dr = this.folderBrowserDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string exportPath = System.IO.Path.Combine(this.folderBrowserDialog1.SelectedPath, "TabSettings.xml");

                KbBimstoreApp.MainTab.ExportToXML(exportPath);

                MessageBox.Show(string.Format("Successfully exported file to: {0}", exportPath), KbBimstoreApp.MainTab.Name);
            }
        }

        private void importSettingsButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "XML Files | *.xml";
            this.openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult dr = this.openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string importPath = this.openFileDialog1.FileName;

                KbBimstoreTab tab = KbBimstoreTab.CreateBimStoreFromSettings(importPath);

                if (tab != null)
                    KbBimstoreApp.MainTab = tab;
                else
                    MessageBox.Show("Error importing settings file. Please try another file", KbBimstoreApp.MainTab.Name);
            }
        }

        private void lockSettingsButton_Click(object sender, EventArgs e)
        {
            if (!KbBimstoreApp.MainTab.Locked)
            {
                using (TabToolBarRenamerLockForm f = new TabToolBarRenamerLockForm())
                {
                    DialogResult dr = f.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        this.dataGridView1.Enabled = false;
                        this.lockSettingsButton.Enabled = false;
                        this.unlockSettingsButton.Enabled = true;

                        KbBimstoreApp.MainTab.Save();
                    }
                }
            }
        }

        private void unlockSettingsButton_Click(object sender, EventArgs e)
        {
            using (TabToolBarRenamerUnlockForm f = new TabToolBarRenamerUnlockForm())
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    this.dataGridView1.Enabled = true;
                    this.lockSettingsButton.Enabled = true;
                    this.unlockSettingsButton.Enabled = false;

                    KbBimstoreApp.MainTab.Save();
                }
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                int rowIndex = row.Index;

                //Can't move tab up or down and can't move toolbars higher than the index of 1 (right below the tab)
                if (rowIndex > 1)
                {
                    this.dataGridView1.Rows.RemoveAt(rowIndex);
                    this.dataGridView1.Rows.Insert(rowIndex - 1, row);

                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        this.dataGridView1.Rows[i].Selected = false;
                    }

                    this.dataGridView1.Rows[rowIndex - 1].Selected = true;

                    //possition in list will be 1 less than index due to the first row in the data grid view being the tab
                    KbBimstoreToolbar toolBar = KbBimstoreApp.MainTab.ToolBars[rowIndex - 1];
                    KbBimstoreApp.MainTab.ToolBars.RemoveAt(rowIndex - 1);
                    KbBimstoreApp.MainTab.ToolBars.Insert(rowIndex - 2, toolBar);
                }
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                int rowIndex = row.Index;

                //Can't move tab down and can't go lower than the last position
                if (rowIndex != this.dataGridView1.Rows.Count - 1 && rowIndex != 0)
                {
                    this.dataGridView1.Rows.RemoveAt(rowIndex);
                    this.dataGridView1.Rows.Insert(rowIndex + 1, row);

                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        this.dataGridView1.Rows[i].Selected = false;
                    }

                    this.dataGridView1.Rows[rowIndex + 1].Selected = true;

                    //possition in list will be 1 less than index due to the first row in the data grid view being the tab
                    KbBimstoreToolbar toolBar = KbBimstoreApp.MainTab.ToolBars[rowIndex - 1];
                    KbBimstoreApp.MainTab.ToolBars.RemoveAt(rowIndex - 1);
                    KbBimstoreApp.MainTab.ToolBars.Insert(rowIndex, toolBar);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addTabToolbarButton_Click(object sender, EventArgs e)
        {

        }
    }
}
