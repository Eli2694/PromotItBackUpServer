using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataLayer;


namespace PersonalUtilities
{
    public class LogDB : ILogger
    {

        public void Init()
        {

        }
        public void Log(LogItem item)
        {
            if (item.exceptionSource == null)
            {
                SqlQuery.InsertInfoToTableInSql("exec LoggingToDataBase" + " " + "'" + item.message + "'" + "," + "NULL" + "," + "'" + item.type + "'" + "," + "'" + item.dateTime + "'");
            }
            else
            {
                SqlQuery.InsertInfoToTableInSql("exec LoggingToDataBase" + " " + "'" + item.message + "'" + "," + "'" + item.exceptionSource.StackTrace.ToString() + "'" + "," + "'" + item.type + "'" + "," + "'" + item.dateTime + "'");
            }
        }
        
        public void LogCheckHoseKeeping()
        {
            DateTime threeMonthsEarlier = DateTime.Now.AddMonths(-3);
            SqlQuery.InsertInfoToTableInSql("delete from Logger where LogDate <  " + "'" + threeMonthsEarlier + "'");
        }
    }
}
