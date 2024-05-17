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
     class Tbl_Rejected_qtyListMap : EntityTypeConfiguration<Tbl_Rejected_qtyListMaster>
    {
        public Tbl_Rejected_qtyListMap()
        {
            HasKey(t => t.RejectedQty_id);
            Property(t => t.RejectedQty_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Grn_id);
            Property(t => t.Qty);
            Property(t => t.RejectedQty_Reason);
            Property(t => t.RejectedQty_Inseption);
            Property(t => t.CreatedBY);
            Property(t => t.UpdatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedOn);
            Property(t => t.DeletedBy);
            Property(t => t.Psc);
            ToTable("Tbl_Rejected_qtyList");
        }
    }
}
