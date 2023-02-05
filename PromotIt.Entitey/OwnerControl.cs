using PromotIt.DataToSql;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class OwnerControl 
    {
        public DataOwner dataOwner = new DataOwner();
        public ReportDifferentUsersCount UsersType()
        {
            
            return dataOwner.UserStatistics();
        }
         
        public List<ReportNonprofitUser> GetNonprofitUsers()
        {
            return dataOwner.NonprofitUserList();
            
        }

        public List<ReportBusinessUser> GetBusinessUsers()
        {
            return dataOwner.BusinessUserList();

        }

        public List<ReportActivistUser> GetActivistUsers()
        {
            return dataOwner.ActivistUserList();

        }

        public int GetCampaignStats()
        {
            return dataOwner.CampaignStats();
        }

        public List<CampaignReportGeneral> GetAllRegisteredCampaigns(string date)
        {
            return dataOwner.AllRegisteredCampaigns(date);
        }

        public List<CampaignReportDonationAndTweets> GetInfoAboutDonationToCampaigns(string date)
        {
            return dataOwner.GetCampaignDonation(date);
        }

        public List<CampaignReportDonationAndTweets> GetTweetsAboutCampaigns(string date)
        {
            return dataOwner.TweetsAboutCampaigns(date);
        }


    }
}
