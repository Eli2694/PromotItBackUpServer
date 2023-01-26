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

namespace PromotIt.microService
{
    public class Activist
    {
        [FunctionName("TwitterActivist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete","put", Route = "Activist/{action}/{param?}/{param2?}/{param3?}/{param4?}")] HttpRequest req, string action, string param, string param2,string param3,string param4,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                
                case "USERID":
                    try
                    {
                        // Get Username,name and id of twitter user
                        var json = MainManager.Instance.ActivistControl.SearchTwitterId(param);
                        if(json == null)
                        {
                            PromotIt.DataToSql.Logger.LogError("Twitter user was not found");
                            return new NotFoundResult();
                        }
                        else
                        {
                            return new OkObjectResult(json);
                        }

                    }
                    
                    catch (Exception ex)
                    {
                        PromotIt.DataToSql.Logger.LogError(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "USERTWEETS":
                    try
                    {

                        //Search for user tweets that include campaign website and campaign hashtag.

                        var json = MainManager.Instance.ActivistControl.GetTweets(param, param2, param3, param4);
                        
                        if (json == null)
                        {
                            PromotIt.DataToSql.Logger.LogError("Tweets were not found");
                            return new NotFoundResult();
                        }
                        else
                        {
                            return new OkObjectResult(json);
                        }

                    }
                    catch (Exception ex)
                    {
                        PromotIt.DataToSql.Logger.LogError(ex.Message);
                        Console.WriteLine(ex.Message);
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "Problam in intiating a campaign");
                        Console.WriteLine(ex.Message);
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "Problam Initiate Activist Points");
                        Console.WriteLine(ex.Message);
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "Problam Update User Points After Tweets");
                        Console.WriteLine(ex.Message);
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "Problam Update Campagin Tweets Amount");
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "get twitter user points");
                        Console.WriteLine(ex.Message);
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
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "problam decrease user points after purchase");
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "TWITTERMESSAGE":
                    try
                    {
                        //After buying a product using points,The site will post a notice about it

                        MainManager.Instance.ActivistControl.SendMessageInTwitter(param,param2);
                       
                    }     
                    catch (Exception ex)
                    {
                        PromotIt.DataToSql.Logger.LogError(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    break;
            }

            
            return new OkObjectResult("");
        }
    }
}
