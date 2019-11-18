using System;
using System.Linq;
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

    public class RemoveDwgImportsHandler : IExternalEventHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        HashSet<ElementId> importInstancesCategoryIds = new HashSet<ElementId>();

        public RemoveDwgImportsHandler()
        {

        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "RemoveDwgImportsHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.doc = this.uiapp.ActiveUIDocument.Document;

            ModifyScene(uiapp, "Remove Dwg Imports Handler", RemoveDwgImports);
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

        private void RemoveDwgImports(int id)
        {
            try
            {
                this.myRequest = new KbBimstoreRequest();

                int importInstNum = 0;
                IEnumerable<ImportInstance> unlinkedImportInstances = new FilteredElementCollector(doc).OfClass(typeof(ImportInstance)).Cast<ImportInstance>().Where(i => i.IsLinked == false);
                foreach (ImportInstance curImportInstance in unlinkedImportInstances)
                {
                    this.importInstancesCategoryIds.Add(curImportInstance.Category.Id);
                    importInstNum++;
                }

                if (importInstNum > 0)
                {
                    doc.Delete(this.importInstancesCategoryIds);

                    TaskDialog.Show("Info", "We have removed " + importInstNum.ToString() + " unlinked .dwg imports");
                }
                else
                {
                    TaskDialog.Show("Info", "There is no unlinked .dwg import");
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
            TaskDialog.Show("Info", "Dwg Imports were removed");
        }

    }
}
