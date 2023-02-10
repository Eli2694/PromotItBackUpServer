using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Campaigns
{
    public class GetPersonalCampaign : CommandManager,ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if(param[0] != null)
            {
                try
                {

                    List<PersonalCampagin> campaigns = MainManager.Instance.CampaignControl.getListOfPersonalCampaigns((string)param[0]);

                    string json = JsonSerializer.Serialize(campaigns);

                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get personal campaigns from database", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Email Parameter was not found in GetPersonalCampagin class", null, "Error");

                return "Faild Request";
            }

        }
    }
}
