using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.JobWork
{
   public class ProductionQcSize_Issue : EntityTypeConfiguration<ProductionSizeQc_Issue>
    {
         public ProductionQcSize_Issue()
        {
            HasKey(t => t.QcSize_id);
            Property(t => t.QcSize_id).HasDatabaseGeneratedOption(ProductionSizeQc_Issue.Identity);
            Property(t => t.Qc_id);
            Property(t => t.Size);
            Property(t => t.Size_Bool);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            
            ToTable("SizeProductionQc_Issue");
        }
    }
}
