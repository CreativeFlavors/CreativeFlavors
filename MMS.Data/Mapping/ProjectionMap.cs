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
    public class ProjectionMap :EntityTypeConfiguration<ProjectionMaster>
    {
        public ProjectionMap()
        {
            HasKey(t => t.ProjectionId);
            Property(t => t.ProjectionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OrderIndicationNo).IsRequired();
            Property(t => t.BuyerMasterId).IsRequired();
            Property(t => t.SeasonMasterId).IsRequired();
            Property(t => t.Style).IsRequired();
            Property(t => t.Date).IsRequired();
            Property(t => t.WeekNo).IsRequired();
            Property(t => t.CustomerPlan).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.IsSize);     
            Property(t => t.SizeRangeMasterId).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("Projection");
        }
    }
}
