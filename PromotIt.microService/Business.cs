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
    public class Business
    {
        [FunctionName("BusinessRepresentative")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Business/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2)
        {


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
                        MainManager.Instance.Log.AddLogItemToQueue("faild to donate product",null,"Error");
                        return new OkObjectResult(response);

                    }
                    else
                    {
                        try
                        {
                            
                            MainManager.Instance.BusinessControl.GetProductInfo(product.productName, decimal.Parse(product.unitPrice), int.Parse(product.unitsInStock), product.CampaignId,product.Email,product.imageURL);

                            string responseMessage = "Donate Product";
                            MainManager.Instance.Log.AddLogItemToQueue("Donate product to campaign",null,"Event");
                            return new OkObjectResult(responseMessage);
                        }
                        catch (Exception ex)
                        {
                            MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "Problam of donating product",ex,"Exception");
                           
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
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," +  "faild to get list of campaigns for business",ex,"Exception");
                        
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
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to delete product",ex,"Exception");
                        
                    }
                    break;
                case "Update":
                        requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        Model.UpdatedProduct uProduct = new Model.UpdatedProduct();
                        uProduct = JsonSerializer.Deserialize<Model.UpdatedProduct>(requestBody);
                        if (uProduct.productName == null)
                        {

                        MainManager.Instance.Log.AddLogItemToQueue("faild to update product",null,"Error");
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
                            MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update product", ex, "Exception");
                                
                            }
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
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get product id",ex,"Exception");
                       

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

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of products",ex,"Exception");

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
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");

                    }
                    break;
                case "GETORDERS":
                    try
                    {
                        List<OrdersToConfirm> orders = MainManager.Instance.BusinessControl.getOrdersOfMyProducts(param);

                        string json = JsonSerializer.Serialize(orders);

                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get list of orders to confirm",ex,"Exception");

                    }
                    break;
                case "CONFIRMORDER":
                    try
                    {
                        MainManager.Instance.BusinessControl.OrderConfirmation(int.Parse(param), param2);

                        string response = "successful Order Confirmation";
                        return new OkObjectResult(response);

                        
                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "unsuccessful Order Confirmation",ex,"Exception");

                    }

                    break;
                case "REGISTER":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.RegisterCompany company = new Model.RegisterCompany();
                    company = JsonSerializer.Deserialize<Model.RegisterCompany>(requestBody);
                    if (company.companyName == null || company.companyWebsite == null || company.Email == null)
                    {
                        string response = "faild registration of business company";
                        MainManager.Instance.Log.AddLogItemToQueue("faild registration of business company",null,"Error");


                        return new OkObjectResult(response);
                    }
                    else
                    {
                        try
                        {
                            MainManager.Instance.BusinessControl.BusinessCompanyRegistration(company);

                            string response = "successful registration of business comapny ";
                            MainManager.Instance.Log.AddLogItemToQueue("successful registration of business comapny",null,"Error");
                            return new OkObjectResult(response);


                        }
                        catch (Exception ex)
                        {

                            MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild registration of business company",ex,"Exception");

                        }
                    }
                    break;

                case "GETCOMPANYNAME":
                    try
                    {
                        string orders = MainManager.Instance.BusinessControl.getBusinessCompanyName(int.Parse(param));

                        string json = JsonSerializer.Serialize(orders);

                        return new OkObjectResult(json);
                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "Problam getting company name",ex,"Exception");

                    }
                    break;
                default:
                    break;
            }



            return new OkObjectResult("");
        }
    }
}
