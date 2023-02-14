using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using PersonalUtilities;
using PromotIt.Entitey;

namespace PromotIt.microService
{
    public class Auth0Services
    {
        [FunctionName("GetRoles")]
        public static async Task<IActionResult> GetRoles(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "roles/{userId}")] HttpRequest req,
             string userId)
        {

            var urlGetRoles = string.Format(Environment.GetEnvironmentVariable("urlGetRoles"), userId);

            var client = new RestClient(urlGetRoles);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("authorization", Environment.GetEnvironmentVariable("Auth0Services"));

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                MainManager.Instance.Log.AddLogItemToQueue("Seccessful role assignment",null,"Event");
                var json = JArray.Parse(response.Content);
                return new OkObjectResult(json);
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Role Not Found",null,"Error");
                return new NotFoundResult();
            }

        }
    }
}
