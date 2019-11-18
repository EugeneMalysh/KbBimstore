using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

using Autodesk.Revit.UI;

namespace KbBimstore
{
    public class CadDetailConverterRequestData
    {
        public string selectedTextStyleName = "";
        public List<string> textStylesNames = new List<string>();
        public List<string> lineStylesNames = new List<string>();
        public Dictionary<Tuple<byte, byte, byte>, string> colorsToLineStyles = new Dictionary<Tuple<byte, byte, byte>, string>();

        public CadDetailConverterRequestData()
        {

        }

        public void SaveSettings(string filePath)
        {
            try
            {
                DataSet myDataSet = new DataSet("DataSet");

                
                DataTable SelectedTable = new DataTable("Selected");
                SelectedTable.Columns.Add("SelectedTextStyle", System.Type.GetType("System.String"));
                if (this.selectedTextStyleName != null)
                {
                    DataRow norDataRow = SelectedTable.NewRow();
                    norDataRow.SetField<string>("SelectedTextStyle", this.selectedTextStyleName);

                    SelectedTable.Rows.Add(norDataRow);
                }
                myDataSet.Tables.Add(SelectedTable);


                DataTable TextStylesTable = new DataTable("TextStyles");
                TextStylesTable.Columns.Add("TextStyle", System.Type.GetType("System.String"));
                if (this.textStylesNames.Count > 0)
                {
                    for (int r = 0; r < this.textStylesNames.Count; r++)
                    {
                        DataRow norDataRow = TextStylesTable.NewRow();
                        norDataRow.SetField<string>("TextStyle", this.textStylesNames[r]);

                        TextStylesTable.Rows.Add(norDataRow);
                    }
                }
                myDataSet.Tables.Add(TextStylesTable);


                DataTable LineStylesTable = new DataTable("LineStyles");
                LineStylesTable.Columns.Add("LineStyle", System.Type.GetType("System.String"));
                if (this.lineStylesNames.Count > 0)
                {
                    for (int r = 0; r < this.lineStylesNames.Count; r++)
                    {
                        DataRow norDataRow = LineStylesTable.NewRow();
                        norDataRow.SetField<string>("LineStyle", this.lineStylesNames[r]);

                        LineStylesTable.Rows.Add(norDataRow);
                    }
                }
                myDataSet.Tables.Add(LineStylesTable);

                
                DataTable ColorsToStylesTable = new DataTable("ColorsToStyles");
                ColorsToStylesTable.Columns.Add("Color", System.Type.GetType("System.String"));
                ColorsToStylesTable.Columns.Add("LineStyle", System.Type.GetType("System.String"));                
                if (this.colorsToLineStyles.Count > 0)
                {
                    foreach (KeyValuePair<Tuple<byte, byte, byte>, string> keyValuePair in this.colorsToLineStyles)
                    {
                        DataRow norDataRow = ColorsToStylesTable.NewRow();
                        string colorStr = keyValuePair.Key.Item1 + "," + keyValuePair.Key.Item2 + "," + keyValuePair.Key.Item3;
                        norDataRow.SetField<string>("Color", colorStr);
                        norDataRow.SetField<string>("LineStyle", keyValuePair.Value);

                        ColorsToStylesTable.Rows.Add(norDataRow);
                    }
                }             
                myDataSet.Tables.Add(ColorsToStylesTable);
                

                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));
                TextWriter writer = new StreamWriter(filePath);
                xmlSer.Serialize(writer, myDataSet);
                writer.Close();

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }
        }

        public void LoadSettings(string filePath)
        {
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));

                if (File.Exists(filePath))
                {
                    TextReader reader = new StreamReader(filePath);
                    DataSet myDataSet = (DataSet)xmlSer.Deserialize(reader);
                    reader.Close();

                    if ((!myDataSet.Tables.Contains("Selected")) || (!myDataSet.Tables.Contains("TextStyles")) || (!myDataSet.Tables.Contains("LineStyles")) || (!myDataSet.Tables.Contains("ColorsToStyles")))
                    {
                        return;
                    }

                    this.selectedTextStyleName = "";
                    DataTable SelectedTable = myDataSet.Tables["Selected"];
                    if (SelectedTable.Rows.Count > 0)
                    {
                        DataRow curDataRow = SelectedTable.Rows[0];
                        this.selectedTextStyleName = curDataRow.Field<string>("SelectedTextStyle");
                    }

                    this.textStylesNames.Clear();
                    DataTable TextStylesTable = myDataSet.Tables["TextStyles"];                    
                    for (int r = 0; r < TextStylesTable.Rows.Count; r++)
                    {
                        DataRow curDataRow = TextStylesTable.Rows[r];
                        this.textStylesNames.Add(curDataRow.Field<string>("TextStyle"));
                    }

                    this.lineStylesNames.Clear();
                    DataTable LineStylesTable = myDataSet.Tables["LineStyles"];
                    for (int r = 0; r < LineStylesTable.Rows.Count; r++)
                    {
                        DataRow curDataRow = LineStylesTable.Rows[r];
                        this.lineStylesNames.Add(curDataRow.Field<string>("LineStyle"));
                    }

                    this.colorsToLineStyles.Clear();
                    DataTable ColorsToStylesTable = myDataSet.Tables["ColorsToStyles"];
                    for (int r = 0; r < ColorsToStylesTable.Rows.Count; r++)
                    {
                        DataRow curDataRow = ColorsToStylesTable.Rows[r];

                        string colorStr = curDataRow.Field<string>("Color");
                        string curLineStyleName = curDataRow.Field<string>("LineStyle");

                        string[] rgbStrs = colorStr.Split(',');
                        if (rgbStrs != null)
                        {
                            if (rgbStrs.Length >= 3)
                            {
                                byte norR = Convert.ToByte(rgbStrs[0]);
                                byte norG = Convert.ToByte(rgbStrs[1]);
                                byte norB = Convert.ToByte(rgbStrs[2]);

                                Tuple<byte, byte, byte> norColorTuple = new Tuple<byte, byte, byte>(norR, norG, norB);
                                
                                this.colorsToLineStyles.Add(norColorTuple, curLineStyleName);
                            }
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
