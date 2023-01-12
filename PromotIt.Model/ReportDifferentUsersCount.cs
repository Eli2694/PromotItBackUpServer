using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Model
{
    public class ReportDifferentUsersCount
    {
        public int totalUsers { get; set; }

        public int Business { get; set; }

        public int Nonprofit { get; set; }

        public int Activist { get; set; }
    }
}
