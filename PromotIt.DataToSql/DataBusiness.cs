using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PersonalUtilities;

namespace PromotIt.DataToSql
{
    public class DataBusiness : BaseDataSql
    {
        //Global Variables
        List<PersonalCampagin> campaignsForBusiness = new List<PersonalCampagin>();
        List<Product> listOfProducts = new List<Product>();
        List<OrdersToConfirm> listOfOrdersToConfirm = new List<OrdersToConfirm>();
        int ProductID;
        string companyName;

        LogManager Log { get; set; }
        public DataBusiness(LogManager log) : base(log)
        {
            Log = LogInstance;
        }
        public void CreateListOfCampaignsForBusiness(SqlDataReader reader)
        {
           

            while (reader.Read())
            {
                PersonalCampagin perCamp = new PersonalCampagin();

                perCamp.campaignName = reader.GetString(0);
                perCamp.campaignWebsite = reader.GetString(1);
                perCamp.campaginHashtag = reader.GetString(2);
                perCamp.CampaignId = reader.GetInt32(3);

                campaignsForBusiness.Add(perCamp);
            }

            return;
        }

        public List<PersonalCampagin> GetListOfCampaigns()
        {
            if (campaignsForBusiness.Count() != 0)
            {
                campaignsForBusiness.Clear();
            }

            try
            {
                SqlQuery.GetAllInforamtionInSqlTable("select CampaginName,CampaginWebsite,CampaginHashtag,CampaignsID from Campaigns where isActive = 1 ", CreateListOfCampaignsForBusiness);
            }
            catch (Exception ex)
            {
                Log.AddLogItemToQueue(ex.Message, ex, "Exception");
                
            }

            
            if(campaignsForBusiness.Count == 0)
            {
                Log.AddLogItemToQueue("Can't find list of personal campaigns for business user",null,"Error");
            }

            return campaignsForBusiness;
        }

        public void DonateProductToCampaign(string name, decimal unitprice, int unitInStock, int campaignId,string email, string image)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec DonateProduct" + " " + "'" + name + "'" + "," + unitprice + "," + unitInStock + "," + campaignId + "," + "'" + email + "'" + "," + "'" + image + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

        }

        
        
        public void CreateListOfCampaignProducts(SqlDataReader reader)
        {

            while (reader.Read())
            {
                Product product = new Product();

                decimal unitprice = reader.GetDecimal(1);
                int unitInStock = reader.GetInt32(2);

                product.productName = reader.GetString(0);
                product.unitPrice = unitprice.ToString();
                product.unitsInStock = unitInStock.ToString();
                product.CampaignId = reader.GetInt32(3);
                product.imageURL = reader.GetString(4);

                listOfProducts.Add(product);
            }

            return;
        }

        public List<Product> GetListOfProductsToSpecificCampaign(int Id,string email)
        {
            if (listOfProducts.Count() != 0)
            {
                listOfProducts.Clear();
            }

            try
            {
                SqlQuery.GetAllInforamtionInSqlTable("exec GetProductsThatBelongToMe" + " " + Id + "," + "'" + email + "'", CreateListOfCampaignProducts);
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            

            if(listOfProducts.Count == 0)
            {
                Log.AddLogItemToQueue("Can't find list of personal donated products",null,"Error");
            }

            return listOfProducts;
        }

        public List<Product> GetListOfProducts(int ID)
        {

            if(listOfProducts.Count() != 0)
            {
                listOfProducts.Clear();
            }

            try
            {
                // Get list of all donated products
                SqlQuery.GetAllInforamtionInSqlTable("select ProductName,UnitPrice,UnitsInStock,CampaignID,ImageURL from Products where CampaignID = " + ID, CreateListOfCampaignProducts);
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            
            if(listOfProducts.Count == 0)
            {
                Log.AddLogItemToQueue("Can't find list of all products donated by business representative",null,"Error");
            }

            return listOfProducts;
        }

        public void CreateListOfOrdersToConfirm(SqlDataReader reader)
        {

            while (reader.Read())
            {
                OrdersToConfirm order = new OrdersToConfirm();

                order.orderID = reader.GetInt32(0);
                order.productId = reader.GetInt32(1);
                order.country = reader.GetString(2);
                order.city = reader.GetString(3);
                order.homeAddress = reader.GetString(4);
                order.postalCode = reader.GetString(5);
                order.phoneNumber = reader.GetString(6);
                order.productName = reader.GetString(7);
                order.unitPrice = (reader.GetDecimal(8)).ToString();
                order.unitsInStock = (reader.GetInt32(9)).ToString();
                order.date = (reader.GetDateTime(10)).ToString();

                listOfOrdersToConfirm.Add(order);
            }

            return;
        }
        public List<OrdersToConfirm> GetListOfPersonalOrders(string email)
        {
            if (listOfOrdersToConfirm.Count() != 0)
            {
                listOfOrdersToConfirm.Clear();
            }

            try
            {
                SqlQuery.GetAllInforamtionInSqlTable("exec GetOrdersThatBelongToMe" + " " + "'" + email + "'", CreateListOfOrdersToConfirm);
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }
      

            if(listOfOrdersToConfirm.Count == 0)
            {
                Log.AddLogItemToQueue("Can't find list of orders by users that purchase a product",null,"Error");
            }

            return listOfOrdersToConfirm;
        }

        public void DelProduct(int campaignId, string productName)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("delete from Products where CampaignID =" + " " + campaignId + " " + "and ProductName = " + " " + "'" + productName + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            

        }

        public int RetriveProductID(int campaignId, string productName)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("select ProductID from Products where CampaignID =" + campaignId + "and ProductName =" + "'" + productName + "'", GetSingleValueOrRowFromDB);

            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            
            return ProductID;
        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            ProductID = (int)command.ExecuteScalar();
            
            return;

        }

        public void UProduct(UpdatedProduct product)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdataProduct " + " " + "'" + product.productName + "'" + "," + decimal.Parse(product.unitPrice) + "," + int.Parse(product.unitsInStock) + "," + product.productId);
            }
            catch (Exception exc)
            {
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");

            }

        }

        public void ConfirmationOfOrder(int orderId, string email)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec OrderConfirmation" + " " + orderId + "," + "'" + email + "'");
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            
        }

        public void CompanyRegistration(RegisterCompany company)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec CreateBusinessCompany" + " " + "'" + company.companyName + "'" + "," + "'" + company.companyWebsite + "'" + "," + "'" + company.RegisteredCompany + "'" + "," + "'" + company.Email + "'");
            }
            catch (Exception exc )
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            
        }

        public string BusinessCompanyName(int ProductID)
        {
               
            SqlQuery.GetSingleRowOrValue("exec getCompanyName" + " " + ProductID, GetSingleStringFromDB);
            if(companyName == "none")
            {
                Log.AddLogItemToQueue("Can't find company name in database", null, "Error");
            }
            return companyName;
        }

        public void GetSingleStringFromDB(SqlCommand command)
        {
            if (command.ExecuteScalar() == null)
            {
                companyName = "none";
            }
            else
            {
                companyName = (string)command.ExecuteScalar();
            }

            return;

        }
    }
}
