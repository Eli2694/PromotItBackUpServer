using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class GetAllCampaignInfo : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            
                try
                {

                    List<UsersCampaign> listOfAssociationsAndCampaigns = MainManager.Instance.userControl.uCampaigns();
                    string json = JsonSerializer.Serialize(listOfAssociationsAndCampaigns);
                return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns ", ex, "Exception");
                      return "Faild Request";


                }
           

        }
    }
}
