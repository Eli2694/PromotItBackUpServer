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
    public class DataActivist 
    {

        //Global
        int Points { get; set; }

        Keys twitterKeys = new Keys();

        List<TwitterCmpaignPromotion> twitterCmpaignPromotions= new List<TwitterCmpaignPromotion>();
        DateTime lastTweetFromDatabase { get; set; }
        public void initiatePoints(string email)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeUserPoints" + " " + "'" + email + "'");

        }

        public void initiateCampagin(int CampaignId, string email,string username)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InitializeTwitterCampaignPromotion" + " " + CampaignId + "," + "'" + email  + "'" + "," + "'" + username + "'");
        }

        public void UpdatePoints(string email, int points)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateUserPoints" + " " + "'" + email + "'" + "," + points);
            }
            catch (Exception ex)
            {

                LogManager.AddLogItemToQueue("Can not update user points", ex, "Exception");
            }
        }

        public void UpdateTweetsPerCampagin(string email, int tweets, int campaignId,string twitterUsername)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec updateTweetsPerCampaign" + " " + "'" + email + "'" + "," + tweets + "," + campaignId + "," + "'" + twitterUsername + "'");
            }
            catch (Exception ex)
            {

                LogManager.AddLogItemToQueue("Can not update Tweets per campaign", ex, "Exception");
            }

            

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

        public List<TwitterCmpaignPromotion> GetListOfCampaignsAndTwitterUserNames()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec GetTwitterUsernameAndPromotedCampaigns", CreateListOfTwitterCmpaignPromotion);

            return twitterCmpaignPromotions;
        }

        public void CreateListOfTwitterCmpaignPromotion(SqlDataReader reader)
        {

            while (reader.Read())
            {
                TwitterCmpaignPromotion perCamp = new TwitterCmpaignPromotion();
                perCamp.twitterUserName = reader.GetString(0);
                perCamp.website= reader.GetString(1);
                perCamp.hashtag = reader.GetString(2);
                perCamp.email = reader.GetString(3);
                perCamp.campaignId = reader.GetInt32(4);

                twitterCmpaignPromotions.Add(perCamp);
            }

            return;
        }

        public DateTime GetLastTweetDay()
        {
            SqlQuery.GetSingleRowOrValue("exec GetLastTweetDate", GetLastTweetDateFromDB);
            return lastTweetFromDatabase;
        }

        public void GetLastTweetDateFromDB(SqlCommand command)
        {
            
            if (command.ExecuteScalar() == null)
            {
                lastTweetFromDatabase = DateTime.Now;
            }
            else
            {
                lastTweetFromDatabase = (DateTime)command.ExecuteScalar();
            }

            return;
        }

        public void InsertTweetInformationToDB(string id,string text,string date)
        {
            if(date == null)
            {
                LogManager.AddLogItemToQueue("Can not find where tweet was created", null, "Error");
                return;
            }

            SqlQuery.InsertInfoToTableInSql("exec InsertTweetInfomration" + " " + "'" + text + "'" + "," + "'" + id + "'" + "," + "'" + date + "'");

        }

    }


}
