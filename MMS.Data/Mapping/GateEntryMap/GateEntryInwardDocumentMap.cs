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
    public class GateEntryInwardDocumentMap : EntityTypeConfiguration<GateEntryInwardDocumentMaster>
    {
        public GateEntryInwardDocumentMap()
        {
            HasKey(t => t.GateEntryDocumentId);
            Property(t => t.GateEntryDocumentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.InwardDocTypeId).IsRequired();            
            Property(t => t.GateEntryNo).IsRequired();
            Property(t => t.EntryDateTime).IsRequired();
            Property(t => t.Mode);
            Property(t => t.Company);
            Property(t => t.FromWhere).IsRequired();
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
            Property(t => t.ReceivedBy).IsRequired();
            Property(t => t.Remarks);
            Property(t => t.IsDeleted);
            Property(t => t.TagName);

            ToTable("GateEntryInwardDocument");
    }
    }
}
