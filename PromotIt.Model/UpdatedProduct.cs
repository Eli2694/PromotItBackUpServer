using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class UpdatedProduct
    {
        public string productName { get; set; }
        public string unitPrice { get; set; }
        public string unitsInStock { get; set; }
        public int productId { get; set; }
    }
}
