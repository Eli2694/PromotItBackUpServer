using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Owner
{
    public class GetCampaignDonation : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null) 
            {
                try
                {

                    List<CampaignReportDonationAndTweets> donationToCampaigns = MainManager.Instance.OwnerControl.GetInfoAboutDonationToCampaigns((string)param[0]);

                    string json = JsonSerializer.Serialize(donationToCampaigns);
                    return json;

                }
                catch (Exception ex)
                {


                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns with donation amount", ex, "Exception");

                    return "Failed Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Parameters were not found in class GetCampaignDonation", null, "Error");

                return "Failed Request";
            }


        }
    }
}
