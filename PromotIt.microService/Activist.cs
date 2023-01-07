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

namespace PromotIt.microService
{
    public class Activist
    {
        [FunctionName("TwitterActivist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Activist/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param, string param2,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody;

            switch (action)
            {
                
                case "GETWEBSITE":
                    try
                    {

                        List<string> listOfCampaignsWebsites = MainManager.Instance.ActivistControl.CampaignWebsites();
                        string json = JsonSerializer.Serialize(listOfCampaignsWebsites);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "GETHASHTAG":
                    try
                    {

                        List<string> listOfCampaignsHashtags = MainManager.Instance.ActivistControl.CampaignHashtag();
                        string json = JsonSerializer.Serialize(listOfCampaignsHashtags);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "GETWEBANDHASHTAG":
                    try
                    {

                        List<string> listOfCampaignsHashtags = MainManager.Instance.ActivistControl.CampaignHashtag();
                        string json = JsonSerializer.Serialize(listOfCampaignsHashtags);
                        return new OkObjectResult(json);

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
