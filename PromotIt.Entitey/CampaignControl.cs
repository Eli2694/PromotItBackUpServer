using PromotIt.DataToSql;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class CampaignControl
    {
        public void GetCampaginInfo(string name, string website, string hashtag, string userEmail,string donation)
        {
            try
            {
                DataCampaign campaign = new DataCampaign();
                campaign.addCampagin(name, website, hashtag,  userEmail, donation);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public List<PersonalCampagin> getListOfPersonalCampaigns(string email)
        {
            List<PersonalCampagin> list = new List<PersonalCampagin>();
            DataCampaign getList = new DataCampaign();
            list = getList.GetListOfPersonalCampaigns(email);
            return list;
            
        }

        public void UpdatePerCamp(PersonalCampagin campaing)
        {
            DataCampaign update = new DataCampaign();
            update.updateCampaign(campaing);

        }

        public void DeleteCampaign(int ID)
        {
            DataCampaign delete = new DataCampaign();   
            delete.deleteCampaign(ID);
        }

        public void UpdateDonationAmount(int campaignId,string unitprice)
        {
            DataCampaign update = new DataCampaign();
            update.DonationAmount(campaignId,unitprice);
        }

        public int GetCampaginID(int productID)
        {
            DataCampaign id = new DataCampaign();
            return id.CampaginID(productID);
        }
    }
}
