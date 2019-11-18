using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    class CreateNewProjectModelMain
    {
        public CreateNewProjectModelLevel firstLevel = new CreateNewProjectModelLevel();
        public List<CreateNewProjectModelLevel> otherLevels = new List<CreateNewProjectModelLevel>();

        public int getLevelsNumber()
        {
            return (1 + otherLevels.Count);
        }

        public void addLevels(int levelsNumber, int startNumber)
        {
            for (int l = 0; l < levelsNumber; l++)
            {
                int norLevelNumber = 1 + otherLevels.Count;
                CreateNewProjectModelLevel norLevel = new CreateNewProjectModelLevel();

                for (int s = 0; s < firstLevel.sheets.Count; s++)
                {
                    int norIndex = norLevelNumber + startNumber;
                    string norString = "" + firstLevel.sheets[s].Item1 + " (" + norIndex.ToString() + ")";
                    int norInt = firstLevel.sheets[s].Item2;

                    norLevel.sheets.Add(new Tuple<string, int>(norString, norInt));
                }

                otherLevels.Add(norLevel);
            }
        }

        public void deleteLevels(int levelsNumber)
        {
            if (levelsNumber >= otherLevels.Count)
            {
                otherLevels.Clear();
            }
            else
            {
                for (int l = 0; l < levelsNumber; l++)
                {
                    int lastLevelNumber = otherLevels.Count - 1;
                    if (lastLevelNumber >= 0)
                    {
                        otherLevels.RemoveAt(lastLevelNumber);
                    }
                }
            }
        }

        public int getSheetsNumber()
        {
            return firstLevel.sheets.Count;
        }

        public void addSheets(int sheetsNumber, int startNumber)
        {
            for (int s = 0; s < sheetsNumber; s++)
            {
                firstLevel.sheets.Add(new Tuple<string, int>("MainSheet", 0));

                for (int l = 0; l < otherLevels.Count; l++)
                {
                    int norIndex = 1 + l + startNumber;
                    string norString = "MainSheet (" + norIndex.ToString() + ")";
                    otherLevels[l].sheets.Add(new Tuple<string, int>(norString, 0));
                }
            }
        }

        public void deleteSheets(int sheetsNumber)
        {
            if (sheetsNumber >= firstLevel.sheets.Count)
            {
                firstLevel.sheets.Clear();

                for (int l = 0; l < otherLevels.Count; l++)
                {
                    otherLevels[l].sheets.Clear();
                }
            }
            else
            {
                for (int s = 0; s < sheetsNumber; s++)
                {
                    int lastSheetNumber = firstLevel.sheets.Count - 1;
                    if (lastSheetNumber >= 0)
                    {
                        firstLevel.sheets.RemoveAt(lastSheetNumber);
                    }
                }

                for (int l = 0; l < otherLevels.Count; l++)
                {
                    for (int s = 0; s < sheetsNumber; s++)
                    {
                        int lastSheetNumber = otherLevels[l].sheets.Count - 1;
                        if (lastSheetNumber >= 0)
                        {
                            otherLevels[l].sheets.RemoveAt(lastSheetNumber);
                        }
                    }
                }
            }
        }
    }
}
