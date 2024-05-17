using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Data.Mapping
{
    public class ComponentMasterMap : EntityTypeConfiguration<ComponentMaster>
    {
        public ComponentMasterMap()
        {
            HasKey(t => t.ComponentMasterId);
            Property(t => t.ComponentMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ComponentName).IsRequired();
            Property(t => t.UsedIn);
            Property(t => t.Image);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("ComponentMaster");
        }
    }
}
