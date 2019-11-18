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
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    class SetDoorOffsetProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private double offsetValue = 1;

        public SetDoorOffsetProcessor(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = uiapp.ActiveUIDocument;
            this.doc = uidoc.Document;
        }

        public void init()
        {
            SetDoorOffsetForm form = new SetDoorOffsetForm();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                offsetValue = form.getOffset();
            }


            List<Tuple<Autodesk.Revit.DB.FamilyInstance, Autodesk.Revit.DB.Wall>> selectedDoorsAndWalls = new List<Tuple<FamilyInstance, Wall>>();

            try
            {
                while (true)
                {
                    Autodesk.Revit.DB.FamilyInstance selectedDoor = null;
                    Autodesk.Revit.DB.Wall selectedWall = null;

                    while (selectedDoor == null)
                    {
                        Reference selectedReference = uidoc.Selection.PickObject(ObjectType.Element, "Select doors in order to set offset. Press ESC key when finished.");
                        if (selectedReference != null)
                        {
                            ElementId selectedElementId = selectedReference.ElementId;
                            if (selectedElementId != null)
                            {
                                Element selectedElement = this.doc.GetElement(selectedElementId);
                                if (selectedElement != null)
                                {
                                    if (selectedElement.Category.Name == "Doors")
                                    {
                                        if (selectedElement is FamilyInstance)
                                        {
                                            selectedDoor = selectedElement as FamilyInstance;
                                        }
                                    }
                                    else
                                    {
                                        TaskDialog.Show("Info", "Your selection is not a door, it will not be counted.");
                                    }
                                }
                            }
                        }
                    }

                    while (selectedWall == null)
                    {
                        Reference selectedReference = uidoc.Selection.PickObject(ObjectType.Element, "Select a wall in order to set offset. Press ESC key when finished.");
                        if (selectedReference != null)
                        {
                            ElementId selectedElementId = selectedReference.ElementId;
                            if (selectedElementId != null)
                            {
                                Element selectedElement = this.doc.GetElement(selectedElementId);
                                if (selectedElement != null)
                                {
                                    if (selectedElement is Wall)
                                    {
                                        selectedWall = selectedElement as Wall;
                                    }
                                    else
                                    {
                                        TaskDialog.Show("Info", "Your selection is not a wall it will not be counted.");
                                    }
                                }
                            }
                        }
                    }


                    if ((selectedDoor != null) && (selectedWall != null))
                    {
                        selectedDoorsAndWalls.Add(new Tuple<FamilyInstance, Wall>(selectedDoor, selectedWall));
                    }
                }

            }
            catch (Exception ex)
            {
            }

            if (selectedDoorsAndWalls.Count > 0)
            {
                SetDoorOffsetHandler handler = new SetDoorOffsetHandler(selectedDoorsAndWalls, this.offsetValue);
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                exEvent.Raise();
            }
        }
    }
}
