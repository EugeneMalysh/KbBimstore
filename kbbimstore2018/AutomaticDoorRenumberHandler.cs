using System;
using System.Text;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{

    public class AutomaticDoorRenumberHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        private Parameter selectedRoomNumberParameter;
        private Parameter selectedDoorNumberParameter;


        public AutomaticDoorRenumberHandler(Parameter selectedRoomNumberParameter, Parameter selectedDoorNumberParameter)
        {
            this.selectedRoomNumberParameter = selectedRoomNumberParameter;
            this.selectedDoorNumberParameter = selectedDoorNumberParameter;
            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "AutomaticDoorRenumberHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;

            ModifyScene(uiapp, "Automatic Door Renumber Handler");
        }

        private void ModifyScene(UIApplication uiapp, String text)
        {
            uidoc = uiapp.ActiveUIDocument;

            if (uidoc != null)
            {
                doc = uidoc.Document;

                using (Transaction trans = new Transaction(doc))
                {
                    trans.Start(text);
                    AutomaticDoorRenumber(1);

                    trans.Commit();
                    trans.Dispose();
                }
            }
        }

        private void AutomaticDoorRenumber(int id)
        {
            try
            {
                selectedDoorNumberParameter.Set(selectedRoomNumberParameter.AsString());
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
            TaskDialog.Show("Info", "Rooms were renumbered");
        }
    }
}
