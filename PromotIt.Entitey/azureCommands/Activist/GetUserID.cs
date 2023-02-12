using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class GetUserID : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    // Get Username,name and id of twitter user
                    var json = MainManager.Instance.ActivistControl.SearchTwitterId((string)param[0]);
                    if (json == null)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue("Twitter user was not found", null, "Error");

                        return "Failed Request";
                    }
                    else
                    {
                        return json;
                    }

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                    return "Failed Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get twitter id becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
