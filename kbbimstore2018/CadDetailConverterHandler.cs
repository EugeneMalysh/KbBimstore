using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{

    public class CadDetailConverterHandler : IExternalEventHandler
    {
        private string fileName = null;
        private Document doc = null;
        private UIDocument uidoc = null;
        private UIApplication uiapp = null;
        private ElementId importInstanceId = null;
        private Autodesk.Revit.DB.View importView = null;

        private delegate void MyOperation(int id);

        public CadDetailConverterRequestData requestData = new CadDetailConverterRequestData();

        public HashSet<ElementId> curCurveElementIds = new HashSet<ElementId>();
        public HashSet<ElementId> preCurveElementIds = new HashSet<ElementId>();
        public HashSet<ElementId> difCurveElementIds = new HashSet<ElementId>();

        public HashSet<ElementId> curGraphycStyleIds = new HashSet<ElementId>();
        public HashSet<ElementId> preGraphycStyleIds = new HashSet<ElementId>();
        public HashSet<ElementId> difGraphycStyleIds = new HashSet<ElementId>();

        public HashSet<ElementId> curTextNotesIds = new HashSet<ElementId>();
        public HashSet<ElementId> preTextNotesIds = new HashSet<ElementId>();
        public HashSet<ElementId> difTextNotesIds = new HashSet<ElementId>();

        public HashSet<ElementId> curTextNoteTypesIds = new HashSet<ElementId>();
        public HashSet<ElementId> preTextNoteTypesIds = new HashSet<ElementId>();
        public HashSet<ElementId> difTextNoteTypesIds = new HashSet<ElementId>();

        public HashSet<ElementId> curLineCategoriesIds = new HashSet<ElementId>();
        public HashSet<ElementId> preLineCategoriesIds = new HashSet<ElementId>();
        public HashSet<ElementId> difLineCategoriesIds = new HashSet<ElementId>();


        public Dictionary<string, TextNoteType> revitTextNoteTypesDict = new Dictionary<string, TextNoteType>();

        public Dictionary<string, Category> revitCategoriesDict = new Dictionary<string, Category>();
        public Dictionary<string, GraphicsStyle> revitLineStylesDict = new Dictionary<string, GraphicsStyle>();
        public Dictionary<Tuple<byte, byte, byte>, List<Category>> autocadColorLineStylesDict = new Dictionary<Tuple<byte, byte, byte>, List<Category>>();
        private Dictionary<string, int> curGraphicsStylesNamesCount = new Dictionary<string, int>();


        public CadDetailConverterHandler(Autodesk.Revit.DB.View importView, string fileName)
        {
            this.importView = importView;
            this.fileName = fileName;
        }

        public String GetName()
        {
            return "ImportAutoCADHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            try
            {
                this.uiapp = uiapp;
                this.uidoc = uiapp.ActiveUIDocument;
                this.doc = uidoc.Document;

                ModifyScene(uiapp, "Import AutoCAD File", ImportAutoCADFile);

                explodeImportInstance();
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

        private void ImportAutoCADFile(int id)
        {
            if (uidoc.Document != null)
            {
                preCurveElementIds.Clear();
                FilteredElementCollector preElementDocFilter = new FilteredElementCollector(doc);
                if (preElementDocFilter != null)
                {
                    FilteredElementIdIterator preElementsIdsIterator = preElementDocFilter.OfClass(typeof(CurveElement)).GetElementIdIterator();
                    while (preElementsIdsIterator.MoveNext())
                    {
                        preCurveElementIds.Add(preElementsIdsIterator.Current);
                    }
                }

                preTextNotesIds.Clear();
                FilteredElementCollector preTextNoteDocFilter = new FilteredElementCollector(doc);
                if (preTextNoteDocFilter != null)
                {
                    FilteredElementIdIterator preElementsIdsIterator = preTextNoteDocFilter.OfClass(typeof(TextNote)).GetElementIdIterator();
                    while (preElementsIdsIterator.MoveNext())
                    {
                        preTextNotesIds.Add(preElementsIdsIterator.Current);
                    }
                }

                preTextNoteTypesIds.Clear();
                FilteredElementCollector preTextNoteTypeDocFilter = new FilteredElementCollector(doc);
                if (preTextNoteTypeDocFilter != null)
                {
                    FilteredElementIdIterator preElementsIdsIterator = preTextNoteTypeDocFilter.OfClass(typeof(TextNoteType)).GetElementIdIterator();
                    while (preElementsIdsIterator.MoveNext())
                    {
                        preTextNoteTypesIds.Add(preElementsIdsIterator.Current);

                        TextNoteType preTextNodeType = doc.GetElement(preElementsIdsIterator.Current) as TextNoteType;
                        if (preTextNodeType != null)
                        {
                            this.revitTextNoteTypesDict.Add(preTextNodeType.Name, preTextNodeType);
                        }
                    }
                }

                Category preLinesStyles = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
                foreach (Category preLineCategory in preLinesStyles.SubCategories)
                {
                    preLineCategoriesIds.Add(preLineCategory.Id);
                    this.revitCategoriesDict.Add(preLineCategory.Name, preLineCategory);
                }


                preGraphycStyleIds.Clear();
                FilteredElementCollector preGraphycsStyleDocFilter = new FilteredElementCollector(doc);
                if (preGraphycsStyleDocFilter != null)
                {
                    FilteredElementIdIterator preElementsIdsIterator = preGraphycsStyleDocFilter.OfClass(typeof(GraphicsStyle)).GetElementIdIterator();
                    while (preElementsIdsIterator.MoveNext())
                    {
                        GraphicsStyle preGraphicsStyle = doc.GetElement(preElementsIdsIterator.Current) as GraphicsStyle;
                        if (preGraphicsStyle != null)
                        {
                            if (preLineCategoriesIds.Contains(preGraphicsStyle.GraphicsStyleCategory.Id))
                            {
                                preGraphycStyleIds.Add(preElementsIdsIterator.Current);

                                revitLineStylesDict.Add(preGraphicsStyle.Name, preGraphicsStyle);
                            }
                        }
                    }
                }

                #region
                deleteImportInstances();
                DWGImportOptions importOptions = new DWGImportOptions();
                importOptions.OrientToView = true;
                importOptions.Placement = ImportPlacement.Origin;
                doc.Import(fileName, importOptions, importView, out importInstanceId);
                if (importInstanceId != null)
                {
                    explodeImportInstance();
                }
                uidoc.RefreshActiveView();


                curCurveElementIds.Clear();
                FilteredElementCollector curElementDocFilter = new FilteredElementCollector(doc);
                if (curElementDocFilter != null)
                {
                    FilteredElementIdIterator curElementsIdsIterator = curElementDocFilter.OfClass(typeof(CurveElement)).GetElementIdIterator();
                    while (curElementsIdsIterator.MoveNext())
                    {
                        curCurveElementIds.Add(curElementsIdsIterator.Current);
                    }
                }

                curTextNotesIds.Clear();
                FilteredElementCollector curDocFilter = new FilteredElementCollector(doc);
                if (curDocFilter != null)
                {
                    FilteredElementIdIterator curElementsIdsIterator = curDocFilter.OfClass(typeof(TextNote)).GetElementIdIterator();
                    while (curElementsIdsIterator.MoveNext())
                    {
                        curTextNotesIds.Add(curElementsIdsIterator.Current);
                    }
                }

                curTextNoteTypesIds.Clear();
                FilteredElementCollector curTextNoteTypeDocFilter = new FilteredElementCollector(doc);
                if (curTextNoteTypeDocFilter != null)
                {
                    FilteredElementIdIterator curElementsIdsIterator = curTextNoteTypeDocFilter.OfClass(typeof(TextNoteType)).GetElementIdIterator();
                    while (curElementsIdsIterator.MoveNext())
                    {
                        curTextNoteTypesIds.Add(curElementsIdsIterator.Current);
                    }
                }

                Category curLinesStyles = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
                foreach (Category curLineCategory in curLinesStyles.SubCategories)
                {
                    curLineCategoriesIds.Add(curLineCategory.Id);
                }


                curGraphycStyleIds.Clear();
                FilteredElementCollector curGraphycsStyleDocFilter = new FilteredElementCollector(doc);
                if (curGraphycsStyleDocFilter != null)
                {
                    FilteredElementIdIterator curElementsIdsIterator = curGraphycsStyleDocFilter.OfClass(typeof(GraphicsStyle)).GetElementIdIterator();
                    while (curElementsIdsIterator.MoveNext())
                    {
                        GraphicsStyle curGraphicsStyle = doc.GetElement(curElementsIdsIterator.Current) as GraphicsStyle;
                        if (curGraphicsStyle != null)
                        {
                            if (curLineCategoriesIds.Contains(curGraphicsStyle.GraphicsStyleCategory.Id))
                            {
                                curGraphycStyleIds.Add(curElementsIdsIterator.Current);
                            }
                        }
                    }
                }
                #endregion

                difCurveElementIds.Clear();
                difCurveElementIds.UnionWith(curCurveElementIds);
                difCurveElementIds.ExceptWith(preCurveElementIds);

                difTextNotesIds.Clear();
                difTextNotesIds.UnionWith(curTextNotesIds);
                difTextNotesIds.ExceptWith(preTextNotesIds);

                difTextNoteTypesIds.Clear();
                difTextNoteTypesIds.UnionWith(curTextNoteTypesIds);
                difTextNoteTypesIds.ExceptWith(preTextNoteTypesIds);

                difLineCategoriesIds.Clear();
                difLineCategoriesIds.UnionWith(curLineCategoriesIds);
                difLineCategoriesIds.ExceptWith(preLineCategoriesIds);

                difGraphycStyleIds.Clear();
                difGraphycStyleIds.UnionWith(curGraphycStyleIds);
                difGraphycStyleIds.ExceptWith(preGraphycStyleIds);


                foreach (Category curLineCategory in curLinesStyles.SubCategories)
                {
                    if (difLineCategoriesIds.Contains(curLineCategory.Id))
                    {
                        Color curColor = curLineCategory.LineColor;
                        Tuple<byte, byte, byte> curColorTuple = new Tuple<byte, byte, byte>(curColor.Red, curColor.Green, curColor.Blue);

                        if (autocadColorLineStylesDict.ContainsKey(curColorTuple))
                        {
                            autocadColorLineStylesDict[curColorTuple].Add(curLineCategory);
                        }
                        else
                        {
                            List<Category> norList = new List<Category>();
                            norList.Add(curLineCategory);
                            autocadColorLineStylesDict.Add(curColorTuple, norList);
                        }
                    }
                }

                #region
                Autodesk.Revit.DB.Options geoOptions = uiapp.Application.Create.NewGeometryOptions();
                geoOptions.ComputeReferences = true;
                FilteredElementCollector curElementsDocFilter = new FilteredElementCollector(doc);
                if (curElementsDocFilter != null)
                {
                    FilteredElementIdIterator curElementsIdsIterator = curElementsDocFilter.WhereElementIsNotElementType().GetElementIdIterator();
                    while (curElementsIdsIterator.MoveNext())
                    {
                        ElementId curElementId = curElementsIdsIterator.Current;
                        if (curElementId != null)
                        {
                            Element curElement = doc.GetElement(curElementId);
                            if (curElement != null)
                            {
                                GeometryElement curGeometryElement = curElement.get_Geometry(geoOptions);
                                if (curGeometryElement != null)
                                {
                                    foreach (GeometryObject curGeometryObject in curGeometryElement)
                                    {
                                        ElementId curGraphicsStyleId = curGeometryObject.GraphicsStyleId;
                                        if (curGraphicsStyleId != null)
                                        {
                                            GraphicsStyle curGraphicsStyle = doc.GetElement(curGraphicsStyleId) as GraphicsStyle;
                                            if (curGraphicsStyle != null)
                                            {
                                                string curGraphicsStyleName = curGraphicsStyle.Name;

                                                Category curGraphicsStyleCategory = curGraphicsStyle.GraphicsStyleCategory;
                                                if (curGraphicsStyleCategory != null)
                                                {
                                                    string curGraphicsStyleCategoryName = curGraphicsStyleCategory.Name;

                                                    if (this.curGraphicsStylesNamesCount.ContainsKey(curGraphicsStyleCategoryName))
                                                    {
                                                        this.curGraphicsStylesNamesCount[curGraphicsStyleCategoryName]++;
                                                    }
                                                    else
                                                    {
                                                        this.curGraphicsStylesNamesCount.Add(curGraphicsStyleCategoryName, 1);
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
                #endregion


                CadDetailConverterOutputForm outputForm = new CadDetailConverterOutputForm(this);
                outputForm.ShowDialog();

                uiapp.Idling += uiapp_Idling;
            }
        }

        private void uiapp_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            uiapp.Idling -= uiapp_Idling;

            CadDetailConverterHandlerFinal finalHandler = new CadDetailConverterHandlerFinal(this);
            ExternalEvent exEvent = ExternalEvent.Create(finalHandler);
            exEvent.Raise();

        }

        public List<string> getRevitTextStylesNames()
        {
            List<string> revitTextStylesNames = new List<string>();

            foreach (KeyValuePair<string, TextNoteType> keyValuePair in revitTextNoteTypesDict)
            {
                revitTextStylesNames.Add(keyValuePair.Key);
            }

            revitTextStylesNames.Sort();

            return revitTextStylesNames;
        }

        public List<string> getRevitLineStylesNames()
        {
            List<string> revitLineStylesNames = new List<string>();

            foreach (KeyValuePair<string, Category> keyValuePair in revitCategoriesDict)
            {
                revitLineStylesNames.Add(keyValuePair.Key);
            }

            revitLineStylesNames.Sort();

            return revitLineStylesNames;
        }

        public Dictionary<ElementId, List<ElementId>> getElementsIdsByStyleType()
        {
            Dictionary<ElementId, List<ElementId>> elemToStyleIds = new Dictionary<ElementId, List<ElementId>>();

            Autodesk.Revit.DB.Options geoOptions = uiapp.Application.Create.NewGeometryOptions();
            geoOptions.ComputeReferences = true;
            FilteredElementCollector curElementsDocFilter = new FilteredElementCollector(doc);
            if (curElementsDocFilter != null)
            {
                FilteredElementIdIterator curElementsIdsIterator = curElementsDocFilter.WhereElementIsNotElementType().GetElementIdIterator();
                while (curElementsIdsIterator.MoveNext())
                {
                    ElementId curElementId = curElementsIdsIterator.Current;
                    if (curElementId != null)
                    {
                        Element curElement = doc.GetElement(curElementId);
                        if (curElement != null)
                        {
                            GeometryElement curGeomElement = curElement.get_Geometry(geoOptions);
                            ElementId curGraphicsStyleId = curGeomElement.GraphicsStyleId;
                            if(elemToStyleIds.ContainsKey(curGraphicsStyleId))
                            {
                                elemToStyleIds[curGraphicsStyleId].Add(curElementId);
                            }
                            else
                            {
                                List<ElementId> elemsList = new List<ElementId>();
                                elemsList.Add(curElementId);
                                elemToStyleIds.Add(curGraphicsStyleId, elemsList);
                            }
                        }
                    }
                }
            }

            return elemToStyleIds;
        }


        public List<Tuple<byte, byte, byte>> getAutocadColorsTuples()
        {
            List<Tuple<byte, byte, byte>> autocadColorsTuples = new List<Tuple<byte, byte, byte>>();

            foreach (KeyValuePair<Tuple<byte, byte, byte>, List<Category>> keyValuePair in autocadColorLineStylesDict)
            {
                autocadColorsTuples.Add(keyValuePair.Key);
            }

            return autocadColorsTuples;
        }

        public void setSelectedtRevitTextStyleName(string textStyleName)
        {
            if (revitTextNoteTypesDict.ContainsKey(textStyleName))
            {
                this.requestData.selectedTextStyleName = revitTextNoteTypesDict[textStyleName].Name;
            }
            else
            {
                this.requestData.selectedTextStyleName = null;
            }
        }

        private void deleteImportInstances()
        {
            FilteredElementCollector docFilter = new FilteredElementCollector(doc);

            if (docFilter != null)
            {
                FilteredElementIterator importInstancesIterator = docFilter.OfClass(typeof(Autodesk.Revit.DB.ImportInstance)).GetElementIterator();
                List<ElementId> allDeleteIds = new List<ElementId>();
                while (importInstancesIterator.MoveNext())
                {
                    Autodesk.Revit.DB.ImportInstance curImportInstance = importInstancesIterator.Current as Autodesk.Revit.DB.ImportInstance;

                    if (curImportInstance != null)
                    {
                        allDeleteIds.Add(curImportInstance.Id);
                    }
                }

                doc.Delete(allDeleteIds);

                uidoc.RefreshActiveView();
                doc = uiapp.ActiveUIDocument.Document;
            }
        }

        private void explodeImportInstance()
        {
            if (importInstanceId != null)
            {
                List<ElementId> importInstancesCollection = new List<ElementId>();
                importInstancesCollection.Add(importInstanceId);
                uiapp.ActiveUIDocument.Selection.SetElementIds(importInstancesCollection);

                SendKeys.SendWait("^(k)");
            }
        }
    }
}
