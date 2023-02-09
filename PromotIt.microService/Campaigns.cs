using Microsoft.AspNetCore.Http;
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
using PersonalUtilities;

namespace PromotIt.microService
{
    public class Campaigns
    {
        [FunctionName("Campaigns")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Campaign/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2)
        {


            string requestBody;

            switch (action)
            {
                case "ADD":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Campaign campaign = new Model.Campaign();
                    campaign = JsonSerializer.Deserialize<Model.Campaign>(requestBody);
                    if (campaign.campaignName == null || campaign.campaignWebsite == null || campaign.campaginHashtag == null || campaign.donationAmount == null )
                    {
                        string response = "faild to insert information into DB";
                        //logManager.AddLogItemToQueue("faild to insert information into DB",null,"Error");

                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            MainManager.Instance.CampaignControl.GetCampaginInfo(campaign.campaignName, campaign.campaignWebsite, campaign.campaginHashtag,  campaign.Email,campaign.donationAmount);

                            string responseMessage = "Insert campaign information into DB";
                            //logManager.AddLogItemToQueue("Register campaign information into DB",null, "Event");
                            return new OkObjectResult(responseMessage);
                        }
                        catch (Exception ex)
                        {

                            //logManager.AddLogItemToQueue(ex.Message + "," + "faild to insert information into DB",ex,"Exception");
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
                        //logManager.AddLogItemToQueue(ex.Message + "," + "faild to get personal campaigns from database",ex,"Exception");
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
                        //logManager.AddLogItemToQueue(ex.Message + "," + "faild to delete campaign from database",ex,"Exception");
                        
                    }
                    break;
                case "Update":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.PersonalCampagin personalCampagin = new Model.PersonalCampagin();
                    personalCampagin = JsonSerializer.Deserialize<Model.PersonalCampagin>(requestBody);
                    if (personalCampagin.campaignName == null || personalCampagin.campaignWebsite == null || personalCampagin.campaginHashtag == null)
                    {
                        string response = "faild update";
                        //logManager.AddLogItemToQueue("faild to update campaign",null,"Error");

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
                            //logManager.AddLogItemToQueue(ex.Message + "," + "faild to update campaign information in database",ex,"Exception");
                        }
                    }
                    break;
                case "GETCAMPAIGNID":
                    try
                    {

                        int campaginID = MainManager.Instance.CampaignControl.GetCampaginID(int.Parse(param));

                        string json = JsonSerializer.Serialize(campaginID);

                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        //logManager.AddLogItemToQueue(ex.Message + "," + "faild to get campaign id",ex,"Exception");
                    }
                    break;
                case "DONATIONAMOUNT":
                    try
                    {
                        MainManager.Instance.CampaignControl.UpdateDonationAmount(int.Parse(param),param2);

                        string response = "successful update donation amount";
                        return new OkObjectResult(response);
                    }
                    catch (Exception ex)
                    {
                        //logManager.AddLogItemToQueue(ex.Message + "," + "faild to update donation amount of campaign",ex,"Exception");
                    }
                    break;
                default:
                    break;
            }

            

            return new OkObjectResult("");
        }
    }
}
