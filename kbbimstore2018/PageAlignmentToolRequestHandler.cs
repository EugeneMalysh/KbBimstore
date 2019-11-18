using System;
using System.Text;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{

    public class PageAlignmentToolRequestHandler : IExternalEventHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        XYZ norXYZ;
        ElementId deltaViewportId;
        List<ElementId> viewportsIds;

        public PageAlignmentToolRequestHandler(XYZ norXYZ, ElementId deltaViewportId, List<ElementId> viewportsIds)
        {
            this.norXYZ = norXYZ;
            this.deltaViewportId = deltaViewportId;
            this.viewportsIds = viewportsIds;
            

            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "PageAlignmentToolRequestHandlerInit";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.curCommandId = RevitCommandId.LookupPostableCommandId(PostableCommand.DesignOptions);

            ModifyScene(uiapp, "Page Alignment Tool", PageAlignmentTool);
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

        private void PageAlignmentTool(int id)
        {
            List<string> viewsNames = new List<string>();

            foreach (ElementId curViewportId in this.viewportsIds)
            {
                if (curViewportId != this.deltaViewportId)
                {
                    Viewport curViewport = doc.GetElement(curViewportId) as Viewport;
                    if (curViewport != null)
                    {
                        View curView = doc.GetElement(curViewport.ViewId) as View;
                        if (curView != null)
                        {
                            if (curView is ViewPlan)
                            {
                                //XYZ curXYZ = curViewport.GetBoxCenter();
                                //XYZ norXYZ = new XYZ((curXYZ.X + deltaXYZ.X), (curXYZ.Y + deltaXYZ.Y), (curXYZ.Z + deltaXYZ.Z));
                                curViewport.SetBoxCenter(norXYZ);

                                viewsNames.Add(curView.Name);
                            }
                        }                                          
                    }
                }
            }

            if (viewsNames.Count > 0)
            {
                StringBuilder strBld = new StringBuilder();
                strBld.Append("Were aligned following views: ");
                strBld.Append(viewsNames[0]);

                for(int i = 1; i < viewsNames.Count; i++)
                {
                    strBld.Append(", " + viewsNames[i]);
                }

                TaskDialog.Show("Info", strBld.ToString());
            }
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
            TaskDialog.Show("Info", "Views were aligned");
        }
    }
}
