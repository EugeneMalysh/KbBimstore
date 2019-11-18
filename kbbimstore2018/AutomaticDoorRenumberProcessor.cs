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
    class AutomaticDoorRenumberProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;

        private Room selectedRoom = null;
        private Element selectedDoor = null;
        private Parameter selectedRoomNumberParameter = null;
        private Parameter selectedDoorNumberParameter = null;

        private List<int> selectedNumbers = new List<int>();
        private List<Parameter> selectedParameters = new List<Parameter>();
        private List<ElementId> selectedElementsIds = new List<ElementId>();

        private Dictionary<ElementId, HashSet<ElementId>> roomsWithDoors = new Dictionary<ElementId, HashSet<ElementId>>();

        public AutomaticDoorRenumberProcessor(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = uiapp.ActiveUIDocument;
            this.doc = uidoc.Document;
        }

        public void init()
        {

            try
            {
                while (true)
                {
                    Reference roomReference = uidoc.Selection.PickObject(ObjectType.Element, "Select a room.");
                    if (roomReference != null)
                    {
                        ElementId roomElementId = roomReference.ElementId;
                        if (roomElementId != null)
                        {
                            Element roomElement = this.doc.GetElement(roomElementId);
                            if (roomElement != null)
                            {
                                if (roomElement is Room)
                                {
                                    this.selectedRoom = roomElement as Room;
                                    this.selectedRoomNumberParameter = getNumberParameter(roomElement);

                                    break;
                                }
                                else
                                {
                                    TaskDialog.Show("Info", "Your selection is not a room, please select a room.");
                                }
                            }
                        }
                    }
                }

                while (true)
                {
                    Reference doorReference = uidoc.Selection.PickObject(ObjectType.Element, "Select a door.");
                    if (doorReference != null)
                    {
                        ElementId doorElementId = doorReference.ElementId;
                        if (doorElementId != null)
                        {
                            Element doorElement = this.doc.GetElement(doorElementId);
                            if (doorElement != null)
                            {
                                if (doorElement.Category.Name == "Doors")
                                {
                                    this.selectedDoor = doorElement;
                                    this.selectedDoorNumberParameter = getNumberParameter(doorElement);

                                    break;
                                }
                                else
                                {
                                    TaskDialog.Show("Info", "Your selection is not a door, please select a door.");
                                }
                            }
                        }
                    }
                }

                if ((this.selectedRoomNumberParameter != null) && (this.selectedDoorNumberParameter != null))
                {
                    AutomaticDoorRenumberHandler handler = new AutomaticDoorRenumberHandler(this.selectedRoomNumberParameter, this.selectedDoorNumberParameter);
                    handler.Execute(uiapp);
                }
                else
                {
                    TaskDialog.Show("Info", "Numper prameters are null");
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }

            #region
            /*
            separateDoorsByRooms();

            if (roomsWithDoors.Count > 0)
            {
                StringBuilder strBld = new StringBuilder();
                StringBuilder subStrBld = new StringBuilder();

                foreach (KeyValuePair<ElementId, HashSet<ElementId>> curKeyValue in roomsWithDoors)
                {
                    Room curRoom = doc.GetElement(curKeyValue.Key) as Room;
                    if (curRoom != null)
                    {
                        subStrBld.Clear();

                        subStrBld.Append(curRoom.Id.ToString() + " -> ");
                        foreach (ElementId curElementId in curKeyValue.Value)
                        {
                            Element curElement = doc.GetElement(curElementId);
                            if (curElement != null)
                            {
                                if (curElement is FamilyInstance)
                                {
                                    subStrBld.Append(curElementId.ToString() + ":" + curElement.Category.Name + ", ");
                                }
                            }
                        }

                        strBld.AppendLine(subStrBld.ToString());
                    }
                }

                AlmMessageBox msgBox = new AlmMessageBox(strBld.ToString());
                msgBox.ShowDialog();
            }
            */
            #endregion
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

        private void separateDoorsByRooms()
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator elementsIterator = docFilter.OfClass(typeof(SpatialElement)).GetElementIterator();

                while (elementsIterator.MoveNext())
                {
                    Element curElement = elementsIterator.Current;
                    if (curElement != null)
                    {
                        if (curElement.GetType().Name == "Room")
                        {
                            Room curRoom = curElement as Room;
                            if (curRoom != null)
                            {
                                ElementId curRoomId = curRoom.Id;

                                SpatialElementBoundaryOptions boundaryOptions = new SpatialElementBoundaryOptions();
                                boundaryOptions.StoreFreeBoundaryFaces = true;
                                boundaryOptions.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.CoreBoundary;

                                IList<IList<Autodesk.Revit.DB.BoundarySegment>> boundarySegments = curRoom.GetBoundarySegments(boundaryOptions);
                                foreach (IList<Autodesk.Revit.DB.BoundarySegment> boundaryList in boundarySegments)
                                {
                                    foreach (Autodesk.Revit.DB.BoundarySegment boundarySegment in boundaryList)
                                    {
                                        if (doc != null)
                                        {
                                            ElementId boundaryElementId = boundarySegment.ElementId;
                                            if (boundaryElementId != null)
                                            {
                                                Element boundaryElement = doc.GetElement(boundaryElementId);
                                                if (boundaryElement != null)
                                                {
                                                    if (boundaryElement.GetType().Name == "Wall")
                                                    {
                                                        Wall curWall = boundaryElement as Wall;
                                                        if (curWall != null)
                                                        {
                                                            IList<ElementId> curInsertsIds = curWall.FindInserts(true, false, false, false);
                                                            foreach (ElementId curInsertId in curInsertsIds)
                                                            {
                                                                Element curInsert = doc.GetElement(curInsertId);
                                                                if (curInsert != null)
                                                                {
                                                                    if (curInsert is FamilyInstance)
                                                                    {
                                                                        FamilyInstance curFamilyInstance = curInsert as FamilyInstance;
                                                                        if (curFamilyInstance != null)
                                                                        {
                                                                            if (curFamilyInstance.Category.Name == "Doors")
                                                                            {
                                                                                if (roomsWithDoors.ContainsKey(curRoomId))
                                                                                {
                                                                                    roomsWithDoors[curRoomId].Add(curInsertId);
                                                                                }
                                                                                else
                                                                                {
                                                                                    HashSet<ElementId> norHashSet = new HashSet<ElementId>();
                                                                                    norHashSet.Add(curInsertId);
                                                                                    roomsWithDoors.Add(curRoomId, norHashSet);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
