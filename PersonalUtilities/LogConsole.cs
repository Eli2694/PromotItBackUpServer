using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUtilities
{
    public class LogConsole : ILogger
    {
        public void Init()
        {

        }
        public void Log(LogItem item)
        {
            string log;

            if (item.exceptionSource == null)
            {
                log = item.type + " - " + item.dateTime + " - " + item.message;
            }
            else
            {
                log = item.type + " - " + item.dateTime + " - " + item.exceptionSource.StackTrace.ToString() + ", " + item.message;
            }

            Console.WriteLine(log);

        }
        
        public void LogCheckHoseKeeping()
        {

        }
    }
}
