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

    public class CadDetailConverterHandlerFinal : IExternalEventHandler
    {
        private Document doc = null;
        private UIDocument uidoc = null;
        private UIApplication uiapp = null;
        private CadDetailConverterHandler parentHandler = null;

        private delegate void MyOperation(int id);

        public CadDetailConverterHandlerFinal(CadDetailConverterHandler parentHandler)
        {
            this.parentHandler = parentHandler;
        }

        public String GetName()
        {
            return "ImportAutoCADHandlerFinal";
        }

        public void Execute(UIApplication uiapp)
        {
            try
            {
                this.uiapp = uiapp;
                this.uidoc = uiapp.ActiveUIDocument;
                this.doc = uidoc.Document;

                ModifyScene(uiapp, "Import AutoCAD File", ImportAutoCADFileFinal);
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

        private void ImportAutoCADFileFinal(int id)
        {
            if (this.parentHandler.requestData != null)
            {
                TextNoteType selectedTextNoteType = null;
                foreach (ElementId preTextNoteTypeId in this.parentHandler.preTextNoteTypesIds)
                {
                    TextNoteType preTextNoteType = doc.GetElement(preTextNoteTypeId) as TextNoteType;
                    if (preTextNoteType != null)
                    {
                        if (preTextNoteType.Name == this.parentHandler.requestData.selectedTextStyleName)
                        {
                            selectedTextNoteType = preTextNoteType;
                        }
                    }
                }

                int convertedTextNotesNumber = 0;
                if (selectedTextNoteType != null)
                {
                    foreach (ElementId difTextNoteId in this.parentHandler.difTextNotesIds)
                    {
                        try
                        {
                            TextNote difTextNote = doc.GetElement(difTextNoteId) as TextNote;
                            if (difTextNote != null)
                            {
                                difTextNote.TextNoteType = selectedTextNoteType;
                                convertedTextNotesNumber++;
                            }
                        }
                        catch (Exception ex)
                        {
                            TaskDialog.Show("Exception", ex.Message);
                        }
                    }
                }


                Dictionary<Tuple<byte, byte, byte>, GraphicsStyle> colorToGraphicsStyle = new Dictionary<Tuple<byte, byte, byte>, GraphicsStyle>();
                foreach (KeyValuePair<Tuple<byte, byte, byte>, string> keyValuePair in this.parentHandler.requestData.colorsToLineStyles)
                {
                    string revitStyleName = keyValuePair.Value;
                    if (this.parentHandler.revitLineStylesDict.ContainsKey(revitStyleName))
                    {
                        GraphicsStyle revitGraphicsStyle = this.parentHandler.revitLineStylesDict[revitStyleName];
                        colorToGraphicsStyle.Add(keyValuePair.Key, revitGraphicsStyle);
                    }
                }


                Dictionary<Tuple<byte, byte, byte>, int> lineStylesDict = new Dictionary<Tuple<byte, byte, byte>, int>();
                foreach (ElementId difElementId in parentHandler.difCurveElementIds)
                {
                    CurveElement difCurveElement = doc.GetElement(difElementId) as CurveElement;
                    if (difCurveElement != null)
                    {
                        GraphicsStyle difLineStyle = difCurveElement.LineStyle as GraphicsStyle;
                        if (difLineStyle != null)
                        {
                            Category difLineStyleCategory = difLineStyle.GraphicsStyleCategory;
                            if (difLineStyleCategory != null)
                            {
                                Color difLineStyleColor = difLineStyleCategory.LineColor;
                                if (difLineStyleColor != null)
                                {
                                    Tuple<byte, byte, byte> difLineStyleColorTuple = new Tuple<byte, byte, byte>(difLineStyleColor.Red, difLineStyleColor.Green, difLineStyleColor.Blue);
                                    if (colorToGraphicsStyle.ContainsKey(difLineStyleColorTuple))
                                    {
                                        GraphicsStyle norGraphicsStyle = colorToGraphicsStyle[difLineStyleColorTuple];
                                        difCurveElement.LineStyle = norGraphicsStyle;
                                    }
                                }
                            }
                        }
                    }
                }

                doc.Delete(this.parentHandler.difTextNoteTypesIds);
                doc.Delete(this.parentHandler.difLineCategoriesIds);

                doc.Regenerate();
                uiapp.ActiveUIDocument.RefreshActiveView();

                TaskDialog.Show("Info", "Finished AutoCAD Details Conversion");

            }  
        }

    }
}
