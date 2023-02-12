using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PromotIt.Entitey;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using PersonalUtilities;

namespace PromotIt.microService
{
    public class Users
    {
        [FunctionName("WebsiteUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Users/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2)
        {


            string dictionaryKey = "Users." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.CommandManager.CommandList[dictionaryKey];
            if (commmand != null)
            {
                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(param, param2, requestBody));
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Value In Command List Was Not Found", null, "Error");
                return new BadRequestObjectResult("Problam Was Found");
            }

            /*
            switch (action)
            {
                case "Order":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Order order = new Model.Order();
                    order = JsonSerializer.Deserialize<Model.Order>(requestBody);
                    if (order.country == null || order.city == null || order.homeAddress == null || order.postalCode == null || order.phoneNumber == null)
                    {
                        string response = "faild to insert information into DB";
                        MainManager.Instance.Log.AddLogItemToQueue("faild to order a product",null,"Error");
                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            MainManager.Instance.userControl.UsersPurchaseInfo(order);
                            string responseMessage = "Insert order information into DB";
                            return new OkObjectResult(responseMessage);
                        }
                        catch (Exception ex)
                        {

                            MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to order a product and save the inforamtion in database",ex,"Exception");
                        }

                    }

                    break;
                case "GET":
                    try
                    {

                        List<UsersCampaign> listOfAssociationsAndCampaigns = MainManager.Instance.userControl.uCampaigns(); 
                        string json = JsonSerializer.Serialize(listOfAssociationsAndCampaigns);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of campaigns ",ex,"Exception");
                    }
                    break;
                case "GETID":
                    try
                    {

                        int userId = MainManager.Instance.userControl.GetUserId(param);
                        string json = JsonSerializer.Serialize(userId);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get user id ",ex,"Exception");
                    }
                    break;
                case "UpdateStock":
                    try
                    {
                        MainManager.Instance.userControl.DecreaseUnitsInStock(int.Parse(param));
                        string response = "successful update";
                        return new OkObjectResult(response);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update product stock",ex,"Exception");
                    }
                    break;
                case "InitWallet":
                    try
                    {
                        MainManager.Instance.userControl.initUserWallet(param);
                        string response = "wallet Initialaztion";
                        return new OkObjectResult(response);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to initialze wallet of a user",ex,"Exception");
                    }
                    break;
                case "GETUSERMONEY":
                    try
                    {

                        string userMoney = MainManager.Instance.userControl.getUserMoney(param);
                        string json = JsonSerializer.Serialize(userMoney);
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get user money from databse",ex,"Exception");
                    }
                    break;
                case "ADDMONEY":
                    try
                    {

                        MainManager.Instance.userControl.updateUserMoney(param,param2);
                        string json = "Add money to user";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to add money to user",ex,"Exception");
                    }
                    break;
                case "DECREASEMONEY":
                    try
                    {

                        MainManager.Instance.userControl.updateUserMoneyAfterPurchase(param, param2);
                        string json = "Decrease money after purchase";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to Decrease money after purchase",ex,"Exception");
                    }
                    break;
                case "ROLES":
                    try
                    {

                        MainManager.Instance.userControl.UpdateRole(param, param2);
                        string json = "update the role of a user";
                        return new OkObjectResult(json);

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update user role from auth0, faild to change user role in database",ex,"Exception");

                    }
                    break;
                default:
                    break;
            }


            return new OkObjectResult("Did not enter switch case");

            */
        }
    }
}
