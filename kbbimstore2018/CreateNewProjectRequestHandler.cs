using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{

    public class CreateNewProjectRequestHandler : IExternalEventHandler
    {
        private UIDocument uidoc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        private CreateNewProjectRequestData requestData;

        public CreateNewProjectRequestHandler(CreateNewProjectRequestData requestData)
        {
            this.requestData = requestData;
            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "KbBimstoreRequestHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            ModifyScene(uiapp, "Create Front View Sheets", CreateFrontViewSheets);
            ModifyScene(uiapp, "Create Main View Sheets", CreateLevelsAndMainViewSheets);

            TaskDialog.Show("Info", "Project was created");
        }

        private void ModifyScene(UIApplication uiapp, String text, MyOperation operation)
        {
            uidoc = uiapp.ActiveUIDocument;

            if (uidoc != null)
            {
                using (Transaction trans = new Transaction(uidoc.Document))
                {
                    if (trans.Start(text) == TransactionStatus.Started)
                    {
                        operation(1);

                        trans.Commit();
                    }
                }
            }
        }

        private List<Element> getAllNonViewElementsOfDoc()
        {
            List<Element> allNonViewElements = new List<Element>();

            FilteredElementCollector docFilter = new FilteredElementCollector(uidoc.Document);
            if (docFilter != null)
            {
                FilteredElementIterator elemsIterator = docFilter.WhereElementIsNotElementType().GetElementIterator();
                while (elemsIterator.MoveNext())
                {
                    Element curElem = elemsIterator.Current;
                    if (!(curElem is Autodesk.Revit.DB.View))
                    {
                        if (!curElem.ViewSpecific)
                        {
                            allNonViewElements.Add(curElem);
                        }
                    }
                }
            }

            return allNonViewElements;
        }

        private void CreateFrontViewSheets(int id)
        {
            if (uidoc.Document != null)
            {

                if (requestData != null)
                {
                    List<Element> allNonViewElements = getAllNonViewElementsOfDoc();

                    for (int f = 0; f < requestData.frontSheetsInfo.Count; f++)
                    {

                        Tuple<string, string, string> curFrontSheetInfo = requestData.frontSheetsInfo[f];

                        ElementId titleBlocktId = GetTitleBlockIdByName(requestData.titleBlockName);
                        if (titleBlocktId == null)
                        {
                            titleBlocktId = new ElementId(-1);
                        }
                        ViewSheet norViewSheet = ViewSheet.Create(uidoc.Document, titleBlocktId);

                        norViewSheet.SheetNumber = curFrontSheetInfo.Item1;
                        norViewSheet.Name = curFrontSheetInfo.Item2;

                        View norSheetViewPlan = null;
                        ViewFamilyType viewFamilyType = null;
                        FilteredElementCollector collectorViewFamilyType = new FilteredElementCollector(uidoc.Document);
                        var viewFamilyTypes = collectorViewFamilyType.OfClass(typeof(ViewFamilyType)).ToElements();
                        foreach (Element e in viewFamilyTypes)
                        {
                            ViewFamilyType v = e as ViewFamilyType;
                            if (v.ViewFamily == ViewFamily.FloorPlan)
                            {
                                viewFamilyType = v;
                                break;
                            }
                        }

                        Level exLevel = null;
                        FilteredElementCollector collectorLevel = new FilteredElementCollector(uidoc.Document);
                        var levels = collectorLevel.OfClass(typeof(Level)).ToElements();
                        foreach (Element e in levels)
                        {
                            exLevel = e as Level;
                        }

                        if ((viewFamilyType != null) && (exLevel != null))
                        {
                            norSheetViewPlan = ViewPlan.Create(uidoc.Document, viewFamilyType.Id, exLevel.Id);
                            norSheetViewPlan.Name = norViewSheet.Name;
                        }

                        if (norSheetViewPlan != null)
                        {
                            View viewTemplate = GetViewByName(curFrontSheetInfo.Item3);
                            if (viewTemplate != null)
                            {

                                List<ElementId> hiddenElemsIds = new List<ElementId>();
                                foreach (Element curHidVisElem in allNonViewElements)
                                {
                                    if (curHidVisElem.IsHidden(viewTemplate))
                                    {
                                        hiddenElemsIds.Add(curHidVisElem.Id);
                                    }
                                }

                                try
                                {
                                    norSheetViewPlan.HideElements(hiddenElemsIds);
                                }
                                catch (Exception ex)
                                {
                                }
                            }

                            try
                            {
                                SetViewForSheet(norSheetViewPlan, norViewSheet, 1);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {

                        }
                    }

                    uidoc.RefreshActiveView();
                }

            }
            else
            {
                TaskDialog.Show("Info", "doc is null");
            }
        }

        private void CreateLevelsAndMainViewSheets(int id)
        {
            if (uidoc.Document != null)
            {
                if (requestData != null)
                {
                    List<Element> allNonViewElements = getAllNonViewElementsOfDoc();

                    if (requestData.mainSheetsInfo.Count == (requestData.levelsAmount * requestData.mainSheetsAmount))
                    {

                        for (int l = 0; l < requestData.levelsAmount; l++)
                        {

                            double norLevelElevation = l * requestData.levelsDistance;

                            Level norLevel = Level.Create(uidoc.Document, norLevelElevation);
                            string levelName = "Level-" + (l + 1).ToString();
                            norLevel.Name = levelName;

                            for (int m = 0; m < requestData.mainSheetsAmount; m++)
                            {
                                Tuple<string, string, string> curMainSheetInfo = requestData.mainSheetsInfo[l * requestData.mainSheetsAmount + m];

                                ElementId titleBlocktId = GetTitleBlockIdByName(requestData.titleBlockName);
                                if (titleBlocktId == null)
                                {
                                    titleBlocktId = new ElementId(-1);
                                }
                                ViewSheet norViewSheet = ViewSheet.Create(uidoc.Document, titleBlocktId);

                                norViewSheet.SheetNumber = curMainSheetInfo.Item1;
                                norViewSheet.Name = curMainSheetInfo.Item2;

                                View norSheetViewPlan = null;
                                ViewFamilyType viewFamilyType = null;

                                FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);
                                var viewFamilyTypes = collector.OfClass(typeof(ViewFamilyType)).ToElements();

                                foreach (Element e in viewFamilyTypes)
                                {
                                    ViewFamilyType v = e as ViewFamilyType;
                                    if (v.ViewFamily == ViewFamily.FloorPlan)
                                    {
                                        viewFamilyType = v;
                                        break;
                                    }
                                }

                                if ((viewFamilyType != null) && (norLevel != null))
                                {
                                    norSheetViewPlan = ViewPlan.Create(uidoc.Document, viewFamilyType.Id, norLevel.Id);
                                    norSheetViewPlan.Name = norViewSheet.Name;
                                }

                                if (norSheetViewPlan != null)
                                {
                                    View viewTemplate = GetViewTemplateByName(curMainSheetInfo.Item3);
                                    bool templateApplied = false;

                                    if (viewTemplate != default(View))
                                    {
                                        List<ElementId> hiddenElemsIds = new List<ElementId>();
                                        foreach (Element curHidVisElem in allNonViewElements)
                                        {
                                            if (curHidVisElem.IsHidden(viewTemplate))
                                            {
                                                hiddenElemsIds.Add(curHidVisElem.Id);
                                            }
                                        }

                                        try
                                        {
                                            norSheetViewPlan.HideElements(hiddenElemsIds);
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        try
                                        {
                                            norSheetViewPlan.ViewTemplateId = viewTemplate.Id;
                                            templateApplied = true;
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    try
                                    {
                                        int curMainScale;
                                        if (!templateApplied)
                                            curMainScale = GetScaleFromString(requestData.mainSheetsScale);
                                        else
                                        {
                                            if (uidoc.Document.DisplayUnitSystem == DisplayUnit.IMPERIAL)
                                                curMainScale = GetScaleFromString(viewTemplate.get_Parameter(BuiltInParameter.VIEW_SCALE_PULLDOWN_IMPERIAL).AsValueString());
                                            else
                                                curMainScale = GetScaleFromString(viewTemplate.get_Parameter(BuiltInParameter.VIEW_SCALE_PULLDOWN_METRIC).AsValueString());
                                        }

                                        SetViewForSheet(norSheetViewPlan, norViewSheet, curMainScale);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                    }

                    uidoc.RefreshActiveView();
                }
            }
            else
            {
                TaskDialog.Show("Info", "doc is null");
            }
        }

        private int GetScaleFromString(string scalestr)
        {
            return (KbBimstoreConst.getScaleValue(scalestr));
        }

        private View GetViewTemplateByName(string name)
        {
            List<View> viewTemplates = new List<View>();

            if (uidoc.Document != null)
            {
                FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document).OfClass(typeof(View));

                foreach (View view in collector.ToElements())
                {
                    if (view.IsTemplate)
                        viewTemplates.Add(view);
                }
            }

            return viewTemplates.FirstOrDefault<View>(x => x.Name == name);
        }

        private View GetViewByName(string name)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(uidoc.Document);

            if (docFilter != null)
            {
                FilteredElementIterator viewsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = uidoc.Document.GetElement(curElementId) as ElementType;

                        if (curElementType != null)
                        {
                            if (curElementType.GetType().Name == "ViewFamilyType")
                            {
                                Autodesk.Revit.DB.ViewFamilyType curViewFamilyType = (ViewFamilyType)curElementType;

                                if (curViewFamilyType != null)
                                {
                                    string curViewName = curViewFamilyType.Name + ": " + curView.Name;

                                    if (curViewName == name)
                                    {
                                        return curView;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private ElementId GetViewIdByName(string name)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(uidoc.Document);

            if (docFilter != null)
            {
                FilteredElementIterator viewsIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    string curViewTypeName = curView.GetType().Name;

                    if ((curViewTypeName == "ViewDrafting") || (curViewTypeName == "ViewPlan"))
                    {
                        Autodesk.Revit.DB.ElementId curElementId = curView.GetTypeId();
                        Autodesk.Revit.DB.ElementType curElementType = uidoc.Document.GetElement(curElementId) as ElementType;

                        if (curElementType != null)
                        {
                            if (curElementType.GetType().Name == "ViewFamilyType")
                            {
                                Autodesk.Revit.DB.ViewFamilyType curViewFamilyType = (ViewFamilyType)curElementType;

                                if (curViewFamilyType != null)
                                {
                                    string curViewName = curViewFamilyType.Name + ": " + curView.Name;

                                    if (curViewName == name)
                                    {
                                        return curView.Id;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private ElementId GetTitleBlockIdByName(string name)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(uidoc.Document);

            if (docFilter != null)
            {
                FilteredElementIterator titleBlocksIterator = docFilter.OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_TitleBlocks).GetElementIterator();

                while (titleBlocksIterator.MoveNext())
                {
                    Autodesk.Revit.DB.Element curTitleBlock = titleBlocksIterator.Current;

                    if (name == curTitleBlock.Name)
                    {
                        return curTitleBlock.Id;
                    }
                }
            }

            return null;
        }

        private void SetViewForSheet(View view, ViewSheet sheet, int scale)
        {
            if (scale > 0)
            {
                try
                {
                    view.Scale = scale;
                }
                catch (Exception ex)
                {
                }
            }

            try
            {
                if (Viewport.CanAddViewToSheet(view.Document, sheet.Id, view.Id))
                {
                    BoundingBoxUV sheetBox = sheet.Outline;
                    XYZ sheetOrigin = sheet.Origin;

                    Viewport viewport = Viewport.Create(view.Document, sheet.Id, view.Id, XYZ.Zero);

                    BoundingBoxXYZ viewportBoundingBox = viewport.get_BoundingBox(sheet);
                    XYZ viewportOrigin = viewportBoundingBox.Min;

                    ElementTransformUtils.MoveElement(view.Document, viewport.Id, new XYZ(sheetOrigin.X - viewportOrigin.X, sheetOrigin.Y - viewportOrigin.Y, 0));
                }
                else
                {
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

    }
}