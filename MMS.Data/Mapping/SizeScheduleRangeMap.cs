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
  public  class SizeScheduleRangeMap: EntityTypeConfiguration<SizeScheduleRange>
    {
        public SizeScheduleRangeMap()
        {
            HasKey(t => t.SizeScheduleID);
            Property(t => t.SizeScheduleID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SizeScheduleMasterID);
            Property(t => t.Frame);
            Property(t => t.Size);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            ToTable("SizeScheduleRange");
        }
    }
}
