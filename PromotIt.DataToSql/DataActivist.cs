using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PersonalUtilities;

namespace PromotIt.DataToSql
{

    interface ITwitter
    {
        Keys GetKeys();
    }

    public class DataActivist : ITwitter
    {

        //Global
        int Points;
        Keys twitterKeys = new Keys();
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
            //Get Activist Points
            if (command.ExecuteScalar() == null)
            {
                Points = 0;
            }
            else
            {
                Points = (int)command.ExecuteScalar();
            }

            return;
        }

        public void DecreaseActivistPoints(int points, string email)
        {
            SqlQuery.InsertInfoToTableInSql("exec decreaseActivistPoints" + " " + points + "," + "'" + email + "'");

        }

        public Keys GetKeys()
        {
            SqlQuery.GetAllInforamtionInSqlTable("select KeyValues from KeysAndTokens", TwitterKeysFromDB);
            if (twitterKeys == null)
            {
                LogManager.AddLogItemToQueue("can't get twitter keys and tokens from database", null, "Error");
            }

            return twitterKeys;
        }

        public void TwitterKeysFromDB(SqlDataReader reader)
        {
            int countRowsInDatabase = 0;
            while (reader.Read())
            {
                if(countRowsInDatabase == 0)
                {
                    twitterKeys.apiKey = reader.GetString(0);
                }
                else if (countRowsInDatabase == 1)
                {
                    twitterKeys.apiKeySecret = reader.GetString(0);
                }
                else if (countRowsInDatabase == 2)
                {
                    twitterKeys.twitterToken = reader.GetString(0);
                }
                else if (countRowsInDatabase == 3)
                {
                    twitterKeys.accessToken = reader.GetString(0);
                }
                else if (countRowsInDatabase == 4)
                {
                    twitterKeys.accessTokenSecret = reader.GetString(0);
                }
                else
                {
                    continue;
                }

                countRowsInDatabase++;
            }

            return;
        }
    }
}
