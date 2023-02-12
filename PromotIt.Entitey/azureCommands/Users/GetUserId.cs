using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class GetUserId: ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null )
            {
                try
                {

                    int userId = MainManager.Instance.userControl.GetUserId((string)param[0]);
                    string json = JsonSerializer.Serialize(userId);
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get user id ", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue(" GetUserId did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
