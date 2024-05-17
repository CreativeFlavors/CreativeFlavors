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
    public class MachineryMasterMap : EntityTypeConfiguration<MachineryMaster>
    {
        public MachineryMasterMap()
        {
            HasKey(t => t.MachineryMasterId);
            Property(t => t.MachineryMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MachineCode);
            Property(t => t.MachineName);
            Property(t => t.Make);
            Property(t => t.AssesListNo);
            Property(t => t.MachineSerialNo);
            Property(t => t.Kilowatt);
            Property(t => t.HorsePower);
            Property(t => t.Volt);
            Property(t => t.OperationUsedFor);
            Property(t => t.Specification);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("MachineryMaster");
        }
    }
}
