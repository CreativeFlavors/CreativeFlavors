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
    public class OrderTypeMap : EntityTypeConfiguration<OrderType>
    {
        public OrderTypeMap()
        {
            HasKey(t => t.OrderTypeID);
            Property(t => t.OrderTypeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OrderTypeCode).IsRequired();
            Property(t => t.OrderTypeName).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("OrderType");
        }
    }
}
