using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class GetProductId : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    int productId = MainManager.Instance.BusinessControl.GetProductId(int.Parse((string)param[0]), (string)param[1]);
                    string json = JsonSerializer.Serialize(productId);

                    return json;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get product id", ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get product id, function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
