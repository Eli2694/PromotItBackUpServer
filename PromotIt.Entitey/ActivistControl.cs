using PromotIt.DataToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class ActivistControl
    {

        public void initiateActivistPoints(string email)
        {
            DataActivist points = new DataActivist();
            points.initiatePoints(email);
        }

        public void initiateCampaginPromotion(int CampaignId,string email)
        {
            DataActivist campagin = new DataActivist();
            campagin.initiateCampagin(CampaignId,email);
        }

        public void UpdateUserPoints(string email , int points)
        {
            DataActivist activist = new DataActivist();
            activist.UpdatePoints(email,points);
        }

        public void UpdateTweetsAmount(string email,int tweets,int campaignId)
        {
            DataActivist tweetsAmount = new DataActivist();
            tweetsAmount.UpdateTweetsPerCampagin(email,tweets,campaignId);
        }

        public int GetActivistPoints(string email)
        {
            DataActivist get = new DataActivist();
            int points = get.ActivistPoints(email);
            return points;
        }

        public void DecreasePointsAmount(int points,string email)
        {
            DataActivist decrease =new DataActivist();
            decrease.DecreaseActivistPoints(points, email);
        }

    }
}
