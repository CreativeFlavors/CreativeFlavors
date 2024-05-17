using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
   public class GateEntryInwardDocumentUploadMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<GateEntryInwardDocumentUploadMaster>
    {
        public GateEntryInwardDocumentUploadMap()
        {
            HasKey(t => t.GateEntryDocumentuploadId);
            Property(t => t.GateEntryDocumentuploadId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryDocumentId).IsRequired();
            Property(t => t.UploadPath);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            ToTable("GateEntryInwardDocumentupload");
        }
    }
}
