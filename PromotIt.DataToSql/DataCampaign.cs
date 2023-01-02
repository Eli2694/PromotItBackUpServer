using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromotIt.DataToSql
{
    public class DataCampaign
    {
        public void addCampagin(string name, string website, string hashtag, string userName, string Email)
        {
            try
            {

                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InsertCampagin" + " " + "'" + name + "'" + "," + "'" + hashtag + "'" + "," + "'" + website + "'" + "," + "'" + userName + "'" + "," + "'" + Email + "'");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        //Global
        List<PersonalCampagin> listOfPersonalCampaigns = new List<PersonalCampagin>();
        public void CreateListOfPersonalCampaigns(SqlDataReader reader)
        {

            while (reader.Read())
            {
                PersonalCampagin perCamp = new PersonalCampagin();

                perCamp.campaignName = reader.GetString(0);
                perCamp.campaignWebsite = reader.GetString(1);
                perCamp.campaginHashtag = reader.GetString(2);
                perCamp.CampaignId = reader.GetInt32(3);

                listOfPersonalCampaigns.Add(perCamp);
            }

            return;
        }

        public List<PersonalCampagin> GetListOfPersonalCampaigns(string email)
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec GetPersonalCampaignList" + "'" + email + "'", CreateListOfPersonalCampaigns);
            return listOfPersonalCampaigns;
        }

        public void updateCampaign(PersonalCampagin campaign)
        {
            SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdatePersonalCampaign" + " " + "'" + campaign.campaignName + "'" + "," + "'" + campaign.campaginHashtag + "'" + "," + "'" + campaign.campaignWebsite + "'" + "," + "'" + campaign.CampaignId + "'");
        }

        public void deleteCampaign(int ID)
        {
            SqlQuery.InsertInfoToTableInSql("exec deleteCampaignAndItsProducts" + " " +  ID);
        }
    }
}
