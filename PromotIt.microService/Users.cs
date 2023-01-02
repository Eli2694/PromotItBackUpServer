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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Users/{action}/{param?}")] HttpRequest req, string action, string param,
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
                    }
                    break;
                default:
                    break;
            }


            return new OkObjectResult("");
        }
    }
}
