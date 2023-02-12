using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class GetCampaignProducts : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    List<Product> products = MainManager.Instance.BusinessControl.ListOfCampaignProducts(int.Parse((string)param[0]), (string)param[1]);

                    string json = JsonSerializer.Serialize(products);

                    return json;
                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of products", ex, "Exception");

                    return "Faild Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get campaign products becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
