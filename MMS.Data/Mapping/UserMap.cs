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
   public class UserMap:EntityTypeConfiguration<Users>
    {
         public UserMap()
        {
            HasKey(t => t.UserID);
            Property(t => t.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Email).IsRequired();
            Property(t => t.Password).IsRequired();
            Property(t => t.PasswordSalt);
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.IsActive);         
            Property(t => t.CreatedDate).IsRequired();
            Property(t => t.UpdatedDate).IsRequired();
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
          
        ToTable("Users");
        }
    }
}
