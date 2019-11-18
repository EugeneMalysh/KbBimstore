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
    class AutomaticDoorRenumberToFromProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;

        private List<FamilyInstance> selectedDoors = new List<FamilyInstance>();

        public AutomaticDoorRenumberToFromProcessor(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = uiapp.ActiveUIDocument;
            this.doc = uidoc.Document;
        }

        public void init()
        {
            try
            {
                FilteredElementCollector docFilter = new FilteredElementCollector(doc, doc.ActiveView.Id).OfClass(typeof(FamilyInstance));
                if (docFilter != null)
                {
                    FilteredElementIdIterator docFilterIterator = docFilter.GetElementIdIterator();
                    while (docFilterIterator.MoveNext())
                    {
                        ElementId selectedElementId = docFilterIterator.Current;
                        if (selectedElementId != null)
                        {
                            Element selectedElement = this.doc.GetElement(selectedElementId);
                            if (selectedElement != null)
                            {
                                if (selectedElement.Category.Name == "Doors")
                                {
                                    if (selectedElement is FamilyInstance)
                                    {
                                        FamilyInstance selectedDoor = selectedElement as FamilyInstance;
                                        if (selectedDoor != null)
                                        {
                                            this.selectedDoors.Add(selectedDoor);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #region
                /*
                while (true)
                {
                    Reference selectedReference = uidoc.Selection.PickObject(ObjectType.Element, "Select doors in order to be renumbered. Press ESC key when finished.");
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
                                            FamilyInstance selectedDoor = selectedElement as FamilyInstance;
                                            if (selectedDoor != null)
                                            {
                                                this.selectedDoors.Add(selectedDoor);
                                            }
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
                 */
                #endregion
            }
            catch (Exception ex)
            {
            }

            if (this.selectedDoors.Count > 0)
            {
                AutomaticDoorRenumberToFromForm form = new AutomaticDoorRenumberToFromForm();
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    int selectedDirection = form.getSelectedDirection();
                    AutomaticDoorRenumberToFromHandler handler = new AutomaticDoorRenumberToFromHandler(this.selectedDoors, selectedDirection);
                    handler.Execute(uiapp);
                }
            }
            else
            {
                TaskDialog.Show("Info", "You did not select any door to renumber.");
            }
        }
    }
}
