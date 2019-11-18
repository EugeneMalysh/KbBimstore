using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    class CreateNewProjectModelLevel
    {
        public List<Tuple<string, int>> sheets = new List<Tuple<string, int>>();

        public CreateNewProjectModelLevel getMyCopy()
        {
            CreateNewProjectModelLevel myCopy = new CreateNewProjectModelLevel();

            for (int s = 0; s < sheets.Count; s++)
            {
                Tuple<string, int> curTuple = sheets[s];
                myCopy.sheets.Add(new Tuple<string, int>(string.Copy(curTuple.Item1), curTuple.Item2));
            }

            return myCopy;
        }
    }
}
