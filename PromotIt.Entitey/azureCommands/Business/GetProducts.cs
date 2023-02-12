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
    public class GetProducts : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {
                    List<Product> products = MainManager.Instance.BusinessControl.getProducts(int.Parse((string)param[0]));

                    string json = JsonSerializer.Serialize(products);

                    return json;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                    return "Faild Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to gets products becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
