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
                    if (product.productName == null || decimal.Parse(product.unitPrice) == 0 || int.Parse(product.unitsInStock) == 0 || product.Email == null)
                    {
                        string response = "faild to donate product";

                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            
                            MainManager.Instance.BusinessControl.GetProductInfo(product.productName, decimal.Parse(product.unitPrice), int.Parse(product.unitsInStock), product.CampaignId,product.Email);

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
                        requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        Model.UpdatedProduct uProduct = new Model.UpdatedProduct();
                        uProduct = JsonSerializer.Deserialize<Model.UpdatedProduct>(requestBody);
                        if (uProduct.productName == null)
                        {
                            string response = "faild update";

                            return new OkObjectResult(response);
                        }
                        else
                        {
                            try
                            {
                                MainManager.Instance.BusinessControl.UpdateProduct(uProduct);
                                string response = "successful update";
                                return new OkObjectResult(response);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "GETPRODUCTID":
                    try
                    {
                        int productId = MainManager.Instance.BusinessControl.GetProductId(int.Parse(param), param2);
                        string json = JsonSerializer.Serialize(productId);

                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "GETPRODUCTS":
                    try
                    {
                        List<Product> products = MainManager.Instance.BusinessControl.ListOfCampaignProducts(int.Parse(param),param2);

                        string json = JsonSerializer.Serialize(products);

                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "PRODUCTS":
                    try
                    {
                        List<Product> products = MainManager.Instance.BusinessControl.getProducts(int.Parse(param));

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
