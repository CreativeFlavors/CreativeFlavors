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
   public class StageMasterMap:EntityTypeConfiguration<StageMaster>
    {
       public StageMasterMap()
        {
            HasKey(t => t.StageMasterId);
            Property(t => t.StageMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProcessCode).IsRequired();
            Property(t => t.OrderType).IsRequired();
            Property(t => t.StageCode).IsRequired();
            Property(t => t.StageName).IsRequired();
            Property(t => t.SubStage).IsRequired();
            Property(t => t.Sequence);
            Property(t => t.IsInspection);           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("StageMaster");
        }
    }
}
