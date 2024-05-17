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
    public class ConveyorMasterMap : EntityTypeConfiguration<ConveyorMaster>
    {
        public ConveyorMasterMap()
        {
            HasKey(t => t.ConveyorMasterId);
            Property(t => t.ConveyorMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ConveyorCode);
            Property(t => t.ConveyorName);
            Property(t => t.CapacityPerDay);
            Property(t => t.CapacityUnits);
            Property(t => t.ConveyorType);   
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("ConveyorMaster");
        }
    }
}
