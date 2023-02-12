using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class DecreaseActivistPoints : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    //Following the purchase of a product with the help of points, the amount of the user's points will be reduced

                    MainManager.Instance.ActivistControl.DecreasePointsAmount(int.Parse((string)param[1]), (string)param[0]);
                    string json = " decrease user points after purchase";
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
                MainManager.Instance.Log.AddLogItemToQueue("faild to get order confirmation becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
