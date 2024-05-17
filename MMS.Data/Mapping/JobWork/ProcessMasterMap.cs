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
   
    public class ProcessMasterMap : EntityTypeConfiguration<ProcessMaster>
    {
        public ProcessMasterMap()
        {
            HasKey(t => t.ProcessId);
            Property(t => t.ProcessId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProcessCode);
            Property(t => t.Ioinvolved);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            ToTable("ProcessMaster");
        }
    }
}
