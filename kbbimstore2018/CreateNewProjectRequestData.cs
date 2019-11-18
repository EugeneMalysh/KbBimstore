using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    public class CreateNewProjectRequestData
    {
        public string titleBlockName = "";
        public double levelsDistance = 0.0d;
        public int levelsAmount = 0;
        public int frontSheetsAmount = 0;
        public int mainSheetsAmount = 0;
        public string mainSheetsScale = "";
        public List<Tuple<string, string, string>> frontSheetsInfo = new List<Tuple<string, string, string>>();
        public List<Tuple<string, string, string>> mainSheetsInfo = new  List<Tuple<string, string, string>>();
    }
}
