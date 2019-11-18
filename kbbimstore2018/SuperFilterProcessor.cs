using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    class SuperFilterProcessor
    {
        private Autodesk.Revit.DB.Document doc = null;
        private Autodesk.Revit.UI.UIDocument uidoc = null;

        public SuperFilterProcessor(Autodesk.Revit.DB.Document doc)
        {
            this.doc = doc;
            this.uidoc = new UIDocument(doc);
        }


        public void init()
        {
            if (this.uidoc != null)
            {
                filterSelection();
            }
        }


        private void filterSelection()
        {

        }

    }
}
