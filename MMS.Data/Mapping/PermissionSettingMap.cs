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
  public  class PermissionSettingMap:EntityTypeConfiguration<PermissionSettingMaster>
    {
      public PermissionSettingMap()
      {
          HasKey(t => t.PermissionSetID);
          Property(t => t.PermissionSetID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          Property(t => t.UserTypeID).IsRequired();
          Property(t => t.PermissionID).IsRequired();
          Property(t => t.CreatedDate);
          Property(t => t.UpdatedDate);
          Property(t => t.CreatedBy);
          Property(t => t.UpdatedBy);
          ToTable("tbl_PermissionSetting");
      }
    }
}
