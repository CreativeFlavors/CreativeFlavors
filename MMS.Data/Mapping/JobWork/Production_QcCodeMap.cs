using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.JobWork
{
    public class Production_QcCodeMap: EntityTypeConfiguration<Production_QcCodeMaster>
    {
        public Production_QcCodeMap()
        {
            HasKey(t => t.ProductionQc_ID);
            Property(t => t.ProductionQc_ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Production_Code);
            Property(t => t.Date);
            Property(t => t.Buyer);
            Property(t => t.Season);
            Property(t => t.Stage);
            Property(t => t.Lot_No);

            Property(t => t.Moved_lot);
            Property(t => t.Io);
            Property(t => t.Moved_Io);
            Property(t => t.Style);
            Property(t => t.QC_Io);
            Property(t => t.Total_Pairs);

            Property(t => t.Size);
            Property(t => t.IO_Size);
            Property(t => t.Qty);
            Property(t => t.Side);
            Property(t => t.CreatedBy);
            Property(t => t.DeletedBy);

            Property(t => t.DeletedDate);
            Property(t => t.IsDeleted);
            Property(t => t.UpdatedDate);
            Property(t => t.UpdatedBy);
            ToTable("ProductionQc_CodeMater");


        }
    }
}
