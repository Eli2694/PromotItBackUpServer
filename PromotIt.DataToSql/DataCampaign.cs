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
        //Global
        int ID;

        public void addCampagin(string name, string website, string hashtag, string Email, string donation)
        {
            try
            {

                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InsertCampagin" + " " + "'" + name + "'" + "," + "'" + hashtag + "'" + "," + "'" + website + "'" + ","  + "'" + Email + "'" + "," + decimal.Parse(donation));



            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + "," + "Can't insert to database  campaign :" + name + "," + website);
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
            if(listOfPersonalCampaigns == null)
            {
                Logger.LogError("Can't find list of personal campaigns for nonprofit user");
            }
            return listOfPersonalCampaigns;
        }

        public void updateCampaign(PersonalCampagin campaign)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdatePersonalCampaign" + " " + "'" + campaign.campaignName + "'" + "," + "'" + campaign.campaginHashtag + "'" + "," + "'" + campaign.campaignWebsite + "'" + "," + "'" + campaign.CampaignId + "'");
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "Can't update personal campaign :" + campaign.campaignName);
            }

            
        }

        public void deleteCampaign(int ID)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec deleteCampaignAndItsProducts" + " " + ID);
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "Can't delete campaign  id:" + ID);
            }

            
            
        }

        public void DonationAmount(int campaignID,string unitPrice)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec updateDonationAmount" + " " + campaignID + "," + decimal.Parse(unitPrice));
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message + "," + "Can't update donation amount of campaign id:" + campaignID );

            }

            
        }

        public int CampaginID(int productID)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("select CampaignID from Products where ProductID =" + productID, GetSingleValueOrRowFromDB);
            }
            catch (Exception ex)
            {

                Logger.LogError("Can't find campaign id that product with " +productID + " " + "belong to" + "," + ex.Message);
            }

           
            
            return ID;

        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            try
            {
                ID = (int)command.ExecuteScalar();
                return;
            }
            catch (Exception ex)
            {
                Logger.LogError("Can't find campaign id" + "," + ex.Message);
                Console.WriteLine(ex.Message);

            }

        }
    }
}
