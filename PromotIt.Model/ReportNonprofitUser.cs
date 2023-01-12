using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class ReportNonprofitUser
    {
        public string email { get; set; }
        public string associationName { get; set; }
        public int createdCampaigns { get; set; }
    }
}
