using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class GetActiveCampaigns : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            try
            {
                List<PersonalCampagin> campaigns = MainManager.Instance.BusinessControl.ListOfCampaignsBusiness();

                string json = JsonSerializer.Serialize(campaigns);

                return json;


            }
            catch (Exception ex)
            {
                MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns for business", ex, "Exception");

                return "Failed Request";

            }

        }
    }
}
