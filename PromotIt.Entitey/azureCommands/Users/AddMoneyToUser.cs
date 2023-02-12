using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class AddMoneyToUser : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {

                    MainManager.Instance.userControl.updateUserMoney((string)param[0], (string)param[1]);
                    string json = "Add money to user";
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to add money to user", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("AddMoneyToUser did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
