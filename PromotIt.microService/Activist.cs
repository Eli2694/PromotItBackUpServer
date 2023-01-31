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
using System.Collections;
using static System.Net.WebRequestMethods;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Newtonsoft.Json.Linq;
using PersonalUtilities;

namespace PromotIt.microService
{
    public class Activist
    {
        [FunctionName("TwitterActivist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete","put", Route = "Activist/{action}/{param?}/{param2?}/{param3?}/{param4?}")] HttpRequest req, string action, string param, string param2,string param3,string param4)
        {
            
            switch (action)
            {
                
                case "USERID":
                    try
                    {
                        // Get Username,name and id of twitter user
                        var json = MainManager.Instance.ActivistControl.SearchTwitterId(param);
                        if(json == null)
                        {
                            LogManager.AddLogItemToQueue("Twitter user was not found",null,"Error");
                            return new NotFoundResult();
                        }
                        else
                        {
                            return new OkObjectResult(json);
                        }

                    }
                    
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message, ex,"Exception");
                        
                    }
                    break;
                case "USERTWEETS":
                    try
                    {

                        //Search for user tweets that include campaign website and campaign hashtag.

                        var json = MainManager.Instance.ActivistControl.GetTweets(param, param2, param3, param4);
                        
                        if (json == null)
                        {
                            LogManager.AddLogItemToQueue("Tweets were not found",null,"Event");
                            return new NotFoundResult();
                        }
                        else
                        {
                            return new OkObjectResult(json);
                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                        
                    }
                    break;
                case "INITIATECAMPAIGN":
                    try
                    {
                        // When User choose to promote a campaign and press the button "Promote"
                        // User id,campaign id and number of tweets zero will be input in sql server.

                        MainManager.Instance.ActivistControl.initiateCampaginPromotion(int.Parse(param),param2);
                        string json = "Initiate Promotion For Campaign";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue("Problam in intiating a campaign",ex,"Exception");
                    }
                    break;
                case "INITIATEPOINTS":
                    try
                    {
                        //Create a row of user id and 0 points in sql server

                        MainManager.Instance.ActivistControl.initiateActivistPoints(param);
                        string json = "Initiate Activist Points";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                       
                        LogManager.AddLogItemToQueue(ex.Message + "," + "Problam Initiate Activist Points", ex,"Exception");
                    }
                    break;
                case "UPDATEPOINTS":
                    try
                    {
                        //after twitter user promote a campaign seccessfully, he will get points that will allow him to buy products

                        MainManager.Instance.ActivistControl.UpdateUserPoints(param, int.Parse(param2));
                        string json = "Update User Points After Tweets";
                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message + "," + "Problam Update User Points After Tweets",ex,"Exception");
                        
                    }
                    break;
                case "UPDATETWEETSAMOUNT":
                    try
                    {
                        //update how many times twitter user promoted a specific campaign

                        MainManager.Instance.ActivistControl.UpdateTweetsAmount(param, int.Parse(param2), int.Parse(param3));
                        string json = "Update Campagin Tweets Amount";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message + "," + "Problam Update Campagin Tweets Amount",ex,"Exception");
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "GETPOINTS":
                    try
                    {
                        // will retrieve the user's current number of points from sql server

                        int points = MainManager.Instance.ActivistControl.GetActivistPoints(param);
                        string json = JsonSerializer.Serialize(points);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                    }
                    break;
                case "DROPOINTS":
                    try
                    {
                        //Following the purchase of a product with the help of points, the amount of the user's points will be reduced

                        MainManager.Instance.ActivistControl.DecreasePointsAmount(int.Parse(param2), param);
                        string json = " decrease user points after purchase";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                    }
                    break;
                case "TWITTERMESSAGE":
                    try
                    {
                        //After buying a product using points,The site will post a notice about it

                        MainManager.Instance.ActivistControl.SendMessageInTwitter(param,param2);
                        LogManager.AddLogItemToQueue("Purchase Product With Twitter Points",null, "Event");

                    }     
                    catch (Exception ex)
                    {
                        LogManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                    }
                    break;

                default:
                    break;
            }

            
            return new OkObjectResult("");
        }
    }
}
