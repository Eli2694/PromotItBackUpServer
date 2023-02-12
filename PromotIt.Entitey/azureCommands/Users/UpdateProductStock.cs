using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class UpdateProductStock : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    MainManager.Instance.userControl.DecreaseUnitsInStock(int.Parse((string)param[0]));
                    string response = "successful update";
                    return response;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update product stock", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("UpdateProductStock did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
