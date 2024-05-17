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
    public class AgentMasterMap : EntityTypeConfiguration<AgentMaster>
    {
        public AgentMasterMap()
        {
            HasKey(t => t.AgentMasterId);
            Property(t => t.AgentMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AgentCode).IsRequired();
            Property(t => t.AgentFullName).IsRequired();
            Property(t => t.Currency).IsRequired();
            Property(t => t.CommisionCriteriaId).IsRequired();
            Property(t => t.AddressLine1).IsRequired();
            Property(t => t.AddressLine2);
            Property(t => t.AddressLine3);
            Property(t => t.Pincode).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.ContactPerson);
            Property(t => t.MobileNo);
            Property(t => t.EmailID);
            Property(t => t.CountryMasterId).IsRequired();
            Property(t => t.CommisionPercentage).IsRequired();
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("AgentMaster");
        }
    }
}
