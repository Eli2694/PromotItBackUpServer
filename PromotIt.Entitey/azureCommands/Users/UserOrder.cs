using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Users
{
    public class UserOrder : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null)
            {
                
                Model.Order order = new Model.Order();
                order = JsonSerializer.Deserialize<Model.Order>((string)param[2]);
                if (order.country == null || order.city == null || order.homeAddress == null || order.postalCode == null || order.phoneNumber == null)
                {
                    string response = "faild to insert information into DB";
                    MainManager.Instance.Log.AddLogItemToQueue("faild to order a product", null, "Error");
                    return response;

                }
                else
                {
                    try
                    {
                        MainManager.Instance.userControl.UsersPurchaseInfo(order);
                        string responseMessage = "Insert order information into DB";
                        return responseMessage;
                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to order a product and save the inforamtion in database", ex, "Exception");
                        return "Faild Request";
                    }

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("UserOrder did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
