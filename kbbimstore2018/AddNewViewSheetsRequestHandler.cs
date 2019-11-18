using System;
using System.Text;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{

    public class AddNewViewSheetsRequestHandler : IExternalEventHandler
    {
        private Document doc;
        private UIDocument uidoc;
        private delegate void MyOperation(int id);
        private UIApplication uiapp;
        AddNewViewSheetsRequestData requestData;

        public AddNewViewSheetsRequestHandler(AddNewViewSheetsRequestData requestData)
        {
            this.requestData = requestData;
        }

        public String GetName()
        {
            return "AddNewViewSheetsHandler";
        }

        public void Execute(UIApplication uiapp)
        {

            try
            {
                this.uiapp = uiapp;
                this.uidoc = uiapp.ActiveUIDocument;
                this.doc = uidoc.Document;

                ModifyScene(uiapp, "Add New View Sheets", AddNewViewSheets);

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exc", ex.Message);
            }

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

        private int GetScaleFromString(string scalestr)
        {
            return (KbBimstoreConst.getScaleValue(scalestr));
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
                    //TaskDialog.Show("Info", "Viewport.CanAddViewToSheet sheet.Name:" + sheet.Name + " view.Name:" + view.Name);

                    BoundingBoxUV sheetBox = sheet.Outline;
                    double yPosition = (sheetBox.Max.V - sheetBox.Min.V) / 2 + sheetBox.Min.V;
                    double xPosition = (sheetBox.Max.U - sheetBox.Min.U) / 2 + sheetBox.Min.U;

                    XYZ orig = new XYZ(xPosition, yPosition, 0);
                    Viewport viewport = Viewport.Create(view.Document, sheet.Id, view.Id, orig);

                    doc.Regenerate();
                }
                else
                {
                    //TaskDialog.Show("Exc", "Viewport.CanAddViewToSheet == false sheet.Name:" + sheet.Name + " view.Name:" + view.Name);
                }
            }
            catch (ArgumentException ex)
            {
                TaskDialog.Show("Exc", ex.Message);
            }
        }

        private Level getFirstLevel(Document document)
        {
            Level firstLevel = null;

            FilteredElementCollector levelsCollector = new FilteredElementCollector(document);
            IList<Element> levelList = levelsCollector.OfClass(typeof(Level)).ToElements();

            if (levelList.Count > 0)
            {
                firstLevel = levelList[0] as Level;
            }

            return firstLevel;
        }

        private void AddNewViewSheets_OLD(int id)
        {
            if (uidoc.Document != null)
            {
                if (requestData != null)
                {
                    for (int i = 0; i < requestData.viewSheetsInfo.Count; i++)
                    {
                        Tuple<string, string, string, string, string> curViewSheetInfo = requestData.viewSheetsInfo[i];

                        ElementId titleBlocktId = GetTitleBlockIdByName(curViewSheetInfo.Item5);
                        if (titleBlocktId == null)
                        {
                            titleBlocktId = new ElementId(-1);
                        }
                        ViewSheet norViewSheet = ViewSheet.Create(uidoc.Document, titleBlocktId);

                        norViewSheet.SheetNumber = curViewSheetInfo.Item1;
                        norViewSheet.Name = curViewSheetInfo.Item2;


                        View viewPlan = GetViewByName(curViewSheetInfo.Item4);
                        if (viewPlan == null)
                        {
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
                            Level exLevel = null;
                            FilteredElementCollector collectorLevel = new FilteredElementCollector(uidoc.Document);
                            var levels = collectorLevel.OfClass(typeof(Level)).ToElements();
                            foreach (Element e in levels)
                            {
                                exLevel = e as Level;
                            }

                            if ((viewFamilyType != null) && (exLevel != null))
                            {
                                viewPlan = ViewPlan.Create(uidoc.Document, viewFamilyType.Id, exLevel.Id);
                                viewPlan.Name = curViewSheetInfo.Item4;
                            }
                        }
                        else
                        {
                        }


                        if (viewPlan != null)
                        {
                            try
                            {
                                int curMainScale = GetScaleFromString(curViewSheetInfo.Item3);
                                SetViewForSheet(viewPlan, norViewSheet, curMainScale);
                            }
                            catch (Exception ex)
                            {
                                TaskDialog.Show("Exc", ex.Message + " " + norViewSheet.SheetNumber);
                            }
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


        private void AddNewViewSheets(int id)
        {
            if (uidoc.Document != null)
            {
                if (requestData != null)
                {

                    Level firstLevel = getFirstLevel(uidoc.Document);
                    if (firstLevel != null)
                    {

                        for (int i = 0; i < requestData.viewSheetsInfo.Count; i++)
                        {
                            Tuple<string, string, string, string, string> curViewSheetInfo = requestData.viewSheetsInfo[i];

                            ElementId titleBlocktId = GetTitleBlockIdByName(curViewSheetInfo.Item5);
                            if (titleBlocktId == null)
                            {
                                titleBlocktId = new ElementId(-1);
                            }
                            ViewSheet norViewSheet = ViewSheet.Create(uidoc.Document, titleBlocktId);

                            norViewSheet.SheetNumber = curViewSheetInfo.Item1;
                            norViewSheet.Name = curViewSheetInfo.Item2;


                            List<Element> allNonViewElements = getAllNonViewElementsOfDoc();

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



                            int curMainScale = GetScaleFromString(curViewSheetInfo.Item3);

                            if ((viewFamilyType != null) && (firstLevel != null))
                            {
                                norSheetViewPlan = ViewPlan.Create(uidoc.Document, viewFamilyType.Id, firstLevel.Id);
                                norSheetViewPlan.Name = norViewSheet.Name + "_" + norViewSheet.SheetNumber;
                            }

                            if (norSheetViewPlan != null)
                            {
                                View viewTemplate = GetViewByName(curViewSheetInfo.Item4);
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
                                    SetViewForSheet(norSheetViewPlan, norViewSheet, curMainScale);
                                }
                                catch (Exception ex)
                                {
                                    TaskDialog.Show("Info", "Exception:" + ex.ToString());
                                }
                            }

                        }

                        uidoc.Document.Regenerate();
                        uidoc.RefreshActiveView();
                    }
                    else
                    {
                        TaskDialog.Show("Info", "firstLevel is null");
                    }
                }
                else
                {
                    TaskDialog.Show("Info", "requestData is null");
                }
            }
            else
            {
                TaskDialog.Show("Info", "doc is null");
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

    }
}
