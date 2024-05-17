using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class MaterialOpeningStockMap : EntityTypeConfiguration<MaterialOpeningMaster>
    {
        public MaterialOpeningStockMap()
        {
            HasKey(t => t.MaterialOpeningId);
            Property(t => t.MaterialOpeningId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Store).IsRequired();
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.MaterialMasterId);
            Property(t => t.MaterialCode);
            Property(t => t.ColorMasterId);
            Property(t => t.Date);
            Property(t => t.PrimaryUomMasterId);
            Property(t => t.SecondaryUomMasterId);
            Property(t => t.Qty);
            Property(t => t.Rate);
            Property(t => t.MaterialPcs);
            Property(t => t.QtyPcs);
            Property(t => t.SizeRangeMasterId);
            Property(t => t.Remarks);
            Property(t => t.CreatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.UpdatedBy);
            Property(t => t.MaterialCode);
            Property(t => t.DeletedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            ToTable("MaterialOpeningMaster");

        }
    }
}
