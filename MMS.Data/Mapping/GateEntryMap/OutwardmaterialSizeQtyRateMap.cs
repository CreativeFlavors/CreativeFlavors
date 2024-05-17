using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class OutwardmaterialSizeQtyRateMap : EntityTypeConfiguration<OutwardMaterialSizeQtyRateMaster>
    {
        public OutwardmaterialSizeQtyRateMap()
        {
            HasKey(t => t.OutwardMaterialSizeQtyRateId);
            Property(t => t.OutwardMaterialSizeQtyRateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryOutwardId);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.Size);
            Property(t => t.Quantity);
            Property(t => t.Rate);
            Property(t => t.MaterialId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("OutwardMaterialSizeQtyRate");
        }
    }
}
