using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    public partial class ToolPalettesOldForm : System.Windows.Forms.Form
    {
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.DB.Document doc;
        private System.Drawing.Size previewSize;

        public readonly string[] plumbingKeywords = { "pipe", "plumbing", "sprinkler" };
        public readonly string[] mechanicalKeywords = { "duct", "air terminals", "mechanical" };
        public readonly string[] electricalKeywords = { "wire", "cable", "conduit", "electrical", "lighting", "communication", "fire alarm", "nurse", "security", "telephone" };

        Dictionary<string, TreeNode> categoryNodesDict = new Dictionary<string, TreeNode>();
        Dictionary<Tuple<string, string>, TreeNode> familyNodesDict = new Dictionary<Tuple<string, string>, TreeNode>();

        public ToolPalettesOldForm(UIApplication uiapp)
        {
            InitializeComponent();

            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;

            previewSize = this.pictureBoxPalette.Size;
            updateTreeView(doc, "mech");
            this.Show();
        }

        private void updateTreeView(Document doc, string groupName)
        {
            string[] keywords;
            if (groupName == "mech")
            {
                keywords = this.mechanicalKeywords;

                this.buttonMechanical.Enabled = false;
                this.buttonPlumbing.Enabled = true;
                this.buttonElectrical.Enabled = true;

                this.buttonMechanical.FlatStyle = FlatStyle.Popup;
                this.buttonPlumbing.FlatStyle = FlatStyle.Standard;
                this.buttonElectrical.FlatStyle = FlatStyle.Standard;
            }
            else if (groupName == "plum")
            {
                keywords = this.plumbingKeywords;

                this.buttonMechanical.Enabled = true;
                this.buttonPlumbing.Enabled = false;
                this.buttonElectrical.Enabled = true;

                this.buttonMechanical.FlatStyle = FlatStyle.Standard;
                this.buttonPlumbing.FlatStyle = FlatStyle.Popup;
                this.buttonElectrical.FlatStyle = FlatStyle.Standard;
            }
            else if (groupName == "elec")
            {
                keywords = this.electricalKeywords;

                this.buttonMechanical.Enabled = true;
                this.buttonPlumbing.Enabled = true;
                this.buttonElectrical.Enabled = false;

                this.buttonMechanical.FlatStyle = FlatStyle.Standard;
                this.buttonPlumbing.FlatStyle = FlatStyle.Standard;
                this.buttonElectrical.FlatStyle = FlatStyle.Popup;
            }
            else
            {
                return;
            }

            this.categoryNodesDict.Clear();
            this.familyNodesDict.Clear();
            this.treeViewTool.Nodes.Clear();

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
                                                TreeNode curCategoryNode = null;
                                                TreeNode curFamilyNode = null;
                                                TreeNode curElementTypeNode = null;

                                                if (this.categoryNodesDict.ContainsKey(curCategoryName))
                                                {
                                                    curCategoryNode = categoryNodesDict[curCategoryName];
                                                }
                                                else
                                                {
                                                    curCategoryNode = new TreeNode(curCategoryName);
                                                    curCategoryNode.Tag = null;

                                                    this.categoryNodesDict.Add(curCategoryName, curCategoryNode);
                                                    this.treeViewTool.Nodes.Add(curCategoryNode);
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
                                                        curFamilyNode = new TreeNode(curFamilyName);
                                                        curFamilyNode.Tag = null;

                                                        this.familyNodesDict.Add(curFamCatTuple, curFamilyNode);
                                                        curCategoryNode.Nodes.Add(curFamilyNode);
                                                    }
                                                }

                                                if (curFamilyNode != null)
                                                {
                                                    curElementTypeNode = new TreeNode(curElementType.Name);
                                                    curElementTypeNode.Tag = curElementType;

                                                    curFamilyNode.Nodes.Add(curElementTypeNode);
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

        private void treeViewTool_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender is TreeView)
            {
                TreeView curTreeView = sender as TreeView;
                if (curTreeView != null)
                {
                    if (curTreeView.SelectedNode != null)
                    {
                        if (curTreeView.SelectedNode.Tag != null)
                        {
                            if (curTreeView.SelectedNode.Tag is ElementType)
                            {
                                ElementType selectedElementType = curTreeView.SelectedNode.Tag as ElementType;
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


        public void setItemImage(Bitmap btmap)
        {
            if (btmap != null)
            {
                try
                {
                    this.pictureBoxPalette.Image = btmap;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void buttonMechanical_Click(object sender, EventArgs e)
        {
            this.pictureBoxPalette.Image = null;
            updateTreeView(doc, "mech");
        }

        private void buttonElectrical_Click(object sender, EventArgs e)
        {
            this.pictureBoxPalette.Image = null;
            updateTreeView(doc, "elec");
        }

        private void buttonPlumbing_Click(object sender, EventArgs e)
        {
            this.pictureBoxPalette.Image = null;
            updateTreeView(doc, "plum");
        }

        private void treeViewTool_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TreeView)
            {
                TreeView curTreeView = sender as TreeView;
                if (curTreeView != null)
                {
                    if (curTreeView.SelectedNode != null)
                    {
                        if (curTreeView.SelectedNode.Tag != null)
                        {
                            if (curTreeView.SelectedNode.Tag is ElementType)
                            {
                                ElementType selectedElementType = curTreeView.SelectedNode.Tag as ElementType;
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
