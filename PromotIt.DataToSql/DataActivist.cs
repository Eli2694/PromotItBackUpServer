using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class DataActivist
    {

        //Global
        int Points;
        public void initiatePoints(string email)
        { 
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeUserPoints" + " " + "'" + email + "'");
        }

        public void initiateCampagin(int CampaignId, string email)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeTwitterCampaignPromotion" + " " + CampaignId + "," + "'" + email + "'");
        }

        public void UpdatePoints(string email, int points)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateUserPoints" + " " + "'" + email + "'" + "," + points);
        }

        public void UpdateTweetsPerCampagin(string email, int tweets, int campaignId)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateTweetsPerCampaign" + " " + "'" + email + "'" + "," + tweets + "," + campaignId);
        }

        public int ActivistPoints(string email)
        {
            SqlQuery.GetSingleRowOrValue("getActivistPoints" + " " + "'" + email + "'", GetSingleValueOrRowFromDB);
            return Points;
        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            try
            {
                Points = (int)command.ExecuteScalar();
                return;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void DecreaseActivistPoints(int points, string email)
        {
            SqlQuery.InsertInfoToTableInSql("exec decreaseActivistPoints" + " " + points + "," + "'" + email + "'");
        }
    }
}
