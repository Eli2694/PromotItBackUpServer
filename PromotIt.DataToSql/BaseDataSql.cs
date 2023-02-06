using PersonalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class BaseDataSql
    {
        public BaseDataSql(LogManager Log) { LogInstance = Log; }
        public LogManager LogInstance { get; set; }
    }
}
