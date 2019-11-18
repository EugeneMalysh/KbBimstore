using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{

    public class DesignOptionsRequestHandler : IExternalEventHandler
    {
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        private DesignOptionsRequestData requestData;

        public DesignOptionsRequestHandler(DesignOptionsRequestData requestData)
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
            return "DesignOptionsRequestHandler";
        }

        public void Execute(UIApplication uiapp)
        {

            ModifyScene(uiapp, "Create Design Options", CreateDesignOptions);

            TaskDialog.Show("Info", "Design options were created");
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

        private void CreateDesignOptions(int id)
        {
            int curScale = GetScaleFromString(requestData.scaleName);

            for (int r = 0; r < requestData.optionsInfos.Count; r++)
            {
                Tuple<string, string, string, string, string, string> curDesignOptionInfo = requestData.optionsInfos[r];

                DesignOption curDesignOption = getDesignOptionIdByNames(curDesignOptionInfo.Item1, curDesignOptionInfo.Item2);

                if (curDesignOption != null)
                {
                    Autodesk.Revit.DB.Level curLevel = GetLevelFromString(curDesignOptionInfo.Item4);

                    if (curLevel != null)
                    {
                        try
                        {
                            Autodesk.Revit.DB.ViewPlan curViewPlan = createViewPlan(curLevel, curDesignOptionInfo.Item3);
                            foreach (Parameter curParameter in curViewPlan.Parameters)
                            {
                                if (curParameter.Definition.Name == "Visible In Option")
                                {
                                    curParameter.Set(curDesignOption.Id);
                                    break;
                                }
                            }

                            if (curViewPlan != null)
                            {
                                Autodesk.Revit.DB.ViewSheet curViewSheet = createViewSheet(curDesignOptionInfo.Item5, curDesignOptionInfo.Item6, requestData.titleBlockName);

                                if (curViewSheet != null)
                                {
                                    SetViewForSheet(curViewPlan, curViewSheet, curScale);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TaskDialog.Show("Excepion", ex.Message + "  " + curDesignOptionInfo.Item4);
                        }
                    }
                }
            }
        }

        private DesignOption getDesignOptionIdByNames(string designOptionSetName, string designOptionName)
        {
            IEnumerable<DesignOption> designOptions = new FilteredElementCollector(doc).OfClass(typeof(DesignOption)).Cast<DesignOption>();

            foreach (DesignOption curDesignOption in designOptions)
            {
                foreach (Parameter curParameter in curDesignOption.Parameters)
                {
                    if (curParameter.Definition.Name == "Design Option Set Id")
                    {
                        ElementId curSetElementId = curParameter.AsElementId();
                        if (curSetElementId != null)
                        {
                            Element curSetElement = doc.GetElement(curSetElementId);
                            if (curSetElement != null)
                            {
                                if ((designOptionSetName == curSetElement.Name) && (designOptionName == curDesignOption.Name))
                                {
                                    return curDesignOption;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private Autodesk.Revit.DB.Level GetLevelFromString(string levelName)
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator levelsIterator = docFilter.OfClass(typeof(Level)).GetElementIterator();

                while (levelsIterator.MoveNext())
                {
                    Level curLevel = levelsIterator.Current as Level;
                    if (curLevel != null)
                    {
                        if (curLevel.Name == levelName)
                        {
                            return curLevel;
                        }
                    }
                }
            }

            return null;
        }

        private int GetScaleFromString(string scalestr)
        {
            return (KbBimstoreConst.getScaleValue(scalestr));
        }

        private Autodesk.Revit.DB.ViewPlan createViewPlan(Level level, string viewName)
        {
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

            if ((viewFamilyType != null) && (level != null))
            {
                ViewPlan viewPlan = ViewPlan.Create(uidoc.Document, viewFamilyType.Id, level.Id);
                viewPlan.Name = viewName;

                return viewPlan;
            }

            return null;
        }

        private Autodesk.Revit.DB.ViewSheet createViewSheet(string viewNumber, string viewName, string titleBlockName)
        {
            ElementId titleBlocktId = GetTitleBlockIdByName(requestData.titleBlockName);
            if (titleBlocktId == null)
            {
                titleBlocktId = new ElementId(-1);
            }

            ViewSheet norViewSheet = ViewSheet.Create(doc, titleBlocktId);
            norViewSheet.SheetNumber = viewNumber;
            norViewSheet.Name = viewName;

            return norViewSheet;
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
                    double yPosition = (sheetBox.Max.V - sheetBox.Min.V) / 2 + sheetBox.Min.V;
                    double xPosition = (sheetBox.Max.U - sheetBox.Min.U) / 2 + sheetBox.Min.U;

                    XYZ orig = new XYZ(xPosition, yPosition, 0);
                    Viewport viewport = Viewport.Create(view.Document, sheet.Id, view.Id, orig);
                }
                else
                {
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

        private List<DesignOption> getExistingDesignOptions()
        {
            IEnumerable<DesignOption> designOptions = new FilteredElementCollector(doc).OfClass(typeof(DesignOption)).Cast<DesignOption>();

            return (new List<DesignOption>(designOptions));
        }

        private List<Tuple<string, string>> getExistingDesignOptionsInfos()
        {
            List<Tuple<string, string>> designOptionsInfos = new List<Tuple<string, string>>();

            List<DesignOption> designOptions = getExistingDesignOptions();
            foreach (DesignOption curDesignOption in designOptions)
            {
                string curDesignOptionName = curDesignOption.Name;
                string curDesignOptionSetName = "";

                foreach (Parameter curParameter in curDesignOption.Parameters)
                {
                    if (curParameter.Definition.Name == "Design Option Set Id")
                    {
                        ElementId curSetElementId = curParameter.AsElementId();
                        if (curSetElementId != null)
                        {
                            Element curSetElement = doc.GetElement(curSetElementId);
                            if (curSetElement != null)
                            {
                                curDesignOptionSetName = curSetElement.Name;
                            }
                        }
                    }
                }

                if ((curDesignOptionName != "") && (curDesignOptionSetName != ""))
                {
                    designOptionsInfos.Add(new Tuple<string, string>(curDesignOptionName, curDesignOptionSetName));
                }
            }

            return designOptionsInfos;
        }

    }
}
