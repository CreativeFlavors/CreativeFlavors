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
    class JobSheet_pairCodeMap : EntityTypeConfiguration<JobSheet_pairCodeMaster>
    {
        public  JobSheet_pairCodeMap()
            {
            HasKey(t => t.jobsheet_pair_code_id);
            Property(t => t.jobsheet_pair_code_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.jobsheet_pair_Code);
            Property(t => t.CreatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            ToTable("JobSheet_pair_Master_tbl");
        }

         
    }
}
