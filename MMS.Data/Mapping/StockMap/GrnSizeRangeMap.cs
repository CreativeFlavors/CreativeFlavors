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
   public class GrnSizeRangeMap : EntityTypeConfiguration<GRNSizeQuantityObject>
    {
        public GrnSizeRangeMap()
        {
            HasKey(t => t.GRNeSizeRangeID);
            Property(t => t.GRNeSizeRangeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size);
            Property(t => t.quantity);
            Property(t => t.Rate);
            Property(t => t.Grnid_FK);
            Property(t => t.SizeRangeRefValue);
            Property(t => t.Qty_Value);
            Property(t => t.Received_value);
            Property(t => t.Shortage_value);
            Property(t => t.Rejectd_value);
            Property(t => t.Accepted_value);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);          
            ToTable("GRNSizeQuantityObject");
        }
    }
}
