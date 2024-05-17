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
    public class ImirMap : EntityTypeConfiguration<ImirForm>
    {
        public ImirMap()
        {
            HasKey(t => t.ImirId);
            Property(t => t.ImirId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ReportNo).IsRequired();
            Property(t => t.Date);
            Property(t => t.GateEntryNo).IsRequired();
            Property(t => t.RefInfrepNo).IsRequired();
            Property(t => t.GrnNo).IsRequired();
            Property(t => t.DcNo).IsRequired();
            Property(t => t.SupplierMasterId).IsRequired();
            Property(t => t.MaterialMasterId).IsRequired();
            Property(t => t.ColourMasterId).IsRequired();
            Property(t => t.Store).IsRequired();
            Property(t => t.Uom).IsRequired();
            Property(t => t.InspectionType).IsRequired();
            Property(t => t.DcQty).IsRequired();
            Property(t => t.ReceivedQty).IsRequired();
            Property(t => t.DcPcs).IsRequired();
            Property(t => t.ReceivedPcs).IsRequired();
            Property(t => t.QtyInspected).IsRequired();
            Property(t => t.QtyAccepted).IsRequired();
            Property(t => t.QtyRejected).IsRequired();
            Property(t => t.QtyRejectionPercent).IsRequired();
            Property(t => t.QtyExcess).IsRequired();
            Property(t => t.PcsInspected).IsRequired();
            Property(t => t.PcsAccepted).IsRequired();
            Property(t => t.PcsRejected).IsRequired();
            Property(t => t.PcsRejectionPercent).IsRequired();
            Property(t => t.PcsExcess).IsRequired();
            Property(t => t.Remarks).IsRequired();
            Property(t => t.InspectedBy).IsRequired();
            Property(t => t.QcExcecutive).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedBy).IsRequired();

            ToTable("IMIR");
        }
    }
}
