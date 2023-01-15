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
        public ReportDifferentUsersCount UsersType()
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.UserStatistics();
        }
         
        public List<ReportNonprofitUser> GetNonprofitUsers()
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.NonprofitUserList();
            
        }

        public List<ReportBusinessUser> GetBusinessUsers()
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.BusinessUserList();

        }

        public List<ReportActivistUser> GetActivistUsers()
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.ActivistUserList();

        }

        public int GetCampaignStats()
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.CampaignStats();
        }

        public List<CampaignReportGeneral> GetAllRegisteredCampaigns(string date)
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.AllRegisteredCampaigns(date);
        }

        public List<CampaignReportDonationAndTweets> GetInfoAboutDonationToCampaigns(string date)
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.GetCampaignDonation(date);

        }

        public List<CampaignReportDonationAndTweets> GetTweetsAboutCampaigns(string date)
        {
            DataOwner dataOwner = new DataOwner();
            return dataOwner.TweetsAboutCampaigns(date);
        }


    }
}
