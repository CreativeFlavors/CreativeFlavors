using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
   public class BuyerMasterMap:EntityTypeConfiguration<BuyerMaster>
    {
       public BuyerMasterMap()
        {
            HasKey(t => t.BuyerMasterId);
            Property(t => t.BuyerMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerCode).IsRequired();
            Property(t => t.BuyerFullName).IsRequired();
            Property(t => t.BuyerShortName).IsRequired();
            Property(t => t.Currency).IsRequired();
            Property(t => t.BuyerAddress1).IsRequired();
            Property(t => t.BuyerAddress2);
            Property(t => t.BuyerPincode);
            Property(t => t.Country).IsRequired();
            Property(t => t.ContactPersion).IsRequired();
            Property(t => t.Designation).IsRequired();
            Property(t => t.ContactNoo).IsRequired();
            Property(t => t.EmailID).IsRequired();
            Property(t => t.STNoHead).IsRequired();
            Property(t => t.CGTNoHead).IsRequired();
            Property(t => t.DeliverAddress1).IsRequired();
            Property(t => t.DeliverAddress2);
            Property(t => t.Pincode);
            Property(t => t.AgentName).IsRequired();
            Property(t => t.AgentAddress1).IsRequired();
            Property(t => t.AgentAddress2);
            Property(t => t.AgentPincode);
            Property(t => t.AgentCountry).IsRequired();
            Property(t => t.AgentCurrency).IsRequired();

            Property(t => t.PaymentsTerms).IsRequired();
            Property(t => t.DeliveryTerms).IsRequired();
            Property(t => t.Insurance).IsRequired();
            Property(t => t.DelierTo).IsRequired();
            Property(t => t.Brand).IsRequired();
            Property(t => t.ShipmentTo).IsRequired();
            Property(t => t.ShimentMode).IsRequired();
            Property(t => t.CountryStamp).IsRequired();

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("BuyerMaster");
          
        }
    }
}
