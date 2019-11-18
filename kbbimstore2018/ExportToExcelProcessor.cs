using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.CSharp;
using Microsoft.Office.Interop;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;


namespace KbBimstore
{

    class ExportToExcelProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Dictionary<string, List<Element>> elementsByCategory = new Dictionary<string, List<Element>>();


        public ExportToExcelProcessor(Autodesk.Revit.UI.UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;
        }

        public void init()
        {
            export(this.doc);
        }

        private void export(Document doc)
        {
            elementsByCategory.Clear();

            FilteredElementCollector docFilter = new FilteredElementCollector(doc, doc.ActiveView.Id).WhereElementIsNotElementType();
            if (docFilter != null)
            {
                try
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    if (excelApp != null)
                    {
                        FilteredElementIdIterator docFilterIterator = docFilter.GetElementIdIterator();
                        while (docFilterIterator.MoveNext())
                        {
                            Element elem = doc.GetElement(docFilterIterator.Current);
                            if (elem != null)
                            {
                                Category elemCategory = elem.Category;
                                if (elemCategory != null)
                                {
                                    string elemCatName = elemCategory.Name;
                                    if (elementsByCategory.ContainsKey(elemCatName))
                                    {
                                        elementsByCategory[elemCatName].Add(elem);
                                    }
                                    else
                                    {
                                        List<Element> norElementsList = new List<Element>();
                                        norElementsList.Add(elem);
                                        elementsByCategory.Add(elemCatName, norElementsList);
                                    }
                                }
                            }
                        }

                        excelApp.Visible = true;

                        List<string> keys = new List<string>(elementsByCategory.Keys);

                        keys.Sort();
                        keys.Reverse();

                        bool first = true;

                        int nElements = 0;
                        int nCategories = keys.Count;

                        Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(Missing.Value);
                        if (excelWorkbook != null)
                        {
                            foreach (string categoryName in keys)
                            {
                                List<Element> elementSet = elementsByCategory[categoryName];

                                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = excelWorkbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value) as Microsoft.Office.Interop.Excel.Worksheet;
                                if (excelWorksheet != null)
                                {
                                    string name = "";

                                    name = (31 < categoryName.Length) ? categoryName.Substring(0, 31) : categoryName;
                                    name = name.Replace(':', '_').Replace('/', '_');

                                    excelWorksheet.Name = name;

                                    List<string> paramNames = new List<string>();
                                    foreach (Element elem in elementSet)
                                    {
                                        ParameterSet parameters = elem.Parameters;

                                        foreach (Parameter parameter in parameters)
                                        {
                                            name = parameter.Definition.Name;

                                            if (!paramNames.Contains(name))
                                            {
                                                paramNames.Add(name);
                                            }
                                        }
                                    }
                                    paramNames.Sort();

                                    excelWorksheet.Cells[1, 1] = "ID";
                                    excelWorksheet.Cells[1, 2] = "IsType";

                                    int column = 3;

                                    foreach (string paramName in paramNames)
                                    {
                                        excelWorksheet.Cells[1, column] = paramName;
                                        ++column;
                                    }
                                    var range = excelWorksheet.get_Range("A1", "Z1");

                                    range.Font.Bold = true;
                                    range.EntireColumn.AutoFit();

                                    int row = 2;

                                    foreach (Element elem in elementSet)
                                    {

                                        excelWorksheet.Cells[row, 1] = elem.Id.IntegerValue;

                                        excelWorksheet.Cells[row, 2] = (elem is ElementType) ? 1 : 0;
                                        column = 3;

                                        string paramValue;

                                        foreach (string paramName in paramNames)
                                        {
                                            paramValue = getParameterValue(elem, paramName);
                                            if (paramValue != null)
                                            {
                                                excelWorksheet.Cells[row, column++] = paramValue;
                                            }
                                            else
                                            {
                                                excelWorksheet.Cells[row, column++] = "*NA*";
                                            }
                                        }

                                        ++nElements;
                                        ++row;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        TaskDialog.Show("Error", "Please start Excel before exporting");
                    }

                    sw.Stop();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public string getParameterValue(Element elem, string paramname)
        {
            ParameterSet parameters = elem.Parameters;

            foreach (Parameter parameter in parameters)
            {
                string name = parameter.Definition.Name;

                if (name == paramname)
                {
                    return parameter.AsValueString();
                }
            }

            return null;
        }
   
    }
}
