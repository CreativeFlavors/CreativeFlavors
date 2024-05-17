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
    public class GateEntryInwardMaterialsMap : EntityTypeConfiguration<GateEntryInwardMaterialsMaster>
    {
        public GateEntryInwardMaterialsMap()
        {
            HasKey(t => t.GateEntryInwardId);
            Property(t => t.GateEntryInwardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryNo).IsRequired();
            Property(t => t.InwardEntryDateTime).IsRequired();
            Property(t => t.InwardMaterialType);
            Property(t => t.IsNewInward);
            Property(t => t.IsReturned);
            Property(t => t.IsJobWork);
            Property(t => t.DcRefNo);
            Property(t => t.DcRefDate);
            Property(t => t.Company);
            Property(t => t.MaterialName);
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
            Property(t => t.StoresId);
            Property(t => t.MaterialNameId);
            Property(t => t.HSNCode);
            Property(t => t.SizeRangeId);
            Property(t => t.UomId);
            Property(t => t.PendingQuantity);
            Property(t => t.Quantity);
            Property(t => t.Piecies);
            Property(t => t.Pcs);
            Property(t => t.ReceivedBy);
            Property(t => t.AcknowledgedBy);
            Property(t => t.Remarks);
            Property(t => t.DcTotal);
            Property(t => t.ArrivedTotal);
            Property(t => t.InwardPo);
            Property(t => t.PoTotal);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.jobsheet_pair_Code_id);
            Property(t => t.Process_Name);
            Property(t => t.Manual_DcNo);
            Property(t => t.IssueSlipId);
            Property(t => t.ComponentId);
            ToTable("GateEntryInwardMaterial");
        }
    }
}
