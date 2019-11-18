using System;
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
using Autodesk.Revit.DB.Lighting;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.ApplicationServices;

namespace KbBimstore
{
    public partial class SuperFilterForm : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document doc;
        Autodesk.Revit.UI.UIDocument uidoc;
        HashSet<ElementId> filteredElementsIds = new HashSet<ElementId>();
        HashSet<Tuple<string, string, string>> selectedTuples = new HashSet<Tuple<string, string, string>>();
        SortedDictionary<Tuple<string, string, string>, HashSet<ElementId>> tuplesToIdsDict = new SortedDictionary<Tuple<string, string, string>, HashSet<ElementId>>();

        public SuperFilterForm(Autodesk.Revit.DB.Document doc)
        {
            InitializeComponent();

            try
            {
                this.doc = doc;
                this.uidoc = new UIDocument(this.doc);

                if (this.uidoc != null)
                {
                    InitUI();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitUI()
        {
            tuplesToIdsDict.Clear();

            ICollection<ElementId> selectedElementsIds = this.uidoc.Selection.GetElementIds();
            if (selectedElementsIds.Count > 0)
            {
                foreach (ElementId selElemId in selectedElementsIds)
                {
                    Element selElem = doc.GetElement(selElemId);

                    if (selElem != null)
                    {
                        string categoryName = "";
                        string familyName = "";
                        string typeName = "";

                        foreach (Parameter param in selElem.Parameters)
                        {
                            if (param.Definition.Name == "Category")
                            {
                                categoryName = param.AsValueString();
                            }
                            else if (param.Definition.Name == "Family")
                            {
                                familyName = param.AsValueString();
                            }
                            else if (param.Definition.Name == "Type Id")
                            {
                                ElementId elemTypeId = param.AsElementId();
                                if (elemTypeId != null)
                                {
                                    Element elemType = doc.GetElement(elemTypeId);
                                    if (elemType != null)
                                    {
                                        typeName = elemType.Name;
                                    }
                                }
                            }
                        }

                        if (categoryName != "Views")
                        {
                            Tuple<string, string, string> elemTuple = new Tuple<string, string, string>(categoryName, familyName, typeName);
                            if (tuplesToIdsDict.ContainsKey(elemTuple))
                            {
                                tuplesToIdsDict[elemTuple].Add(selElemId);
                            }
                            else
                            {
                                HashSet<ElementId> elemIdsSet = new HashSet<ElementId>();
                                elemIdsSet.Add(selElemId);
                                tuplesToIdsDict.Add(elemTuple, elemIdsSet);
                            }
                        }
                    }
                }

                SortedDictionary<string, SortedSet<string>> parentTreeNodes = new SortedDictionary<string, SortedSet<string>>();
                foreach (KeyValuePair<Tuple<string, string, string>, HashSet<ElementId>> curKeyValuePair in tuplesToIdsDict)
                {
                    string catName = curKeyValuePair.Key.Item1;
                    string famName = curKeyValuePair.Key.Item2;

                    if (parentTreeNodes.ContainsKey(catName))
                    {
                        parentTreeNodes[catName].Add(famName);
                    }
                    else
                    {
                        SortedSet<string> norFamNames = new SortedSet<string>();
                        norFamNames.Add(famName);
                        parentTreeNodes.Add(catName, norFamNames);
                    }
                }

                Dictionary<Tuple<string,string>, SortedSet<string>> lastNodesDict = new Dictionary<Tuple<string,string>, SortedSet<string>>();
                foreach (KeyValuePair<Tuple<string, string, string>, HashSet<ElementId>> curKeyValuePair in tuplesToIdsDict)
                {
                    Tuple<string, string, string> curTriple = curKeyValuePair.Key;
                    
                    Tuple<string, string> curDuple = new Tuple<string,string>(curTriple.Item1, curTriple.Item2);
                    if(lastNodesDict.ContainsKey(curDuple))
                    {
                        lastNodesDict[curDuple].Add(curTriple.Item3);
                    }
                    else
                    {
                        SortedSet<string> norSortedSet = new SortedSet<string>();
                        norSortedSet.Add(curTriple.Item3);
                        lastNodesDict.Add(curDuple, norSortedSet);
                    }
                }


                this.treeViewFilter.Nodes.Clear();
                foreach (KeyValuePair<string, SortedSet<string>> curKeyValuePair in parentTreeNodes)
                {
                    int f = 0;
                    string catName = curKeyValuePair.Key;
                    TreeNode[] famNodes = new TreeNode[curKeyValuePair.Value.Count];
                    
                    foreach (string famName in curKeyValuePair.Value)
                    {
                        TreeNode[] lastNodes = null;

                        Tuple<string, string> curDuple = new Tuple<string, string>(catName, famName);
                        if (lastNodesDict.ContainsKey(curDuple))
                        {
                            lastNodes = new TreeNode[lastNodesDict[curDuple].Count];

                            int l = 0;
                            foreach (string lastNodeName in lastNodesDict[curDuple])
                            {
                                lastNodes[l] = new TreeNode(lastNodeName);
                                lastNodes[l].Checked = true;
                                l++;
                            }
                        }

                        if (lastNodes != null)
                        {
                            famNodes[f] = new TreeNode(famName, lastNodes);
                        }
                        else
                        {
                            famNodes[f] = new TreeNode(famName);
                        }
                        famNodes[f].Checked = true;
                        f++;
                    }

                    
                    TreeNode norCatNode = new TreeNode(catName, famNodes);
                    norCatNode.Checked = true;

                    this.treeViewFilter.Nodes.Add(norCatNode);
                }


                this.ShowDialog();
            }
            else
            {
                TaskDialog.Show("Info", "Please select elements");
            }
        }

        private void updateFiltering()
        {            
            selectedTuples.Clear();
            foreach (TreeNode nodeL0 in this.treeViewFilter.Nodes)
            {
                if (nodeL0.Checked)
                {
                    selectedTuples.Add(new Tuple<string, string, string>(nodeL0.Text, "", ""));
                }
                else
                {
                    foreach (TreeNode nodeL1 in nodeL0.Nodes)
                    {
                        if (nodeL1.Checked)
                        {
                            selectedTuples.Add(new Tuple<string, string, string>(nodeL0.Text, nodeL1.Text, ""));
                        }
                        else
                        {
                            foreach (TreeNode nodeL2 in nodeL1.Nodes)
                            {
                                if (nodeL2.Checked)
                                {
                                    selectedTuples.Add(new Tuple<string, string, string>(nodeL0.Text, nodeL1.Text, nodeL2.Text));
                                }
                            }
                        }
                    }
                }
            }


            
            filteredElementsIds.Clear();
            foreach (KeyValuePair<Tuple<string, string, string>, HashSet<ElementId>> keyValuePair in tuplesToIdsDict)
            {
                bool isInculded = false;
                Tuple<string, string, string> curTuple = keyValuePair.Key;

                Tuple<string, string, string> checkTuple1 = new Tuple<string, string, string>(curTuple.Item1, "", "");
                if (selectedTuples.Contains(checkTuple1))
                {
                    isInculded = true;
                }
                else
                {
                    Tuple<string, string, string> checkTuple2 = new Tuple<string, string, string>(curTuple.Item1, curTuple.Item2, "");
                    if (selectedTuples.Contains(checkTuple2))
                    {
                        isInculded = true;
                    }
                    else
                    {
                        Tuple<string, string, string> checkTuple3 = new Tuple<string, string, string>(curTuple.Item1, curTuple.Item2, curTuple.Item3);
                        if (selectedTuples.Contains(checkTuple3))
                        {
                            isInculded = true;
                        }
                    }
                }

                if (isInculded)
                {
                    foreach (ElementId elemId in keyValuePair.Value)
                    {
                        filteredElementsIds.Add(elemId);
                    }
                }
            }

            uidoc.Selection.SetElementIds(filteredElementsIds);

        }

        private void treeViewFilter_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null)
            {
                TreeNode parentNode = e.Node.Parent;
                if (parentNode != null)
                {
                    if (parentNode.Checked && e.Node.Checked)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void treeViewFilter_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                foreach (TreeNode childNode in e.Node.Nodes)
                {
                    childNode.Checked = e.Node.Checked;
                }

                updateFiltering();
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            updateFiltering();

            this.Close();
            this.Dispose();
        }
    }
}
