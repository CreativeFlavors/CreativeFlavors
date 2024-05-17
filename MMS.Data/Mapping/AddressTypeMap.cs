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
    public class AddressTypeMap : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeMap() {

            HasKey(t => t.AddTypeID);
            Property(t => t.AddTypeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AddTypeName).IsRequired();
            Property(t => t.IsActive).IsRequired();
            ToTable("AddressType");
        }
    }
}
