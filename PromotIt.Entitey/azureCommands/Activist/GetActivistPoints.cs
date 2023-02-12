using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class GetActivistPoints : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    // will retrieve the user's current number of points from sql server

                    int points = MainManager.Instance.ActivistControl.GetActivistPoints((string)param[0]);
                    string json = JsonSerializer.Serialize(points);
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get activist points becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
