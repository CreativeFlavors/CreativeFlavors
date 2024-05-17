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
    public class StoreMasterMp : EntityTypeConfiguration<StoreMaster>
    {
        public StoreMasterMp()
        {
            HasKey(t => t.StoreMasterId);
            Property(t => t.StoreMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StoreCode).IsRequired();
            Property(t => t.StoreName).IsRequired();
            Property(t => t.Location).IsRequired();         
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("StoreMaster");
        }
    }
}
