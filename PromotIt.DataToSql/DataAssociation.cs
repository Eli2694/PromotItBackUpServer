using PromotIt.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class DataAssociation
    {
        public void addAssociationSql(string AssociationName, string AssociationEmail, string AssociationWebsite, string RegisteredAssociation, string FullName, string Email)
        {
            try
            {

                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec checkAssociation" + " " + "'" + AssociationName + "'" + ","+ "'" + AssociationEmail + "'" + ","+ "'" + AssociationWebsite + "'" + ","+ "'" + RegisteredAssociation + "'" + ","+ "'" + FullName + "'" + ","+ "'" + Email + "'");

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + "," + "faild to register association");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
