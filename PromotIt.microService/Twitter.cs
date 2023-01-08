using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.Entitey;
using PromotIt.Model;
using System.IO;
using System.Text.Json;

namespace PromotIt.microService
{
    public class Twitter
    {
        [FunctionName("DataFromTwitter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Twitter/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param, string param2,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody;



            switch (action)
            {
                case "GETUSERID":
                    try
                    {



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "GETIMELINE":
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
