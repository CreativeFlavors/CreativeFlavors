using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class FloorReturnSizeRangeMap : EntityTypeConfiguration<FloorReturnSizeRange>
    {
        public FloorReturnSizeRangeMap()
        {
            HasKey(t => t.SizeRangeFloorMaterialId);
            Property(t => t.SizeRangeFloorMaterialId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FloorReturnMaterialDetailsId);
            Property(t => t.SizeRange);
            Property(t => t.Quantity);
            Property(t => t.ReturnedQuantity);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("FloorReturnMaterialSizeRange");

        }
    }
  
}
