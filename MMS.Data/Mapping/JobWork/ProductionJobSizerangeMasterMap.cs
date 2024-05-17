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
    class ProductionJobSizerangeMasterMap: EntityTypeConfiguration<ProductionJobSizerangeMaster>
    {
        public ProductionJobSizerangeMasterMap()
            {
            HasKey(t => t.Job_Production_Size_id);
            Property(t => t.Job_Production_Size_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Sizerange);
            Property(t => t.Qty);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("Job_Production_sizerage");
        }
    }
}
