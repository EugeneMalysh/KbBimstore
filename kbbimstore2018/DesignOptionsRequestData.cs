using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    public class DesignOptionsRequestData
    {
        public int viewsAmount = 0;
        public string scaleName = "";
        public string titleBlockName = "";

        public List<Tuple<string, string, string, string, string, string>> optionsInfos = new List<Tuple<string, string, string, string, string, string>>();
    }
}
