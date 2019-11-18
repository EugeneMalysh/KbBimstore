using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{

    public class DesignOptionsRequestHandlerInit : IExternalEventHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;


        public DesignOptionsRequestHandlerInit()
        {
            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "DesignOptionsRequestHandlerInit";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.curCommandId = RevitCommandId.LookupPostableCommandId(PostableCommand.DesignOptions);
            
            ModifyScene(uiapp, "Create Design Options", CreateDesignOptions);
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
            StringBuilder strBld = new StringBuilder();

            FilteredElementCollector curColector = new FilteredElementCollector(doc).OfClass(typeof(DesignOption));
            FilteredElementIterator curIterator = curColector.GetElementIterator();

            while(curIterator.MoveNext())
            {
                Element curElement = curIterator.Current;
                if(curElement.Name.Contains("Option"))
                {
                    string curInfoStr = "";
                    curInfoStr += "Id=" + curElement.Id.ToString() + ", ";
                    curInfoStr += "Name=" + curElement.Name + ", ";

                    ParameterSet orderedParams = curElement.Parameters;
                    foreach (Parameter curParam in orderedParams)
                    {
                        if (curParam.Definition.Name == "Design Option Set Id")
                        {
                            curInfoStr += curParam.Definition.Name + "=" + curParam.AsElementId().ToString();

                            ElementId curDesignOptionSetId = curParam.AsElementId();
                            if (curDesignOptionSetId != null)
                            {
                                Element curDesignOptionSet = doc.GetElement(curDesignOptionSetId);

                                if (curDesignOptionSet != null)
                                {
                                    curInfoStr += ", Design Option Set=" + curDesignOptionSet.Name;
                                }
                            }
                            
                        }
                    }

                    strBld.AppendLine(curInfoStr);
                }
            }


            AlmMessageBox mesBox = new AlmMessageBox(strBld.ToString());
            mesBox.Show();
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
            TaskDialog.Show("Info", "Design options, views and sheets were created");
        }
    }
}
