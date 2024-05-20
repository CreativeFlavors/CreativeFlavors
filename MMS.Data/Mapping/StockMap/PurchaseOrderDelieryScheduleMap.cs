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
    public class PurchaseOrderDelieryScheduleMap : EntityTypeConfiguration<PurchaseOrderDelierySchedule>
    {
        public PurchaseOrderDelieryScheduleMap()
        {
            HasKey(t => t.IO);
            Property(t => t.IO).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PoOrderID);
            Property(t => t.Material);
            Property(t => t.Quantity);
            Property(t => t.Date);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("PurchaseOrderDelierySchedule");
        }
    }
}
