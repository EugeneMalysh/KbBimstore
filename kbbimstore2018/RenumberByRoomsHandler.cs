﻿using System;
using System.Text;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using System.Windows;

namespace KbBimstore
{

    public class RenumberByRoomsHandler
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private delegate void MyOperation(int id);
        private KbBimstoreRequest myRequest;
        RevitCommandId curCommandId;
        AddInCommandBinding curCommandBinding;

        private List<Tuple<Parameter, int>> parametersNewValues;


        public RenumberByRoomsHandler(List<Tuple<Parameter, int>> parametersNewValues)
        {
            this.parametersNewValues = parametersNewValues;
            this.myRequest = new KbBimstoreRequest();
        }

        public KbBimstoreRequest Request
        {
            get { return myRequest; }
        }

        public String GetName()
        {
            return "RenumberByRoomsHandler";
        }

        public void Execute(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.curCommandId = RevitCommandId.LookupPostableCommandId(PostableCommand.DesignOptions);
            ModifyScene(uiapp, "Renumber By Rooms Handler");
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
                    RenumberByRooms(1);

                    trans.Commit();
                    trans.Dispose();
                }
            }
        }

        private void RenumberByRooms(int id)
        {
            for (int i = 0; i < parametersNewValues.Count; i++)
            {
                try
                {
                    Tuple<Parameter, int> paramNewValue = parametersNewValues[i];
                    int numb = parametersNewValues[0].Item2 + i;
                    paramNewValue.Item1.Set(numb.ToString());
                }
                catch (Exception ex)
                {
                }
            }

            doc.Regenerate();
        }

        private void curCommandBinding_Executed(object sender, ExecutedEventArgs e)
        {
            TaskDialog.Show("Info", "Rooms were renumbered");
        }
    }
}
