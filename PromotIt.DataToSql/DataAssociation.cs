using PromotIt.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalUtilities;

namespace PromotIt.DataToSql
{
    public class DataAssociation : BaseDataSql
    {

        LogManager Log { get; set; }
        public DataAssociation(LogManager log) : base(log)
        {
            Log = LogInstance;
        }
        public void addAssociationSql(string AssociationName, string AssociationEmail, string AssociationWebsite, string RegisteredAssociation, string FullName, string Email)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec checkAssociation" + " " + "'" + AssociationName + "'" + "," + "'" + AssociationEmail + "'" + "," + "'" + AssociationWebsite + "'" + "," + "'" + RegisteredAssociation + "'" + "," + "'" + FullName + "'" + "," + "'" + Email + "'");

        }
    }
}
