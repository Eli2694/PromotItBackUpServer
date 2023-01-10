using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PromotIt.Entitey;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections;
using static System.Net.WebRequestMethods;
using Tweetinvi;
using Tweetinvi.Exceptions;

namespace PromotIt.microService
{
    public class Activist
    {
        [FunctionName("TwitterActivist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Activist/{action}/{param?}/{param2?}/{param3?}/{param4?}")] HttpRequest req, string action, string param, string param2,string param3,string param4,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            //Global Variable
            string requestBody;
           

            switch (action)
            {
                
                case "USERID":
                    try
                    {
                        var urlUsername = "https://api.twitter.com/2/users/by?usernames=param";
                        urlUsername = urlUsername.Replace("param", param);

                        var client = new RestClient(urlUsername);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAJKQlAEAAAAAWKtKxa67%2BYRUEITfP4nqREyhEfA%3DMYX2gdz2C9OblxoSNhRShM7bWTgt2wzEZJBxjEdPeOWv5vTgGf");

                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            
                            var json =  JObject.Parse(response.Content);
                            return new OkObjectResult(json);
                        }
                        else
                        {
                            return new NotFoundResult();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "USERTWEETS":
                    try
                    {


                        //string urlUserTweetTimeline = "https://api.twitter.com/2/users/{0}/tweets";
                        //string urlUserTweetTimelineOutput = String.Format(urlUserTweetTimeline, param);

                        //string rfc3339DateTime = DateTimeOffset.Parse(param).ToString("yyyy-MM-ddTHH:mm:ssZ");

                        string urlTimeline = "https://api.twitter.com/2/tweets/search/recent?tweet.fields=created_at&max_results=100&start_time={0}:00Z&query=from:{1} %23{2} url:{3} has:hashtags has:links";
                        string urlTimelineOutput = String.Format(urlTimeline, param,param2,param3,param4);

                       

                        var client = new RestClient(urlTimelineOutput);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAJKQlAEAAAAAWKtKxa67%2BYRUEITfP4nqREyhEfA%3DMYX2gdz2C9OblxoSNhRShM7bWTgt2wzEZJBxjEdPeOWv5vTgGf");

                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            // Still need to understand how and if to parse the response
                            var json = JObject.Parse(response.Content);
                            return new OkObjectResult(json);
                        }
                        else
                        {
                            return new NotFoundResult();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "INITIATECAMPAIGN":
                    try
                    {
                        MainManager.Instance.ActivistControl.initiateCampaginPromotion(int.Parse(param),param2);
                        string json = "Initiate Promotion For Campaign";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "INITIATEPOINTS":
                    try
                    {
                        MainManager.Instance.ActivistControl.initiateActivistPoints(param);
                        string json = "Initiate Activist Points";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "UPDATEPOINTS":
                    try
                    {

                        MainManager.Instance.ActivistControl.UpdateUserPoints(param, int.Parse(param2));
                        string json = "Update User Points After Tweets";
                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "UPDATETWEETSAMOUNT":
                    try
                    {
                        MainManager.Instance.ActivistControl.UpdateTweetsAmount(param, int.Parse(param2), int.Parse(param3));
                        string json = "Update User Points After Tweets";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "GETPOINTS":
                    try
                    {
                        int points = MainManager.Instance.ActivistControl.GetActivistPoints(param);
                        string json = JsonSerializer.Serialize(points);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "DROPOINTS":
                    try
                    {
                        MainManager.Instance.ActivistControl.DecreasePointsAmount(int.Parse(param2), param);
                        string json = "Update User Points After Tweets";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "TWITTERMESSAGE":
                    try
                    {
                        var userClient = new TwitterClient("rMkWttO130zoQ6UJ0lYRoCvj2", "9IeGrLFCn6SGBfCiNLfNkel7mpFCD4OoaR5vXHJW61KT7gqciv", "1525509104617279489-wD0UG7IFlDWYd7TXG9ONCZu9jIs1G5", "vzWvPtrtx3otb2x3tx9SMYAusbFIUrZsqzl9AUMWkykCg");

                        Console.WriteLine(userClient);
                        var user = await userClient.Users.GetAuthenticatedUserAsync();
                        string str = $"Hello {param}, you purchase a product of {param2} company";
                        var tweet = await userClient.Tweets.PublishTweetAsync(str);


                    }
                    catch (TwitterException e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    break;
            }


            return new OkObjectResult("Did not enter switch case");
        }
    }
}
