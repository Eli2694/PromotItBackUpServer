using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataLayer;
using System.Data.SqlClient;
using PromotIt.Model;

namespace PromotIt.DataToSql
{
    public class DataUser
    {
        //Global Variable
        List<UsersCampaign> uscampaigns = new List<UsersCampaign>();
        int userID;
        string money;


        public void addUserToTableInSql(string FullName, string Email)
        {
            try
            {
                
                SqlQuery.InsertInfoToTableInSql("exec checkNewSiteUser" + " " +"'" +  FullName + "'" +"," +"'" + Email + "'");

            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.Message);
            }
            
        }

        
        public void CreateListOfAssociationsAndCampaigns(SqlDataReader reader)
        {

            while (reader.Read())
            {
                UsersCampaign user = new UsersCampaign();

                user.CampaignId = reader.GetInt32(0);
                user.AssociationName = reader.GetString(1);
                user.AssociationWebsite = reader.GetString(2);
                user.campaignName = reader.GetString(3);
                user.campaginHashtag = reader.GetString(4);
                user.campaignWebsite = reader.GetString(5);
                user.donationAmount = (reader.GetDecimal(6)).ToString();

                uscampaigns.Add(user);
            }

            return;
        }

        public List<UsersCampaign> GetListOfCampaignsForUsers()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec GetAllInfoAboutCampaigns", CreateListOfAssociationsAndCampaigns);
            if(uscampaigns == null)
            {
                Logger.LogError("Can't get user campaigns from database");
            }
            return uscampaigns;
        }

        public int ReceiveUserId(string email)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("select UserID from Users where Email = " + "'" + email + "'", GetSingleValueOrRowFromDB);
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't get user id from database" + "," + ex.Message + "," + "user email:" + email);
            }
             
            return userID;
        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            try
            {
                userID = (int)command.ExecuteScalar();
                return;
            }
            catch (Exception ex)
            {
                Logger.LogError("Can't get  user id from database" + "," + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        public void UserOrder(Order info)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("insert into orders values(" + info.userId + "," + info.productId + "," + "'" + info.country + "'" + "," + "'" + info.city + "'" + "," + "'" + info.homeAddress + "'" + "," + "'" + info.postalCode + "'" + "," + "'" + info.phoneNumber + "'" + "," + "GetDate()" + "," + 0 + ")");
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't insert purchase order into database" + "," + ex.Message + "," + "user id:" + info.userId);
            }

            
        }

        public void decrUnitsInStock(int ProductId)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec UpdateUnitsInStockAfterPurchase" + " " + ProductId);
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't update units stock of product after purchase" + "," + ex.Message + "," + "product id:" + ProductId);
            }

            
        }

        public void initWallet(string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec InitializeWallet" + " " + "'" + email + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't initialize user wallet in database " + "," + ex.Message + "," + "user email:" + email);
            }

            

        }

        public string getUserMoneyFromDB(string email)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("exec getUserMoney" + " " + "'" + email + "'", GetValueFromDB);

                return money;
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't get user money from database " + "," + ex.Message + "," + "user email:" + email);
            }


            return money = "0";
        }

        public void GetValueFromDB(SqlCommand command)
        {
            try
            {
                decimal sqlMoney = (decimal)command.ExecuteScalar();
                money = sqlMoney.ToString();
                return;
            }
            catch (Exception ex)
            {
                Logger.LogError("Can't get the money from database" + "," + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        public void updateMoney(string money, string email)
        {

            try
            {
                SqlQuery.InsertInfoToTableInSql("exec UpdateUserMoney" + " " + decimal.Parse(money) + "," + "'" + email + "'");

            }
            catch (Exception ex)
            {

                Logger.LogError("Can't update user money in database" + "," + ex.Message + "," + "user email:" + email);
            }
            
        }

        public void updateMoneyAfterPurchase(string money, string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec DecreaseUserMoneyAfterBuy" + " " + decimal.Parse(money) + "," + "'" + email + "'");
            }
            catch (Exception ex)
            {
                

                Logger.LogError("Can't decrease user money after purchase" + "," + ex.Message + "," +"user email:" + email);
            }

            

        }

        public void UpdateUserRole(string role,string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdateRole" + " " + "'" + role + "'" + "," + "'" + email + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't update user role" + "," + ex.Message + "," + "user email:" + email);
            }

            
        }


    }
}
