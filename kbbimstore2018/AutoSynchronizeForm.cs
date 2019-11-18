using System;
using System.Windows.Forms;

namespace KbBimstore
{
    public partial class AutoSynchronizeForm : System.Windows.Forms.Form
    {
        public AutoSynchronizeForm()
        {
            InitializeComponent();
        }

        public int getAutoSaveInerval()
        {
            int interval = 0;

            interval += Decimal.ToInt32(this.numericUpDownAutosaveHr.Value * 60);
            interval += Decimal.ToInt32(this.numericUpDownAutosaveMin.Value);

            return interval;
        }

        public int getAutoSyncInerval()
        {
            int interval = 0;

            interval += Decimal.ToInt32(this.numericUpDownAutosyncHr.Value * 60);
            interval += Decimal.ToInt32(this.numericUpDownAutosyncMin.Value);

            return interval;
        }

        public bool AutoSyncOn()
        {
            return this.autoSyncOnRadioButton.Checked;
        }

        public bool AutoSaveOn()
        {
            return this.autoSaveOnRadioButton.Checked;
        }

        private void buttonAutosyncStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;          
        }
    }
}
