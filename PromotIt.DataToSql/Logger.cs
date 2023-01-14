using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class Logger
    {
        private static void Log(string str)
        {
            string fileName = "C:\\Users\\User\\source\\repos\\PromotIt\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
            string log = DateTime.Now + " - " + str + "\n";
            File.AppendAllText(fileName, log);
        }
        public static void LogError(string str)
        {
            Log("Error - " + str);
        }
        public static void LogEvent(string str)
        {
            Log("Event - " + str);
        }
    }
}
