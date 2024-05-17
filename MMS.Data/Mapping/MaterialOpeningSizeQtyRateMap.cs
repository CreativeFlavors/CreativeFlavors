using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class MaterialOpeningSizeQtyRateMap : EntityTypeConfiguration<MaterialOpeningSizeQtyRate>
    {
        public MaterialOpeningSizeQtyRateMap()
        {
            HasKey(t => t.MaterialOpeningSizeQtyRateId);
            Property(t => t.MaterialOpeningSizeQtyRateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size);
            Property(t => t.Quantity);
            Property(t => t.Rate);
            Property(t => t.MaterialOpeningId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedDate);
            ToTable("MaterialOpeningSizeQtyRate");
        }
    }
}
