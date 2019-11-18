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
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    class PageAlignmentToolProcessor
    {
        bool scaleResult = false;
        private int selectedScale = -1;
        private PageAlignmentToolForm form;
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Dictionary<ElementId, XYZ> selViewportsIdsAndPositions;

        public PageAlignmentToolProcessor(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.doc = uiapp.ActiveUIDocument.Document;
            this.selViewportsIdsAndPositions = new Dictionary<ElementId, XYZ>();

            form = new PageAlignmentToolForm(this.uiapp);
            form.getScaleComboBox().SelectedValueChanged += PageAlignmentToolProcessor_SelectedValueChanged;
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                TaskDialog.Show("Info", "Please select viewport on the sheet and drag it");

                geSelectedtViewportsIdsAndPositions();

                this.uiapp.ViewActivating += uiapp_ViewActivating;
                this.uiapp.ViewActivated += uiapp_ViewActivated;
                this.uiapp.Idling += uiapp_Idling;
            }
        }

        private void PageAlignmentToolProcessor_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedScaleStr = form.getScaleComboBox().SelectedItem.ToString();
            this.selectedScale = KbBimstoreConst.getScaleValue(selectedScaleStr);
        }

        private void geSelectedtViewportsIdsAndPositions()
        {
            this.selViewportsIdsAndPositions.Clear();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc).OfClass(typeof(Autodesk.Revit.DB.Viewport));
            if (docFilter != null)
            {
                FilteredElementIterator docFilterIterator = docFilter.GetElementIterator();
                while (docFilterIterator.MoveNext())
                {
                    Autodesk.Revit.DB.Viewport curViewport = docFilterIterator.Current as Autodesk.Revit.DB.Viewport;
                    if (curViewport != null)
                    {
                        ElementId curViewId = curViewport.ViewId;
                        if (curViewId != null)
                        {
                            Autodesk.Revit.DB.View curView = doc.GetElement(curViewId) as Autodesk.Revit.DB.View;
                            if (curView != null)
                            {
                                if (curView.Scale == this.selectedScale)
                                {
                                    XYZ curXYZ = curViewport.GetBoxCenter();
                                    this.selViewportsIdsAndPositions.Add(curViewport.Id, curXYZ);
                                }
                            }
                        }
                    }
                }
            }
        }


        private bool updatePositionsInit(Autodesk.Revit.DB.ViewSheet curViewSheet)
        {
            Autodesk.Revit.DB.XYZ norXYZ = null;
            Autodesk.Revit.DB.ElementId deltaViewportId = null;

            if (curViewSheet != null)
            {
                ICollection<ElementId> curViewportsIdsCollection = curViewSheet.GetAllViewports();
                foreach (ElementId curViewportId in curViewportsIdsCollection)
                {
                    if (this.selViewportsIdsAndPositions.ContainsKey(curViewportId))
                    {
                        Autodesk.Revit.DB.Viewport curViewport = doc.GetElement(curViewportId) as Autodesk.Revit.DB.Viewport;
                        if (curViewport != null)
                        {
                            norXYZ = curViewport.GetBoxCenter();

                            break;
                        }
                    }
                }
            }

            List<ElementId> viewportIds = new List<ElementId>(this.selViewportsIdsAndPositions.Keys);
            PageAlignmentToolRequestHandler handler = new PageAlignmentToolRequestHandler(norXYZ, deltaViewportId, viewportIds);
            ExternalEvent exEvent = ExternalEvent.Create(handler);
            exEvent.Raise();

            return true;
        }

        private bool updatePositions(Autodesk.Revit.DB.ViewSheet curViewSheet)
        {
            Autodesk.Revit.DB.XYZ norXYZ = null;
            Autodesk.Revit.DB.XYZ deltaXYZ = null;
            Autodesk.Revit.DB.ElementId deltaViewportId = null;

            if (curViewSheet != null)
            {
                ICollection<ElementId> curViewportsIdsCollection = curViewSheet.GetAllViewports();
                foreach (ElementId curViewportId in curViewportsIdsCollection)
                {
                    if (this.selViewportsIdsAndPositions.ContainsKey(curViewportId))
                    {
                        Autodesk.Revit.DB.Viewport curViewport = doc.GetElement(curViewportId) as Autodesk.Revit.DB.Viewport;
                        if (curViewport != null)
                        {
                            norXYZ = curViewport.GetBoxCenter();
                            Autodesk.Revit.DB.XYZ preXYZ = this.selViewportsIdsAndPositions[curViewportId];
                            Autodesk.Revit.DB.XYZ curXYZ = curViewport.GetBoxCenter();

                            if ((curXYZ.X != preXYZ.X) || (curXYZ.Y != preXYZ.Y) || (curXYZ.Z != preXYZ.Z))
                            {
                                deltaViewportId = curViewportId;
                                deltaXYZ = new XYZ((curXYZ.X - preXYZ.X), (curXYZ.Y - preXYZ.Y), (curXYZ.Z - preXYZ.Z));

                                break;
                            }
                        }
                    }
                }
            }

            if ((deltaXYZ != null) && (deltaViewportId != null))
            {
                List<ElementId> viewportIds = new List<ElementId>(this.selViewportsIdsAndPositions.Keys);
                PageAlignmentToolRequestHandler handler = new PageAlignmentToolRequestHandler(norXYZ, deltaViewportId, viewportIds);
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                exEvent.Raise();

                return true;
            }

            return false;
        }

        private void uiapp_ViewActivating(object sender, Autodesk.Revit.UI.Events.ViewActivatingEventArgs e)
        {
            geSelectedtViewportsIdsAndPositions();
        }

        private void uiapp_ViewActivated(object sender, Autodesk.Revit.UI.Events.ViewActivatedEventArgs e)
        {
            if (doc.ActiveView.GetType().Name == "ViewSheet")
            {
                Autodesk.Revit.DB.ViewSheet curViewSheet = doc.ActiveView as Autodesk.Revit.DB.ViewSheet;

                if (curViewSheet != null)
                {
                    //updatePositionsInit(curViewSheet);
                }
            }
        }

        private void uiapp_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            if (doc.ActiveView.GetType().Name == "ViewSheet")
            {
                Autodesk.Revit.DB.ViewSheet curViewSheet = doc.ActiveView as Autodesk.Revit.DB.ViewSheet;

                if (curViewSheet != null)
                {
                    if (updatePositions(curViewSheet))
                    {
                        this.uiapp.ViewActivating -= uiapp_ViewActivating;
                        this.uiapp.ViewActivated -= uiapp_ViewActivated;
                        this.uiapp.Idling -= uiapp_Idling;
                    }
                }
            }
        }
    }
}
