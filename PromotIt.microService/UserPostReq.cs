﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PromotIt.Entitey;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace PromotIt.microService
{
    public class UserPostReq
    {
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Model.Users user = new Model.Users();
            user = JsonSerializer.Deserialize<Model.Users>(requestBody);
            if(user.FullName == null || user.Email == null)
            {
                string response = "faild Insert User information into DB";

                return new OkObjectResult(response);

            }
            else
            {
                try
                {
                    MainManager.Instance.userControl.UserInforamtion(user.FullName, user.Email);
                    string responseMessage = "Insert User information into DB";
                    return new OkObjectResult(responseMessage);
                }
                catch (Exception ex)
                {

                   Console.WriteLine(ex.Message);
                }

            }

            return new OkObjectResult("");


        }
    }
}
