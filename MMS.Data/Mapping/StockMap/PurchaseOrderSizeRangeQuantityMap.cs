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
    public class PurchaseOrderSizeRangeQuantityMap : EntityTypeConfiguration<PurchaseOrderSizeRangeQuantity>
    {
        public PurchaseOrderSizeRangeQuantityMap()
        {
            HasKey(t => t.PurchaseSizeRangeID);
            Property(t => t.PurchaseSizeRangeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size);
            Property(t => t.quantity);
            Property(t => t.Rate);
            Property(t => t.PoOrderID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("PurchaseOrderSizeRangeQuantity");
        }
    }
}
