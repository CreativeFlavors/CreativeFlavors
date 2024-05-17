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
    public class StockAdjustmentMap : EntityTypeConfiguration<StockAdjustmentForm>
    {
        public StockAdjustmentMap()
        {
            HasKey(t => t.StockAdjustmentId);
            Property(t => t.StockAdjustmentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AsOnDate);
            Property(t => t.StoreId);
            Property(t => t.CategoryId);
            Property(t => t.GroupId);
            Property(t => t.SubGroupId);
            Property(t => t.MaterialType);
            Property(t => t.MaterialMasterId);
            Property(t => t.UomId).IsRequired();
            Property(t => t.MaterialMasterId);
            Property(t => t.Rate);
            Property(t => t.IsApproved);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("StockAdjustment");
        }
    }
}
