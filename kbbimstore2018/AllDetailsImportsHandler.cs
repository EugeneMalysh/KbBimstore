using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    class AllDetailsImportsHandler : IExternalEventHandler
    {
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest = new KbBimstoreRequest();

        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.DB.Document toDocument;
        private Autodesk.Revit.DB.Document fromDocument;
        private List<Autodesk.Revit.DB.View> fromSelectedViews;

        public AllDetailsImportsHandler(UIApplication uiapp, Document toDocument, Document fromDocument, List<View> fromSelectedViews)
        {
            this.uiapp = uiapp;
            this.toDocument = toDocument;
            this.fromDocument = fromDocument;
            this.fromSelectedViews = fromSelectedViews;
        }


        private List<View> GetAllViews(Document myDoc)
        {
            List<View> allViews = new List<View>();

            FilteredElementCollector viewFilter = new FilteredElementCollector(myDoc);
            if (viewFilter != null)
            {
                FilteredElementIterator viewsIterator = viewFilter.OfClass(typeof(Autodesk.Revit.DB.View)).GetElementIterator();

                while (viewsIterator.MoveNext())
                {
                    Autodesk.Revit.DB.View curView = viewsIterator.Current as Autodesk.Revit.DB.View;
                    if (curView != null)
                    {
                        allViews.Add(curView);
                    }
                }
            }

            return allViews;
        }


        private void UnhideAllElements(Document myDoc)
        {
            List<ElementId> allNonViewElementsIds = new List<ElementId>();

            FilteredElementCollector docFilter = new FilteredElementCollector(myDoc);
            if (docFilter != null)
            {
                FilteredElementIterator docIterator = docFilter.WhereElementIsNotElementType().GetElementIterator();

                while (docIterator.MoveNext())
                {
                    Element curElem = docIterator.Current;
                    if (!(curElem is Autodesk.Revit.DB.View))
                    {
                        allNonViewElementsIds.Add(curElem.Id);
                    }
                }
            }

            List<View> allViews = GetAllViews(myDoc);
            foreach (View curView in allViews)
            {
                curView.UnhideElements(allNonViewElementsIds);
            }
        }

        List<ElementId> filterElementsIdsByView(Document myDoc, View myView)
        {
            List<ElementId> elemsIdsByViews = new List<ElementId>();

            if (myDoc != null)
            {
                FilteredElementCollector docFilter = new FilteredElementCollector(myDoc, myView.Id);
                if (docFilter != null)
                {
                    docFilter.WherePasses(new ElementCategoryFilter(ElementId.InvalidElementId, true));
                    ICollection<ElementId> elemsIds = docFilter.ToElementIds();
                    elemsIdsByViews.AddRange(elemsIds);
                }
            }

            return elemsIdsByViews;
        }

        Dictionary<ElementId, List<ElementId>> elementsIdsByViews(Document myDoc, List<View> myViews)
        {
            Dictionary<ElementId, List<ElementId>> elemsIdsByViews = new Dictionary<ElementId, List<ElementId>>();

            foreach (View myView in myViews)
            {
                if (myDoc != null)
                {
                    FilteredElementCollector docFilter = new FilteredElementCollector(myDoc, myView.Id);
                    if (docFilter != null)
                    {
                        docFilter.WherePasses(new ElementCategoryFilter(ElementId.InvalidElementId, true));
                        ICollection<ElementId> elemsIds = docFilter.ToElementIds();
                        elemsIdsByViews.Add(myView.Id, new List<ElementId>(elemsIds));
                    }
                }
            }

            return elemsIdsByViews;
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "AllDetailsImports";
        }

        public void Execute(UIApplication uiapp)
        {

            ModifyScene(uiapp, "AllDetailsImports", AllDetailsImports);
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

        private void AllDetailsImports(int id)
        {
            CopyPasteOptions options = new CopyPasteOptions();
            options.SetDuplicateTypeNamesHandler(new HideAndAcceptDuplicateTypeNamesHandler());

            List<View> fromAllViews = GetAllViews(fromDocument);
            Dictionary<ElementId, List<ElementId>> selectedElemsIdsByViews = elementsIdsByViews(fromDocument, fromSelectedViews);

            foreach (KeyValuePair<ElementId, List<ElementId>> selectedKeyValuePairs in selectedElemsIdsByViews)
            {
                List<View> toAllViews = GetAllViews(toDocument);

                ElementId fromSelectedViewId = selectedKeyValuePairs.Key;
                List<ElementId> fromSelectedElementsIds = selectedKeyValuePairs.Value;

                List<ElementId> fromSelectedViewIdCol = new List<ElementId>();
                fromSelectedViewIdCol.Add(fromSelectedViewId);

                ICollection<ElementId> toSelectedViewIdCol = null;
                View toSelectedView = null;
                try
                {
                    toSelectedViewIdCol = ElementTransformUtils.CopyElements(fromDocument, fromSelectedViewIdCol, toDocument, Transform.Identity, options);
                    if (toSelectedViewIdCol.Count > 0)
                    {
                        toSelectedView = toDocument.GetElement(toSelectedViewIdCol.First()) as View;
                    }
                }
                catch (Exception ex)
                {
                    toSelectedViewIdCol = null;
                    toSelectedView = null;
                }


                View fromSelectedView = fromDocument.GetElement(fromSelectedViewId) as View;
                if ((fromSelectedView != null) && (toSelectedView != null))
                {
                    ICollection<ElementId> toSelectedElementsIdsCol = null;
                    try
                    {
                        toSelectedElementsIdsCol = ElementTransformUtils.CopyElements(fromSelectedView, fromSelectedElementsIds, toSelectedView, Transform.Identity, options);
                    }
                    catch (Exception ex)
                    {
                        toSelectedElementsIdsCol = null;
                    }
                }
            }

            TaskDialog.Show("Info", "Were imported " + selectedElemsIdsByViews.Count.ToString() + " views");
        }
    }

    class HideAndAcceptDuplicateTypeNamesHandler : IDuplicateTypeNamesHandler
    {
        public DuplicateTypeAction OnDuplicateTypeNamesFound(DuplicateTypeNamesHandlerArgs args)
        {
            return DuplicateTypeAction.UseDestinationTypes;
        }
    }
}
