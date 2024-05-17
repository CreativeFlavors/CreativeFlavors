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


    public class SupplierSubMasterMap : EntityTypeConfiguration<SupplierSubMaster>
    {
        public SupplierSubMasterMap()
        {
            HasKey(t => t.SupplierSubMasterID);
            Property(t => t.SupplierSubMasterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierMasterFKID).IsRequired();
            Property(t => t.MaterialGroupID).IsRequired();
            Property(t => t.MateriaSublMasterID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);       
            ToTable("SupplierSubMaster");
        }
    }
}
