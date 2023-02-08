using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PersonalUtilities;

namespace PromotIt.DataToSql
{
    public class DataCampaign : BaseDataSql
    {
        //Global
        int ID;
        List<PersonalCampagin> listOfPersonalCampaigns = new List<PersonalCampagin>();

        LogManager Log { get; set; }
        public DataCampaign(LogManager log) : base(log)
        {
            Log = LogInstance;
        }

        public void addCampagin(string name, string website, string hashtag, string Email, string donation)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec InsertCampagin" + " " + "'" + name + "'" + "," + "'" + hashtag + "'" + "," + "'" + website + "'" + "," + "'" + Email + "'" + "," + decimal.Parse(donation));
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            

        }
        
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
                Log.AddLogItemToQueue("Can't find list of personal campaigns for nonprofit user",null,"Error");
            }
            return listOfPersonalCampaigns;
        }

        public void updateCampaign(PersonalCampagin campaign)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSqlAndGetAnswer("exec UpdatePersonalCampaign" + " " + "'" + campaign.campaignName + "'" + "," + "'" + campaign.campaginHashtag + "'" + "," + "'" + campaign.campaignWebsite + "'" + "," + "'" + campaign.CampaignId + "'");
            }
            catch (Exception exc)
            { 
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

        }

        public void deleteCampaign(int ID)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec deleteCampaignAndItsProducts" + " " + ID);
            }
            catch (Exception exc)
            { 
                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            

        }

        public void DonationAmount(int campaignID,string unitPrice)
        {
            try
            {
                SqlQuery.InsertInfoToTableInSql("exec updateDonationAmount" + " " + campaignID + "," + decimal.Parse(unitPrice));
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }

            
        }

        public int CampaginID(int productID)
        {
            try
            {
                SqlQuery.GetSingleRowOrValue("select CampaignID from Products where ProductID =" + productID, GetSingleValueOrRowFromDB);
            }
            catch (Exception exc)
            {

                Log.AddLogItemToQueue(exc.Message, exc, "Exception");
            }
      
            return ID;

        }

        public void GetSingleValueOrRowFromDB(SqlCommand command)
        {
            ID = (int)command.ExecuteScalar();
            return;

        }
    }
}
