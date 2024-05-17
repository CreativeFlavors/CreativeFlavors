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

      class Job_ApprovedpriceMap: EntityTypeConfiguration<Job_ApprovedPriceMaster>
    {
        public Job_ApprovedpriceMap()
        {
            HasKey(t => t.Jw_ApprovedPriceID);
            Property(t => t.Jw_ApprovedPriceID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Date);
            Property(t => t.JW_Name);
            Property(t => t.Process_Name);
            Property(t => t.Stage_From);
            Property(t => t.Stage_To);
            Property(t => t.Rate_For_JW);
            Property(t => t.Rate_For_JW_id);
            Property(t => t.Tax_Details);
            Property(t => t.Job_ExcessPercentage);
            Property(t => t.Lead_Time_Days);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.Finished_Material);
            Property(t => t.Issue_Type);
            Property(t => t.Product_BuyerStyle);
            Property(t => t.GSt);
            ToTable("Job_ApprovedPrice");
        }
    }
}
