using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Campaigns
{
    public class AddCampaign : ICommand
    {
        //
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null)
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign = JsonSerializer.Deserialize<Model.Campaign>((string)param[2]);

                if (campaign.campaignName == null || campaign.campaignWebsite == null || campaign.campaginHashtag == null || campaign.donationAmount == null)
                {
                    string response = "faild to insert information into DB";
                    MainManager.Instance.Log.AddLogItemToQueue("faild to insert information into DB", null, "Error");
                    return response;
                }
                else
                {
                    try
                    {
                        MainManager.Instance.CampaignControl.GetCampaginInfo(campaign.campaignName, campaign.campaignWebsite, campaign.campaginHashtag, campaign.Email, campaign.donationAmount);

                        string responseMessage = "Insert campaign information into DB";
                        MainManager.Instance.Log.AddLogItemToQueue("Register campaign information into DB", null, "Event");

                        return responseMessage;
                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to insert information into DB", ex, "Exception");

                        return "Failed Request";
                    }

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Register campaign information into DB is faild - object not found", null, "Error");

                return "Failed Request";
            }
            
        }
    }
}
