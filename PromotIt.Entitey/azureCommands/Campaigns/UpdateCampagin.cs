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
    public class UpdateCampagin : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null)
            {
                Model.PersonalCampagin personalCampagin = new Model.PersonalCampagin();
                personalCampagin = JsonSerializer.Deserialize<Model.PersonalCampagin>((string)param[2]);
                if (personalCampagin.campaignName == null || personalCampagin.campaignWebsite == null || personalCampagin.campaginHashtag == null)
                {
                    string response = "faild update";
                    MainManager.Instance.Log.AddLogItemToQueue("faild to update campaign", null, "Error");

                    return response;
                }
                else
                {
                    try
                    {
                        MainManager.Instance.CampaignControl.UpdatePerCamp(personalCampagin);
                        string response = "successful campagin update";
                        return response;

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update campaign information in database", ex, "Exception");

                        return "Failed Request";
                    }
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Campaign Object Parameter was not found in UpdateCampagin class", null, "Error");

                return "Failed Request";
            }           
        }
    }
}
