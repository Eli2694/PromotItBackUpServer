using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class DataActivist
    {

        //Global
        List<string> InformationFromCampaigns = new List<string>();
        public List<string> Websites()
        {
            SqlQuery.GetAllInforamtionInSqlTable("select CampaginWebsite from campaigns", CreateListOfOrdersToConfirm);
            return InformationFromCampaigns;
        }

        public List<string> Hashtags()
        {
            SqlQuery.GetAllInforamtionInSqlTable("select CampaginHashtag from campaigns", CreateListOfOrdersToConfirm);
            return InformationFromCampaigns;
        }

        public void CreateListOfOrdersToConfirm(SqlDataReader reader)
        {
            InformationFromCampaigns.Clear();

            while (reader.Read())
            {
                string campaignInfo = reader.GetString(0);

                InformationFromCampaigns.Add(campaignInfo);
            }

            return;
        }
    }
}
