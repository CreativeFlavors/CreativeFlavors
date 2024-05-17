using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
   public class DriverMasterMap : EntityTypeConfiguration<DriverMaster>
    {
        public DriverMasterMap()
        {
            HasKey(t => t.DriverID);
            Property(t => t.DriverID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DriverName);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);


            ToTable("DriverMaster");
        }
    }
}
