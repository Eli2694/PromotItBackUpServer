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
    public class ReportUserCount : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            
                try
                {

                    ReportDifferentUsersCount Users = MainManager.Instance.OwnerControl.UsersType();

                    string json = JsonSerializer.Serialize(Users);
                    return json;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "faild to get statistics information about users", ex, "Exception");

                return "Failed Request";
              
                }    

        }
    }
}
