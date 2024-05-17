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
    public class StockTransferMasterMap : EntityTypeConfiguration<StockTransferMaster>
    {
        public StockTransferMasterMap()
        {
            HasKey(t => t.StockTransferID);
            Property(t => t.StockTransferID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GrnTypeMasterId).IsRequired();
            Property(t => t.GRNNo).IsRequired();
            Property(t => t.IssuedTo).IsRequired();
            Property(t => t.StoreMasterId);
            Property(t => t.TransportDetails).IsRequired();
            Property(t => t.Remarks).IsRequired();
            Property(t => t.Rate);
            Property(t => t.Value).IsRequired();
            Property(t => t.PartyDCNo);
            Property(t => t.MatCategoryId);
            Property(t => t.ColourId).IsRequired();
            Property(t => t.Quantity);
            Property(t => t.ClosingStockInAllStores).IsRequired();
            Property(t => t.ClosingStockingInCurrentStores);
            Property(t => t.ReservedStock);
            Property(t => t.FreeStock);
            Property(t => t.InvoiceRef);
            Property(t => t.InvoiceDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("StockTransferMaster");
        }
    }
}
