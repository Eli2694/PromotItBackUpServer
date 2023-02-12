using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class DeleteProduct: ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    MainManager.Instance.BusinessControl.DeleteProduct(int.Parse((string)param[0]), (string)param[1]);

                    string response = "Successful product delete";
                    return response;
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to delete product", ex, "Exception");

                    return "Failed Request";

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to delete product becuase function did not get the parameters",null, "Error");

                return "Failed Request";
            }
            
        }
    }
}
