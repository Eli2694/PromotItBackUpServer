using PromotIt.DataToSql;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class BusinessControl
    {
        public List<PersonalCampagin> ListOfCampaignsBusiness()
        {
            List<PersonalCampagin> list = new List<PersonalCampagin>();
            DataBusiness get = new DataBusiness();  

            
            list = get.GetListOfCampaigns();
            return list;

        }

        public void GetProductInfo(string name, decimal unitprice,int unitInStock,int campaignId)
        {
            DataBusiness donate = new DataBusiness();
            donate.DonateProductToCampaign(name,unitprice,unitInStock,campaignId);
        }

        public List<Product> ListOfCampaignProducts(int campaignId)
        {
            List<Product> products = new List<Product>();
            DataBusiness list = new DataBusiness();
            products =  list.GetListOfProductsToSpecificCampaign(campaignId);
            return products;
        }

        public void DeleteProduct(int campaignId,string productName)
        {
            DataBusiness delete = new DataBusiness();
            delete.DelProduct(campaignId, productName);

        }
    }
}
