using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Business
{
    public class DonateToCampaign : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null)
            {
                
                Model.Product product = new Model.Product();
                product = JsonSerializer.Deserialize<Model.Product>((string)param[2]);
                if (product.productName == null || decimal.Parse(product.unitPrice) == 0 || int.Parse(product.unitsInStock) == 0 || product.Email == null)
                {
                    string response = "faild to donate product";
                    MainManager.Instance.Log.AddLogItemToQueue("faild to donate product", null, "Error");
                    return response;

                }
                else
                {
                    try
                    {

                        MainManager.Instance.BusinessControl.GetProductInfo(product.productName, decimal.Parse(product.unitPrice), int.Parse(product.unitsInStock), product.CampaignId, product.Email, product.imageURL);

                        string responseMessage = "Product Was Donated";
                        MainManager.Instance.Log.AddLogItemToQueue("Donate product to campaign", null, "Event");
                        return responseMessage;
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "Problam Donating A Product", ex, "Exception");

                        return "Failed Request";
                    }

                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Error Donate Product To Campaign", null, "Error");

                return "Failed Request";
            }

        }
    }
}
