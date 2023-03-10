using Microsoft.AspNetCore.Http;
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
using PersonalUtilities;

namespace PromotIt.microService
{
    public class AssociationReq
    {
        [FunctionName("Association")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Model.Association association = new Model.Association();
            association = JsonSerializer.Deserialize<Model.Association>(requestBody);
            if (association.AssociationName == null || association.AssociationEmail == null || association.AssociationWebsite == null)
            {

                //logManager.AddLogItemToQueue("faild to insert association into DB", null, "Error");
                string response = "faild to insert information into DB";      
                return new OkObjectResult(response);

            }
            else
            {
                try
                {
                    MainManager.Instance.AssociationControl.AssociationInforamtion(association.AssociationName, association.AssociationEmail, association.AssociationWebsite, association.RegisteredAssociation, association.FullName, association.Email);

                    string responseMessage = "register association information into DB";
                    return new OkObjectResult(responseMessage);
                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "register association information into DB",ex,"Exception");
                    Console.WriteLine(ex.Message);
                }

            }

            return new OkObjectResult("");
        }
    }
}
