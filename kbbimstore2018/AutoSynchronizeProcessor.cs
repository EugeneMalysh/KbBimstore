using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{
    
    class AutoSynchronizeProcessor
    {
        private bool startAutoSync = false;

        private List<FamilyInstance> selectedDoors = new List<FamilyInstance>();

        public AutoSynchronizeProcessor(bool start = true)
        {
            this.startAutoSync = start;
        }

        public void init()
        {
            try
            {
                if (this.startAutoSync)
                {
                    using (AutoSynchronizeForm form = new AutoSynchronizeForm())
                    {
                        DialogResult result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            if (KbBimstoreApp.autosynchTimer.Enabled) KbBimstoreApp.autosynchTimer.Stop();

                            AutoSyncData data = new AutoSyncData();
                            data.AutoSync = form.AutoSyncOn();
                            data.AutoSave = form.AutoSaveOn();
                            data.AutoSyncInterval = form.getAutoSyncInerval();
                            data.AutoSaveInterval = form.getAutoSaveInerval();

                            Update(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }

            KbBimstoreApp.hasUserOpenedAutoSyncDialog = true;
        }

        public static void Update(AutoSyncData data)
        {
            if (KbBimstoreApp.autosynchTimer.Enabled) KbBimstoreApp.autosynchTimer.Stop();

            if (data.AutoSync)
            {
                KbBimstoreApp.autosynchInterval = data.AutoSyncInterval;
                KbBimstoreApp.AutoSync();
                KbBimstoreApp.autosynchTimer.Start();
                KbBimstoreApp.autosynchIsActive = true;
            }
            else if (KbBimstoreApp.autosynchIsActive)
            {
                KbBimstoreApp.autosynchIsActive = false;
            }

            if (data.AutoSave)
            {
                KbBimstoreApp.autoSaveInterval = data.AutoSaveInterval;
                KbBimstoreApp.autoSaveIsActive = true;

                if (!KbBimstoreApp.autosynchIsActive) //don't try to start a running timer
                {
                    KbBimstoreApp.autosynchTimer.Start();
                }
            }
            else if (KbBimstoreApp.autoSaveIsActive)
            {
                KbBimstoreApp.autoSaveIsActive = false;
            }

            KbBimstoreApp.hasUserOpenedAutoSyncDialog = true;

            data.Serialize(KbBimstoreApp.AutoSyncSettingsFilePath);
        }
    }
}
