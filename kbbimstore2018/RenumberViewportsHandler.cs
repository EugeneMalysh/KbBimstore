using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{

    public class RenumberViewportsHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;

        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        AddInCommandBinding curCommandBinding;

        private List<Tuple<ElementId, int>> viewportIdsAndNumbers = new List<Tuple<ElementId, int>>();


        public RenumberViewportsHandler(List<Tuple<ElementId, int>> viewportIdsAndNumbers)
        {
            this.viewportIdsAndNumbers = viewportIdsAndNumbers;
            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "RenumberViewportsHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;

            ModifyScene(uiapp, "Renumber Viewports Handler");
        }

        private void ModifyScene(UIApplication uiapp, String text)
        {
            uidoc = uiapp.ActiveUIDocument;

            if (uidoc != null)
            {
                doc = uidoc.Document;

                using (Transaction trans = new Transaction(doc))
                {
                    trans.Start(text);
                    RenumberViewports(1);

                    trans.Commit();
                    trans.Dispose();
                }
            }
        }

        private void RenumberViewports(int id)
        {

            if (this.viewportIdsAndNumbers.Count > 0)
            {

                int startNumber = this.viewportIdsAndNumbers[0].Item2;
                for (int i = 0; i < this.viewportIdsAndNumbers.Count; i++)
                {
                    try
                    {
                        ElementId curElementId = this.viewportIdsAndNumbers[i].Item1;
                        Element curElement = this.doc.GetElement(curElementId);
                        if (curElement != null)
                        {
                            int norNumber = 1000000 + startNumber + i;

                            Parameter curParam = curElement.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);


                            if (curParam.StorageType == StorageType.Integer)
                            {
                                curParam.Set(norNumber);
                            }
                            else if (curParam.StorageType == StorageType.Double)
                            {
                                curParam.Set((double)norNumber);
                            }
                            else if (curParam.StorageType == StorageType.String)
                            {
                                curParam.Set(norNumber.ToString());
                                curParam.SetValueString(norNumber.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }

                for (int i = 0; i < this.viewportIdsAndNumbers.Count; i++)
                {
                    try
                    {
                        ElementId curElementId = this.viewportIdsAndNumbers[i].Item1;
                        Element curElement = this.doc.GetElement(curElementId);
                        if (curElement != null)
                        {
                            int norNumber = startNumber + i;

                            Parameter curParam = curElement.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);


                            if (curParam.StorageType == StorageType.Integer)
                            {
                                curParam.Set(norNumber);
                            }
                            else if (curParam.StorageType == StorageType.Double)
                            {
                                curParam.Set((double)norNumber);
                            }
                            else if (curParam.StorageType == StorageType.String)
                            {
                                curParam.Set(norNumber.ToString());
                                curParam.SetValueString(norNumber.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }

            try
            {
                this.doc.Regenerate();
                this.uidoc.RefreshActiveView();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
        }
    }
}
