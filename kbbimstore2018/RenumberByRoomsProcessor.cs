using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    class RenumberByRoomsProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;

        private List<int> selectedNumbers = new List<int>();               
        private List<Parameter> selectedParameters = new List<Parameter>();
        private List<ElementId> selectedElementsIds = new List<ElementId>(); 

        public RenumberByRoomsProcessor(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = uiapp.ActiveUIDocument;
            this.doc = uidoc.Document;
        }

        public void init()
        {
            selectedElementsIds.Clear();
            selectedParameters.Clear();

            if (this.uiapp != null)
            {              
                try
                {

                    while (true)
                    {
                        Reference selectedReference = uidoc.Selection.PickObject(ObjectType.Element, "Select rooms in order to be renumbered. Press ESC key when finished.");
                        if (selectedReference != null)
                        {
                            ElementId selectedElementId = selectedReference.ElementId;
                            if (selectedElementId != null)
                            {
                                if (selectedElementsIds.Contains(selectedElementId))
                                {
                                    TaskDialog.Show("Info", "You selected ducplicated element, it will not be counted.");
                                }
                                else
                                {
                                    Element selectedElement = this.doc.GetElement(selectedElementId);
                                    if (selectedElement != null)
                                    {
                                        if (selectedElement.GetType().Name == "Room")
                                        {
                                            selectedElementsIds.Add(selectedElementId);

                                            Parameter numberParam = getNumberParameter(selectedElement);
                                            if (numberParam != null)
                                            {                                                                                                
                                                int number = 1 + selectedNumbers.Count;
                                                try
                                                {
                                                    number = Convert.ToInt32(numberParam.AsString());
                                                }
                                                catch (Exception ex)
                                                {
                                                    number = 1 + selectedNumbers.Count;
                                                }

                                                selectedParameters.Add(numberParam);
                                                selectedNumbers.Add(number);
                                            }
                                        }
                                        else
                                        {
                                            TaskDialog.Show("Info", "Your selection is not a room, it will not be counted.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {                    
                }

                List<Tuple<Parameter, int>> parametersNewValues = new List<Tuple<Parameter, int>>();
                for (int i = 0; i < selectedParameters.Count; i++)
                {
                    parametersNewValues.Add(new Tuple<Parameter,int>(selectedParameters[i], selectedNumbers[i]));
                }
                
                RenumberByRoomsHandler handler = new RenumberByRoomsHandler(parametersNewValues);
                handler.Execute(uiapp);
            }
        }

        private Parameter getNumberParameter(Element elem)
        {            
            Parameter param = null;

            if (elem != null)
            {
                string paramName = "";

                if (elem is Room)
                {
                    paramName = "Number";
                }
                else if (elem is FamilyInstance)
                {
                    paramName = "Mark";
                }

                foreach (Parameter curParam in elem.Parameters)
                {
                    if (curParam.Definition.Name == paramName)
                    {
                        param = curParam;
                        break;
                    }
                }
            }

            return param;
        }

        private void setParameterToValue(Parameter p, int i)
        {
            if (p.StorageType == StorageType.Integer)
                p.Set(i);
            else if (p.StorageType == StorageType.String)
                p.Set(i.ToString());
        }
    }
}
