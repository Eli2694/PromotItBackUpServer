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
    public class Users
    {
        [FunctionName("WebsiteUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", "put", Route = "Users/{action}/{param?}/{param2?}")] HttpRequest req, string action, string param,string param2)
        {

            string dictionaryKey = "Users." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.CommandManager.CommandList[dictionaryKey];
            if (commmand != null)
            {
                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(param, param2, requestBody));
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Value In Command List Was Not Found", null, "Error");
                return new BadRequestObjectResult("Problam Was Found");
            }
        }
    }
}
