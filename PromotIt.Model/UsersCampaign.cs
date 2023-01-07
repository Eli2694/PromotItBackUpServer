using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class UsersCampaign
    {
        public int CampaignId { get; set; }
        public string AssociationName { get; set; }
        public string AssociationWebsite { get; set; }
        public string campaignName { get; set; }
        public string campaginHashtag { get; set; }
        public string campaignWebsite { get; set; }
        public string donationAmount { get; set; }
    }
}
