using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataLayer
{
    public class SqlQuery
    {

        //Global ConnectionString 
        static string  connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromotIt;Data Source=localhost\sqlexpress";
        public static void InsertInfoToTableInSql(string SqlQuery)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = SqlQuery;


                try
                {
                    connection.Open();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;

                }

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {

                    // Execute insert command
                    command.ExecuteNonQuery();

                }
            }
        }

        public static void InsertInfoToTableInSqlAndGetAnswer(string SqlQuery)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = SqlQuery;

                try
                {
                    connection.Open();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;

                }

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {

                    // Execute insert command
                    string answer = command.ExecuteScalar().ToString();
                    //Console.WriteLine(answer);

                }
            }
        }

        public delegate void SetDataReader_delegate(SqlDataReader reader);
        public static void GetAllInforamtionInSqlTable(string SqlQuery, SetDataReader_delegate Ptrfunc)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = SqlQuery;

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        connection.Open();

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;

                    }
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Ptrfunc(reader);

                    }
                }
            }
        }
    }
}
