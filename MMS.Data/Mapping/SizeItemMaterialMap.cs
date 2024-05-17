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
 public   class SizeItemMaterialMap:EntityTypeConfiguration<SizeItemMaterial>
    {
        public SizeItemMaterialMap()
        {
            HasKey(t => t.SizeMaterialD);
            Property(t => t.SizeMaterialD).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Qty).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.SizeRange).IsRequired();
            Property(t => t.MaterialMasterID).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBY);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            ToTable("SizeItemMaterial");
        }
    }
}
