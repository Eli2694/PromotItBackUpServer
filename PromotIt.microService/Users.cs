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

namespace PromotIt.microService
{
    public class Users
    {
        [FunctionName("WebsiteUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Users/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody;

            switch (action)
            {
                case "Order":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Order order = new Model.Order();
                    order = JsonSerializer.Deserialize<Model.Order>(requestBody);
                    if (order.country == null || order.city == null || order.homeAddress == null || order.postalCode == null || order.phoneNumber == null)
                    {
                        string response = "faild to insert information into DB";
                        PromotIt.DataToSql.Logger.LogError("faild to order a product");
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

                            Console.WriteLine(ex.Message);
                            PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to order a product and save the inforamtion in database");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to get list of campaigns ");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to get user id ");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to update product stock");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to initialze wallet of a user");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to get user money from databse");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to add money to user");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to Decrease money after purchase");
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
                        Console.WriteLine(ex.Message);
                        PromotIt.DataToSql.Logger.LogError(ex.Message + "," + "faild to update user role from auth0, faild to change user role in database");

                    }
                    break;
                default:
                    break;
            }


            return new OkObjectResult("Did not enter switch case");
        }
    }
}
