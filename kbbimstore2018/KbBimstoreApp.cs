using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Windows;

using KbBimstore.KBRevitLicensing;

using DetailAddin_KB;

namespace KbBimstore
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class KbBimstoreApp : IExternalApplication
    {
        public static string TAB_NAME = "BIMeta";
        public DetailApplication detailApplication = null;

        public static UIControlledApplication activeUiContApp = null;
        public static Document activeDoc = null;
        public static Autodesk.Revit.DB.View activeView = null;

        public static string AddInPath = typeof(KbBimstoreApp).Assembly.Location;
        public static string DataFolderPath = Path.GetDirectoryName(AddInPath) + "\\data";
        public static string TempFolderPath = Path.GetDirectoryName(AddInPath) + "\\temp";
        public static string ImagesFolderPath = Path.GetDirectoryName(AddInPath) + "\\images";
        public static string TemplatesFolderPath = Path.GetDirectoryName(AddInPath) + "\\templates";
        public static string TileSettingsFilePath = Path.GetDirectoryName(KbBimstoreApp.AddInPath) + "\\data\\tilesettings.xml";
        public static string ImporSsettingsFilePath = Path.GetDirectoryName(KbBimstoreApp.AddInPath) + "\\data\\importsettings.xml";
        public static string MyKeyboardShortcutsFilePath = Path.GetDirectoryName(KbBimstoreApp.AddInPath) + "\\data\\KeyboardShortcuts.xml";
        public static string ToolbarManagerFilePath = Path.GetDirectoryName(KbBimstoreApp.AddInPath) + "\\toolbar\\data.xml";
        public static string AutoSyncSettingsFilePath = Path.GetDirectoryName(KbBimstoreApp.AddInPath) + "\\data\\timer_settings.xml";

        public static string TabToolbarRenamerSettingsFileName = "ttr.xml";
        public static string TabToolbarRenamerSettingsFilePath = Path.Combine(Path.GetDirectoryName(AddInPath), TabToolbarRenamerSettingsFileName);

        public static KbBimstoreTab MainTab = null;

        public static bool hasUserOpenedAutoSyncDialog = false;
        private static DateTime? documentOpenedTime = null;

        public static DateTime autosynchLastDateTime = DateTime.Now;
        public static DateTime autosynchPrevDateTime = DateTime.Now;

        public static DateTime autosaveLastDateTime = DateTime.Now;
        public static DateTime autosavePrevDateTime = DateTime.Now;

        public static bool autosynchIsActive = false;
        public static bool autoSaveIsActive = false;

        public static int autosynchInterval = 5;
        public static int autoSaveInterval = 5;
        public static Timer autosynchTimer = new Timer();

        private static int licenseState = -1;
        private static PushButton createProjectPushButton = null;
        private static PushButton addNewViewSheetstPushButton = null;
        private static PushButton designOptionsPushButton = null;
        private static PushButton pageAlignmentToolPushButton = null;
        private static PushButton licenseToolPushButton = null;
        private static PushButton baseAndTransitionDetailsPushlButton = null;
        private static PushButton doorAndWindowDetailsPushlButton = null;
        private static PushButton ceilingDetailsPushlButton = null;
        private static PushButton millworkPushlButton = null;
        private static PushButton partitionPushlButton = null;
        private static PushButton importDetailsSettingsPushlButton = null;
        private static PushButton cadDetailConverterPushButton = null;
        //private static PulldownButton ribbonPanelRenumberPullDown = null;
        private static PushButton renumberByRoomsPushButton = null;
        private static PushButton renumberByDoorsPushButton = null;
        private static PushButton renumberViewPortsPushButton = null;
        private static PushButton automaticDoorRenumberPushButton = null;
        private static PushButton automaticDoorRenumberToFromPushButton = null;
        private static PushButton revitCityLinkPushButton = null;
        private static PushButton googleSearchPushButton = null;
        private static PushButton kBRenderWebsitePushButton = null;
        private static PushButton kBTutorialWebsitePushButton = null;
        //private static PulldownButton windowTileSizesPulldownButton = null;
        private static PushButton windowTileSizesPushButtonLeft = null;
        private static PushButton windowTileSizesPushButtonRight = null;
        private static PushButton windowTileSizesPushButtonTop = null;
        private static PushButton windowTileSizesPushButtonBottom = null;
        private static PushButton windowTileSizesPushButtonBigger = null;
        private static PushButton windowTileSizesPushButtonSmaller = null;
        private static PushButton windowTileSizesPushButtonLoad = null;
        private static PushButton windowTileSizesPushButtonSave = null;
        private static PushButton autosincPushButton = null;
        //private static PulldownButton autosincPulldownButton = null;
        private static PushButton autoSynchronizeStartPushButton = null;
        private static PushButton autoSynchronizeStopPushButton = null;
        private static PushButton toolPalettesPushButton = null;
        private static PushButton setDoorOffsetPushButton = null;
        private static PushButton removeDwgImportsPushButton = null;
        private static PushButton viewDepthOverridePushButton = null;
        private static PushButton exportToExcelPushButton = null;
        private static PushButton dwgFamilyRemoverPushButton = null;
        private static PushButton scheduleSpellCheckButton = null;
        private static Autodesk.Revit.UI.TextBox searchTextBox = null;

        private static PushButton tabToolbarRenamerPushButton = null;

        private static PushButton toolbarManagerButtonPushButton = null;

        public Dictionary<string, List<string>> tabItems = null;

        private static Autodesk.Windows.RibbonButton superSearchRibbonButton = null;
        private static Autodesk.Revit.UI.RibbonPanel ribbonPanelRenumber = null;
        private static Autodesk.Revit.UI.RibbonPanel ribbonPanelWindow = null;
        private static Autodesk.Revit.UI.RibbonPanel ribbonPanelAdditional = null;

        public static LicenseStarter myLicenseStarter = null;
        public static LicenseUpdater myLicenseUpdater = null;
        public static WindowTileSizesProcessor TileSizesProcessor = null;
        public static KBRevitWebBrowser WebBrowserDockableWindow = null;
        public static ToolPaletteUI ToolPaletUI = null;
        private readonly object pushButtonData;
        private readonly object panel;

        public static int getLicenseState()
        {
            return KbBimstoreApp.licenseState;
        }

        public static void setLicenseState(int lstate)
        {
#if DEBUG
            KbBimstoreApp.licenseState = 4;
            Autodesk.Revit.UI.TaskDialog.Show("b", "Change this!");
#else
            KbBimstoreApp.licenseState = lstate;
#endif
            if (KbBimstoreApp.licenseToolPushButton != null) KbBimstoreApp.licenseToolPushButton.Enabled = true;

            if (KbBimstoreApp.licenseState >= 1)
            {
                if (KbBimstoreApp.createProjectPushButton != null) KbBimstoreApp.createProjectPushButton.Enabled = true;
                if (KbBimstoreApp.addNewViewSheetstPushButton != null) KbBimstoreApp.addNewViewSheetstPushButton.Enabled = true;
                if (KbBimstoreApp.designOptionsPushButton != null) KbBimstoreApp.designOptionsPushButton.Enabled = true;
                if (KbBimstoreApp.pageAlignmentToolPushButton != null) KbBimstoreApp.pageAlignmentToolPushButton.Enabled = true;
                if (KbBimstoreApp.baseAndTransitionDetailsPushlButton != null) KbBimstoreApp.baseAndTransitionDetailsPushlButton.Enabled = true;
                if (KbBimstoreApp.doorAndWindowDetailsPushlButton != null) KbBimstoreApp.doorAndWindowDetailsPushlButton.Enabled = true;
                if (KbBimstoreApp.ceilingDetailsPushlButton != null) KbBimstoreApp.ceilingDetailsPushlButton.Enabled = true;
                if (KbBimstoreApp.millworkPushlButton != null) KbBimstoreApp.millworkPushlButton.Enabled = true;
                if (KbBimstoreApp.partitionPushlButton != null) KbBimstoreApp.partitionPushlButton.Enabled = true;
                if (KbBimstoreApp.importDetailsSettingsPushlButton != null) KbBimstoreApp.importDetailsSettingsPushlButton.Enabled = true;
                if (KbBimstoreApp.cadDetailConverterPushButton != null) KbBimstoreApp.cadDetailConverterPushButton.Enabled = false;

                if (KbBimstoreApp.renumberByRoomsPushButton != null) KbBimstoreApp.renumberByRoomsPushButton.Enabled = true;
                if (KbBimstoreApp.renumberByDoorsPushButton != null) KbBimstoreApp.renumberByDoorsPushButton.Enabled = true;
                if (KbBimstoreApp.renumberViewPortsPushButton != null) KbBimstoreApp.renumberViewPortsPushButton.Enabled = true;
                if (KbBimstoreApp.automaticDoorRenumberPushButton != null) KbBimstoreApp.automaticDoorRenumberPushButton.Enabled = true;
                if (KbBimstoreApp.automaticDoorRenumberToFromPushButton != null) KbBimstoreApp.automaticDoorRenumberToFromPushButton.Enabled = true;
                if (KbBimstoreApp.revitCityLinkPushButton != null) KbBimstoreApp.revitCityLinkPushButton.Enabled = true;
                if (KbBimstoreApp.googleSearchPushButton != null) KbBimstoreApp.googleSearchPushButton.Enabled = true;
                if (KbBimstoreApp.kBRenderWebsitePushButton != null) KbBimstoreApp.kBRenderWebsitePushButton.Enabled = true;
                if (KbBimstoreApp.kBTutorialWebsitePushButton != null) KbBimstoreApp.kBTutorialWebsitePushButton.Enabled = true;
                //if (KbBimstoreApp.windowTileSizesPulldownButton != null) KbBimstoreApp.windowTileSizesPulldownButton.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonLeft != null) KbBimstoreApp.windowTileSizesPushButtonLeft.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonRight != null) KbBimstoreApp.windowTileSizesPushButtonRight.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonTop != null) KbBimstoreApp.windowTileSizesPushButtonTop.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonBottom != null) KbBimstoreApp.windowTileSizesPushButtonBottom.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonBigger != null) KbBimstoreApp.windowTileSizesPushButtonBigger.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonSmaller != null) KbBimstoreApp.windowTileSizesPushButtonSmaller.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonLoad != null) KbBimstoreApp.windowTileSizesPushButtonLoad.Enabled = true;
                if (KbBimstoreApp.windowTileSizesPushButtonSave != null) KbBimstoreApp.windowTileSizesPushButtonSave.Enabled = true;

                if (KbBimstoreApp.autosincPushButton != null) KbBimstoreApp.autosincPushButton.Enabled = true;
                //if (KbBimstoreApp.autosincPulldownButton != null) KbBimstoreApp.autosincPulldownButton.Enabled = true;
                if (KbBimstoreApp.autoSynchronizeStartPushButton != null) KbBimstoreApp.autoSynchronizeStartPushButton.Enabled = true;
                if (KbBimstoreApp.autoSynchronizeStopPushButton != null) KbBimstoreApp.autoSynchronizeStopPushButton.Enabled = true;

                if (KbBimstoreApp.toolPalettesPushButton != null) KbBimstoreApp.toolPalettesPushButton.Enabled = true;
                if (KbBimstoreApp.setDoorOffsetPushButton != null) KbBimstoreApp.setDoorOffsetPushButton.Enabled = true;
                if (KbBimstoreApp.removeDwgImportsPushButton != null) KbBimstoreApp.removeDwgImportsPushButton.Enabled = true;
                if (KbBimstoreApp.viewDepthOverridePushButton != null) KbBimstoreApp.viewDepthOverridePushButton.Enabled = true;
                if (KbBimstoreApp.exportToExcelPushButton != null) KbBimstoreApp.exportToExcelPushButton.Enabled = true;
                if (KbBimstoreApp.superSearchRibbonButton != null) KbBimstoreApp.superSearchRibbonButton.IsEnabled = true;
                if (KbBimstoreApp.TileSizesProcessor != null) KbBimstoreApp.TileSizesProcessor.setAlmEnabled(true);
                if (KbBimstoreApp.WebBrowserDockableWindow != null) KbBimstoreApp.WebBrowserDockableWindow.setAlmEnabled(true);
                if (KbBimstoreApp.ToolPaletUI != null) KbBimstoreApp.ToolPaletUI.setAlmEnabled(true);
                if (KbBimstoreApp.ribbonPanelRenumber != null) KbBimstoreApp.ribbonPanelRenumber.Enabled = true;
                if (KbBimstoreApp.ribbonPanelWindow != null) KbBimstoreApp.ribbonPanelWindow.Enabled = true;
                if (KbBimstoreApp.ribbonPanelAdditional != null) KbBimstoreApp.ribbonPanelAdditional.Enabled = true;
                if (KbBimstoreApp.searchTextBox != null) KbBimstoreApp.searchTextBox.Enabled = true;

                if (lstate == 2) //diable features not available at "Standard"
                {
                    if (KbBimstoreApp.createProjectPushButton != null) KbBimstoreApp.createProjectPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.addNewViewSheetstPushButton != null) KbBimstoreApp.addNewViewSheetstPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.automaticDoorRenumberPushButton != null) KbBimstoreApp.automaticDoorRenumberPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.automaticDoorRenumberToFromPushButton != null) KbBimstoreApp.automaticDoorRenumberToFromPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.revitCityLinkPushButton != null) KbBimstoreApp.revitCityLinkPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.googleSearchPushButton != null) KbBimstoreApp.googleSearchPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.autosincPushButton != null) KbBimstoreApp.autosincPushButton.Enabled = false; //not enabled in base mode
                    //if (KbBimstoreApp.autosincPulldownButton != null) KbBimstoreApp.autosincPulldownButton.Enabled = true;
                    if (KbBimstoreApp.autoSynchronizeStartPushButton != null) KbBimstoreApp.autoSynchronizeStartPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.autoSynchronizeStopPushButton != null) KbBimstoreApp.autoSynchronizeStopPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.setDoorOffsetPushButton != null) KbBimstoreApp.setDoorOffsetPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.viewDepthOverridePushButton != null) KbBimstoreApp.viewDepthOverridePushButton.Enabled = false; //not enabled in base mode
                }
                else if (lstate == 3) //diable features not available in "Premium"
                {
                    if (KbBimstoreApp.createProjectPushButton != null) KbBimstoreApp.createProjectPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.automaticDoorRenumberToFromPushButton != null) KbBimstoreApp.automaticDoorRenumberToFromPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.autosincPushButton != null) KbBimstoreApp.autosincPushButton.Enabled = false; //not enabled in base mode
                    //if (KbBimstoreApp.autosincPulldownButton != null) KbBimstoreApp.autosincPulldownButton.Enabled = true;
                    if (KbBimstoreApp.autoSynchronizeStartPushButton != null) KbBimstoreApp.autoSynchronizeStartPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.autoSynchronizeStopPushButton != null) KbBimstoreApp.autoSynchronizeStopPushButton.Enabled = false; //not enabled in base mode
                    if (KbBimstoreApp.viewDepthOverridePushButton != null) KbBimstoreApp.viewDepthOverridePushButton.Enabled = false; //not enabled in base mode
                }
                else if (lstate == 4)
                {
                    if (KbBimstoreApp.cadDetailConverterPushButton != null) KbBimstoreApp.cadDetailConverterPushButton.Enabled = true; //only available in ultimate
                    if (KbBimstoreApp.dwgFamilyRemoverPushButton != null) KbBimstoreApp.dwgFamilyRemoverPushButton.Enabled = true;
                }
            }
            else
            {
                if (KbBimstoreApp.createProjectPushButton != null) KbBimstoreApp.createProjectPushButton.Enabled = false;
                if (KbBimstoreApp.addNewViewSheetstPushButton != null) KbBimstoreApp.addNewViewSheetstPushButton.Enabled = false;
                if (KbBimstoreApp.designOptionsPushButton != null) KbBimstoreApp.designOptionsPushButton.Enabled = false;
                if (KbBimstoreApp.pageAlignmentToolPushButton != null) KbBimstoreApp.pageAlignmentToolPushButton.Enabled = false;
                if (KbBimstoreApp.baseAndTransitionDetailsPushlButton != null) KbBimstoreApp.baseAndTransitionDetailsPushlButton.Enabled = false;
                if (KbBimstoreApp.doorAndWindowDetailsPushlButton != null) KbBimstoreApp.doorAndWindowDetailsPushlButton.Enabled = false;
                if (KbBimstoreApp.ceilingDetailsPushlButton != null) KbBimstoreApp.ceilingDetailsPushlButton.Enabled = false;
                if (KbBimstoreApp.millworkPushlButton != null) KbBimstoreApp.millworkPushlButton.Enabled = false;
                if (KbBimstoreApp.partitionPushlButton != null) KbBimstoreApp.partitionPushlButton.Enabled = false;
                if (KbBimstoreApp.importDetailsSettingsPushlButton != null) KbBimstoreApp.importDetailsSettingsPushlButton.Enabled = false;
                if (KbBimstoreApp.cadDetailConverterPushButton != null) KbBimstoreApp.cadDetailConverterPushButton.Enabled = false;
                if (KbBimstoreApp.renumberByRoomsPushButton != null) KbBimstoreApp.renumberByRoomsPushButton.Enabled = false;
                if (KbBimstoreApp.renumberByDoorsPushButton != null) KbBimstoreApp.renumberByDoorsPushButton.Enabled = false;
                if (KbBimstoreApp.renumberViewPortsPushButton != null) KbBimstoreApp.renumberViewPortsPushButton.Enabled = false;
                if (KbBimstoreApp.automaticDoorRenumberPushButton != null) KbBimstoreApp.automaticDoorRenumberPushButton.Enabled = false;
                if (KbBimstoreApp.automaticDoorRenumberToFromPushButton != null) KbBimstoreApp.automaticDoorRenumberToFromPushButton.Enabled = false;
                if (KbBimstoreApp.revitCityLinkPushButton != null) KbBimstoreApp.revitCityLinkPushButton.Enabled = false;
                if (KbBimstoreApp.googleSearchPushButton != null) KbBimstoreApp.googleSearchPushButton.Enabled = false;
                if (KbBimstoreApp.kBRenderWebsitePushButton != null) KbBimstoreApp.kBRenderWebsitePushButton.Enabled = false;
                if (KbBimstoreApp.kBTutorialWebsitePushButton != null) KbBimstoreApp.kBTutorialWebsitePushButton.Enabled = false;
                //if (KbBimstoreApp.windowTileSizesPulldownButton != null) KbBimstoreApp.windowTileSizesPulldownButton.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonLeft != null) KbBimstoreApp.windowTileSizesPushButtonLeft.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonRight != null) KbBimstoreApp.windowTileSizesPushButtonRight.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonTop != null) KbBimstoreApp.windowTileSizesPushButtonTop.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonBottom != null) KbBimstoreApp.windowTileSizesPushButtonBottom.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonBigger != null) KbBimstoreApp.windowTileSizesPushButtonBigger.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonSmaller != null) KbBimstoreApp.windowTileSizesPushButtonSmaller.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonLoad != null) KbBimstoreApp.windowTileSizesPushButtonLoad.Enabled = false;
                if (KbBimstoreApp.windowTileSizesPushButtonSave != null) KbBimstoreApp.windowTileSizesPushButtonSave.Enabled = false;
                if (KbBimstoreApp.autosincPushButton != null) KbBimstoreApp.autosincPushButton.Enabled = false;
                //if (KbBimstoreApp.autosincPulldownButton != null) KbBimstoreApp.autosincPulldownButton.Enabled = false;
                if (KbBimstoreApp.autoSynchronizeStartPushButton != null) KbBimstoreApp.autoSynchronizeStartPushButton.Enabled = false;
                if (KbBimstoreApp.autoSynchronizeStopPushButton != null) KbBimstoreApp.autoSynchronizeStopPushButton.Enabled = false;
                if (KbBimstoreApp.toolPalettesPushButton != null) KbBimstoreApp.toolPalettesPushButton.Enabled = false;
                if (KbBimstoreApp.setDoorOffsetPushButton != null) KbBimstoreApp.setDoorOffsetPushButton.Enabled = false;
                if (KbBimstoreApp.removeDwgImportsPushButton != null) KbBimstoreApp.removeDwgImportsPushButton.Enabled = false;
                if (KbBimstoreApp.viewDepthOverridePushButton != null) KbBimstoreApp.viewDepthOverridePushButton.Enabled = false;
                if (KbBimstoreApp.exportToExcelPushButton != null) KbBimstoreApp.exportToExcelPushButton.Enabled = false;
                if (KbBimstoreApp.superSearchRibbonButton != null) KbBimstoreApp.superSearchRibbonButton.IsEnabled = false;
                if (KbBimstoreApp.TileSizesProcessor != null) KbBimstoreApp.TileSizesProcessor.setAlmEnabled(false);
                if (KbBimstoreApp.WebBrowserDockableWindow != null) KbBimstoreApp.WebBrowserDockableWindow.setAlmEnabled(false);
                if (KbBimstoreApp.ToolPaletUI != null) KbBimstoreApp.ToolPaletUI.setAlmEnabled(false);
                if (KbBimstoreApp.ribbonPanelRenumber != null) KbBimstoreApp.ribbonPanelRenumber.Enabled = false;
                if (KbBimstoreApp.ribbonPanelWindow != null) KbBimstoreApp.ribbonPanelWindow.Enabled = false;
                if (KbBimstoreApp.ribbonPanelAdditional != null) KbBimstoreApp.ribbonPanelAdditional.Enabled = false;
                if (KbBimstoreApp.searchTextBox != null) KbBimstoreApp.searchTextBox.Enabled = false;
                if (KbBimstoreApp.dwgFamilyRemoverPushButton != null) KbBimstoreApp.dwgFamilyRemoverPushButton.Enabled = true;
                if (KbBimstoreApp.scheduleSpellCheckButton != null) KbBimstoreApp.scheduleSpellCheckButton.Enabled = false;
            }
        }

        public static string getRevitInstallationPath()
        {

            return null;
        }

        public static void AutoSync()
        {
            if (KbBimstoreApp.activeDoc != null)
            {
                try
                {
                    if (!activeDoc.IsWorkshared)
                        return;

                    TransactWithCentralOptions transOpts = new TransactWithCentralOptions();
                    SynchronizeWithCentralOptions syncOpts = new SynchronizeWithCentralOptions();
                    RelinquishOptions relinquishOpts = new RelinquishOptions(false);
                    syncOpts.SetRelinquishOptions(relinquishOpts);
                    syncOpts.SaveLocalAfter = false;
                    syncOpts.Comment = "Changes to Workset";

                    KbBimstoreApp.activeDoc.SynchronizeWithCentral(transOpts, syncOpts);
                }
                catch (Exception ex)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Exception", ex.Message);
                }
            }
        }

        private void AutosynchTimer_Tick(object sender, EventArgs e)
        {
            HandleShowDialog(); //for first time show

            DateTime nowDateTime = DateTime.Now;

            TimeSpan diffTimeSpan = nowDateTime.Subtract(KbBimstoreApp.autosynchLastDateTime);
            TimeSpan timeSinceLastSave = nowDateTime.Subtract(KbBimstoreApp.autosaveLastDateTime);

            if (KbBimstoreApp.autosynchIsActive && diffTimeSpan.Milliseconds >= KbBimstoreApp.autosynchInterval)
            {
                AutoSync();

                KbBimstoreApp.autosynchPrevDateTime = KbBimstoreApp.autosynchLastDateTime;
                KbBimstoreApp.autosynchLastDateTime = nowDateTime;
            }
            else if (KbBimstoreApp.autoSaveIsActive && timeSinceLastSave.Milliseconds >= KbBimstoreApp.autoSaveInterval)
            {
                try
                {
                    KbBimstoreApp.activeDoc.Save();
                }
                catch
                {
                    //read only file
                }

                KbBimstoreApp.autosavePrevDateTime = KbBimstoreApp.autosaveLastDateTime;
                KbBimstoreApp.autosaveLastDateTime = nowDateTime;
            }
        }

        private void HandleShowDialog()
        {
            if (!hasUserOpenedAutoSyncDialog)
            {
                if (activeDoc != null && documentOpenedTime != null && DateTime.Now.Subtract(documentOpenedTime.Value).TotalSeconds > 60)
                {
                    hasUserOpenedAutoSyncDialog = true;

                    AutoSyncData data = AutoSyncData.Deserialize(KbBimstoreApp.AutoSyncSettingsFilePath);

                    if (data != null)
                    {
                        AutoSynchronizeProcessor.Update(data);
                    }
                    else
                    {
                        KbBimstoreApp.autosynchIsActive = false;
                        KbBimstoreApp.autoSaveIsActive = false;
                        KbBimstoreApp.autoSaveInterval = int.MaxValue;
                        KbBimstoreApp.autosynchInterval = int.MaxValue;
                    }

                    KbBimstoreApp.autosynchTimer.Interval = 600000;
                }
            }
        }

        public Result OnStartup(UIControlledApplication uicontapp)
        {
            KbBimstoreApp.activeUiContApp = uicontapp;

            //KB-Renamer Addin Stuff
            MainTab = KbBimstoreTab.CreateBimStoreFromSettings(TabToolbarRenamerSettingsFilePath);

            TileSizesProcessor = new WindowTileSizesProcessor();
            WebBrowserDockableWindow = new KBRevitWebBrowser();
            ToolPaletUI = new ToolPaletteUI();

            myLicenseStarter = new LicenseStarter();
            AlmInit();
            myLicenseStarter.startlicensecheck();

            //KB-Detail Stuff
            this.detailApplication = new DetailApplication();
            detailApplication.OnStartup(uicontapp, KbBimstoreApp.licenseState);

            return Result.Succeeded;
        }

        public void AlmInit()
        {
            bool KBBimStoreTabExists = false;

            try
            {
                KbBimstoreApp.activeUiContApp.CreateRibbonTab("BIMeta");
            }
            catch (Exception ex)
            {
                KBBimStoreTabExists = true;
            }

            if (!KBBimStoreTabExists)
            {
                try
                {
                    CreateRibbonPanel(KbBimstoreApp.activeUiContApp);
                    CreateToolPalette(KbBimstoreApp.activeUiContApp);
                    CreateKbShortcuts(KbBimstoreApp.activeUiContApp);

                    KbBimstoreApp.activeUiContApp.ViewActivating += uicontapp_ViewActivating;
                    KbBimstoreApp.activeUiContApp.ViewActivated += uicontapp_ViewActivated;
                    KbBimstoreApp.activeUiContApp.ApplicationClosing += uicontapp_ApplicationClosing;
                    KbBimstoreApp.activeUiContApp.ControlledApplication.DocumentOpened += ControlledApplication_DocumentOpened;

                    KbBimstoreApp.autosynchTimer.Tick += AutosynchTimer_Tick;
                    KbBimstoreApp.autosynchTimer.Interval = 10000;
                }
                catch (Exception ex)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Error creating BIMeta Ribbon: ", ex.ToString());
                }
            }
        }

        private void ControlledApplication_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
            documentOpenedTime = DateTime.Now;
            autosynchTimer.Start();
            ToolPaletUI.UpdateDoc(e.Document);
        }

        public static Document getCurrentActiveDocument()
        {
            if (KbBimstoreApp.activeDoc == null)
            {
                if (activeView == null)
                {
                    return null;
                }
                else
                {
                    return KbBimstoreApp.activeView.Document;
                }
            }
            else
            {
                return KbBimstoreApp.activeDoc;
            }
        }

        public static Level getCurrentActiveLevel()
        {
            Level activeLevel = null;

            if (KbBimstoreApp.activeDoc != null)
            {
                if (KbBimstoreApp.activeView != null)
                {
                    ElementId levelId = KbBimstoreApp.activeView.LevelId;
                    if (levelId != null)
                    {
                        Element levelElem = KbBimstoreApp.activeDoc.GetElement(levelId);
                        if (levelElem != null)
                        {
                            if (levelElem is Level)
                            {
                                activeLevel = levelElem as Level;
                            }
                        }
                    }
                }
            }

            return activeLevel;
        }

        private void uicontapp_ViewActivating(object sender, ViewActivatingEventArgs e)
        {
            try
            {
                KbBimstoreApp.activeDoc = e.Document;
                KbBimstoreApp.activeView = e.CurrentActiveView;
            }
            catch (Exception ex)
            {
                KbBimstoreApp.activeDoc = null;
                KbBimstoreApp.activeView = null;
            }
        }

        private void uicontapp_ViewActivated(object sender, ViewActivatedEventArgs e)
        {
            try
            {
                KbBimstoreApp.activeDoc = e.Document;
                KbBimstoreApp.activeView = e.CurrentActiveView;
            }
            catch (Exception ex)
            {
                KbBimstoreApp.activeDoc = null;
                KbBimstoreApp.activeView = null;
            }
        }

        private void uicontapp_ApplicationClosing(object sender, ApplicationClosingEventArgs e)
        {
            KbBimstoreApp.activeDoc = null;
            KbBimstoreApp.activeView = null;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            KbBimstoreApp.activeDoc = null;
            KbBimstoreApp.activeView = null;

            detailApplication.OnShutdown(application);

            return Result.Succeeded;
        }

        public void CreateRibbonPanel(UIControlledApplication application)
        {
            //Create manage panel first
            CreateManagePanel(application);

            for (int i = 0; i < MainTab.ToolBars.Count; i++)
            {
                var toolbar = MainTab.ToolBars[i];

                switch (toolbar.TabIdentity)
                {
                    case 0:
                        CreateDetailItemsPanel(application, toolbar);
                        break;
                    case 1:
                        CreateDetailAndSettingsConverterPanel(application, toolbar);
                        break;
                    case 2:
                        CreateWebsiteToolsPanel(application, toolbar);
                        break;
                    case 3:
                        CreateAutoSyncPanel(application, toolbar);
                        break;
                    case 4:
                        CreateRenumberToolsPanel(application, toolbar);
                        break;
                    case 5:
                        CreateWindowSizingPanel(application, toolbar);
                        break;
                    case 6:
                        CreateAdditionalPluginsPanel(application, toolbar);
                        break;
                    case 7:
                        CreateRevitCityPanel(application, toolbar);
                        break;
                    default:
                        break;
                }
            }

            #region
            try
            {
                foreach (Autodesk.Windows.RibbonTab curTab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
                {
                    if (curTab.Id == "Modify")
                    {
                        StringBuilder strbld = new StringBuilder();

                        Autodesk.Windows.RibbonPanel superSearchRibbonPanel = new Autodesk.Windows.RibbonPanel();
                        Autodesk.Windows.RibbonPanelSource superSearchRibbonPanelSource = new Autodesk.Windows.RibbonPanelSource();
                        superSearchRibbonPanelSource.Title = "Super Filter";
                        superSearchRibbonPanel.Source = superSearchRibbonPanelSource;
                        curTab.Panels.Add(superSearchRibbonPanel);

                        KbBimstoreApp.superSearchRibbonButton = new Autodesk.Windows.RibbonButton();
                        superSearchRibbonButton.Size = RibbonItemSize.Large;
                        superSearchRibbonButton.ShowText = true;
                        superSearchRibbonButton.Text = "  Super Filter  ";
                        superSearchRibbonButton.IsActive = true;
                        superSearchRibbonButton.IsEnabled = true;
                        superSearchRibbonButton.AllowInToolBar = true;
                        superSearchRibbonButton.AllowInStatusBar = true;
                        superSearchRibbonButton.ShowToolTipOnDisabled = true;
                        superSearchRibbonButton.ToolTip = "Super Filter";
                        superSearchRibbonButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "SuperFilter_S.png"), UriKind.Absolute));
                        superSearchRibbonButton.ShowImage = true;
                        superSearchRibbonPanelSource.Items.Add(superSearchRibbonButton);
                        superSearchRibbonButton.CommandHandler = new SuperSearchHandler(activeUiContApp);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            #endregion

        }

        private PushButton AddItemToCorrectPanel(PushButtonData data, string defaultPanelName, KbBimstoreToolbar toolbar = null)
        {
            if (tabItems == null)
                if (!FillTabItems()) //file doesn't exist meaning it hasn't been created yet
                    return AddItemToPanel(data, defaultPanelName, toolbar);

            string itemText = data.Text.Trim();

            foreach (KeyValuePair<string, List<string>> kvp in tabItems)
            {
                if (kvp.Value.Contains(itemText))
                {
                    return AddItemToPanel(data, kvp.Key, toolbar);
                    //return AddItemToPanel(data, defaultPanelName, toolbar); //don't use the default, send in the provided from the toolbar manager... right?
                }
            }

            return null;
        }

        private PushButton AddItemToPanel(PushButtonData data, string panelName, KbBimstoreToolbar toolbar = null)
        {
            if (activeUiContApp.GetRibbonPanels(TAB_NAME).Select(x => x.Name).Contains(panelName))
                return activeUiContApp.GetRibbonPanels(TAB_NAME).Where(x => x.Name == panelName).First().AddItem(data) as PushButton;
            else
            {
                var rp = activeUiContApp.CreateRibbonPanel(TAB_NAME, panelName);

                if (toolbar != null)
                {
                    rp.Enabled = toolbar.Enabled && MainTab.Enabled;
                    rp.Visible = rp.Enabled;
                }
                return rp.AddItem(data) as PushButton;
            }
        }
        /// <summary>
        /// FillTabItems()
        /// Заполнение словаря из файла
        /// </summary>
        /// <returns></returns>
        public bool FillTabItems()
        {
            if (!File.Exists(ToolbarManagerFilePath))
                return false;

            System.Xml.Linq.XElement rootElement = System.Xml.Linq.XElement.Load(ToolbarManagerFilePath);

            tabItems = new Dictionary<string, List<string>>();

            foreach (var el in rootElement.Elements())
            {
                tabItems.Add(el.Name.LocalName.Replace('-', ' '), el.Value.Split(',').ToList());
            }

            return true;
        }

        private void CreateManagePanel(UIControlledApplication application)
        {
            string panelName = "Manage";

            //Autodesk.Revit.UI.RibbonPanel ribbonPanelKBR = application.CreateRibbonPanel(TabName, panelName);

            PushButtonData createProjectPushButtonData = new PushButtonData("Create Project", "Create Project", AddInPath, "KbBimstore.CreateProject");
            KbBimstoreApp.createProjectPushButton = AddItemToCorrectPanel(createProjectPushButtonData, panelName);
            createProjectPushButton.ToolTip = "Easily Create Levels, Views, Templates, And Sheets Based On Your Reference File";
            createProjectPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CreateProject_S.png"), UriKind.Absolute));
            createProjectPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CreateProject_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp0 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#twokb");
            createProjectPushButton.SetContextualHelp(contextHelp0);
            createProjectPushButton.Enabled = MainTab.Enabled;

            PushButtonData addNewViewSheetstPushButtonData = new PushButtonData("Add New Views and Sheets", "Add Views/Sheets", AddInPath, "KbBimstore.AddNewViewSheets");
            KbBimstoreApp.addNewViewSheetstPushButton = AddItemToCorrectPanel(addNewViewSheetstPushButtonData, panelName);
            addNewViewSheetstPushButton.ToolTip = "Right Click To Add New Sheets and Views";
            addNewViewSheetstPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AddNewViewSheets_S.png"), UriKind.Absolute));
            addNewViewSheetstPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AddNewViewSheets_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp1 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#threekb");
            addNewViewSheetstPushButton.SetContextualHelp(contextHelp1);
            addNewViewSheetstPushButton.Enabled = MainTab.Enabled;

            PushButtonData designOptionsPushButtonData = new PushButtonData("Design Options", "Design Options", AddInPath, "KbBimstore.DesignOptions");
            KbBimstoreApp.designOptionsPushButton = AddItemToCorrectPanel(designOptionsPushButtonData, panelName);
            designOptionsPushButton.ToolTip = "Create Your Templates And Views For Schematic Design";
            designOptionsPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DesignOptions_S.png"), UriKind.Absolute));
            designOptionsPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DesignOptions_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp2 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#fourkb");
            designOptionsPushButton.SetContextualHelp(contextHelp2);
            designOptionsPushButton.Enabled = MainTab.Enabled;

            PushButtonData pageAlignmentToolButtonData = new PushButtonData("View Alignment Tool", "Align Views", AddInPath, "KbBimstore.PageAlignmentTool");
            KbBimstoreApp.pageAlignmentToolPushButton = AddItemToCorrectPanel(pageAlignmentToolButtonData, panelName);
            pageAlignmentToolPushButton.ToolTip = "Click On One Point Of The View Port and Corresponding Point To Move All Your Views";
            pageAlignmentToolPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "PageAlignmentTool_S.png"), UriKind.Absolute));
            pageAlignmentToolPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "PageAlignmentTool_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp3 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#fivekb");
            pageAlignmentToolPushButton.SetContextualHelp(contextHelp3);
            pageAlignmentToolPushButton.Enabled = MainTab.Enabled;

            PushButtonData licenseButtonData = new PushButtonData("License", "License", AddInPath, "KbBimstore.LicensingCommand");
            KbBimstoreApp.licenseToolPushButton = AddItemToCorrectPanel(licenseButtonData, panelName);
            licenseToolPushButton.ToolTip = "Click On This Button To Review Your License Details";
            licenseToolPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "License_S.png"), UriKind.Absolute));
            licenseToolPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "License_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp4 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sixkb");
            licenseToolPushButton.SetContextualHelp(contextHelp4);

            PushButtonData tabToolbarRenamerButtonData = new PushButtonData("TabToolbarRenamer", "Tab / Toolbar Renamer", AddInPath, "KbBimstore.TabToolbarRenamerCommand");
            KbBimstoreApp.tabToolbarRenamerPushButton = AddItemToCorrectPanel(tabToolbarRenamerButtonData, panelName);
            tabToolbarRenamerPushButton.ToolTip = "Click On This Button To Rename and Renumber the Panels";
            tabToolbarRenamerPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "TabToolbarRenamer.png"), UriKind.Absolute));
            tabToolbarRenamerPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "TabToolbarRenamer.png"), UriKind.Absolute));
            //ContextualHelp contextHelp5 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sixkb");
            //tabToolbarRenamerPushButton.SetContextualHelp(contextHelp5);

            PushButtonData toolbarManagerButtonData = new PushButtonData("ToolbarManager", "Toolbar Manager", AddInPath, "KbBimstore.ToolbarManagerCommand");
            KbBimstoreApp.toolbarManagerButtonPushButton = AddItemToCorrectPanel(toolbarManagerButtonData, panelName);
            toolbarManagerButtonPushButton.ToolTip = "Click On This Button To Manage Toolbar and Associated Buttons";
            toolbarManagerButtonPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ToolbarManager.png"), UriKind.Absolute));
            toolbarManagerButtonPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ToolbarManager.png"), UriKind.Absolute));
            //ContextualHelp contextHelp5 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sixkb");
            //toolbarManagerButtonPushButton.SetContextualHelp(contextHelp5);
        }

        private void CreateDetailItemsPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            //Autodesk.Revit.UI.RibbonPanel ribbonPanelDetail = application.CreateRibbonPanel("KB-BimStore", toolbar.Name);
            //ribbonPanelDetail.Enabled = toolbar.Enabled && MainTab.Enabled;

            PushButtonData baseAndTransitionDetailsPushlButtonData = new PushButtonData("Base & Transition", "Base & Transition", AddInPath, "KbBimstore.BaseAndTransitionDetails");
            KbBimstoreApp.baseAndTransitionDetailsPushlButton = AddItemToCorrectPanel(baseAndTransitionDetailsPushlButtonData, toolbar.Name, toolbar);
            baseAndTransitionDetailsPushlButton.ToolTip = "Base & Transition Details";
            baseAndTransitionDetailsPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "BaseAndTransitionDetails_S.png"), UriKind.Absolute));
            baseAndTransitionDetailsPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "BaseAndTransitionDetails_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp5 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sevenkb");
            baseAndTransitionDetailsPushlButton.SetContextualHelp(contextHelp5);


            PushButtonData doorAndWindowDetailsPushlButtonData = new PushButtonData("Door & Window", "Door & Window", AddInPath, "KbBimstore.DoorAndWindowDetails");
            KbBimstoreApp.doorAndWindowDetailsPushlButton = AddItemToCorrectPanel(doorAndWindowDetailsPushlButtonData, toolbar.Name, toolbar);
            doorAndWindowDetailsPushlButton.ToolTip = "Door & Window Details";
            doorAndWindowDetailsPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DoorAndWindowDetails_S.png"), UriKind.Absolute));
            doorAndWindowDetailsPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DoorAndWindowDetails_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp6 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sevenkb");
            doorAndWindowDetailsPushlButton.SetContextualHelp(contextHelp6);


            PushButtonData ceilingDetailsPushlButtonData = new PushButtonData("Ceiling", "Ceiling", AddInPath, "KbBimstore.CeilingDetails");
            KbBimstoreApp.ceilingDetailsPushlButton = AddItemToCorrectPanel(ceilingDetailsPushlButtonData, toolbar.Name, toolbar);
            ceilingDetailsPushlButton.ToolTip = "Ceiling Details";
            ceilingDetailsPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CeilingDetails_S.png"), UriKind.Absolute));
            ceilingDetailsPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CeilingDetails_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp7 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sevenkb");
            ceilingDetailsPushlButton.SetContextualHelp(contextHelp7);


            PushButtonData millworkPushlButtonData = new PushButtonData("Millwork", "Millwork", AddInPath, "KbBimstore.Millwork");
            KbBimstoreApp.millworkPushlButton = AddItemToCorrectPanel(millworkPushlButtonData, toolbar.Name, toolbar);
            millworkPushlButton.ToolTip = "Millwork";
            millworkPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "Millwork_S.png"), UriKind.Absolute));
            millworkPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "Millwork_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp8 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sevenkb");
            millworkPushlButton.SetContextualHelp(contextHelp8);


            PushButtonData partitionPushlButtonData = new PushButtonData("Partition", "Partition", AddInPath, "KbBimstore.Partition");
            KbBimstoreApp.partitionPushlButton = AddItemToCorrectPanel(partitionPushlButtonData, toolbar.Name, toolbar);
            partitionPushlButton.ToolTip = "Partition";
            partitionPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "Partition_S.png"), UriKind.Absolute));
            partitionPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "Partition_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp9 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sevenkb");
            partitionPushlButton.SetContextualHelp(contextHelp9);
        }

        private void CreateDetailAndSettingsConverterPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            //Autodesk.Revit.UI.RibbonPanel ribbonPanelConverter = application.CreateRibbonPanel("KB-BimStore", toolbar.Name);
            //ribbonPanelConverter.Enabled = toolbar.Enabled && MainTab.Enabled;

            PushButtonData importDetailsSettingsPushlButtonData = new PushButtonData("Import Details Settings", "Settings", AddInPath, "KbBimstore.ImportDetailsSettings");
            KbBimstoreApp.importDetailsSettingsPushlButton = AddItemToCorrectPanel(importDetailsSettingsPushlButtonData, toolbar.Name, toolbar);
            importDetailsSettingsPushlButton.ToolTip = "Select The Origin Of Your Details With This Tool";
            importDetailsSettingsPushlButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ImportDetailsSettings_S.png"), UriKind.Absolute));
            importDetailsSettingsPushlButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ImportDetailsSettings_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp10 = new ContextualHelp(ContextualHelpType.Url,
           "http://kb-bimstore.com/contextual-help/#sevenkb");
            importDetailsSettingsPushlButton.SetContextualHelp(contextHelp10);


            PushButtonData cadDetailConverterPushButtonData = new PushButtonData("Cad Detail Converter", "AutoCAD Converter", AddInPath, "KbBimstore.CadDetailConverter");
            KbBimstoreApp.cadDetailConverterPushButton = AddItemToCorrectPanel(cadDetailConverterPushButtonData, toolbar.Name, toolbar);
            cadDetailConverterPushButton.ToolTip = "Convert AutoCAD Details Into Revit Details Quickly";
            cadDetailConverterPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CadDetailConverter_S.png"), UriKind.Absolute));
            cadDetailConverterPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "CadDetailConverter_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp11 = new ContextualHelp(ContextualHelpType.Url,
           "http://kb-bimstore.com/contextual-help/#eightkb");
            cadDetailConverterPushButton.SetContextualHelp(contextHelp11);
        }

        private void CreateWebsiteToolsPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            //Autodesk.Revit.UI.RibbonPanel ribbonPanelWebsite = application.CreateRibbonPanel("KB-BimStore", toolbar.Name);
            //ribbonPanelWebsite.Enabled = toolbar.Enabled && MainTab.Enabled;

            PushButtonData googleSearchPushButtonData = new PushButtonData("Search", "Search", AddInPath, "KbBimstore.GoogleSearch");
            KbBimstoreApp.googleSearchPushButton = AddItemToCorrectPanel(googleSearchPushButtonData, toolbar.Name, toolbar);
            googleSearchPushButton.ToolTip = "Search Tool";
            googleSearchPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "GoogleSearch_S.png"), UriKind.Absolute));
            googleSearchPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "GoogleSearch_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp17 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#ninekb");
            googleSearchPushButton.SetContextualHelp(contextHelp17);

            PushButtonData kBRenderWebsitePushButtonData = new PushButtonData("BIMeta", "BIMeta", AddInPath, "KbBimstore.KBRenderWebsite");
            KbBimstoreApp.kBRenderWebsitePushButton = AddItemToCorrectPanel(kBRenderWebsitePushButtonData, toolbar.Name, toolbar);
            kBRenderWebsitePushButton.ToolTip = "BIMeta Website";
            kBRenderWebsitePushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "KBRenderWebsite_S.png"), UriKind.Absolute));
            kBRenderWebsitePushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "KBRenderWebsite_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp18 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#ninekb");
            kBRenderWebsitePushButton.SetContextualHelp(contextHelp18);

            PushButtonData kBTutorialWebsitePushButtonData = new PushButtonData("Training", "Training", AddInPath, "KbBimstore.KBTutorialWebsite");
            KbBimstoreApp.kBTutorialWebsitePushButton = AddItemToCorrectPanel(kBTutorialWebsitePushButtonData, toolbar.Name, toolbar);
            kBTutorialWebsitePushButton.ToolTip = "Training Library";
            kBTutorialWebsitePushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "VideoLibrary_S.png"), UriKind.Absolute));
            kBTutorialWebsitePushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "VideoLibrary_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp19 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#ninekb");
            kBTutorialWebsitePushButton.SetContextualHelp(contextHelp19);
        }

        private void CreateRevitCityPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            Autodesk.Revit.UI.RibbonPanel ribbonPanelRC = application.CreateRibbonPanel("BIMeta", "                    " + "RevitCity" + "       ");
            ribbonPanelRC.Enabled = toolbar.Enabled && MainTab.Enabled;
            ribbonPanelRC.Visible = ribbonPanelRC.Enabled;

            TextBoxData searchTextBoxData = new TextBoxData("RevitCity Search");
            searchTextBox = ribbonPanelRC.AddItem(searchTextBoxData) as Autodesk.Revit.UI.TextBox;
            searchTextBox.Width = 200.0;
            searchTextBox.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RevitCityLink_S.png"), UriKind.Absolute));
            searchTextBox.ToolTip = "Move Out Of Main Toolbar To Use Revit City Link";
            searchTextBox.ShowImageAsButton = true;
            searchTextBox.EnterPressed += searchTextBox_EnterPressed;
            ContextualHelp contextHelp52 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#tenkb");
            searchTextBox.SetContextualHelp(contextHelp52);
        }

        private void CreateAdditionalPluginsPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            PushButtonData toolPalettesPushButtonData = new PushButtonData("Tool Palettes", "Tool Palettes", AddInPath, "KbBimstore.ToolPalettes");
            KbBimstoreApp.toolPalettesPushButton = AddItemToCorrectPanel(toolPalettesPushButtonData, toolbar.Name, toolbar);
            toolPalettesPushButton.ToolTip = "Double Click On The Tool You Want And Place Your Object";
            toolPalettesPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ToolPalettes_S.png"), UriKind.Absolute));
            toolPalettesPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ToolPalettes_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp30 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#eighteenkb");
            toolPalettesPushButton.SetContextualHelp(contextHelp30);


            PushButtonData setDoorOffsetPushButtonData = new PushButtonData("Set Door Offset", "Set Door Offset", AddInPath, "KbBimstore.SetDoorOffset");
            KbBimstoreApp.setDoorOffsetPushButton = AddItemToCorrectPanel(setDoorOffsetPushButtonData, toolbar.Name, toolbar);
            setDoorOffsetPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "SetDoorOffset_S.png"), UriKind.Absolute));
            setDoorOffsetPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "SetDoorOffset_L.png"), UriKind.Absolute));
            setDoorOffsetPushButton.ToolTip = "Select doors and walls in order to set offset. Press ESC key when finished.";
            ContextualHelp contextHelp31 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#nighteenkb");
            setDoorOffsetPushButton.SetContextualHelp(contextHelp31);


            PushButtonData removeDwgImportsPushButtonData = new PushButtonData("Remove All DWG Import Files", "Remove All DWG Import Files", AddInPath, "KbBimstore.RemoveDwgImports");
            KbBimstoreApp.removeDwgImportsPushButton = AddItemToCorrectPanel(removeDwgImportsPushButtonData, toolbar.Name, toolbar);
            removeDwgImportsPushButton.ToolTip = "Remove All DWG Import Files";
            removeDwgImportsPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RemoveDwgImports_S.png"), UriKind.Absolute));
            removeDwgImportsPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RemoveDwgImports_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp32 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#twentykb");
            removeDwgImportsPushButton.SetContextualHelp(contextHelp32);


            PushButtonData viewDepthOverridePushButtonData = new PushButtonData("View Depth Override", "View Depth Override", AddInPath, "KbBimstore.ViewDepthOverrideCommand");
            KbBimstoreApp.viewDepthOverridePushButton = AddItemToCorrectPanel(viewDepthOverridePushButtonData, toolbar.Name, toolbar);
            viewDepthOverridePushButton.ToolTip = "Use This Tool To Automatically Base Your Elevations On Depth";
            viewDepthOverridePushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ViewDepthOverride_S.png"), UriKind.Absolute));
            viewDepthOverridePushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ViewDepthOverride_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp33 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#twentyonekb");
            viewDepthOverridePushButton.SetContextualHelp(contextHelp33);


            PushButtonData exportToExcelPushButtonData = new PushButtonData("Export To Excel", "Export To Excel", AddInPath, "KbBimstore.ExportToExcel");
            KbBimstoreApp.exportToExcelPushButton = AddItemToCorrectPanel(exportToExcelPushButtonData, toolbar.Name, toolbar);
            exportToExcelPushButton.ToolTip = "Exports All Of Your Family Info Into Excel";
            exportToExcelPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ExportToExcel_S.png"), UriKind.Absolute));
            exportToExcelPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "ExportToExcel_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp34 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#twentytwokb");
            exportToExcelPushButton.SetContextualHelp(contextHelp34);

            PushButtonData dwgFamilyRemoverPushButtonData = new PushButtonData("DWG Remover", "DWG Remover", AddInPath, "KbBimstore.RemoveDWGCommand");
            KbBimstoreApp.dwgFamilyRemoverPushButton = AddItemToCorrectPanel(dwgFamilyRemoverPushButtonData, toolbar.Name, toolbar);
            dwgFamilyRemoverPushButton.ToolTip = "Remove specific DWG files from loaded Revit Families";
            //dwgFamilyRemoverPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DWG_Remover_S.png"), UriKind.Absolute));
            //dwgFamilyRemoverPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DWG_Remover_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp35 = new ContextualHelp(ContextualHelpType.Url, "https://kb-bimstore.com/contextual-help/#twentythreekb");
            dwgFamilyRemoverPushButton.SetContextualHelp(contextHelp35);

            PushButtonData scheduleSpellCheckButton = new PushButtonData("Schedule Spell Check", "Schedule Spell Check", AddInPath, "KbBimstore.ScheduleSpellCheckCommand");
            KbBimstoreApp.scheduleSpellCheckButton = AddItemToCorrectPanel(scheduleSpellCheckButton, toolbar.Name, toolbar);
            //scheduleSpellCheckButton.ToolTip = "Remove specific DWG files from loaded Revit Families";
            //scheduleSpellCheckButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DWG_Remover_S.png"), UriKind.Absolute));
            //scheduleSpellCheckButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "DWG_Remover_L.png"), UriKind.Absolute));
            //ContextualHelp contextHelp36 = new ContextualHelp(ContextualHelpType.Url, "https://kb-bimstore.com/contextual-help/#twentythreekb");
            //scheduleSpellCheckButton.SetContextualHelp(contextHelp35);
        }

        private void CreateRenumberToolsPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            PushButtonData renumberByRoomsPushButtonData = new PushButtonData("Renumber By Rooms", "Rooms", AddInPath, "KbBimstore.RenumberByRooms");
            KbBimstoreApp.renumberByRoomsPushButton = AddItemToCorrectPanel(renumberByRoomsPushButtonData, toolbar.Name, toolbar);
            renumberByRoomsPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberByRooms_S.png"), UriKind.Absolute));
            renumberByRoomsPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberByRooms_L.png"), UriKind.Absolute));
            renumberByRoomsPushButton.ToolTip = "Select Rooms in order to be renumbered. Press ESC key when finished.";
            ContextualHelp contextHelp12 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#twelvelkb");
            renumberByRoomsPushButton.SetContextualHelp(contextHelp12);


            PushButtonData renumberByDoorsPushButtonData = new PushButtonData("Renumber By Doors", "Doors", AddInPath, "KbBimstore.RenumberByDoors");
            KbBimstoreApp.renumberByDoorsPushButton = AddItemToCorrectPanel(renumberByDoorsPushButtonData, toolbar.Name, toolbar);
            renumberByDoorsPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberByDoors_S.png"), UriKind.Absolute));
            renumberByDoorsPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberByDoors_L.png"), UriKind.Absolute));
            renumberByDoorsPushButton.ToolTip = "Select Doors in order to be renumbered. Press ESC key when finished.";
            ContextualHelp contextHelp13 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#thirteenkb");
            renumberByDoorsPushButton.SetContextualHelp(contextHelp13);


            PushButtonData renumberViewPortsPushButtonData = new PushButtonData("Renumber View Ports", "ViewPorts", AddInPath, "KbBimstore.RenumberViewPorts");
            KbBimstoreApp.renumberViewPortsPushButton = AddItemToCorrectPanel(renumberViewPortsPushButtonData, toolbar.Name, toolbar);
            renumberViewPortsPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberViewPorts_S.png"), UriKind.Absolute));
            renumberViewPortsPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "RenumberViewPorts_L.png"), UriKind.Absolute));
            renumberViewPortsPushButton.ToolTip = "Select viewports in order to be renumbered. Press ESC key when finished.";
            ContextualHelp contextHelp14 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#fourteenkb");
            renumberViewPortsPushButton.SetContextualHelp(contextHelp14);


            PushButtonData automaticDoorRenumberPushButtonData = new PushButtonData("Automatic Door Renumber", "Automatic Door", AddInPath, "KbBimstore.AutomaticDoorRenumber");
            KbBimstoreApp.automaticDoorRenumberPushButton = AddItemToCorrectPanel(automaticDoorRenumberPushButtonData, toolbar.Name, toolbar);
            automaticDoorRenumberPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutomaticDoorRenumber_S.png"), UriKind.Absolute));
            automaticDoorRenumberPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutomaticDoorRenumber_L.png"), UriKind.Absolute));
            automaticDoorRenumberPushButton.ToolTip = "Select a room and a door to be renumbered.";
            ContextualHelp contextHelp15 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#fifteenkb");
            automaticDoorRenumberPushButton.SetContextualHelp(contextHelp15);


            PushButtonData automaticDoorRenumberToFromPushButtonData = new PushButtonData("Automatic Door Renumber To|From", "Automatic Door To|From", AddInPath, "KbBimstore.AutomaticDoorRenumberToFrom");
            KbBimstoreApp.automaticDoorRenumberToFromPushButton = AddItemToCorrectPanel(automaticDoorRenumberToFromPushButtonData, toolbar.Name, toolbar);
            automaticDoorRenumberToFromPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutomaticDoorRenumberToFrom_S.png"), UriKind.Absolute));
            automaticDoorRenumberToFromPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutomaticDoorRenumberToFrom_L.png"), UriKind.Absolute));
            automaticDoorRenumberToFromPushButton.ToolTip = "Select doors in order to be renumbered. Press ESC key when finished.";
            ContextualHelp contextHelp16 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#sixteenkb");
            automaticDoorRenumberToFromPushButton.SetContextualHelp(contextHelp16);
        }

        private void CreateWindowSizingPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            PushButtonData windowTileSizesPushButtonDataLeft = new PushButtonData("Left", "Left", AddInPath, "KbBimstore.WindowTileSizeLeft");
            KbBimstoreApp.windowTileSizesPushButtonLeft = AddItemToCorrectPanel(windowTileSizesPushButtonDataLeft, toolbar.Name, toolbar);
            windowTileSizesPushButtonLeft.ToolTip = "Left";
            windowTileSizesPushButtonLeft.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesLeft_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonLeft.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesLeft_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp20 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonLeft.SetContextualHelp(contextHelp20);


            PushButtonData windowTileSizesPushButtonDataRight = new PushButtonData("Right", "Right", AddInPath, "KbBimstore.WindowTileSizeRight");
            KbBimstoreApp.windowTileSizesPushButtonRight = AddItemToCorrectPanel(windowTileSizesPushButtonDataRight, toolbar.Name, toolbar);
            windowTileSizesPushButtonRight.ToolTip = "Right";
            windowTileSizesPushButtonRight.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesRight_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonRight.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesRight_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp21 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonRight.SetContextualHelp(contextHelp21);


            PushButtonData windowTileSizesPushButtonDataTop = new PushButtonData("Top", "Top", AddInPath, "KbBimstore.WindowTileSizeTop");
            KbBimstoreApp.windowTileSizesPushButtonTop = AddItemToCorrectPanel(windowTileSizesPushButtonDataTop, toolbar.Name, toolbar);
            windowTileSizesPushButtonTop.ToolTip = "Top";
            windowTileSizesPushButtonTop.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesTop_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonTop.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesTop_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp22 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonTop.SetContextualHelp(contextHelp22);


            PushButtonData windowTileSizesPushButtonDataBottom = new PushButtonData("Bottom", "Bottom", AddInPath, "KbBimstore.WindowTileSizeBottom");
            KbBimstoreApp.windowTileSizesPushButtonBottom = AddItemToCorrectPanel(windowTileSizesPushButtonDataBottom, toolbar.Name, toolbar);
            windowTileSizesPushButtonBottom.ToolTip = "Bottom";
            windowTileSizesPushButtonBottom.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesBottom_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonBottom.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesBottom_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp23 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonBottom.SetContextualHelp(contextHelp23);


            PushButtonData windowTileSizesPushButtonDataBigger = new PushButtonData("Bigger", "Bigger", AddInPath, "KbBimstore.WindowTileSizeBigger");
            KbBimstoreApp.windowTileSizesPushButtonBigger = AddItemToCorrectPanel(windowTileSizesPushButtonDataBigger, toolbar.Name, toolbar);
            windowTileSizesPushButtonBigger.ToolTip = "Bigger";
            windowTileSizesPushButtonBigger.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesBigger_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonBigger.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesBigger_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp24 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonBigger.SetContextualHelp(contextHelp24);


            PushButtonData windowTileSizesPushButtonDataSmaller = new PushButtonData("Smaller", "Smaller", AddInPath, "KbBimstore.WindowTileSizeSmaller");
            KbBimstoreApp.windowTileSizesPushButtonSmaller = AddItemToCorrectPanel(windowTileSizesPushButtonDataSmaller, toolbar.Name, toolbar);
            windowTileSizesPushButtonSmaller.ToolTip = "Smaller";
            windowTileSizesPushButtonSmaller.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesSmaller_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonSmaller.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesSmaller_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp25 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonSmaller.SetContextualHelp(contextHelp25);


            PushButtonData windowTileSizesPushButtonDataLoad = new PushButtonData("Load", "Load", AddInPath, "KbBimstore.WindowTileSizeLoad");
            KbBimstoreApp.windowTileSizesPushButtonLoad = AddItemToCorrectPanel(windowTileSizesPushButtonDataLoad, toolbar.Name, toolbar);
            windowTileSizesPushButtonLoad.ToolTip = "Load";
            windowTileSizesPushButtonLoad.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesLoad_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonLoad.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesLoad_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp26 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonLoad.SetContextualHelp(contextHelp26);


            PushButtonData windowTileSizesPushButtonDataSave = new PushButtonData("Save", "Save", AddInPath, "KbBimstore.WindowTileSizeSave");
            KbBimstoreApp.windowTileSizesPushButtonSave = AddItemToCorrectPanel(windowTileSizesPushButtonDataSave, toolbar.Name, toolbar);
            windowTileSizesPushButtonSave.ToolTip = "Save";
            windowTileSizesPushButtonSave.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesSave_S.png"), UriKind.Absolute));
            windowTileSizesPushButtonSave.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "WindowTileSizesSave_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp27 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#seventeenkb");
            windowTileSizesPushButtonSave.SetContextualHelp(contextHelp27);
        }

        private void CreateAutoSyncPanel(UIControlledApplication application, KbBimstoreToolbar toolbar)
        {
            PushButtonData autoSynchronizeStartPushButtonData = new PushButtonData("Start Auto Synchronize", "Start", AddInPath, "KbBimstore.AutoSynchronizeStart");
            KbBimstoreApp.autoSynchronizeStartPushButton = AddItemToCorrectPanel(autoSynchronizeStartPushButtonData, toolbar.Name, toolbar);
            autoSynchronizeStartPushButton.ToolTip = "This Tool Will Auto Synchronize Your Project In Intervals";
            autoSynchronizeStartPushButton.Image = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutoSynchronizeStart_S.png"), UriKind.Absolute));
            autoSynchronizeStartPushButton.LargeImage = new BitmapImage(new Uri(Path.Combine(ImagesFolderPath, "AutoSynchronizeStart_L.png"), UriKind.Absolute));
            ContextualHelp contextHelp28 = new ContextualHelp(ContextualHelpType.Url, "http://kb-bimstore.com/contextual-help/#elevenkb");
            autoSynchronizeStartPushButton.SetContextualHelp(contextHelp28);
        }

        void searchTextBox_EnterPressed(object sender, TextBoxEnterPressedEventArgs e)
        {
            //Autodesk.Revit.UI.TaskDialog.Show("Sender type", sender.GetType().ToString());
            if (sender is Autodesk.Revit.UI.TextBox)
            {
                Autodesk.Revit.UI.TextBox myTextBox = sender as Autodesk.Revit.UI.TextBox;
                if (myTextBox != null)
                {
                    string searchText = myTextBox.Value.ToString().Trim();
                    if (searchText.Length > 0)
                    {
                        System.Diagnostics.Process.Start("http://www.revitcity.com/downloads.php?action=search&keywords=" + searchText + "&user_name=&sort=score");
                    }
                }
            }
        }

        public static void CreateToolPalette(UIControlledApplication uicontapp)
        {
            DockablePaneId toolPaletteId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21258}"));

            try
            {
                DockablePaneProviderData data = new DockablePaneProviderData();

                data.FrameworkElement = ToolPaletUI as System.Windows.FrameworkElement;
                data.InitialState = new DockablePaneState();
                data.InitialState.DockPosition = DockPosition.Tabbed;
                data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;


                DockablePane myDocPane = uicontapp.GetDockablePane(toolPaletteId);
                if (myDocPane == null)
                {
                    uicontapp.RegisterDockablePane(toolPaletteId, "BIMeta Tool Palette", (ToolPaletUI as IDockablePaneProvider));
                }
            }
            catch (Exception ex)
            {
                try
                {
                    uicontapp.RegisterDockablePane(toolPaletteId, "BIMeta Tool Palette", (ToolPaletUI as IDockablePaneProvider));
                }
                catch (Exception ex2)
                {

                }
            }
        }

        private void CreateKbShortcuts(UIControlledApplication uicontapp)
        {
            try
            {
                String appDataDirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                String kbShortcutsDirPath = appDataDirPath + "\\Autodesk\\Revit\\" + uicontapp.ControlledApplication.VersionName;
                String kbShortcutsFilePath = kbShortcutsDirPath + "\\KeyboardShortcuts.xml";

                if (File.Exists(kbShortcutsFilePath))
                {
                    XmlDocument curXmlDocument = new XmlDocument();
                    curXmlDocument.Load(kbShortcutsFilePath);

                    if (curXmlDocument.DocumentElement != null)
                    {
                        foreach (XmlNode curXmlNode in curXmlDocument.DocumentElement.ChildNodes)
                        {
                            if (curXmlNode.Attributes.Count > 0)
                            {
                                XmlAttribute ShortcutsAttribute = curXmlNode.Attributes["Shortcuts"];
                                XmlAttribute CommandIdAttribute = curXmlNode.Attributes["CommandId"];
                                XmlAttribute CommandNameAttribute = curXmlNode.Attributes["CommandName"];

                                if (CommandIdAttribute != null)
                                {
                                    if (ShortcutsAttribute != null)
                                    {
                                        if (ShortcutsAttribute.Value == "Ctrl+K")
                                        {
                                            if (CommandIdAttribute.Value != "Dialog_Essentials_ImportInstanceExplode:Control_Essentials_ImportExplode")
                                            {
                                                curXmlNode.Attributes.Remove(ShortcutsAttribute);
                                            }
                                        }
                                    }

                                    if (CommandIdAttribute.Value == "Dialog_Essentials_ImportInstanceExplode:Control_Essentials_ImportExplode")
                                    {
                                        if (ShortcutsAttribute != null)
                                        {
                                            curXmlNode.Attributes["Shortcuts"].Value = "Ctrl+K";
                                        }
                                        else
                                        {
                                            XmlAttribute norXmlAttribute = curXmlDocument.CreateAttribute("Shortcuts");
                                            norXmlAttribute.Value = "Ctrl+K";
                                            curXmlNode.Attributes.Append(norXmlAttribute);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    curXmlDocument.Save(kbShortcutsFilePath);
                }
                else
                {
                    if (File.Exists(KbBimstoreApp.MyKeyboardShortcutsFilePath))
                    {
                        File.Copy(KbBimstoreApp.MyKeyboardShortcutsFilePath, kbShortcutsFilePath, true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }

    class SuperSearchHandler : System.Windows.Input.ICommand
    {
        private Document doc;
        private Autodesk.Revit.DB.View view;
        private UIControlledApplication uicontapp;

        public event EventHandler CanExecuteChanged;

        public SuperSearchHandler(UIControlledApplication uicontapp)
        {
            this.uicontapp = uicontapp;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (KbBimstoreApp.activeDoc != null)
                {
                    SuperFilterForm form = new SuperFilterForm(KbBimstoreApp.activeDoc);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class AvailabilityNoOpenDocument : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            if (a.ActiveUIDocument == null)
            {

                return true;
            }
            return false;
        }
    }

    [Transaction(TransactionMode.ReadOnly)]
    public class ShowDockableWindow : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            DockablePaneId dpid = new DockablePaneId(
              new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            DockablePane dp = commandData.Application
              .GetDockablePane(dpid);

            dp.Show();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.ReadOnly)]
    public class HideDockableWindow : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            DockablePaneId dpid = new DockablePaneId(
              new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            DockablePane dp = commandData.Application
              .GetDockablePane(dpid);

            dp.Hide();
            return Result.Succeeded;
        }
    }

    class SynchLockCallback : ICentralLockedCallback
    {
        public bool ShouldWaitForLockAvailability()
        {
            return false;
        }
    }
}
