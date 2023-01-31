using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUtilities
{
    public interface ILogger
    {
        void Init();
        void Log(LogItem item);
        void LogCheckHoseKeeping();
    }
}
