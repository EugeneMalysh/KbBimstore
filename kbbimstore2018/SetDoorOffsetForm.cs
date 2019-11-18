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
    public partial class SetDoorOffsetForm : Form
    {
        private double offsetValue = 0;

        public SetDoorOffsetForm()
        {
            InitializeComponent();
        }

        public double getOffset()
        {            
            try
            {
                double ofsVal = Convert.ToDouble(this.textBoxOffset.Text);
                this.offsetValue = ofsVal;
            }
            catch (Exception ex)
            {
            }

            return this.offsetValue;
        }

        private void textBoxOffset_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                double ofsVal = Math.Abs(Convert.ToDouble(this.textBoxOffset.Text));
                this.offsetValue = ofsVal;
            }
            catch (Exception ex)
            {
                this.textBoxOffset.Text = this.offsetValue.ToString();
                e.Cancel = true;
            }
        }

        private void textBoxOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double ofsVal = Math.Abs(Convert.ToDouble(this.textBoxOffset.Text));
                    this.offsetValue = ofsVal;
                }
                catch (Exception ex)
                {
                    this.textBoxOffset.Text = this.offsetValue.ToString();
                }
            }
        }

        private void buttonOffset_Click(object sender, EventArgs e)
        {
            try
            {
                double ofsVal = Math.Abs(Convert.ToDouble(this.textBoxOffset.Text));
                this.offsetValue = ofsVal;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Info", "Offset must have a numeric value, please correct.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
