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
                        request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAK1GkwEAAAAATJMQLTlD8X7QTFNRBQgMwMHopRg%3Dh29cNFKMspSJwyFMJgOHqly02SHrdSu3aHKuhUGIMNbDnsKA0r");

                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            // Still need to understand how and if to parse the response
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


                        string urlTimeline = "https://api.twitter.com/2/tweets/search/recent?tweet.fields=created_at&max_results=100&start_time={0}:00Z&query=from:{1} %23{2} url:{3} has:hashtags has:links";
                        string urlTimelineOutput = String.Format(urlTimeline, param,param2,param3,param4);

                       

                        var client = new RestClient(urlTimelineOutput);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAK1GkwEAAAAATJMQLTlD8X7QTFNRBQgMwMHopRg%3Dh29cNFKMspSJwyFMJgOHqly02SHrdSu3aHKuhUGIMNbDnsKA0r");

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
                case "GETCAMPAIGN":
                    try
                    {


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
