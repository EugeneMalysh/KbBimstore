using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KbBimstore
{

    public partial class AutomaticDoorRenumberToFromForm : Form
    {

        public AutomaticDoorRenumberToFromForm()
        {
            InitializeComponent();
        }

        public int getSelectedDirection()
        {
            if(this.radioButtonToRoom.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
