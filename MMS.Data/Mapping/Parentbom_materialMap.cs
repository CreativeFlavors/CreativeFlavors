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
    public class Parentbom_materialMap : EntityTypeConfiguration<parentbom_material>
    {
        public Parentbom_materialMap()
        {
            HasKey(t => t.BomMaterialId);
            Property(t => t.BomMaterialId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductId);
            Property(t => t.BomID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.RequiredQty);
            Property(t => t.IsDelete);
            Property(t => t.IsActive);
            ToTable("parentbom_material");
        }

    }
}
