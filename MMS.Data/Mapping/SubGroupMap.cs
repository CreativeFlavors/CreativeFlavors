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
    public class SubGroupMap : EntityTypeConfiguration<SubGroup>
    {
        public SubGroupMap()
        {
            HasKey(t => t.SubGroupID);
            Property(t => t.SubGroupID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          //  Property(t => t.SubGroupCode).IsRequired();
            Property(t => t.SubGroupDescription);
            Property(t => t.IsDeleted);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeletedBy);
            Property(t => t.IsDeletedOn);
            ToTable("SubGroup");
        }
    }
}
