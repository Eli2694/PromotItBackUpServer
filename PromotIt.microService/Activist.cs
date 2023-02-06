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

            LogManager logManager = new LogManager();

            switch (action)
            {
                
                case "USERID":
                    try
                    {
                        // Get Username,name and id of twitter user
                        var json = MainManager.Instance.ActivistControl.SearchTwitterId(param);
                        if(json == null)
                        {
                            logManager.AddLogItemToQueue("Twitter user was not found",null,"Error");
                            return new NotFoundResult();
                        }
                        else
                        {
                            return new OkObjectResult(json);
                        }

                    }                   
                    catch (Exception ex)
                    {
                        logManager.AddLogItemToQueue(ex.Message, ex,"Exception");
                        
                    }
                    break;
                case "INITIATECAMPAIGN":
                    try
                    {                       
                        // User id, Twitter Username,campaign id and zero number of tweets  will be input in sql server.

                        MainManager.Instance.ActivistControl.initiateCampaginPromotion(int.Parse(param),param2,param3);
                        string json = "Initiate Promotion For Campaign";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        logManager.AddLogItemToQueue("Problam in inserting to database User id,Twitter Username,campaign id and zero number of tweets", ex,"Exception");
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

                        logManager.AddLogItemToQueue(ex.Message + "," + "Problam Initiate Activist Points", ex,"Exception");
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
                        logManager.AddLogItemToQueue(ex.Message, ex, "Exception");
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
                        logManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                    }
                    break;
                case "TWITTERMESSAGE":
                    try
                    {
                        //After buying a product using points,The site will post a notice about it

                        MainManager.Instance.ActivistControl.SendMessageInTwitter(param,param2);
                        logManager.AddLogItemToQueue("Purchase Product With Twitter Points",null, "Event");

                    }     
                    catch (Exception ex)
                    {
                        logManager.AddLogItemToQueue(ex.Message, ex, "Exception");
                    }
                    break;

                default:
                    break;
            }

            
            return new OkObjectResult("");
        }
    }
}
