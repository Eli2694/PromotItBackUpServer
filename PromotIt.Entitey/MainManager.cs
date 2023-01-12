using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class MainManager
    {
        //constructor
        private MainManager() { }

        // Singleton variable
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance
        {
            get { return instance; }
        }

        //Class instance to get access to its functions
        public UserControl userControl = new UserControl();
        public AssociationControl AssociationControl = new AssociationControl();
        public CampaignControl CampaignControl = new CampaignControl();
        public BusinessControl BusinessControl = new BusinessControl();
        public ActivistControl ActivistControl = new ActivistControl();
        public OwnerControl OwnerControl = new OwnerControl();
    }
}
