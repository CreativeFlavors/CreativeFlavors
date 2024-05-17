using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    class MRPSelectedValuesMap : EntityTypeConfiguration<MRPSelectedValues>
    {
        public MRPSelectedValuesMap()
        {
            HasKey(t => t.MRPSelectedID);
            Property(t => t.MRPSelectedID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IONumberID);
            Property(t => t.SimpleMRPID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedOn);
            Property(t => t.DeletedBy);         
            ToTable("MRPSelectedValues");
        }
    }
}
