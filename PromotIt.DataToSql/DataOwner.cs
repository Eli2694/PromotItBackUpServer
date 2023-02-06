using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PersonalUtilities;

namespace PromotIt.DataToSql
{
    public class DataOwner : BaseDataSql
    {
        //Global Variable
        ReportDifferentUsersCount users = new ReportDifferentUsersCount();
        List<ReportNonprofitUser> nonprofitUsers = new List<ReportNonprofitUser>();
        List<ReportBusinessUser> reportBusinessUsers = new List<ReportBusinessUser>();
        List<ReportActivistUser> reportActivistUsers = new List<ReportActivistUser>();
        int totalCampaignCount;
        List<CampaignReportGeneral> generalCampaignReport = new List<CampaignReportGeneral>();
        List<CampaignReportDonationAndTweets> campaignReportDonationOrTweets = new List<CampaignReportDonationAndTweets>();
        int determineDonationOrTweets;

        LogManager Log { get; set; }
        public DataOwner(LogManager log) : base(log)
        {
            Log = LogInstance;
        }

        public ReportDifferentUsersCount UserStatistics()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec CountDifferentUsers", GetStatistics);
            if (users == null)
            {
                Log.AddLogItemToQueue("Can't report users statistics",null,"Error");
            }
            return users;
        }

        public void GetStatistics(SqlDataReader reader)
        {

            while (reader.Read())
            {

                users.totalUsers = reader.GetInt32(0);
                users.Business = reader.GetInt32(1);
                users.Nonprofit = reader.GetInt32(2);
                users.Activist = reader.GetInt32(3);
                
            }

            return;
        }

        public List<ReportNonprofitUser> NonprofitUserList()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec Nonprofit", Nonprofit);

            if (nonprofitUsers == null)
            {
                Log.AddLogItemToQueue("Can't report nonprofit users",null,"Error");
            }
            return nonprofitUsers;
        }

        public void Nonprofit(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportNonprofitUser nonprofit = new ReportNonprofitUser();
     
                nonprofit.email = reader.GetString(0);
                nonprofit.associationName = reader.GetString(1);
                nonprofit.createdCampaigns = reader.GetInt32(2);
                nonprofitUsers.Add(nonprofit);

            }

            return;
        }

        public List<ReportBusinessUser> BusinessUserList()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec Buisness", Buisness);

            if(reportBusinessUsers == null)
            {
                Log.AddLogItemToQueue("Can't report business users",null,"Error");
            }
            return reportBusinessUsers;
        }

        public void Buisness(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportBusinessUser businessUsers = new ReportBusinessUser();  
                businessUsers.email = reader.GetString(0);
                businessUsers.companyName = reader.GetString(1);
                businessUsers.donatedProductAmount = reader.GetInt32(2);

                reportBusinessUsers.Add(businessUsers);

            }

            return;
        }

        public List<ReportActivistUser> ActivistUserList()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec Activist", Activist);
            if(reportActivistUsers == null)
            {
                Log.AddLogItemToQueue("Can't report activist users",null,"Error");
            }
            return reportActivistUsers;
        }

        public void Activist(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportActivistUser activist = new ReportActivistUser();

                activist.email = reader.GetString(0);
                activist.campaignName = reader.GetString(1);
                activist.numberOfTweets = reader.GetInt32(2);
                activist.lastDayOfWork = reader.GetDateTime(3).ToString();
                reportActivistUsers.Add(activist);

            }

            return;
        }

        public int CampaignStats()
        {
            SqlQuery.GetSingleRowOrValue("select count(*) from Campaigns where Campaigns.isActive = 1 ", GetTotalCampaginCount);
            return totalCampaignCount;
        }

        public void GetTotalCampaginCount(SqlCommand command)
        {
            totalCampaignCount = (int)command.ExecuteScalar();
            return;

        }

        public List<CampaignReportGeneral> AllRegisteredCampaigns(string date)
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec AllCampaignsForOwner" + " " + "'" + date + "'", GeneralCampaigns);

            if(generalCampaignReport == null)
            {
                Log.AddLogItemToQueue("Can't find campaigns for owner report",null,"Error");
            }

            return generalCampaignReport;
        }

        public void GeneralCampaigns(SqlDataReader reader)
        {

            while (reader.Read())
            {
                CampaignReportGeneral campaigns = new CampaignReportGeneral();

                campaigns.associationName = reader.GetString(0);
                campaigns.campaignName = reader.GetString(1);
                campaigns.creationDate = (reader.GetDateTime(2)).ToString();

                generalCampaignReport.Add(campaigns);

            }

            return;
        }

        public List<CampaignReportDonationAndTweets> GetCampaignDonation(string date)
        {
            determineDonationOrTweets = 1;
            SqlQuery.GetAllInforamtionInSqlTable("exec getDonationAmountOfCampaigns" + " " + "'" + date + "'" , DonationAmountOfCampaignsOrNumberOfTweets);
            return campaignReportDonationOrTweets;
        }

        public void DonationAmountOfCampaignsOrNumberOfTweets(SqlDataReader reader)
        {

            while (reader.Read())
            {
                CampaignReportDonationAndTweets campaigns = new CampaignReportDonationAndTweets();

                if(determineDonationOrTweets ==1)
                {
                    campaigns.date = (reader.GetDateTime(0)).ToString();
                    campaigns.name = reader.GetString(1);
                    campaigns.amount = reader.GetDecimal(2).ToString();
                }

                if(determineDonationOrTweets == 2)
                {
                    campaigns.date = (reader.GetDateTime(0)).ToString();
                    campaigns.name = reader.GetString(1);
                    campaigns.amount = reader.GetInt32(2).ToString();
                } 

                campaignReportDonationOrTweets.Add(campaigns);

            }

            return;
        }

        public List<CampaignReportDonationAndTweets> TweetsAboutCampaigns(string date)
        {
            determineDonationOrTweets = 2;
            SqlQuery.GetAllInforamtionInSqlTable("exec getTweetsOfCampaigns" + " " + "'" +  date + "'", DonationAmountOfCampaignsOrNumberOfTweets);
            return campaignReportDonationOrTweets;
        }
    }
}
