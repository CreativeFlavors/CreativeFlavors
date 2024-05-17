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
    public class SizeItemsStockAdjustmentMap : EntityTypeConfiguration<SizeItemsStockAdjustment>
    {
        public SizeItemsStockAdjustmentMap()
        {
            HasKey(t => t.SizeMaterialD);
            Property(t => t.SizeMaterialD).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StockAdjustmentFk);
            Property(t => t.BookStock);
            Property(t => t.PhysicalStock);
            Property(t => t.VariationStock);
            Property(t => t.PlusStock);
            Property(t => t.MinusStock);
            Property(t => t.MaterialMasterId);
            Property(t => t.Size);
            Property(t => t.Quantity);
            Property(t => t.CreatedBY);
            Property(t => t.UpdatedBy);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            ToTable("SizeItemsStockAdjustment");
        }
    }
}
