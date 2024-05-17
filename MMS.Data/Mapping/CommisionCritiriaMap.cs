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
  public  class CommisionCritiriaMap:EntityTypeConfiguration<CommisionCriteria>
    {
      public CommisionCritiriaMap()
        {
            HasKey(t => t.CommisionCriteriaId);
            Property(t => t.CommisionCriteriaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CommisionName).IsRequired();
            Property(t => t.Criteria).IsRequired();
            Property(t => t.Value).IsRequired();
            Property(t => t.CommisionPercent).IsRequired();           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("CommisionCriteria");
        }
    }
}
