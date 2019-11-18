using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using System.Windows.Media.Imaging;

namespace KbBimstore
{

    public partial class ToolPaletteUI : Page, IDockablePaneProvider
    {
        #region Data
        private Guid m_targetGuid;
        private DockPosition m_position = DockPosition.Bottom;
        private int m_left = 1;
        private int m_right = 1;
        private int m_top = 1;
        private int m_bottom = 1;
        #endregion
                
        private Autodesk.Revit.UI.UIApplication uiapp = null;
        private Autodesk.Revit.UI.UIDocument uidoc = null;
        private Autodesk.Revit.DB.Document doc = null;
        private System.Drawing.Size previewSize = new System.Drawing.Size(160, 160);

        public readonly string[] plumbingKeywords = { "pipe", "plumbing", "sprinkler" };
        public readonly string[] mechanicalKeywords = { "duct", "air terminals", "mechanical" };
        public readonly string[] electricalKeywords = { "wire", "cable", "conduit", "electrical", "lighting", "communication", "fire alarm", "nurse", "security", "telephone" };

        Dictionary<string, TreeViewItem> categoryNodesDict = new Dictionary<string, TreeViewItem>();
        Dictionary<Tuple<string, string>, TreeViewItem> familyNodesDict = new Dictionary<Tuple<string, string>, TreeViewItem>();


        public ToolPaletteUI()
        {
            InitializeComponent();
        }

        private bool almEnabled = false;
        public void setAlmEnabled(bool enable)
        {
            this.almEnabled = enable;
            updateTreeView(doc, "mech");
        }

        public void init(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;

            if (this.uiapp != null)
            {
                updateTreeView(doc, "mech");
            }
        }


        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Tabbed;
            //DockablePaneId targetPane;
            //if (m_targetGuid == Guid.Empty)
            //    targetPane = null;
            //else targetPane = new DockablePaneId(m_targetGuid);
            //if (m_position == DockPosition.Tabbed)
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
            //if (m_position == DockPosition.Floating)
            //{
            //data.InitialState.SetFloatingRectangle(new Autodesk.Revit.UI.Rectangle(10, 710, 10, 710));
            //data.InitialState.DockPosition = DockPosition.Tabbed;
            //}
            //Log.Message("***Intial docking parameters***");
            //Log.Message(APIUtility.GetDockStateSummary(data.InitialState));
        }

        public void SetInitialDockingParameters(int left, int right, int top, int bottom, DockPosition position,
            Guid targetGuid)
        {
            m_position = position;
            m_left = left;
            m_right = right;
            m_top = top;
            m_bottom = bottom;
            m_targetGuid = targetGuid;
        }

        public void UpdateDoc(Document doc)
        {
            UIDocument d = new UIDocument(doc);
            this.uidoc = d;
            this.doc = doc;
            
            this.updateTreeView(doc, "mech");
        }

        private void updateTreeView(Document doc, string groupName)
        {
            if (this.almEnabled)
            {
                if (doc != null)
                {
                    try
                    {
                        string[] keywords;
                        if (groupName == "mech")
                        {
                            keywords = this.mechanicalKeywords;

                            this.buttonMechanical.IsEnabled = false;
                            this.buttonPlumbing.IsEnabled = true;
                            this.buttonElectrical.IsEnabled = true;
                        }
                        else if (groupName == "plum")
                        {
                            keywords = this.plumbingKeywords;

                            this.buttonMechanical.IsEnabled = true;
                            this.buttonPlumbing.IsEnabled = false;
                            this.buttonElectrical.IsEnabled = true;
                        }
                        else if (groupName == "elec")
                        {
                            keywords = this.electricalKeywords;

                            this.buttonMechanical.IsEnabled = true;
                            this.buttonPlumbing.IsEnabled = true;
                            this.buttonElectrical.IsEnabled = false;
                        }
                        else
                        {
                            return;
                        }

                        this.categoryNodesDict.Clear();
                        this.familyNodesDict.Clear();
                        this.treeViewTool.Items.Clear();

                        FilteredElementCollector docFilter = new FilteredElementCollector(doc).OfClass(typeof(ElementType));
                        if (docFilter != null)
                        {
                            StringBuilder strBld = new StringBuilder();

                            FilteredElementIdIterator docFilterIterator = docFilter.GetElementIdIterator();
                            while (docFilterIterator.MoveNext())
                            {
                                Element curElement = doc.GetElement(docFilterIterator.Current);
                                if (curElement != null)
                                {
                                    if (curElement is ElementType)
                                    {
                                        ElementType curElementType = curElement as ElementType;
                                        if (curElementType != null)
                                        {
                                            if (this.uidoc.CanPlaceElementType(curElementType))
                                            {
                                                string curFamilyName = curElementType.FamilyName;

                                                Category curCategory = curElementType.Category;
                                                if (curCategory != null)
                                                {
                                                    string curCategoryName = curCategory.Name;
                                                    foreach (string curKeyword in keywords)
                                                    {

                                                        if (curCategoryName.IndexOf(curKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                                        {
                                                            TreeViewItem curCategoryNode = null;
                                                            TreeViewItem curFamilyNode = null;
                                                            TreeViewItem curElementTypeNode = null;

                                                            if (this.categoryNodesDict.ContainsKey(curCategoryName))
                                                            {
                                                                curCategoryNode = categoryNodesDict[curCategoryName];
                                                            }
                                                            else
                                                            {
                                                                curCategoryNode = new TreeViewItem();
                                                                curCategoryNode.Header = curCategoryName;
                                                                curCategoryNode.Tag = null;

                                                                this.categoryNodesDict.Add(curCategoryName, curCategoryNode);
                                                                this.treeViewTool.Items.Add(curCategoryNode);
                                                            }

                                                            if (curCategoryNode != null)
                                                            {
                                                                Tuple<string, string> curFamCatTuple = new Tuple<string, string>(curCategoryName, curFamilyName);
                                                                if (this.familyNodesDict.ContainsKey(curFamCatTuple))
                                                                {
                                                                    curFamilyNode = this.familyNodesDict[curFamCatTuple];
                                                                }
                                                                else
                                                                {
                                                                    curFamilyNode = new TreeViewItem();
                                                                    curFamilyNode.Header = curFamilyName;
                                                                    curFamilyNode.Tag = null;

                                                                    this.familyNodesDict.Add(curFamCatTuple, curFamilyNode);
                                                                    curCategoryNode.Items.Add(curFamilyNode);
                                                                }
                                                            }

                                                            if (curFamilyNode != null)
                                                            {
                                                                curElementTypeNode = new TreeViewItem();
                                                                curElementTypeNode.Header = curElementType.Name;
                                                                curElementTypeNode.Tag = curElementType;

                                                                curFamilyNode.Items.Add(curElementTypeNode);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Autodesk.Revit.UI.TaskDialog.Show("Exception", ex.Message);
                    }
                }
            }
        }


        public void setItemImage(Bitmap btmap)
        {
            if (btmap != null)
            {
                try
                {
                    this.pictureBoxPalette.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(btmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void buttonMechanical_Click(object sender, RoutedEventArgs e)
        {
            this.pictureBoxPalette.Source = null;
            updateTreeView(doc, "mech");
        }

        private void buttonElectrical_Click(object sender, RoutedEventArgs e)
        {
            this.pictureBoxPalette.Source = null;
            updateTreeView(doc, "elec");
        }

        private void buttonPlumbing_Click(object sender, RoutedEventArgs e)
        {
            this.pictureBoxPalette.Source = null;
            updateTreeView(doc, "plum");
        }

        private void treeViewTool_DoubleClick(object sender, RoutedEventArgs e)
        {

        }

        private void DockableDialogs_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }

        private void treeViewTool_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is System.Windows.Controls.TreeView)
            {
                System.Windows.Controls.TreeView curTreeView = sender as System.Windows.Controls.TreeView;
                if (curTreeView != null)
                {
                    if (curTreeView.SelectedItem != null)
                    {
                        if (curTreeView.SelectedItem is TreeViewItem)
                        {
                            TreeViewItem selectedNode = curTreeView.SelectedItem as TreeViewItem;
                            if (selectedNode != null)
                            {
                                if (selectedNode.Tag != null)
                                {
                                    if (selectedNode.Tag is ElementType)
                                    {
                                        ElementType selectedElementType = selectedNode.Tag as ElementType;
                                        if (selectedElementType != null)
                                        {
                                            setItemImage(selectedElementType.GetPreviewImage(this.previewSize));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeViewTool_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.TreeView)
            {
                System.Windows.Controls.TreeView curTreeView = sender as System.Windows.Controls.TreeView;
                if (curTreeView != null)
                {
                    if (curTreeView.SelectedItem != null)
                    {
                        if (curTreeView.SelectedItem is TreeViewItem)
                        {
                            TreeViewItem selectedNode = curTreeView.SelectedItem as TreeViewItem;
                            if (selectedNode != null)
                            {
                                if (selectedNode.Tag != null)
                                {
                                    if (selectedNode.Tag is ElementType)
                                    {
                                        ElementType selectedElementType = selectedNode.Tag as ElementType;
                                        if (selectedElementType != null)
                                        {
                                            if (this.uidoc.CanPlaceElementType(selectedElementType))
                                            {
                                                this.uidoc.PostRequestForElementTypePlacement(selectedElementType);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}