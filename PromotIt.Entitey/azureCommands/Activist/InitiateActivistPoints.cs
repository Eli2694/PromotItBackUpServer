using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class InitiateActivistPoints : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null )
            {
                try
                {
                    //Create a row of user id and 0 points in sql server

                    MainManager.Instance.ActivistControl.initiateActivistPoints((string)param[0]);
                    string json = "Initiate Activist Points";
                    return json;

                }
                catch (Exception ex)
                {

                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message + "," + "Problam Initiate Activist Points", ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("Problam initiate activist points becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
