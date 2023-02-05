using PromotIt.DataToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class AssociationControl : BaseEntity
    {

        public DataAssociation association = new DataAssociation();

        public void AssociationInforamtion(string AssociationName, string AssociationEmail,string AssociationWebsite, string RegisteredAssociation, string FullName, string Email)
        {
            try
            {
                
                association.addAssociationSql(AssociationName, AssociationEmail, AssociationWebsite, RegisteredAssociation, FullName, Email);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
