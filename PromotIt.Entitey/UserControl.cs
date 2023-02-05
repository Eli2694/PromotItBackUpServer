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
        public DataUser dataUser = new DataUser();

        //create function in Entitey layer that get the user data from react and move it to dataTosql layer
        public void UserInforamtion(string FullName,string Email)
        {
            try
            {     
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
            
            ret = dataUser.GetListOfCampaignsForUsers();
            return ret;
        }

        public int GetUserId(string email)
        {
            
            int ID = dataUser.ReceiveUserId(email);
            return ID;
            
        }

        public void UsersPurchaseInfo(Order order)
        {
            dataUser.UserOrder(order);
        }

        public void DecreaseUnitsInStock(int ProductId)
        {
            dataUser.decrUnitsInStock(ProductId);
        }

        public void initUserWallet(string email)
        {
            dataUser.initWallet(email);
        }

        public string getUserMoney(string email)
        {
            return dataUser.getUserMoneyFromDB(email);
            
        }

        public void updateUserMoney(string money,string email)
        {
            dataUser.updateMoney(money, email);

        }

        public void updateUserMoneyAfterPurchase(string money, string email)
        {
            dataUser.updateMoneyAfterPurchase(money, email);

        }
        public void UpdateRole(string role,string email)
        {
            dataUser.UpdateUserRole(role, email);
        }
    }
}
