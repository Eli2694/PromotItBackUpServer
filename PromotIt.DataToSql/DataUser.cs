using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataLayer;
using System.Data.SqlClient;
using PromotIt.Model;
using PersonalUtilities;

namespace PromotIt.DataToSql
{
    public class DataUser : BaseDataSql
    {
        //Global Variable
        List<UsersCampaign> uscampaigns = new List<UsersCampaign>();
        int userID;
        string money;

        LogManager Log { get; set; }
        public DataUser(LogManager log) : base(log)
        {
            Log = LogInstance;
        }
        public void addUserToTableInSql(string FullName, string Email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec checkNewSiteUser" + " " + "'" + FullName + "'" + "," + "'" + Email + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
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
            if(uscampaigns.Count != 0)
            {
                uscampaigns.Clear();
            }

            try
            {
                SqlQuery.GetAllInforamtionInSqlTable("exec GetAllInfoAboutCampaigns", CreateListOfAssociationsAndCampaigns);
            }
            catch (Exception ex)
            {

                Log.AddLogItemToQueue(ex.Message, ex, "Exception");
            }

            
            if(uscampaigns.Count() == 0)
            {
                Log.AddLogItemToQueue("Can't get user campaigns from database",null,"Error");
            }
            return uscampaigns;
        }

        public int ReceiveUserId(string email)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("select UserID from Users where Email = " + "'" + email + "'", GetSingleValueOrRowFromDB);
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            

            return userID;
        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            userID = (int)command.ExecuteScalar();
            return;

        }

        public void UserOrder(Order info)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("insert into orders values(" + info.userId + "," + info.productId + "," + "'" + info.country + "'" + "," + "'" + info.city + "'" + "," + "'" + info.homeAddress + "'" + "," + "'" + info.postalCode + "'" + "," + "'" + info.phoneNumber + "'" + "," + "GetDate()" + "," + 0 + ")");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

        }

        public void decrUnitsInStock(int ProductId)
        {

            try
            {

                SqlQuery.InsertInfoToTableInSql("exec UpdateUnitsInStockAfterPurchase" + " " + ProductId);
            }
            catch (Exception exc)
            { 
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }


        }

        public void initWallet(string email)
        {
            

            try
            {
                SqlQuery.InsertInfoToTableInSql("exec InitializeWallet" + " " + "'" + email + "'");
            }
            catch (Exception exc)
            { 
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }
            

        }

        public string getUserMoneyFromDB(string email)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("exec getUserMoney" + " " + "'" + email + "'", GetValueFromDB);
            }
            catch (Exception exc)
            {
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
                throw;
            }

            

            if(money == null)
            {
                return money = "0";
            }

            return money;        
        }

        public void GetValueFromDB(SqlCommand command)
        {
            decimal sqlMoney = (decimal)command.ExecuteScalar();
            money = sqlMoney.ToString();
            return;

        }

        public void updateMoney(string money, string email)
        {

            try
            {
                SqlQuery.InsertInfoToTableInSql("exec UpdateUserMoney" + " " + decimal.Parse(money) + "," + "'" + email + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }            
            
        }

        public void updateMoneyAfterPurchase(string money, string email)
        {
            

            try
            {
                SqlQuery.InsertInfoToTableInSql("exec DecreaseUserMoneyAfterBuy" + " " + decimal.Parse(money) + "," + "'" + email + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }
            

        }

        public void UpdateUserRole(string role,string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdateRole" + " " + "'" + role + "'" + "," + "'" + email + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

        }

    }
}
