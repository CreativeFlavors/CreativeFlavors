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
    public class PermissionMap:EntityTypeConfiguration<PermissionMaster>
    {
        public PermissionMap()
        {
            HasKey(t => t.PermissionID);
            Property(t => t.PermissionID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PermissionName);
            Property(t => t.PermissionDesc);           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("tbl_Permission");
        }
    }
}
