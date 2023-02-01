using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUtilities
{
    public class LogItem
    {
        public string type { get; set; } // warning,event,exception
        public Exception exceptionSource { get; set; }
        public string message { get; set; }
        public DateTime dateTime { get; set; }  

    }
}
