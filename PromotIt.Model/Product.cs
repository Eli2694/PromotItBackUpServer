using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class Product
    {
        public string productName { get; set; }
        public string unitPrice { get; set; }
        public string unitsInStock { get; set; }
        public int CampaignId { get; set; }
        public string Email { get; set; }
        public string imageURL { get; set; }

    }
}
