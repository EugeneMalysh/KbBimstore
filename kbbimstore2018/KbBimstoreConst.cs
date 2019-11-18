using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KbBimstore
{
    class KbBimstoreConst
    {
        static readonly Dictionary<String, int> scalesValues = new Dictionary<String, int> {
            {"1:1", 1},
            {"1:2", 2},
            {"1:5", 5},
            {"1:10", 10},
            {"1:20", 20},
            {"1:50", 50},
            {"1:100", 100},
            {"1:200", 200},
            {"1:500", 500},
            {"1:1000", 1000},
            {"1:2000", 2000},
            {"1:5000", 5000},
            {"12\" = 1'-0\"", 1},
            {"6\" = 1'-0\"", 2},
            {"3\" = 1'-0\"", 4},
            {"1 1/2\" = 1'-0\"", 8},
            {"1\" = 1'-0\"", 12},
            {"3/4\" = 1'-0\"", 16},
            {"1/2\" = 1'-0\"", 24},
            {"3/8\" = 1'-0\"", 32},
            {"1/4\" = 1'-0\"", 48},
            {"3/16\" = 1'-0\"", 64},
            {"1/8\" = 1'-0\"", 96},
            {"1\" = 10'-0\"", 120},
            {"3/32\" = 1'-0\"", 128},
            {"1/16\" = 1'-0\"", 192},
            {"1\" = 20'-0\"", 240},
            {"3/64\" = 1'-0\"", 256},
            {"1\" = 30'-0\"", 360},
            {"1/32\" = 1'-0\"", 384},
            {"1\" = 40'-0\"", 480},
            {"1\" = 50'-0\"", 600},
            {"1\" = 60'-0\"", 720},
            {"1/64\" = 1'-0\"", 768},
            {"1\" = 80'-0\"", 960},
            {"1\" = 100'-0\"", 1200},
            {"1\" = 160'-0\"", 1920},
            {"1\" = 200'-0\"", 2400},
            {"1\" = 300'-0\"", 3600},
            {"1\" = 400'-0\"", 4800},
        };

        public static List<string> getScalesNames()
        {
            List<string> result = scalesValues.Keys.ToList();

            return result;
        }

        public static int getScaleValue(string name)
        {
            int result = 1;

            if (scalesValues.ContainsKey(name))
            {
                result = scalesValues[name];
            }

            return result;
        }
    }
}
