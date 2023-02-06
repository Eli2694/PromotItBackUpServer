using PersonalUtilities;
using PromotIt.DataToSql;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class BusinessControl : BaseEntity
    {

        public DataBusiness dataBusiness { get; set; }

        LogManager Log;
        public BusinessControl(LogManager log) : base(log)
        {
            Log = LogInstance;

            dataBusiness = new DataBusiness(LogInstance);

        }


        public List<PersonalCampagin> ListOfCampaignsBusiness()
        {
            List<PersonalCampagin> list = new List<PersonalCampagin>();      
            list = dataBusiness.GetListOfCampaigns();
            return list;

        }

        public void GetProductInfo(string name, decimal unitprice,int unitInStock,int campaignId,string email,string image)
        {
            
            dataBusiness.DonateProductToCampaign(name,unitprice,unitInStock,campaignId, email, image);
        }

        public List<Product> ListOfCampaignProducts(int campaignId,string email)
        {
            List<Product> products = new List<Product>();
            products = dataBusiness.GetListOfProductsToSpecificCampaign(campaignId, email);
            return products;
        }

        public List<Product> getProducts(int campaignId)
        {
            List<Product> products = new List<Product>();
            products = dataBusiness.GetListOfProducts(campaignId);
            return products;
        }

        public List<OrdersToConfirm> getOrdersOfMyProducts(string email)
        {
            List<OrdersToConfirm> orders = new List<OrdersToConfirm>();
            orders = dataBusiness.GetListOfPersonalOrders(email);

            return orders;
        }

        public void DeleteProduct(int campaignId,string productName)
        {
            dataBusiness.DelProduct(campaignId, productName);

        }

        public int GetProductId(int campaignId,string productName)
        {
            
            int id  = dataBusiness.RetriveProductID(campaignId, productName);
            return id;
        }

        public void UpdateProduct(UpdatedProduct product)
        {
            dataBusiness.UProduct(product);
        }

        public void OrderConfirmation(int orderId,string email)
        {
            dataBusiness.ConfirmationOfOrder(orderId, email);
        }

        public void BusinessCompanyRegistration(RegisterCompany company)
        {
            dataBusiness.CompanyRegistration(company);
        }

        public string getBusinessCompanyName(int ProductID)
        {
            string name = dataBusiness.BusinessCompanyName(ProductID);
            return name;
        }

    }
}
