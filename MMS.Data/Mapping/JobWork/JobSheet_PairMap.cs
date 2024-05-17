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
    class JobSheet_PairMap: EntityTypeConfiguration<JobSheet_PairMaster>
    {
        public JobSheet_PairMap()
        {
            HasKey(t => t.jobsheet_pair_id);
            Property(t => t.jobsheet_pair_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Exertnal_work);
            Property(t => t.jobsheet_pair_Code_id);
            Property(t => t.Date);
            Property(t => t.JW_Name);
            Property(t => t.Process_Name);
            Property(t => t.UC_NO_id);
            Property(t => t.Buyer);
            Property(t => t.Season);
            Property(t => t.Stores);
            Property(t => t.Category);
            Property(t => t.Group_);

            Property(t => t.Material);
            Property(t => t.Qty);
            Property(t => t.Qty_Uom);
            Property(t => t.Uc_Noms);
            Property(t => t.Uc_Noms_Uom);
            Property(t => t.Uc_value);

            Property(t => t.Delivery_Date);
            Property(t => t.Jw_Rate);
            Property(t => t.Value);

            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.GST);
            Property(t => t.Gst_Amt);
            Property(t => t.Total);
            ToTable("JobSheet_pair");
        }
    }
}
