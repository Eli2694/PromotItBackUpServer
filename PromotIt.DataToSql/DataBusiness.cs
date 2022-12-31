using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class DataBusiness
    {
        List<PersonalCampagin> campaignsForBusiness = new List<PersonalCampagin>();
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
            SqlQuery.GetAllInforamtionInSqlTable("select CampaginName,CampaginWebsite,CampaginHashtag,CampaignsID from Campaigns", CreateListOfCampaignsForBusiness);
            return campaignsForBusiness;
        }

        public void DonateProductToCampaign(string name, decimal unitprice, int unitInStock, int campaignId)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec DonateProduct" + " " + "'" + name + "'" + "," + "'" + unitprice + "'" + "," + "'" + unitInStock + "'" + "," + "'" + campaignId + "'");
        }

        //Global
        List<Product> listOfProducts = new List<Product>();
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

                listOfProducts.Add(product);
            }

            return;
        }

        public List<Product> GetListOfProductsToSpecificCampaign(int Id)
        {
            SqlQuery.GetAllInforamtionInSqlTable("select ProductName,UnitPrice,UnitsInStock,CampaignID from Products where CampaignID =" + Id, CreateListOfCampaignProducts);
            return listOfProducts;
        }

        public void DelProduct(int campaignId, string productName)
        {

            SqlQuery.InsertInfoToTableInSql("delete from Products where CampaignID =" + " " + campaignId + " " + "and ProductName = " + " " +"'" + productName+"'");

        }
    }
}
