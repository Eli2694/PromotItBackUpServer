using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Owner
{
    public class CampaignStatistics : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            
                try
                {

                    int Users = MainManager.Instance.OwnerControl.GetCampaignStats();

                    string json = JsonSerializer.Serialize(Users);
                    return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get total number of campaigns ", ex, "Exception");

                return "Failed Request";
                }
        }
    }
}
