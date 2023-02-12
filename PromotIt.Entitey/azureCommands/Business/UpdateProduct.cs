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
    public class UpdateProduct: ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null)
            {
                
                Model.UpdatedProduct uProduct = new Model.UpdatedProduct();
                uProduct = JsonSerializer.Deserialize<Model.UpdatedProduct>((string)param[2]);
                if (uProduct.productName == null)
                {

                    MainManager.Instance.Log.AddLogItemToQueue("faild to update product", null, "Error");
                    string response = "faild update";
                    return response;
                }
                else
                {
                    try
                    {
                        MainManager.Instance.BusinessControl.UpdateProduct(uProduct);
                        string response = "successful update";
                        return response;

                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to update product", ex, "Exception");

                        return "Failed Request";

                    }
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to update product", null, "Error");

                return "Failed Request";
            }

        }

    }
}
