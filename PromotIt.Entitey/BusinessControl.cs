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

        public void GetProductInfo(string name, decimal unitprice,int unitInStock,int campaignId,string email)
        {
            DataBusiness donate = new DataBusiness();
            donate.DonateProductToCampaign(name,unitprice,unitInStock,campaignId, email);
        }

        public List<Product> ListOfCampaignProducts(int campaignId,string email)
        {
            List<Product> products = new List<Product>();
            DataBusiness list = new DataBusiness();
            products =  list.GetListOfProductsToSpecificCampaign(campaignId, email);
            return products;
        }

        public List<Product> getProducts(int campaignId)
        {
            List<Product> products = new List<Product>();
            DataBusiness list = new DataBusiness();
            products = list.GetListOfProducts(campaignId);
            return products;
        }

        public List<OrdersToConfirm> getOrdersOfMyProducts(string email)
        {
            List<OrdersToConfirm> orders = new List<OrdersToConfirm>();
            DataBusiness orderList = new DataBusiness();
            orders = orderList.GetListOfPersonalOrders(email);

            return orders;
        }

        public void DeleteProduct(int campaignId,string productName)
        {
            DataBusiness delete = new DataBusiness();
            delete.DelProduct(campaignId, productName);

        }

        public int GetProductId(int campaignId,string productName)
        {
            DataBusiness productId = new DataBusiness();
            
            int id  = productId.RetriveProductID(campaignId, productName);
            return id;
        }

        public void UpdateProduct(UpdatedProduct product)
        {
            DataBusiness newProduct = new DataBusiness();
            newProduct.UProduct(product);
        }

        public void OrderConfirmation(int orderId,string email)
        {
            DataBusiness confirmation = new DataBusiness();
            confirmation.ConfirmationOfOrder(orderId, email);
        }

       
    }
}
