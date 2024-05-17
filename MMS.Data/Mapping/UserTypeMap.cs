using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Data.Mapping
{
    public class UserTypeMap : EntityTypeConfiguration<UserTypeMaster>
    {
        public UserTypeMap()
        {
            HasKey(t => t.UserTypeID);
            Property(t => t.UserTypeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserTypeDesc);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("tbl_UserType");
          }
    }
}
