using System;

using NHunspell;

using Autodesk.Revit.DB;

using MessageBox = System.Windows.Forms.MessageBox;

namespace KbBimstore.Schedules
{
    public static class SpellingChecker
    {
        private static readonly Color INCORRECT_SPELLING_BACKGROUND_COLOR = new Color(255, 82, 82);

        public static void Run(View currentView)
        {
            if (currentView is ViewSchedule)
                Check(currentView as ViewSchedule);
            else
                MessageBox.Show("Current view is not a schedule.");
        }

        private static void Check(ViewSchedule schedule)
        {
            using (Transaction t = new Transaction(schedule.Document, "spelling"))
            {
                t.Start();
                foreach (SectionType sectionType in Enum.GetValues(typeof(SectionType)))
                {
                    TableSectionData section = schedule.GetTableData().GetSectionData(sectionType);
                    if (section != null && section.IsValidObject)
                        Check(section);
                }
                t.Commit();
            }
        }

        private static void Check(TableSectionData section)
        {
            using (Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                for (int i = 0; i < section.NumberOfRows; i++)
                {
                    for (int j = 0; j < section.NumberOfColumns; j++)
                    {
                        string text = section.GetCellText(i, j);
                        foreach (string word in text.Split(' '))
                        {
                            if (!hunspell.Spell(word))
                            {
                                TableCellStyle style = section.GetTableCellStyle(i, j);
                                style.BackgroundColor = INCORRECT_SPELLING_BACKGROUND_COLOR;
                                section.SetCellStyle(i, j, style);
                            }
                        }
                    }
                }
            }
        }
    }
}
