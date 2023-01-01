using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotIt.DataLayer;
using System.Data.SqlClient;
using PromotIt.Model;

namespace PromotIt.DataToSql
{
    public class DataUser
    {
        public void addUserToTableInSql(string FullName, string Email)
        {
            try
            {
                
                SqlQuery.InsertInfoToTableInSql("exec checkNewSiteUser" + " " +"'" +  FullName + "'" +"," +"'" + Email + "'");

            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.Message);
            }
            
        }

        //Global Variable
        List<UsersCampaign> uc = new List<UsersCampaign>();
        public void CreateListOfAssociationsAndCampaigns(SqlDataReader reader)
        {

            while (reader.Read())
            {
                UsersCampaign user = new UsersCampaign();

                user.CampaignId = reader.GetInt32(0);
                user.AssociationName = reader.GetString(1);
                user.AssociationWebsite = reader.GetString(2);
                user.campaignName = reader.GetString(3);
                user.campaginHashtag = reader.GetString(4);
                user.campaignWebsite = reader.GetString(5);

                uc.Add(user);
            }

            return;
        }

        public List<UsersCampaign> GetListOfCampaignsForUsers()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec GetAllInfoAboutCampaigns", CreateListOfAssociationsAndCampaigns);

            return uc;
        }
    }
}
