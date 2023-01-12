using PromotIt.DataLayer;
using PromotIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class DataOwner
    {
        //Global Variable
        ReportDifferentUsersCount users = new ReportDifferentUsersCount();
        List<ReportNonprofitUser> nonprofitUsers = new List<ReportNonprofitUser>();
        List<ReportBusinessUser> reportBusinessUsers = new List<ReportBusinessUser>();
        List<ReportActivistUser> reportActivistUsers = new List<ReportActivistUser>();

        public ReportDifferentUsersCount UserStatistics()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec CountDifferentUsers", GetStatistics);
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
            return nonprofitUsers;
        }

        public void Nonprofit(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportNonprofitUser nonprofit = new ReportNonprofitUser();

                if(reader.GetString(0) == null)
                {
                    nonprofit.email = "none";
                }
                else
                {
                    nonprofit.email = reader.GetString(0);
                }

                if (reader.GetString(1) == null)
                {
                    nonprofit.associationName = "none";
                }
                else
                {
                    nonprofit.associationName = reader.GetString(1);
                }

                nonprofit.createdCampaigns = reader.GetInt32(2);

                nonprofitUsers.Add(nonprofit);

            }

            return;
        }

        public List<ReportBusinessUser> BusinessUserList()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec Buisness", Buisness);
            return reportBusinessUsers;
        }

        public void Buisness(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportBusinessUser businessUsers = new ReportBusinessUser();

                if (reader.GetString(0) == null)
                {
                    businessUsers.email = "none";
                }
                else
                {
                    businessUsers.email = reader.GetString(0);
                }

                if (reader.GetString(1) == null)
                {
                    businessUsers.companyName = "none";
                }
                else
                {
                    businessUsers.companyName = reader.GetString(1);
                }

                businessUsers.donatedProductAmount = reader.GetInt32(2);

                reportBusinessUsers.Add(businessUsers);

            }

            return;
        }

        public List<ReportActivistUser> ActivistUserList()
        {
            SqlQuery.GetAllInforamtionInSqlTable("exec Activist", Activist);
            return reportActivistUsers;
        }

        public void Activist(SqlDataReader reader)
        {

            while (reader.Read())
            {
                ReportActivistUser activist = new ReportActivistUser();

                if (reader.GetString(0) == null)
                {
                    activist.email = "none";
                }
                else
                {
                    activist.email = reader.GetString(0);
                }

                if (reader.GetString(1) == null)
                {
                    activist.campaignName = "none";
                }
                else
                {
                    activist.campaignName = reader.GetString(1);
                }

                activist.numberOfTweets = reader.GetInt32(2);


                if (reader.GetDateTime(3) == null)
                {
                    activist.lastDayOfWork = "none";
                }
                else
                {
                    activist.campaignName = reader.GetDateTime(3).ToString();
                }

                reportActivistUsers.Add(activist);

            }

            return;
        }
    }
}
