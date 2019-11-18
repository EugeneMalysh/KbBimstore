using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.UI;

namespace KbBimstore
{
    public partial class CadDetailConverterOutputForm : Form
    {
        private CadDetailConverterHandler handler;

        public CadDetailConverterOutputForm(CadDetailConverterHandler handler)
        {
            this.handler = handler;

            InitializeComponent();

            InitUI();
        }

        private void InitUI()
        {
            this.comboBoxTextStyle.Items.Clear();
            this.dataGridViewLineStyles.Rows.Clear();

            string[] revitTextStylesNames = handler.getRevitTextStylesNames().ToArray();
            string[] revitLineStylesNames = handler.getRevitLineStylesNames().ToArray();

            List<Tuple<byte, byte, byte>> lineStylesColors = handler.getAutocadColorsTuples();

            if (revitTextStylesNames != null)
            {
                this.comboBoxTextStyle.Items.AddRange(revitTextStylesNames);
                this.comboBoxTextStyle.SelectedIndex = 0;
            }
           
            for (int i = 0; i < lineStylesColors.Count; i++)
            {
                this.dataGridViewLineStyles.Rows.Add(new DataGridViewRow());
                int lastRowIndex = this.dataGridViewLineStyles.Rows.Count - 1;

                DataGridViewTextBoxCell colorCell = this.dataGridViewLineStyles.Rows[lastRowIndex].Cells[0] as DataGridViewTextBoxCell;
                if (colorCell != null)
                {
                    colorCell.Value = lineStylesColors[i].Item1.ToString() + " " + lineStylesColors[i].Item2.ToString() + " " + lineStylesColors[i].Item3.ToString();
                    colorCell.Style.BackColor = Color.FromArgb(255, ((int)lineStylesColors[i].Item1), ((int)lineStylesColors[i].Item2), ((int)lineStylesColors[i].Item3));
                    colorCell.ReadOnly = true;
                }

                if (revitLineStylesNames.Length > 0)
                {
                    DataGridViewComboBoxCell stylesNamesCell = this.dataGridViewLineStyles.Rows[lastRowIndex].Cells[1] as DataGridViewComboBoxCell;
                    if (stylesNamesCell != null)
                    {
                        stylesNamesCell.Items.Clear();
                        stylesNamesCell.Items.AddRange(revitLineStylesNames);
                        stylesNamesCell.Value = revitLineStylesNames[0];
                    }
                }
            }
        }

        private void createRequestData()
        {
            this.handler.requestData.selectedTextStyleName = this.comboBoxTextStyle.SelectedItem.ToString();
            
            this.handler.requestData.textStylesNames.Clear();
            this.handler.requestData.textStylesNames.AddRange(handler.getRevitTextStylesNames());

            this.handler.requestData.lineStylesNames.Clear();
            this.handler.requestData.lineStylesNames.AddRange(handler.getRevitLineStylesNames());

            this.handler.requestData.colorsToLineStyles.Clear();
            foreach(DataGridViewRow curRow in this.dataGridViewLineStyles.Rows)
            {
                DataGridViewTextBoxCell colorCell = curRow.Cells[0] as DataGridViewTextBoxCell;
                DataGridViewComboBoxCell stylesNamesCell = curRow.Cells[1] as DataGridViewComboBoxCell;

                if ((colorCell != null) && (stylesNamesCell != null))
                {
                    Color curColor = colorCell.Style.BackColor;
                    
                    Tuple<byte, byte, byte> curColorTuple = new Tuple<byte, byte, byte>(curColor.R, curColor.G, curColor.B);
                    string curLineStyleName = stylesNamesCell.Value.ToString();

                    this.handler.requestData.colorsToLineStyles.Add(curColorTuple, curLineStyleName);
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            DialogResult result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = this.saveFileDialog.FileName;

                createRequestData();
                this.handler.requestData.SaveSettings(filePath);
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                createRequestData();

                this.Close();
                this.Dispose();
            }            
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = this.openFileDialog.FileName;
                this.handler.requestData.LoadSettings(filePath);

                List<string> textStylesNames = this.handler.getRevitTextStylesNames();
                if (this.handler.requestData.selectedTextStyleName != null)
                {
                    int selectionIndex = textStylesNames.IndexOf(this.handler.requestData.selectedTextStyleName);
                    if (selectionIndex >= 0)
                    {
                        this.comboBoxTextStyle.SelectedIndex = selectionIndex;
                    }
                }

                this.dataGridViewLineStyles.Rows.Clear();
                string[] revitLineStylesNames = this.handler.requestData.lineStylesNames.ToArray();
                foreach(KeyValuePair<Tuple<byte,byte,byte>,string> keyValuePair in this.handler.requestData.colorsToLineStyles)
                {
                    this.dataGridViewLineStyles.Rows.Add(new DataGridViewRow());
                    int lastRowIndex = this.dataGridViewLineStyles.Rows.Count - 1;

                    Tuple<byte, byte, byte> colorTuple = keyValuePair.Key;
                    DataGridViewTextBoxCell colorCell = this.dataGridViewLineStyles.Rows[lastRowIndex].Cells[0] as DataGridViewTextBoxCell;                    
                    if (colorCell != null)
                    {
                        colorCell.Value = colorTuple.Item1.ToString() + " " + colorTuple.Item2.ToString() + " " + colorTuple.Item3.ToString();
                        colorCell.Style.BackColor = Color.FromArgb(255, ((int)colorTuple.Item1), ((int)colorTuple.Item2), ((int)colorTuple.Item3));
                        colorCell.ReadOnly = true;
                    }

                    if (revitLineStylesNames.Length > 0)
                    {
                        DataGridViewComboBoxCell stylesNamesCell = this.dataGridViewLineStyles.Rows[lastRowIndex].Cells[1] as DataGridViewComboBoxCell;
                        if (stylesNamesCell != null)
                        {
                            stylesNamesCell.Items.Clear();
                            stylesNamesCell.Items.AddRange(revitLineStylesNames);
                            stylesNamesCell.Value = keyValuePair.Value;
                        }
                    }
                }
            }
        }

        private void comboBoxTextStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            handler.setSelectedtRevitTextStyleName(this.comboBoxTextStyle.SelectedItem.ToString());
        }

        private void CadDetailConverterOutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void CadDetailConverterOutputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                this.handler.requestData = null;
            }
        }

    }
}
