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
    public class ProductionBOMMap : EntityTypeConfiguration<ProductionBOM>
    {
        public ProductionBOMMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MaterialId);
            Property(t => t.BOMProductId);
            Property(t => t.BOMProductCode);
            Property(t => t.BOMProductName);
            Property(t => t.MaterialCategoryMasterid);
            Property(t => t.Bomid);
            Property(t => t.Description);
            Property(t => t.UomIdMaster);
            Property(t => t.Qty);
            Property(t => t.ConsumeunitId);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.IsActive);
            ToTable("productionbom");
        }
    }
}
