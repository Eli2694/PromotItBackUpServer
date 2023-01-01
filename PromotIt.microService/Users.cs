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
    public class Users
    {
        [FunctionName("WebsiteUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Users/{action}/{param?}")] HttpRequest req, string action, string param,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody;

            switch (action)
            {
                case "ADD":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Campaign campaign = new Model.Campaign();
                    //campaign = JsonSerializer.Deserialize<Model.Campaign>(requestBody);
                    if (campaign.campaignName == null || campaign.campaignWebsite == null || campaign.campaginHashtag == null)
                    {
                        string response = "faild to insert information into DB";

                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            MainManager.Instance.CampaignControl.GetCampaginInfo(campaign.campaignName, campaign.campaignWebsite, campaign.campaginHashtag, campaign.FullName, campaign.Email);

                            string responseMessage = "Insert campaign information into DB";
                            return new OkObjectResult(responseMessage);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }

                    }

                    break;
                case "GET":
                    try
                    {

                        List<UsersCampaign> listOfAssociationsAndCampaigns = MainManager.Instance.userControl.uCampaigns(); 
                        string json = JsonSerializer.Serialize(listOfAssociationsAndCampaigns);
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


            return new OkObjectResult("");
        }
    }
}
