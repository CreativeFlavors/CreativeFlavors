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
   public  class GEInwardDocTypeMap : EntityTypeConfiguration<GateEntryDocTypeMaster>
    {
        public GEInwardDocTypeMap()
    {
        HasKey(t => t.InwardDocumentTypeId);
        Property(t => t.InwardDocumentTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        Property(t => t.DocumentType);
       
        Property(t => t.CreatedBy);
        Property(t => t.UpdatedBy);
        Property(t => t.IsDeleted);

        Property(t => t.CreatedDate);
        Property(t => t.UpdatedDate);


        ToTable("GEInwardDocType");
    }
}
}
