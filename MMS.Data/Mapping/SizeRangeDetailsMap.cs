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
    public class SizeRangeDetailsMap : EntityTypeConfiguration<SizeRangeDetails>
    {
        public SizeRangeDetailsMap()
        {
            HasKey(t => t.SizeRangeDetailsId);
            Property(t => t.SizeRangeDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SizeNo).IsRequired();
            Property(t => t.SizeRangeName).IsRequired();
         //   Property(t => t.SizeRangeMasterId).IsRequired();                 
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("SizeRangeDetails");
        }
    }
}
