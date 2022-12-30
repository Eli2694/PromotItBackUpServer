using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataLayer;
using System.Data.SqlClient;

namespace PromotIt.DataToSql
{
    public class DataUser
    {
        public void addUserToTableInSql(string FullName, string Email)
        {
            try
            {
                
                SqlQuery.InsertInfoToTableInSql("exec checkNewSiteUser" + " " +"'" +  FullName + "'" +"," +"'" + Email + "'");

            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.Message);
            }
            
        }
    }
}
