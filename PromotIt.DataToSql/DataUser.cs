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
            SqlQuery.InsertInfoToTableInSql("exec checkNewSiteUser" + " " + "'" + FullName + "'" + "," + "'" + Email + "'");

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
                Log.AddLogItemToQueue("Can't get user campaigns from database",null,"Error");
            }
            return uscampaigns;
        }

        public int ReceiveUserId(string email)
        {
            SqlQuery.GetSingleRowOrValue("select UserID from Users where Email = " + "'" + email + "'", GetSingleValueOrRowFromDB);

            return userID;
        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            userID = (int)command.ExecuteScalar();
            return;

        }

        public void UserOrder(Order info)
        {
            SqlQuery.InsertInfoToTableInSql("insert into orders values(" + info.userId + "," + info.productId + "," + "'" + info.country + "'" + "," + "'" + info.city + "'" + "," + "'" + info.homeAddress + "'" + "," + "'" + info.postalCode + "'" + "," + "'" + info.phoneNumber + "'" + "," + "GetDate()" + "," + 0 + ")");


        }

        public void decrUnitsInStock(int ProductId)
        {
            SqlQuery.InsertInfoToTableInSql("exec UpdateUnitsInStockAfterPurchase" + " " + ProductId);


        }

        public void initWallet(string email)
        {
            SqlQuery.InsertInfoToTableInSql("exec InitializeWallet" + " " + "'" + email + "'");

            

        }

        public string getUserMoneyFromDB(string email)
        {
            SqlQuery.GetSingleRowOrValue("exec getUserMoney" + " " + "'" + email + "'", GetValueFromDB);
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

            SqlQuery.InsertInfoToTableInSql("exec UpdateUserMoney" + " " + decimal.Parse(money) + "," + "'" + email + "'");
            
        }

        public void updateMoneyAfterPurchase(string money, string email)
        {
            SqlQuery.InsertInfoToTableInSql("exec DecreaseUserMoneyAfterBuy" + " " + decimal.Parse(money) + "," + "'" + email + "'");

            

        }

        public void UpdateUserRole(string role,string email)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdateRole" + " " + "'" + role + "'" + "," + "'" + email + "'");


        }


    }
}
