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
   public class BomSizeMatchingMap:EntityTypeConfiguration<BomSizeMatching>
    {
        public BomSizeMatchingMap()
        {
            HasKey(t => t.BOMSizeMatchingID);
            Property(t => t.BOMSizeMatchingID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BomMaterialID).IsRequired();
            Property(t => t.Frame);
            Property(t => t.Size);
            Property(t => t.Rate);
            Property(t => t.SizeScheduleMasterID);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedOn);
            Property(t => t.DeletedBy);
            Property(t => t.Qty);
            ToTable("BomSizeMatching");
        }
    }
}
