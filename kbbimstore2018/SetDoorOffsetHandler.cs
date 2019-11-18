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

    public class SetDoorOffsetHandler : IExternalEventHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        private double selectedOffset = 0;
        List<Tuple<Autodesk.Revit.DB.FamilyInstance, Autodesk.Revit.DB.Wall>> selectedDoorsAndWalls = null;


        public SetDoorOffsetHandler(List<Tuple<Autodesk.Revit.DB.FamilyInstance, Autodesk.Revit.DB.Wall>> selectedDoorsAndWalls, double selectedOffset)
        {
            this.selectedOffset = selectedOffset;
            this.selectedDoorsAndWalls = selectedDoorsAndWalls;

            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "Set Door Offset";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.doc = this.uiapp.ActiveUIDocument.Document;

            ModifyScene(uiapp, "Set Door Offset", SetDoorOffset);
        }

        private void ModifyScene(UIApplication uiapp, String text, MyOperation operation)
        {
            uidoc = uiapp.ActiveUIDocument;

            if (uidoc != null)
            {
                doc = uidoc.Document;

                using (Transaction trans = new Transaction(doc))
                {
                    if (trans.Start(text) == TransactionStatus.Started)
                    {
                        operation(1);

                        trans.Commit();
                    }
                }
            }
        }

        private void SetDoorOffset(int id)
        {
            try
            {

                foreach (Tuple<Autodesk.Revit.DB.FamilyInstance, Autodesk.Revit.DB.Wall> curSelectedDoorAndWall in selectedDoorsAndWalls)
                {
                    Autodesk.Revit.DB.FamilyInstance selectedDoor = curSelectedDoorAndWall.Item1;
                    Autodesk.Revit.DB.Wall selectedWall = curSelectedDoorAndWall.Item2;

                    if ((selectedDoor != null) && (selectedWall != null))
                    {
                        try
                        {
                            double selectedDoorWidth = selectedDoor.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();
                            double selectedWallWidth = selectedWall.Width;

                            LocationCurve selectedWallLocationCurve = selectedWall.Location as LocationCurve;
                            Curve selectedWalCurve = selectedWallLocationCurve.Curve;

                            Wall hostWall = selectedDoor.Host as Wall;
                            LocationCurve hostWallLocationCurve = hostWall.Location as LocationCurve;
                            Curve hostWallCurve = hostWallLocationCurve.Curve;

                            IntersectionResultArray ira = new IntersectionResultArray();
                            SetComparisonResult scr = selectedWalCurve.Intersect(hostWallCurve, out ira);

                            var iter = ira.GetEnumerator();
                            if (iter.MoveNext())
                            {
                                IntersectionResult ir = iter.Current as IntersectionResult;
                                if (ir != null)
                                {
                                    XYZ intersectionXYZ = ir.XYZPoint;
                                    double intersectionParam = hostWallCurve.Project(intersectionXYZ).Parameter;

                                    LocationPoint doorPoint = selectedDoor.Location as LocationPoint;
                                    XYZ doorXYZ = doorPoint.Point;
                                    double doorParam = hostWallCurve.Project(doorXYZ).Parameter;

                                    XYZ translation = null;
                                    XYZ doorEdgeXYZ = null;
                                    XYZ intersectionOffsetXYZ = null;
                                    if (intersectionParam > doorParam)
                                    {
                                        intersectionOffsetXYZ = hostWallCurve.Evaluate(intersectionParam - this.selectedOffset - selectedWallWidth / 2, false);
                                        doorEdgeXYZ = hostWallCurve.Evaluate(doorParam + selectedDoorWidth / 2, false);
                                        translation = intersectionOffsetXYZ.Subtract(doorEdgeXYZ);
                                    }
                                    else
                                    {
                                        intersectionOffsetXYZ = hostWallCurve.Evaluate(intersectionParam + this.selectedOffset + selectedWallWidth / 2, false);
                                        doorEdgeXYZ = hostWallCurve.Evaluate(doorParam - selectedDoorWidth / 2, false);
                                        translation = doorEdgeXYZ.Subtract(intersectionOffsetXYZ).Negate();
                                    }

                                    ElementTransformUtils.MoveElement(doc, selectedDoor.Id, translation);
                                }
                            }

                        }
                        catch (Exception Exception)
                        {
                        }
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
            TaskDialog.Show("Info", "Set Door Offset");
        }

    }
}
