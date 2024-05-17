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
   public class SizeRangeMasterMap:EntityTypeConfiguration<SizeRangeMaster>
    {
       public SizeRangeMasterMap()
        {
            HasKey(t => t.SizeRangeMasterId);
            Property(t => t.SizeRangeMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SizeRangeRef).IsRequired();
            Property(t => t.SizeRangeRefValue).IsRequired();                
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("SizeRangeMaster");
        }
    }
}
