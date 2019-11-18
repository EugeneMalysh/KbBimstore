using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using View = Autodesk.Revit.DB.View;

namespace KbBimstore
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ViewDepthOverrideCommand : IExternalCommand
    {
        private const int SetCount = 5;
        private UIDocument _activeUIDocument;
        private Application _application;
        private LineWeightSettings _lineWeightSettings;
        private ICollection<ElementId>[] _elementsSet; 


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                _activeUIDocument = commandData.Application.ActiveUIDocument;
                _application = commandData.Application.Application;
                _elementsSet = new ICollection<ElementId>[SetCount];

                for (int x = 0; x < SetCount; x++)
                {
                    _elementsSet[x] = new List<ElementId>();
                }

                var frm = new ViewDepthOverrideForm();
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (frm.LineWeightSettings != null)
                    {
                        _lineWeightSettings = frm.LineWeightSettings;
                        ViewDepthOverride();
                        return Result.Succeeded;
                    }
                }
            }
            catch (Exception e)
            {
            }
            return Result.Failed;
        }


        private bool FillSets(View view)
        {
            Transform identity = Transform.Identity;
            identity.set_Basis(0, view.RightDirection);
            identity.set_Basis(1, view.UpDirection);
            identity.set_Basis(2, view.ViewDirection);
            identity.Origin = view.Origin;

            Transform inverse = identity.Inverse;

            double farPoint = view.get_Parameter(BuiltInParameter.VIEWER_BOUND_OFFSET_FAR).AsDouble();
            var clippingParameter = view.get_Parameter(BuiltInParameter.VIEWER_BOUND_FAR_CLIPPING);
            if (clippingParameter == null)
                return false;

            if (clippingParameter.AsInteger() == 0)
            {
                farPoint = 1E+19;
            }
            var segments = farPoint/SetCount;
            foreach (Element element in
                from Element q in
                    (new FilteredElementCollector(_activeUIDocument.Document)).WhereElementIsNotElementType()
                        .WhereElementIsViewIndependent()
                where (q.Category != null && q.HasPhases())
                select q)
            {
                BoundingBoxXYZ boundingBox = element.get_Geometry(new Options()).GetBoundingBox();
                XYZ max = (boundingBox.Max + boundingBox.Min)/2;
                var elementDistance = Math.Abs(inverse.OfPoint(max).Z);

                for (int x = 1; x <= SetCount; x++)
                {
                    if (elementDistance < segments*x && elementDistance >= segments*(x - 1))
                    {
                        _elementsSet[x - 1].Add(element.Id);
                        break;
                    }
                }
            }
            return true;
        }
        
        public void ViewDepthOverride()
        {
            ////known issues: doesn't work with linked files adn 3D Views
            var uidoc = _activeUIDocument;
            var doc = uidoc.Document;
            
            if (!FillSets(doc.ActiveView))
            {
                TaskDialog.Show("View Depth Override", "View not supported");
                return;
            }
            //Begin the transaction to override the elements
            using (var t = new Transaction(doc, "View Depth Override"))
            {
                t.Start();
                for (int x = 0; x < SetCount; x++)
                {
                    if (_elementsSet[x].Count != 0)
                    {
                        SetLineWeight(_elementsSet[x], x);
                    }
                    //else
                    //{
                    //    //Just a precaution, not sure it is really necessary
                    //    TaskDialog.Show("View Depth Override",
                    //        "Something went wrong in the " + x +
                    //        " segment to be overridden.\n\nPlease check that the view depth in the current view is just enough to include the objects you need.");
                    //    break;
                    //}
                }

                doc.Regenerate();
                uidoc.RefreshActiveView();
                t.Commit();
            }
        }

        private bool SetLineWeight(IEnumerable<ElementId> elementIds, int elementsLocation)
        {
            try
            {
                LineWeightSettingItem lineWeightSetting = null;
                switch (elementsLocation)
                {
                    case 0:
                        lineWeightSetting = _lineWeightSettings.ForegroundElementsSettings;
                        break;

                    case 1:
                        lineWeightSetting = _lineWeightSettings.Middle1ElementsSettings;
                        break;

                    case 2:
                        lineWeightSetting = _lineWeightSettings.Middle2ElementsSettings;
                        break;

                    case 3:
                        lineWeightSetting = _lineWeightSettings.Middle3ElementsSettings;
                        break;

                    case 4:
                        lineWeightSetting = _lineWeightSettings.BackgroundElementsSettings;
                        break;
                }

                foreach (var elementId in elementIds)
                {
                    var overrideGraphicSettings = _activeUIDocument.ActiveView.GetElementOverrides(elementId);
                    if (lineWeightSetting.IsProjection)
                        overrideGraphicSettings.SetProjectionLineWeight(lineWeightSetting.LineWeight);
                    else
                        overrideGraphicSettings.SetCutLineWeight(lineWeightSetting.LineWeight);

                    _activeUIDocument.ActiveView.SetElementOverrides(elementId, overrideGraphicSettings);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
