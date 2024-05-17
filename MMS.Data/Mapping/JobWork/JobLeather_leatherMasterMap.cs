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
    class JobLeather_leatherMasterMap : EntityTypeConfiguration<JobLeather_leatherMaster>
    {
        public JobLeather_leatherMasterMap()
        {
            HasKey(t => t.Job_Lether_to_lether_id);
            Property(t => t.Job_Lether_to_lether_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Date_from);
            Property(t => t.Job_Lether_to_lether_Code);
            Property(t => t.Jobwork_raw_material);
            Property(t => t.Encho_Raw_Material);
            Property(t => t.Process_Name);
            Property(t => t.Jw_Name);
            Property(t => t.Process_Name);
            Property(t => t.Buyer);
            Property(t => t.Season);
            Property(t => t.Stores);
            Property(t => t.Group_);

            Property(t => t.Category);
            Property(t => t.Material);
            Property(t => t.Finished_Material);
            Property(t => t.Qty).HasPrecision(18, 4);
            Property(t => t.Qty_Uom);
            Property(t => t.Rate);

            Property(t => t.GST);
            Property(t => t.Value);
            Property(t => t.Gst_Amt);
            Property(t => t.Total);
            Property(t => t.Delivery_Date);

            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("Job_lether_lether");
        }
    }
}

