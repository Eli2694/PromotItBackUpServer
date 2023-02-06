using PersonalUtilities;
using PromotIt.DataToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class AssociationControl :BaseEntity
    {

        public DataAssociation association { get; set; }

        LogManager Log;
        public AssociationControl(LogManager log) : base(log)
        {
            Log = LogInstance;

            association = new DataAssociation(LogInstance);

            
        }

        public void AssociationInforamtion(string AssociationName, string AssociationEmail,string AssociationWebsite, string RegisteredAssociation, string FullName, string Email)
        {
            try
            {
                
                association.addAssociationSql(AssociationName, AssociationEmail, AssociationWebsite, RegisteredAssociation, FullName, Email);
            }
            catch (Exception ex)
            {

                Log.AddLogItemToQueue("Can't Register Association", ex, "Exception");
            }

        }
    }
}
