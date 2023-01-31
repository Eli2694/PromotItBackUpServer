
using Newtonsoft.Json.Linq;
using PersonalUtilities;
using PromotIt.DataToSql;
using PromotIt.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;


namespace PromotIt.Entitey
{

    

    public class ActivistControl
    {

        public void initiateActivistPoints(string email)
        {
            DataActivist points = new DataActivist();
            points.initiatePoints(email);
        }

        public void initiateCampaginPromotion(int CampaignId,string email)
        {
            DataActivist campagin = new DataActivist();
            campagin.initiateCampagin(CampaignId,email);
        }

        public void UpdateUserPoints(string email , int points)
        {
            DataActivist activist = new DataActivist();
            activist.UpdatePoints(email,points);
        }

        public void UpdateTweetsAmount(string email,int tweets,int campaignId)
        {
            DataActivist tweetsAmount = new DataActivist();
            tweetsAmount.UpdateTweetsPerCampagin(email,tweets,campaignId);
        }

        public int GetActivistPoints(string email)
        {
            DataActivist get = new DataActivist();
            int points = get.ActivistPoints(email);
            return points;
        }

        public void DecreasePointsAmount(int points,string email)
        {
            DataActivist decrease =new DataActivist();
            decrease.DecreaseActivistPoints(points, email);
        }


        //Global
        Keys twitterKeysAndTokens = new Keys();  
        public JObject SearchTwitterId(string username)
        {
            try
            {
                var urlUsername = "https://api.twitter.com/2/users/by?usernames=param";
                urlUsername = urlUsername.Replace("param", username);
                DataActivist keys= new DataActivist();
                twitterKeysAndTokens = keys.GetKeys();

                var client = new RestClient(urlUsername);
                var request = new RestRequest("", Method.Get);
                request.AddHeader("authorization", "Bearer" + " " + twitterKeysAndTokens.twitterToken);

                var response = client.Execute(request);
                if (response.IsSuccessful)
                {

                    var json = JObject.Parse(response.Content);
                    return json;
                }
                else
                {
                    LogManager.AddLogItemToQueue("Twitter user was not found",null,"Error");
                    return null;
                }
            }
            catch (TwitterException exc)
            {
                LogManager.AddLogItemToQueue(exc.Message, exc,"Exception");
                return null;
            }

        }

        public JObject GetTweets(string param, string param2, string param3, string param4)
        {
            try
            {
                string urlTimeline = "https://api.twitter.com/2/tweets/search/recent?tweet.fields=created_at&max_results=100&start_time={0}:00Z&query=from:{1} %23{2} url:{3} has:hashtags has:links";
                string urlTimelineOutput = String.Format(urlTimeline, param, param2, param3, param4);

                if(twitterKeysAndTokens.apiKeySecret == null)
                {
                    DataActivist keys = new DataActivist();
                    twitterKeysAndTokens = keys.GetKeys();
                }

                var client = new RestClient(urlTimelineOutput);
                var request = new RestRequest("", Method.Get);
                request.AddHeader("authorization", "Bearer" + " " + twitterKeysAndTokens.twitterToken);


                var response = client.Execute(request);
                if (response.IsSuccessful)
                {

                    var json = JObject.Parse(response.Content);
                    return json;
                }
                else
                {
                    return null;
                }
            }
            catch (TwitterException exc)
            {
                LogManager.AddLogItemToQueue(exc.Message, exc, "Exception");
                return null;
            }

        }

        public int counter = 1;
        public async void SendMessageInTwitter(string param, string param2)
        {
            try
            {

                if (twitterKeysAndTokens.accessTokenSecret == null || twitterKeysAndTokens.apiKeySecret == null || twitterKeysAndTokens.accessToken == null || twitterKeysAndTokens.apiKey == null)
                {
                    DataActivist keys = new DataActivist();
                    twitterKeysAndTokens = keys.GetKeys();
                }

                var userClient = new TwitterClient(twitterKeysAndTokens.apiKey,twitterKeysAndTokens.apiKeySecret,twitterKeysAndTokens.accessToken,twitterKeysAndTokens.accessTokenSecret);

                var user = await userClient.Users.GetAuthenticatedUserAsync();
                string str = $"{counter}.Hello {param}, you purchase a product of '{param2}' company";
                counter++;
                var tweet = await userClient.Tweets.PublishTweetAsync(str);
            }
            catch (TwitterException e)
            {
                LogManager.AddLogItemToQueue(e.Message + "," + " problam sending a tweet about user purchase with points",e,"Exception");
                Console.WriteLine(e.ToString());
            }
            
        }

    }
}
