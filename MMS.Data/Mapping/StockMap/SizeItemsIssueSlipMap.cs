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
   
    public class SizeItemsIssueSlipMap : EntityTypeConfiguration<SizeItemsIssueSlip>
    {
        public SizeItemsIssueSlipMap()
        {
            HasKey(t => t.SizeMaterialD);
            Property(t => t.SizeMaterialD).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Qty);
            Property(t => t.Rate);
            Property(t => t.MultipleIssueslipFk);
            Property(t => t.SizeRange);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBY);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            ToTable("SizeItemsIssueSlip");
        }
    }
}
