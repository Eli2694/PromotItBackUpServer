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
    public class RegisterBusinessCompany : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[2] != null )
            {
                Model.RegisterCompany company = new Model.RegisterCompany();
                company = JsonSerializer.Deserialize<Model.RegisterCompany>((string)param[2]);
                if (company.companyName == null || company.companyWebsite == null || company.Email == null)
                {
                    string response = "faild registration of business company";
                    MainManager.Instance.Log.AddLogItemToQueue("faild registration of business company", null, "Error");


                    return response;
                }
                else
                {
                    try
                    {
                        MainManager.Instance.BusinessControl.BusinessCompanyRegistration(company);

                        string response = "successful registration of business comapny ";
                        MainManager.Instance.Log.AddLogItemToQueue("successful registration of business comapny", null, "Error");
                        return response;


                    }
                    catch (Exception ex)
                    {

                        MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild registration of business company", ex, "Exception");

                        return "Faild Request";

                    }
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild registration of business company becuase function did not get the parameters", null, "Error");

                return "Faild Request";
            }

        }
    }
}
