
using Newtonsoft.Json.Linq;
using PersonalUtilities;
using PromotIt.DataToSql;
using PromotIt.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Models;
using Tweetinvi.Exceptions;


namespace PromotIt.Entitey
{
    public class ActivistControl
    {
        public ActivistControl()
        {
            DataActivist keys = new DataActivist();
            twitterKeysAndTokens = keys.GetKeys();

            Task.Run(GetTweets);
        }

        //Global
        Keys twitterKeysAndTokens = new Keys();

        public void initiateActivistPoints(string email)
        {
            DataActivist points = new DataActivist();
            points.initiatePoints(email);
        }

        public void initiateCampaginPromotion(int CampaignId,string email,string username)
        {
            DataActivist campagin = new DataActivist();
            campagin.initiateCampagin(CampaignId,email, username);
        }

        public void UpdateUserPoints(string email , int points)
        {
            DataActivist activist = new DataActivist();
            activist.UpdatePoints(email,points);
        }

        public void UpdateTweetsAmount(string email,int tweets,int campaignId,string username)
        {
            DataActivist tweetsAmount = new DataActivist();
            tweetsAmount.UpdateTweetsPerCampagin(email,tweets,campaignId,username);
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

        public void GetTweets()
        {
            try
            {
                DataActivist dataActivist = new DataActivist();
                string TwitterStartSearchData;

                while (true)
                {
                    
                    if (twitterKeysAndTokens.apiKeySecret == null)
                    {
                        DataActivist keys = new DataActivist();
                        twitterKeysAndTokens = keys.GetKeys();
                    }
                
                    List<TwitterCmpaignPromotion> CampaignsAndTwitterUserName = dataActivist.GetListOfCampaignsAndTwitterUserNames();

                    // Get Last Date Of A Tweet 
                    DateTime LastTweetDay = dataActivist.GetLastTweetDay();
                    LastTweetDay = LastTweetDay.AddMinutes(1);
                    if (LastTweetDay.AddDays(7) < DateTime.Now)
                    {
                        TwitterStartSearchData = DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTHH:mm");
                    }
                    else
                    {
                        TwitterStartSearchData = LastTweetDay.ToString("yyyy-MM-ddTHH:mm");
                    }

                    foreach (TwitterCmpaignPromotion twitter in CampaignsAndTwitterUserName)
                    {
                        // Shows only the website name without http
                        int startIndex = twitter.website.IndexOf("://") + 3;
                        int endIndex = twitter.website.IndexOf("/", startIndex);
                        string host = twitter.website.Substring(startIndex, endIndex - startIndex);

                        string urlTimeline = "https://api.twitter.com/2/tweets/search/recent?start_time={0}:00Z&query=from:{1} %23{2} url:{3} has:hashtags has:links &tweet.fields=created_at,referenced_tweets&max_results=100";
                        string urlTimelineOutput = String.Format(urlTimeline, TwitterStartSearchData, twitter.twitterUserName, twitter.hashtag, host);

                        var client = new RestClient(urlTimelineOutput);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", "Bearer" + " " + twitterKeysAndTokens.twitterToken);

                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            LogManager.AddLogItemToQueue("Get Successful Response From Twitter", null, "Event");

                            var jsonObject = JObject.Parse(response.Content);

                            int resultCount = (int)jsonObject["meta"]["result_count"];

                            if (resultCount != 0) 
                            {
                                UpdateUserPoints(twitter.email, resultCount);
                                UpdateTweetsAmount(twitter.email, resultCount, twitter.campaignId,twitter.twitterUserName);

                                JArray dataArray = (JArray)jsonObject["data"];

                                foreach (JObject data in dataArray.Children<JObject>())
                                {
                                    string createdAt = (string)data["created_at"];
                                    string text = (string)data["text"];
                                    string id = (string)data["id"];

                                    dataActivist.InsertTweetInformationToDB(id,text,createdAt);
                                }
                            }            

                        }
                        else
                        {
                            LogManager.AddLogItemToQueue("Get Unsuccessful Response From Twitter", null, "Error");
                        }
                    }

                    Thread.Sleep(1000 * 60 * 5);
                }

            }
            catch (TwitterException exc)
            {
                LogManager.AddLogItemToQueue(exc.Message, exc, "Exception");
                
            }
            catch (Exception exc)
            {
                LogManager.AddLogItemToQueue(exc.Message, exc, "Exception");

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
