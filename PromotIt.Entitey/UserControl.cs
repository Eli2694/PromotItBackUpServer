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

        public int GetUserId(string email)
        {
            DataUser id = new DataUser();
            int ID = id.ReceiveUserId(email);
            return ID;
            
        }

        public void UsersPurchaseInfo(Order order)
        {
            DataUser purchase = new DataUser();
            purchase.UserOrder(order);
        }

        public void DecreaseUnitsInStock(int ProductId)
        {
            DataUser stock = new DataUser();
            stock.decrUnitsInStock(ProductId);
        }
    }
}
