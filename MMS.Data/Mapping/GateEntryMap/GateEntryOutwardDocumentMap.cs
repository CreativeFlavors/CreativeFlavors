using MMS.Core.Entities.GateEntry;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class GateEntryOutwardDocumentMap : EntityTypeConfiguration<GateEntryOutwardDocumentMaster>
    {
        public GateEntryOutwardDocumentMap()
        {
            HasKey(t => t.GateEntryOutwardDocumentId);
            Property(t => t.GateEntryOutwardDocumentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryOutwardDocumentId);            
            Property(t => t.GateEntryNo);
            Property(t => t.EntryDateTime);
            Property(t => t.Mode);
            Property(t => t.Company);
            Property(t => t.CourierNo);
            Property(t => t.Unit);
            Property(t => t.PersonName);
            Property(t => t.ModeofTransport);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.VehicleNo);
            Property(t => t.AddressToWhom);
            Property(t => t.HandOverTo);
            Property(t => t.ReceivedBy);
            Property(t => t.Remarks);
            Property(t => t.IsDeleted);
          
            ToTable("GateEntryOutwardDocument");
    }
    }
}
