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
    public class GetAllCampaigns : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null) {
                try
                {

                    List<CampaignReportGeneral> campaigns = MainManager.Instance.OwnerControl.GetAllRegisteredCampaigns((string)param[0]);

                    string json = JsonSerializer.Serialize(campaigns);
                    return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get all registered campaigns ", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Parameters were not found in class GetAllCampaigns", null, "Error");

                return "Failed Request";
            }



        }
    }
}
