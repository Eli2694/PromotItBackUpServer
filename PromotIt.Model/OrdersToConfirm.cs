using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class OrdersToConfirm
    {
        public int orderID { get; set; }
        public int productId { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string homeAddress { get; set; }
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public string productName { get; set; }
        public string unitPrice { get; set; }
        public string unitsInStock { get; set; }
        public string date { get; set; }
    }
}
