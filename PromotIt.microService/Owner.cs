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
using PersonalUtilities;

namespace PromotIt.microService
{
    public class Owner
    {

        [FunctionName("Owner")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Owner/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param, string param2)
        {
            string dictionaryKey = "Owner." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.CommandManager.CommandList[dictionaryKey];
            if (commmand != null)
            {
                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(param, param2, requestBody));
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Value In Command List Was Not Found", null, "Error");
                return new BadRequestObjectResult("Problam Was Found");
            }

            /*
           switch (action)
           {

               case "GET":
                   try
                   {

                       ReportDifferentUsersCount Users = MainManager.Instance.OwnerControl.UsersType();

                       string json = JsonSerializer.Serialize(Users);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message);
                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get statistics information about users",ex,"Exception");
                   }
                   break;
               case "GETNONPROFIT":
                   try
                   {

                       List<ReportNonprofitUser> Users = MainManager.Instance.OwnerControl.GetNonprofitUsers();

                       string json = JsonSerializer.Serialize(Users);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {


                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get information about nonprofit users",ex,"Exception");

                   }
                   break;
               case "GETBUSINESS":
                   try
                   {

                       List<ReportBusinessUser> Users = MainManager.Instance.OwnerControl.GetBusinessUsers();

                       string json = JsonSerializer.Serialize(Users);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {

                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get information about business users",ex,"Exception");
                   }
                   break;
               case "GETACTIVIST":
                   try
                   {

                       List<ReportActivistUser> Users = MainManager.Instance.OwnerControl.GetActivistUsers();

                       string json = JsonSerializer.Serialize(Users);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {

                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get information about activist users",ex,"Exception");
                   }
                   break;
               case "CAMPAIGNSTATS":
                   try
                   {

                       int Users = MainManager.Instance.OwnerControl.GetCampaignStats();

                       string json = JsonSerializer.Serialize(Users);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {

                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get total number of campaigns ",ex,"Exception");
                   }
                   break;
               case "GETOTALCAMPAIGNS":
                   try
                   {

                       List<CampaignReportGeneral> campaigns = MainManager.Instance.OwnerControl.GetAllRegisteredCampaigns(param);

                       string json = JsonSerializer.Serialize(campaigns);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {

                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get all registered campaigns ",ex,"Exception");
                   }
                   break;
               case "GETCAMPAIGNDONATION":
                   try
                   {

                       List<CampaignReportDonationAndTweets> donationToCampaigns = MainManager.Instance.OwnerControl.GetInfoAboutDonationToCampaigns(param);

                       string json = JsonSerializer.Serialize(donationToCampaigns);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {


                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns with donation amount",ex,"Exception");
                   }
                   break;
               case "GETCAMPAIGNTWEETS":
                   try
                   {

                       List<CampaignReportDonationAndTweets> tweetsOfCampaign = MainManager.Instance.OwnerControl.GetTweetsAboutCampaigns(param);

                       string json = JsonSerializer.Serialize(tweetsOfCampaign);
                       return new OkObjectResult(json);

                   }
                   catch (Exception ex)
                   {

                       MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns with number of tweets in twitter by activist user",ex,"Exception");
                   }
                   break;


               default:
                   break;
           }


           return new OkObjectResult("Did not enter switch case");

           */
        }
    }
}
