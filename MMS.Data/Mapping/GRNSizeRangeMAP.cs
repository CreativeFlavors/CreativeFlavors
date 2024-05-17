using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
  public  class GRNSizeRangeMAP : EntityTypeConfiguration<SizeRangeQtyRate>
    {
        public GRNSizeRangeMAP()
        {
            HasKey(t => t.SizeQRId);
            Property(t => t.SizeQRId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Qty);
            Property(t => t.Rate);
            Property(t => t.SizeRange);
            Property(t => t.OrderEntryId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("SizeRangeQtyRate");
        }
    }
}
