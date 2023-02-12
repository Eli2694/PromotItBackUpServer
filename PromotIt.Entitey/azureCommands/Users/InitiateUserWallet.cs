using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class InitiateUserWallet : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    MainManager.Instance.userControl.initUserWallet((string)param[0]);
                    string response = "wallet Initialaztion";
                    return response;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to initialze wallet of a user", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("InitiateUserWallet did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
