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
    public class LeatherSizeMap : EntityTypeConfiguration<LeatherSizeMaster>
    {
        public LeatherSizeMap()
        {
            HasKey(t => t.LeatherSizeMasterId);
            Property(t => t.LeatherSizeMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Length).IsRequired();
            Property(t => t.Width).IsRequired();
            Property(t => t.Average);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("LeatherSizeMaster");
        }
    }
}
