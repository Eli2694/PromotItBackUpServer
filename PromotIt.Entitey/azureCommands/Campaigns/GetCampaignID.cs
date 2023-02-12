using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Campaigns
{
    public class GetCampaignID : ICommand
    {

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null) {
                try
                {

                    int campaginID = MainManager.Instance.CampaignControl.GetCampaginID(int.Parse((string)param[0]));

                    string json = JsonSerializer.Serialize(campaginID);

                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get campaign id", ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("ID Parameter was not found in GetCampaignID class", null, "Error");

                return "Failed Request";
            }

        }
    }
}
