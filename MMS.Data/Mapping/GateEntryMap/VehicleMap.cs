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
   public class VehicleMap : EntityTypeConfiguration<VehicleMaster>
    {
        public VehicleMap()
        {
            HasKey(t => t.VehicleId);
            Property(t => t.VehicleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.VehicleNo);
            Property(t => t.VehicleName);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);


            ToTable("VehicleMaster");
        }
    }
}
