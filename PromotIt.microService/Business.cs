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
    public class Business
    {
        [FunctionName("BusinessRepresentative")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Business/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody;

            switch (action)
            {
                case "Donate":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product = new Model.Product();
                    product = JsonSerializer.Deserialize<Model.Product>(requestBody);
                    if (product.productName == null || decimal.Parse(product.unitPrice) == 0 || int.Parse(product.unitsInStock) == 0)
                    {
                        string response = "faild to donate product";

                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            
                            MainManager.Instance.BusinessControl.GetProductInfo(product.productName, decimal.Parse(product.unitPrice), int.Parse(product.unitsInStock), product.CampaignId);

                            string responseMessage = "Donate Product";
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
                        List<PersonalCampagin> campaigns = MainManager.Instance.BusinessControl.ListOfCampaignsBusiness();

                        string json = JsonSerializer.Serialize(campaigns);

                        return new OkObjectResult(json);


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "DELETEPRPDUCT":
                    try
                    {
                        MainManager.Instance.BusinessControl.DeleteProduct(int.Parse(param), param2);
                        string response = "successful delete";
                        return new OkObjectResult(response);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "Update":
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "GETPRODUCTID":
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "GETPRODUCTS":
                    try
                    {
                        List<Product> products = MainManager.Instance.BusinessControl.ListOfCampaignProducts(int.Parse(param));

                        string json = JsonSerializer.Serialize(products);

                        return new OkObjectResult(json);
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
