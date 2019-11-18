
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace KbRevitstore
{
    [Transaction(TransactionMode.Automatic)]
    [Regeneration(RegenerationOption.Manual)]
    class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }
    }
}
