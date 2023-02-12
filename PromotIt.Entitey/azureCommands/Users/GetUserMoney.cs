using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class GetUserMoney :ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {

                    string userMoney = MainManager.Instance.userControl.getUserMoney((string)param[0]);
                    string json = JsonSerializer.Serialize(userMoney);
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get user money from databse", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("GetUserMoney did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
