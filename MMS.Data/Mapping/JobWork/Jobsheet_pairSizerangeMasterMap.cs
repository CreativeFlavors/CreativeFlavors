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
     class Jobsheet_pairSizerangeMasterMap: EntityTypeConfiguration<Jobsheet_pairSizerangeMaster>
    {
        public Jobsheet_pairSizerangeMasterMap()
        {
            HasKey(t => t.Job_sheet_pair_Size_id);
            Property(t => t.Job_sheet_pair_Size_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.jobsheet_pair_id);
            Property(t => t.jobsheet_pair_Code_id);
            Property(t => t.Sizerange);
            Property(t => t.Qty);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            ToTable("Job_sheet_pair_sizerage");
        }
    }
}

