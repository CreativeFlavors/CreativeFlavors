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
    public class GEMaterialTypeMap : EntityTypeConfiguration<GEMaterialTypeMaster>
    {
        public GEMaterialTypeMap()
        {
            HasKey(t => t.GEMaterialTypeId);
            Property(t => t.GEMaterialTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GEMaterialTypeId);            
            Property(t => t.MaterialType).IsRequired();
            Property(t => t.IsDeleted);
          
            ToTable("GEMaterialType");
    }
    }
}
