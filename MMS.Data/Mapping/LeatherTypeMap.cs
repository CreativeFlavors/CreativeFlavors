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
   public class LeatherTypeMap: EntityTypeConfiguration<LeatherType>
    {
        public LeatherTypeMap()
        {
            HasKey(t => t.LeatherTypeID);
            Property(t => t.LeatherTypeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.LeatherTypeCode);
            Property(t => t.LeatherTypeDescription);
            Property(t => t.IsDeleted);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedDate);
            //Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedON);
            ToTable("LeatherType");
        }
    }
}
