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
    public class GateEntryOutwardMap : EntityTypeConfiguration<GateEntryOutwardMaster>
    {
        public GateEntryOutwardMap()
        {
            HasKey(t => t.GateEntryOutwardId);
            Property(t => t.GateEntryOutwardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryNo).IsRequired();
            Property(t => t.OutwardEntryDateTime).IsRequired();
            Property(t => t.OutwardMaterialType);
            Property(t => t.IsNewOutward);
            Property(t => t.IsDocuments);
            Property(t => t.IsJobWork);
            Property(t => t.IsSales);
            Property(t => t.Uom);
            Property(t => t.StoresRefNo);
            Property(t => t.StoresRefDate);
            Property(t => t.StoresName);
            Property(t => t.UnitDivision);
            Property(t => t.ReturnType);
            Property(t => t.MaterialType);
            Property(t => t.DcNo);
            Property(t => t.DcDate);
            Property(t => t.InvoiceNo);
            Property(t => t.InvoiceDate);
            Property(t => t.ModeofTransport);
            Property(t => t.InvoiceCurrency);
            Property(t => t.InvoiceValue);
            Property(t => t.VehicleNo);
            Property(t => t.PoNoId);
            Property(t => t.SupplierId);
            Property(t => t.PersonId);
            Property(t => t.SizeRangeId);
            Property(t => t.MaterialNameId);
            Property(t => t.HSNCode);
            Property(t => t.UomId);
            Property(t => t.PendingQuantity);
            Property(t => t.Quantity);
            Property(t => t.Piecies);
            Property(t => t.Pcs);
            Property(t => t.PreparedBy);
            Property(t => t.ApprovedBy);
            Property(t => t.Remarks);
            Property(t => t.Rate);
            Property(t => t.TotalQty);
            Property(t => t.Value);
            Property(t => t.Total);
            Property(t => t.SupplierAcknowledgedBy);
            Property(t => t.TransportCompany);
            Property(t => t.VehicleArrangedBy);
            Property(t => t.Purpose);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            ToTable("GateEntryOutward");
        }
    }
}
