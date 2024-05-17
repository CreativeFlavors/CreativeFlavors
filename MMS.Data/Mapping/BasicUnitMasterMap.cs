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
    public class BasicUnitMasterMap:EntityTypeConfiguration<BasicUnitMaster>
    {
        public BasicUnitMasterMap()
        {
            HasKey(t => t.BasicUnitMasterID);
            Property(t => t.BasicUnitMasterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CategoryId).IsRequired();
            Property(t => t.GroupID).IsRequired();
            Property(t => t.MaterialID).IsRequired();
            Property(t => t.ShortUnitValue).IsRequired();
            Property(t => t.ShortUnitID).IsRequired();
            Property(t => t.LongUnitValue).IsRequired();
            Property(t => t.LongUnitID).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("tbl_BasicUnitMaster");
        }
    }
}
