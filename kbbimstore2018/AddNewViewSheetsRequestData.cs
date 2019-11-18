using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    public class AddNewViewSheetsRequestData
    {
        public List<Tuple<string, string, string, string, string>> viewSheetsInfo = new List<Tuple<string, string, string, string, string>>();
    }
}
