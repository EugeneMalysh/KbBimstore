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
    public partial class AlmMessageBox : Form
    {
        public AlmMessageBox()
        {
            InitializeComponent();
        }

        public AlmMessageBox(string text)
        {
            InitializeComponent();

            this.textBox.Text = text;
        }
    }
}
