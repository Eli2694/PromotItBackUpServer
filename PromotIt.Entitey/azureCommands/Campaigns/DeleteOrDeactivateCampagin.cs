using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Campaigns
{
    public class DeleteOrDeactivateCampagin : CommandManager, ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {
                    MainManager.Instance.CampaignControl.DeleteCampaign(int.Parse((string)param[0]));

                    string response = "successful delete";
                    return response;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to delete campaign from database", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Id Parameter was not found in DeleteOrDeactivateCampagin class", null, "Error");

                return "Faild Request";

            }

           
        }
    }
}
