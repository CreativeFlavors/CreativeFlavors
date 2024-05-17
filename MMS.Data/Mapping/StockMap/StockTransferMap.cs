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
    public class StockTransferMap : EntityTypeConfiguration<StockTransfer>
    {
        public StockTransferMap()
        {
            HasKey(t => t.StockTransferID);
            Property(t => t.StockTransferID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TypeId).IsRequired();
            Property(t => t.MaterialCategoryID).IsRequired();
            Property(t => t.MaterialGroupID).IsRequired();
            Property(t => t.ColorID).IsRequired();
            Property(t => t.QuantityAmount).IsRequired();
            Property(t => t.QuantityValue).IsRequired();
            Property(t => t.IssuedToFrom).IsRequired();
            Property(t => t.Stores).IsRequired();
            Property(t => t.DcGrnNo).IsRequired();
            Property(t => t.TransportDetails).IsRequired();
            Property(t => t.ClosingStockInAllStores).IsRequired();
            Property(t => t.ClosingStockInCurrentStores).IsRequired();
            Property(t => t.ReservedStock).IsRequired();
            Property(t => t.FreeStock).IsRequired();
            Property(t => t.Remarks).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.Value).IsRequired();
            Property(t => t.PartyDcNo).IsRequired();
            Property(t => t.InvoiceRef).IsRequired();
            Property(t => t.InvoiceDate).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedBy).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("tbl_StockTransferMaster");
        }
    }
}
