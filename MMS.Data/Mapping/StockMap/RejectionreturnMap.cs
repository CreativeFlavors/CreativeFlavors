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
    public class RejectionreturnMap : EntityTypeConfiguration<RejectionReturnSup>
    {
        public RejectionreturnMap()
        {
            HasKey(t => t.RejectionReturnId);
            Property(t => t.RejectionReturnId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RejectionDcNo).IsRequired();
            Property(t => t.GrnNo).IsRequired();
            Property(t => t.Date);
            Property(t => t.PoNo).IsRequired();
            Property(t => t.SupplierMasterId).IsRequired();
            Property(t => t.IMIRNo).IsRequired();
            Property(t => t.MaterialGroupId).IsRequired();
            Property(t => t.Uom).IsRequired();
            Property(t => t.MaterialMasterId).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.ColourMasterId).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.Remarks).IsRequired();
            Property(t => t.Pcs).IsRequired();
            Property(t => t.AmountTotal);
            Property(t => t.GatePassDc);

            ToTable("RejectionReturn");
        }
    }
}
