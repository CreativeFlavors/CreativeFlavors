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
    public class StockConversionMap : EntityTypeConfiguration<StockConversionForm>
    {
        public StockConversionMap()
        {
            HasKey(t => t.StockConversionId);
            Property(t => t.StockConversionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DocNo).IsRequired();
            Property(t => t.FromStore).IsRequired();
            Property(t => t.ToStore).IsRequired();
            Property(t => t.Date);
            Property(t => t.Remarks).IsRequired();
            Property(t => t.StockValueChange);
            Property(t => t.MaterialGroupId).IsRequired();
            Property(t => t.UomId).IsRequired();
            Property(t => t.MaterialMasterId).IsRequired();
            Property(t => t.ColourMasterId).IsRequired();
            Property(t => t.IoNo).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.ReservedStock).IsRequired();
            Property(t => t.FreeStock).IsRequired();
            Property(t => t.StockInAllStores).IsRequired();
            Property(t => t.StockInCurrentStores).IsRequired();
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("StockConversion");
        }
    }
}
