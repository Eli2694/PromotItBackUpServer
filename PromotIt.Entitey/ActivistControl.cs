
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
using static System.Net.Mime.MediaTypeNames;


namespace PromotIt.Entitey
{
    public class ActivistControl : BaseEntity
    {
        LogManager Log { get; set; }
        public DataActivist activist { get; set; }
        public ActivistControl(LogManager log) : base(log)
        {
            Log = LogInstance;

            activist = new DataActivist(LogInstance);

            twitterKeysAndTokens = activist.GetKeys();

            Task.Run(GetTweets);
        }

        //Global
        Keys twitterKeysAndTokens = new Keys();

        public void initiateActivistPoints(string email)
        {
            
            activist.initiatePoints(email);
        }

        public void initiateCampaginPromotion(int CampaignId,string email,string username)
        {
            activist.initiateCampagin(CampaignId,email, username);
        }

        public void UpdateUserPoints(string email , int points)
        {
            activist.UpdatePoints(email,points);
        }

        public void UpdateTweetsAmount(string email,int tweets,int campaignId,string username)
        {
            
            activist.UpdateTweetsPerCampagin(email,tweets,campaignId,username);
        }

        public int GetActivistPoints(string email)
        {
            
            int points = activist.ActivistPoints(email);
            return points;
        }

        public void DecreasePointsAmount(int points,string email)
        {
            activist.DecreaseActivistPoints(points, email);
        }  
        public JObject SearchTwitterId(string username)
        {
            try
            {
                var urlUsername = Environment.GetEnvironmentVariable("urlUsername");
                urlUsername = urlUsername.Replace("param", username);
                //DataActivist keys= new DataActivist();
                twitterKeysAndTokens = activist.GetKeys();

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
                    Log.AddLogItemToQueue("Twitter user was not found",null,"Error");
                    return null;
                }
            }
            catch (TwitterException exc)
            {
                Log.AddLogItemToQueue(exc.Message, exc,"Exception");
                return null;
            }

        }

        public void GetTweets()
        {
            try
            {
                //DataActivist dataActivist = new DataActivist();
                string TwitterStartSearchData;

                while (true)
                {
                    int tweetCounter = 0;
                    
                
                    List<TwitterCmpaignPromotion> CampaignsAndTwitterUserName = activist.GetListOfCampaignsAndTwitterUserNames();

                    // Get Last Date Of A Tweet 
                    DateTime LastTweetDate = activist.GetLastTweetDay();
                    LastTweetDate = LastTweetDate.AddMinutes(1);

                    TwitterStartSearchData = LastTweetDate.ToString("yyyy-MM-ddTHH:mm");

                    foreach (TwitterCmpaignPromotion twitter in CampaignsAndTwitterUserName)
                    {
                        // Shows only the website name without http
                        int startIndex = twitter.website.IndexOf("://") + 3;
                        int endIndex = twitter.website.IndexOf("/", startIndex);
                        string host = twitter.website.Substring(startIndex, endIndex - startIndex);

                        string urlSearchTweets = Environment.GetEnvironmentVariable("urlSearchTweets");
                        string urlTimelineOutput = String.Format(urlSearchTweets, TwitterStartSearchData, twitter.twitterUserName, twitter.hashtag, host);

                        var client = new RestClient(urlTimelineOutput);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", "Bearer" + " " + twitterKeysAndTokens.twitterToken);

                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            Log.AddLogItemToQueue("Get Successful Response From Twitter", null, "Event");

                            var jsonObject = JObject.Parse(response.Content);

                            int resultCount = (int)jsonObject["meta"]["result_count"];

                            if (resultCount != 0) 
                            {
                                tweetCounter++;
                                UpdateUserPoints(twitter.email, resultCount);
                                UpdateTweetsAmount(twitter.email, resultCount, twitter.campaignId,twitter.twitterUserName);

                                JArray dataArray = (JArray)jsonObject["data"];

                                foreach (JObject data in dataArray.Children<JObject>())
                                {
                                    string createdAt = (string)data["created_at"];
                                    string text = (string)data["text"];
                                    string id = (string)data["id"];

                                    activist.InsertTweetInformationToDB(id,text,createdAt);
                                }
                            }
                            
                            if(tweetCounter == 0 && LastTweetDate.AddDays(7) < DateTime.Now)
                            {
                                TwitterStartSearchData = DateTime.Now.AddDays(7).ToString("yyyy-MM-ddTHH:mm");

                                activist.InsertTweetInformationToDB("0000", "none", TwitterStartSearchData);
                            }

                        }
                        else
                        {
                            Log.AddLogItemToQueue("Get Unsuccessful Response From Twitter", null, "Error");
                        }
                    }

                    Thread.Sleep(1000 * 60 * 5);
                }

            }
            catch (TwitterException exc)
            {
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
                
            }
            catch (Exception exc)
            {
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");

            }

        }

        public int counter = 1;
        public async void SendMessageInTwitter(string param, string param2)
        {
            try
            {

                

                var userClient = new TwitterClient(twitterKeysAndTokens.apiKey,twitterKeysAndTokens.apiKeySecret,twitterKeysAndTokens.accessToken,twitterKeysAndTokens.accessTokenSecret);

                var user = await userClient.Users.GetAuthenticatedUserAsync();
                string str = $"{counter}.Hello {param}, you purchase a product of '{param2}' company";
                counter++;
                var tweet = await userClient.Tweets.PublishTweetAsync(str);
            }
            catch (TwitterException e)
            {
                Log.AddLogItemToQueue(e.Message + "," + " problam sending a tweet about user purchase with points",e,"Exception");
                Console.WriteLine(e.ToString());
            }
            
        }

    }
}
