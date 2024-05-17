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
    public class IndentSizeRangeMap : EntityTypeConfiguration<IndentSizeRangeQty>
    {
        public IndentSizeRangeMap()
        {
            HasKey(t => t.IdentSizeRangeID);
            Property(t => t.IdentSizeRangeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size);
            Property(t => t.quantity);
            Property(t => t.Rate);
            Property(t => t.IndentMaterialID);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy); 
            ToTable("IndentSizeRange");

        }
    }
}
