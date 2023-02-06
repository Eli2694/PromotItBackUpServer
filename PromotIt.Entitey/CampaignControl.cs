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
    public class CampaignControl : BaseEntity
    {
        public DataCampaign campaign { get; set; }

        LogManager Log;
        public CampaignControl(LogManager log) : base(log)
        {
            Log = LogInstance;

            campaign = new DataCampaign(LogInstance);

        }
        public void GetCampaginInfo(string name, string website, string hashtag, string userEmail,string donation)
        {
       
            campaign.addCampagin(name, website, hashtag, userEmail, donation);
        }

        public List<PersonalCampagin> getListOfPersonalCampaigns(string email)
        {
            List<PersonalCampagin> list = new List<PersonalCampagin>();
            list = campaign.GetListOfPersonalCampaigns(email);
            return list;
            
        }

        public void UpdatePerCamp(PersonalCampagin campaing)
        {
            campaign.updateCampaign(campaing);

        }

        public void DeleteCampaign(int ID)
        {
            campaign.deleteCampaign(ID);
        }

        public void UpdateDonationAmount(int campaignId,string unitprice)
        {
            campaign.DonationAmount(campaignId,unitprice);
        }

        public int GetCampaginID(int productID)
        {
            return campaign.CampaginID(productID);
        }
    }
}
