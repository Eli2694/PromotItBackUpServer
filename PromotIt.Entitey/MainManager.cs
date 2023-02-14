using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PersonalUtilities;
using PromotIt.DataLayer;
using PromotIt.Model;
using static PersonalUtilities.LogManager;


namespace PromotIt.Entitey
{
    public class MainManager
    {
        public CommandManager CommandManager { get; set; }
        public UserControl userControl { get; set; }
        public AssociationControl AssociationControl { get; set; }
        public CampaignControl CampaignControl { get; set; }
        public BusinessControl BusinessControl { get; set; }
        public ActivistControl ActivistControl { get; set; }
        public OwnerControl OwnerControl { get; set; }
        public SqlQuery sqlQuery { get; set; }
        public LogManager Log { get; set; }

        //constructor
        private MainManager() 
        {
            init();
        }

        public void init()
        {
            

            CommandManager = new CommandManager();
            sqlQuery= new SqlQuery();

            Target(LogProvider.File);
            Log = new LogManager();

            
            userControl = new UserControl(Log);
            AssociationControl = new AssociationControl(Log);
            CampaignControl = new CampaignControl(Log);
            BusinessControl = new BusinessControl(Log);
            ActivistControl = new ActivistControl(Log);
            OwnerControl = new OwnerControl(Log);

        }

        // Singleton variable
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance
        {
            get { return instance; }
        }

        
    }
}
