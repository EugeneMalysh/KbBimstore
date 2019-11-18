
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;

namespace KbRevitstore
{
    [Transaction(TransactionMode.Automatic)]
    [Regeneration(RegenerationOption.Manual)]
    public class Commands : IExternalCommand
    {

        public class MySelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                return elem is Room;
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return true;
            }
        }

        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            try
            {
                int roomNum = 1;
                while (true)
                {
                    Reference selRef = uidoc.Selection.PickObject(ObjectType.Element,
                        new MySelectionFilter(),
                        "Pick a room 2");

                    Room room = (Room)uidoc.Document.GetElement(selRef.ElementId);

                    FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);

                    collector.WherePasses(new RoomFilter());

                    ParameterValueProvider provider = new ParameterValueProvider(new ElementId(BuiltInParameter.ROOM_NUMBER));

                    FilterStringEquals evaluator = new FilterStringEquals();

                    FilterStringRule rule = new FilterStringRule(provider, evaluator, roomNum.ToString(), false);


                    ElementParameterFilter filter = new ElementParameterFilter(rule);

                    collector.WherePasses(filter);

                    IList<Element> rooms = collector.ToElements();

                    if (rooms.Count > 0)
                    {
                        ((Room)(rooms[0])).Number = room.Number;
                    }

                    room.Number = roomNum.ToString();

                    uidoc.Document.Regenerate();

                    roomNum++;
                }

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }


            return Result.Succeeded;
        }
    }
}
