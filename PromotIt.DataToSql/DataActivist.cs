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
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeUserPoints" + " " + "'" + email + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "cant initialize user points of:" + " " + email);
            }

            
        }

        public void initiateCampagin(int CampaignId, string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeTwitterCampaignPromotion" + " " + CampaignId + "," + "'" + email + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "cant initialize campaign to promote of twitter user:" + " " + email);
            }


            
        }

        public void UpdatePoints(string email, int points)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateUserPoints" + " " + "'" + email + "'" + "," + points);

            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "can't update tweet points  of user   :" + " " + email );
            }

            
        }

        public void UpdateTweetsPerCampagin(string email, int tweets, int campaignId)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateTweetsPerCampaign" + " " + "'" + email + "'" + "," + tweets + "," + campaignId);
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "can't update tweet of campaign that the twitter user:" + " " + email + " " + " is promoting");
            }

            
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
                //Get Activist Points
                if(command.ExecuteScalar() == null)
                {
                    Points = 0;
                }
                else
                {
                    Points = (int)command.ExecuteScalar();
                }
                
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.LogError(ex.Message + "," + "faild to get active points");
                
            }
        }

        public void DecreaseActivistPoints(int points, string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec decreaseActivistPoints" + " " + points + "," + "'" + email + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "can't decrease activist  points after purchase:" + " " + email);
            }
            
        }

        public Keys GetKeys()
        {
            try
            {
                SqlQuery.GetAllInforamtionInSqlTable("select KeyValues from KeysAndTokens", TwitterKeysFromDB);
                return twitterKeys;
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "can't get twitter keys and tokens from database");
                return null;
            }
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
