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


    }
}
