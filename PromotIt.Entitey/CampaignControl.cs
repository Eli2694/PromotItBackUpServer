﻿using PromotIt.DataToSql;
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
        public void GetCampaginInfo(string name, string website, string hashtag, string userName, string userEmail)
        {
            try
            {
                DataCampaign campaign = new DataCampaign();
                campaign.addCampagin(name, website, hashtag, userName, userEmail);
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
    }
}
