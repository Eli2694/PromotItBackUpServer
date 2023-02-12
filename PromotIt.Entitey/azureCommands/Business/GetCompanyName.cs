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
    public class GetCompanyName : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    string orders = MainManager.Instance.BusinessControl.getBusinessCompanyName(int.Parse((string)param[0]));

                    string json = JsonSerializer.Serialize(orders);

                    return json;
                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "Problam getting company name", ex, "Exception");

                    return "Faild Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get business company name becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
