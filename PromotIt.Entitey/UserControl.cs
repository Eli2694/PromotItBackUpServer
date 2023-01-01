using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataToSql;
using PromotIt.Model;

namespace PromotIt.Entitey
{
    public class UserControl
    {
        //create function in Entitey layer that get the user data from react and move it to dataTosql layer
        public void UserInforamtion(string FullName,string Email)
        {
            try
            {
                DataUser dataUser = new DataUser();
                dataUser.addUserToTableInSql(FullName, Email);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }

        public List<UsersCampaign> uCampaigns()
        {
            List<UsersCampaign> ret;
            DataUser list = new DataUser();
            ret = list.GetListOfCampaignsForUsers();
            return ret;
        }
    }
}
