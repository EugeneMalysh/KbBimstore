using System;
using System.Text;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{

    public class AutomaticDoorRenumberToFromHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        private int selectedDirection = 1;
        private List<FamilyInstance> selectedDoors = new List<FamilyInstance>();


        public AutomaticDoorRenumberToFromHandler(List<FamilyInstance> selectedDoors, int selectedDirection)
        {
            this.selectedDirection = selectedDirection;
            this.selectedDoors = selectedDoors;
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
            this.doc = this.uiapp.ActiveUIDocument.Document;

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
                foreach (FamilyInstance curDoor in this.selectedDoors)
                {
                    Parameter doorNumbParam = getNumberParameter(curDoor);

                    Room toRoom = curDoor.ToRoom;
                    Room fromRoom = curDoor.FromRoom;

                    string toRoomNumber = "";
                    if (toRoom != null)
                    {
                        Parameter toRoomNumbParam = getNumberParameter(toRoom);
                        if (toRoomNumbParam != null)
                        {
                            toRoomNumber = toRoomNumbParam.AsString();
                        }
                    }

                    string fromRoomNumber = "";
                    if (fromRoom != null)
                    {
                        Parameter fromRoomParam = getNumberParameter(fromRoom);
                        if (fromRoomParam != null)
                        {
                            fromRoomNumber = fromRoomParam.AsString();
                        }
                    }

                    if (this.selectedDirection == 1)
                    {
                        doorNumbParam.Set(toRoomNumber);
                    }
                    else
                    {
                        doorNumbParam.Set(fromRoomNumber);
                    }
                }
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

        private Parameter getNumberParameter(Element elem)
        {
            Parameter param = null;

            if (elem != null)
            {
                string paramName = "";

                if (elem is Room)
                {
                    paramName = "Number";
                }
                else if (elem is FamilyInstance)
                {
                    paramName = "Mark";
                }

                foreach (Parameter curParam in elem.Parameters)
                {
                    if (curParam.Definition.Name == paramName)
                    {
                        param = curParam;
                        break;
                    }
                }
            }

            return param;
        }
    }
}
