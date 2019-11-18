
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace KbBimstore
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class TabToolbarRenamerCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            using (TabToolbarRenamerForm form = new TabToolbarRenamerForm())
            {
                DialogResult result = form.ShowDialog();

                //Saves the final settings as an xml file
                KbBimstoreApp.MainTab.Save();
            }

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ToolbarManagerCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            using (ToolbarManager.Forms.ToolbarManagerForm form = new ToolbarManager.Forms.ToolbarManagerForm())
            {
                DialogResult result = form.ShowDialog();
            }

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class CreateProject : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            CreateNewProjectForm form = new CreateNewProjectForm(commandData.Application);
            DialogResult result = form.ShowDialog();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class AddNewViewSheets : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AddNewViewSheetsForm form = new AddNewViewSheetsForm(commandData.Application);
            DialogResult result = form.ShowDialog();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class DesignOptions : IExternalCommand
    {
        bool allowToCreate = true;
        Autodesk.Revit.UI.UIApplication uiapp;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiapp = commandData.Application;

            IEnumerable<DesignOption> designOptions = new FilteredElementCollector(uiapp.ActiveUIDocument.Document).OfClass(typeof(DesignOption)).Cast<DesignOption>();

            if (designOptions.ToList<DesignOption>().Count > 0)
            {
                DesignOptionsForm form = new DesignOptionsForm(uiapp);
                DialogResult result = form.ShowDialog();
            }
            else
            {
                uiapp.Idling += Application_Idling;
                createNewDesignOptions();
            }

            return Result.Succeeded;
        }

        private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            if (allowToCreate)
            {
                allowToCreate = false;
                IEnumerable<DesignOption> designOptions = new FilteredElementCollector(uiapp.ActiveUIDocument.Document).OfClass(typeof(DesignOption)).Cast<DesignOption>();

                if (designOptions.ToList<DesignOption>().Count > 0)
                {
                    DesignOptionsForm form = new DesignOptionsForm(uiapp);
                    DialogResult result = form.ShowDialog();
                }
                else
                {
                    createNewDesignOptions();
                }
            }
        }

        private void createNewDesignOptions()
        {
            string mesStr = "There are no existing design options, do you want to create?";
            DialogResult result = MessageBox.Show(mesStr, "Create New Design Options", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                allowToCreate = true;
                RevitCommandId commandId = RevitCommandId.LookupPostableCommandId(PostableCommand.DesignOptions);
                uiapp.PostCommand(commandId);
            }
            else
            {
                allowToCreate = false;
                uiapp.Idling -= Application_Idling;
            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class PageAlignmentTool : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            PageAlignmentToolProcessor processor = new PageAlignmentToolProcessor(commandData.Application);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ImportDetailsSettings : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsSettingsForm form = new AllDetailsImportsSettingsForm(commandData.Application);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class BaseAndTransitionDetails : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsForm form = new AllDetailsImportsForm(commandData.Application, "BaseAndTransitionDetails");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class CeilingDetails : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsForm form = new AllDetailsImportsForm(commandData.Application, "CeilingDetails");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class DoorAndWindowDetails : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsForm form = new AllDetailsImportsForm(commandData.Application, "DoorAndWindowDetails");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class Millwork : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsForm form = new AllDetailsImportsForm(commandData.Application, "Millwork");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class Partition : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AllDetailsImportsForm form = new AllDetailsImportsForm(commandData.Application, "Partition");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class CadDetailConverter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            CadDetailConverterSelectForm form = new CadDetailConverterSelectForm(commandData.Application);
            DialogResult result = form.ShowDialog();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RenumberByRooms : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RenumberByRoomsProcessor proc = new RenumberByRoomsProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RenumberByDoors : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RenumberByDoorsProcessor proc = new RenumberByDoorsProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RenumberViewPorts : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RenumberViewportsProcessor proc = new RenumberViewportsProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class AutomaticDoorRenumber : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AutomaticDoorRenumberProcessor proc = new AutomaticDoorRenumberProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class AutomaticDoorRenumberToFrom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AutomaticDoorRenumberToFromProcessor proc = new AutomaticDoorRenumberToFromProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RevitCityLink : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            DockablePaneId webBrowserId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            DockablePane webBrowserPane = commandData.Application.GetDockablePane(webBrowserId);
            if (webBrowserPane != null)
            {
                webBrowserPane.Show();
                KbBimstoreApp.WebBrowserDockableWindow.ShowLink("http://www.revitcity.com");
            }
             */
            System.Diagnostics.Process.Start("http://www.revitcity.com/downloads.php?action=search&keywords=chair&user_name=&sort=score");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class GoogleSearch : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            DockablePaneId webBrowserId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            DockablePane webBrowserPane = commandData.Application.GetDockablePane(webBrowserId);
            if (webBrowserPane != null)
            {
                webBrowserPane.Show();
                KbBimstoreApp.WebBrowserDockableWindow.ShowLink("https://www.google.com");
            }
             */
            System.Diagnostics.Process.Start("https://www.google.com");

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class KBRenderWebsite : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            DockablePaneId webBrowserId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            DockablePane webBrowserPane = commandData.Application.GetDockablePane(webBrowserId);
            if(webBrowserPane != null)
            {
                webBrowserPane.Show();
                KbBimstoreApp.WebBrowserDockableWindow.ShowLink("http://www.kb-bimstore.com");                
            }
             */
            System.Diagnostics.Process.Start("http://www.kb-bimstore.com");
            return Result.Succeeded;

        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class KBTutorialWebsite : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            DockablePaneId webBrowserId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            DockablePane webBrowserPane = commandData.Application.GetDockablePane(webBrowserId);
            if(webBrowserPane != null)
            {
                webBrowserPane.Show();
                KbBimstoreApp.WebBrowserDockableWindow.ShowLink("https://kb-videoaccess.vids.io/");                
            }
             */
            System.Diagnostics.Process.Start("https://kb-videoaccess.vids.io/");
            return Result.Succeeded;
        }



    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ToolPalettes : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData.Application);

            return Result.Succeeded;
        }

        public static void Init(UIApplication uiApp)
        {
            DockablePane toolPalettePane = null;
            DockablePaneId toolPaletteId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21258}"));

            try
            {
                toolPalettePane = uiApp.GetDockablePane(toolPaletteId);
            }
            catch (Exception ex)
            {
                toolPalettePane = null;
            }


            if (toolPalettePane == null)
            {
                KbBimstoreApp.CreateToolPalette(KbBimstoreApp.activeUiContApp);
            }

            try
            {
                toolPalettePane = uiApp.GetDockablePane(toolPaletteId);
            }
            catch (Exception ex)
            {
                toolPalettePane = null;
            }


            if (toolPalettePane != null)
            {
                try
                {
                    KbBimstoreApp.ToolPaletUI.init(uiApp);
                    toolPalettePane.Show();
                }
                catch (Exception ex)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Exception", ex.Message);
                }

            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeLeft : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Left);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeRight : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Right);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeTop : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Top);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeBottom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Bottom);

            return Result.Succeeded;
        }
    }


    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeBigger : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Bigger);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeSmaller : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Smaller);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeLoad : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Load);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class WindowTileSizeSave : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            KbBimstoreApp.TileSizesProcessor.init(commandData.Application, TileAction.Save);

            return Result.Succeeded;
        }
    }


    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class SetDoorOffset : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SetDoorOffsetProcessor proc = new SetDoorOffsetProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RemoveDwgImports : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RemoveDwgImportsHandler handler = new RemoveDwgImportsHandler();
            handler.Execute(commandData.Application);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ExportToExcel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ExportToExcelProcessor proc = new ExportToExcelProcessor(commandData.Application);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class AutoSynchronizeStart : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AutoSynchronizeProcessor proc = new AutoSynchronizeProcessor(true);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class AutoSynchronizeStop : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AutoSynchronizeProcessor proc = new AutoSynchronizeProcessor(false);
            proc.init();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class LicensingCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (KbBimstoreApp.myLicenseUpdater == null)
            {
                KbBimstoreApp.myLicenseUpdater = new KBRevitLicensing.LicenseUpdater();
            }

            KbBimstoreApp.myLicenseUpdater.UpdateLicense();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class RemoveDWGCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DWGFamilyRemover.App app = new DWGFamilyRemover.App();
            app.Run(commandData.Application.ActiveUIDocument.Document);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ScheduleSpellCheckCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Schedules.SpellingChecker.Run(commandData.View);

            return Result.Succeeded;
        }
    }
}
