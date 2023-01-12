using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class ReportActivistUser
    {
        public string email { get; set; }
        public string campaignName { get; set; }
        public int numberOfTweets { get; set; }
        public string lastDayOfWork { get; set; }
    }
}
