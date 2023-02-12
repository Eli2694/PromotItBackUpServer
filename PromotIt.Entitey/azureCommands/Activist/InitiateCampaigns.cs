using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class InitiateCampaigns : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null && param[1] != null && param[2] != null)
            {
                try
                {
                    // User id, Twitter Username,campaign id and zero number of tweets  will be input in sql server.

                    MainManager.Instance.ActivistControl.initiateCampaginPromotion(int.Parse((string)param[0]), (string)param[1], (string)param[2]);

                    string json = "Initiate Promotion For Campaign";
                    return json;

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue("Problam in inserting to database User id,Twitter Username,campaign id and zero number of tweets", ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to initiate promotion for campaign becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
