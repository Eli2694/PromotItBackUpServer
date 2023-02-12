using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class DeacreaseUserMoney : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {

                    MainManager.Instance.userControl.updateUserMoneyAfterPurchase((string)param[0], (string)param[1]);
                    string json = "Decrease money after purchase";
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to Decrease money after purchase", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("DeacreaseUserMoney did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
