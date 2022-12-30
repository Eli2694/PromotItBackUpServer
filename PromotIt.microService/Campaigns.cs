﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PromotIt.Entitey;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using PromotIt.Model;

namespace PromotIt.microService
{
    public class Campaigns
    {
        [FunctionName("Campaigns")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Campaign/{action}/{param?}")] HttpRequest req, string action, string param,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody;

            switch (action)
            {
                case "ADD":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Campaign campaign = new Model.Campaign();
                    campaign = JsonSerializer.Deserialize<Model.Campaign>(requestBody);
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

                        List<PersonalCampagin> campaigns = MainManager.Instance.CampaignControl.getListOfPersonalCampaigns(param);

                        string json = JsonSerializer.Serialize(campaigns);

                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "DELETE":
                    try
                    {
                        MainManager.Instance.CampaignControl.DeleteCampaign(int.Parse(param));

                        string response = "successful delete";
                        return new OkObjectResult(response);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "Update":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.PersonalCampagin personalCampagin = new Model.PersonalCampagin();
                    personalCampagin = JsonSerializer.Deserialize<Model.PersonalCampagin>(requestBody);
                    if (personalCampagin.campaignName == null || personalCampagin.campaignWebsite == null || personalCampagin.campaginHashtag == null)
                    {
                        string response = "faild update";

                        return new OkObjectResult(response);
                    }
                    else
                    {
                        try
                        {
                            MainManager.Instance.CampaignControl.UpdatePerCamp(personalCampagin);
                            string response = "successful update";
                            return new OkObjectResult(response);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;
                default:
                    break;
            }

            

            return new OkObjectResult("");
        }
    }
}
