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
    public class SkillMasterMap : EntityTypeConfiguration<SkillMaster>
    {
       public SkillMasterMap()
        {
            HasKey(t => t.SkillMasterId);
            Property(t => t.SkillMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SkillCode).IsRequired();
            Property(t => t.SkillName).IsRequired();              
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("SkillMaster");
        }
    }
}
