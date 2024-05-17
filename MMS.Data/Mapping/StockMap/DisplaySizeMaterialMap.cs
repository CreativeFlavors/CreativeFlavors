using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
 public   class DisplaySizeMaterialMap: EntityTypeConfiguration<DisplaySizeMaterial>
    {
        public DisplaySizeMaterialMap()
        {
            HasKey(t => t.DisplaySizeMaterialD);
            Property(t => t.DisplaySizeMaterialD).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SizeIsChecked);
            Property(t => t.SizeRange);
            Property(t => t.BOMmaterialID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);          
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);            
            ToTable("DisplaySizeMaterial");
        }
    }
}
