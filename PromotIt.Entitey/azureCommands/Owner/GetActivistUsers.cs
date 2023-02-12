using Microsoft.AspNetCore.Mvc;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Owner
{
    public class GetActivistUsers : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            
                try
                {

                    List<ReportActivistUser> Users = MainManager.Instance.OwnerControl.GetActivistUsers();

                    string json = JsonSerializer.Serialize(Users);
                    return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get information about activist users", ex, "Exception");

                    return "Failed Request";
                }
        }
    }
}
