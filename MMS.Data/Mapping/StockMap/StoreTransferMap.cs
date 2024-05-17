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
    public class StoreTransferMap : EntityTypeConfiguration<StoreTransfer>
    {
        public StoreTransferMap()
        {
            HasKey(t => t.StoreTransferId);
            Property(t => t.StoreTransferId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TrnNo).IsRequired();
            Property(t => t.Date);
            Property(t => t.FromStores);
            Property(t => t.ToStores);
            Property(t => t.MaterialCategoryId).IsRequired();
            Property(t => t.MaterialGroupId).IsRequired();
            Property(t => t.MaterialMasterId).IsRequired();
            Property(t => t.ColourMasterId).IsRequired();
            Property(t => t.Description).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.ClosingInAllStores).IsRequired();
            Property(t => t.ClosingInCurrentStores).IsRequired();
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("StoreTransferForm");

        }
    }
}
