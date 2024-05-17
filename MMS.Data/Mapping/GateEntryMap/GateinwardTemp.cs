using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities.GateEntry;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class GateinwaedTemp : EntityTypeConfiguration<InwardMaterialSizeQtyRateMasterTemp>
    {
        
        public GateinwaedTemp()
        {
            HasKey(t => t.InwardMaterialSizeQtyRateId);
            Property(t => t.InwardMaterialSizeQtyRateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryInwardId);
            Property(t => t.Size);
            Property(t => t.Quantity);
            Property(t => t.Rate);
            Property(t => t.MaterialId);
            Property(t => t.PoQty);
            Property(t => t.DcQty);
            Property(t => t.ArrivedQty);
            Property(t => t.PendingQty);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("InwardMaterialSizeQtyRate_Temptable");
        }
    }
}
    