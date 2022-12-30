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

namespace PromotIt.microService
{
    public class Business
    {
        [FunctionName("BusinessRepresentative")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "delete", Route = "Business/{action}/{param?}")] HttpRequest req, string action, string param,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody;

            switch (action)
            {
                case "ADD":
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }

                    break;
                case "GET":
                    try
                    {

                   

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "DELETE":
                    try
                    {
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "Update":
                    try
                    {

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
