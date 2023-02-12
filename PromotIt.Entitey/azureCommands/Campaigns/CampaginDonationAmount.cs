using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Campaigns
{
    public class CampaginDonationAmount : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    MainManager.Instance.CampaignControl.UpdateDonationAmount(int.Parse((string)param[0]), (string)param[1]);

                    string response = "successful update donation amount";
                    return response;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update donation amount of campaign", ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("CampaignId Or UnitPrice Parameters were not found in CampaginDonationAmount class", null, "Error");

                return "Failed Request";
            }

        }
    }
}
