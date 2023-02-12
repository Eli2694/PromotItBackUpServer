using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class ConfirmOrders : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    MainManager.Instance.BusinessControl.OrderConfirmation(int.Parse((string)param[0]), (string)param[1]);

                    string response = "successful Order Confirmation";
                    return response;


                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "unsuccessful Order Confirmation", ex, "Exception");

                    return "Faild Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get order confirmation becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
