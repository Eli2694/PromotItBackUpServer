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
    public class GetNonprofitUsers : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            
                try
                {

                    List<ReportNonprofitUser> Users = MainManager.Instance.OwnerControl.GetNonprofitUsers();

                    string json = JsonSerializer.Serialize(Users);
                    return json;

                }
                catch (Exception ex)
                {


                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get information about nonprofit users", ex, "Exception");

                    return "Failed Request";

                }
            
            

        }
    }
}
