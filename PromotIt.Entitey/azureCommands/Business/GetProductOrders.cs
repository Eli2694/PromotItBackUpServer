using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class GetProductOrders: ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {
                    List<OrdersToConfirm> orders = MainManager.Instance.BusinessControl.getOrdersOfMyProducts((string)param[0]);

                    string json = JsonSerializer.Serialize(orders);

                    return json;
                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of orders to confirm", ex, "Exception");

                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to get orders becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
