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
    public class GetCampaignTweets : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {

                    List<CampaignReportDonationAndTweets> tweetsOfCampaign = MainManager.Instance.OwnerControl.GetTweetsAboutCampaigns((string)param[0]);

                    string json = JsonSerializer.Serialize(tweetsOfCampaign);
                    return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns with number of tweets in twitter by activist user", ex, "Exception");

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
