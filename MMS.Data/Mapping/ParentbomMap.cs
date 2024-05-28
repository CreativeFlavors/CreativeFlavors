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
    public class ParentbomMap : EntityTypeConfiguration<parentbom>
    {
        public ParentbomMap() {
            HasKey(t => t.BomId);
            Property(t => t.BomId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BomNo);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.Description);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.Date);
            Property(t => t.LastBom);
            Property(t => t.IsActive);
            Property(t => t.IsDelete);
            ToTable("parentbom");
        }
    }
}
