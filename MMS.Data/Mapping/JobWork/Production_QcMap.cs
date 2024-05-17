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
   public class Production_QcMap: EntityTypeConfiguration<Production_QcMaster>
    {
        public Production_QcMap()
        {
            HasKey(t => t.Qc_id);
            Property(t => t.Qc_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductionQc_ID);
            Property(t => t.From_Stage);
            Property(t => t.Size);
            Property(t => t.Qty);
            Property(t => t.Side);
            Property(t => t.Reason);
            Property(t => t.QC_Io);
            Property(t => t.Type);
            Property(t => t.Component);
            Property(t => t.Style);
            Property(t => t.To_stage);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);

            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            ToTable("ProductionQc_Issue");
        }
    }
}
