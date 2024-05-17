using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
 
    public class MaterialOpeningPinCardMap : EntityTypeConfiguration<OpeningStockPinCard>
    {
        public MaterialOpeningPinCardMap()
        {
            HasKey(t => t.MaterialOpeningStockID);
            Property(t => t.MaterialOpeningStockID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.materialmasterid);
            Property(t => t.MaterialDescription);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.shortunitname);
            Property(t => t.categoryname);
            Property(t => t.storename);
            Property(t => t.groupname);
            Property(t => t.Rate);
            Property(t => t.materialpcs);
            Property(t => t.OpeningAsOnStock);
            Property(t => t.Issues);
            Property(t => t.Receipt);
            Property(t => t.ClosingStock);
            Property(t => t.ClosingStockValue);
            Property(t => t.MaterialType);
            ToTable("OpeningStockPinCard");
        }
    }
}
