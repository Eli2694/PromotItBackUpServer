using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey.azureCommands.Activist
{
    public class SendTwitterMessage : ICommand
    {
        public object ExecuteCommand(params object[] param) // param,param2,param3,param4, requestBody
        {
            if (param[0] != null && param[1] != null)
            {
                try
                {
                    //After buying a product using points,The site will post a notice about it

                    MainManager.Instance.ActivistControl.SendMessageInTwitter((string)param[0],(string)param[1]);

                    MainManager.Instance.Log.AddLogItemToQueue("Purchase Product With Twitter Points", null, "Event");

                    return "Seccessfull Request";

                }
                catch (Exception ex)
                {
                    MainManager.Instance.Log.AddLogItemToQueue(ex.Message, ex, "Exception");

                    return "Failed Request";
                }
            }
            else
            {
                MainManager.Instance.Log.AddLogItemToQueue("faild to send twitter message becuase function did not get the parameters", null, "Error");

                return "Failed Request";
            }

        }
    }
}
