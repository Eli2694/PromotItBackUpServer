using PromotIt.DataToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class ActivistControl
    {
        public List<string> CampaignWebsites()
        {
            List<string> Websites = new List<string>();
            DataActivist dataActivist = new DataActivist();
            Websites = dataActivist.Websites();
            return Websites;    
        }

        public List<string> CampaignHashtag()
        {
            List<string> Hashtags = new List<string>();
            DataActivist dataActivist = new DataActivist();
            Hashtags =  dataActivist.Hashtags();
            return Hashtags;
        }

        
    }
}
