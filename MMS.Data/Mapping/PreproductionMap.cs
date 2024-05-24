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
    public class PreproductionMap : EntityTypeConfiguration<preproduction>
    {
        public PreproductionMap()
        {

            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.SalesOrderNo);
            Property(t => t.SalesOrderDate);
            Property(t => t.BuyerId);
            Property(t => t.ProductId);
            Property(t => t.Qty);
            Property(t => t.Status);
            Property(t => t.CreatedBy);
            Property(t => t.IsActive);
            ToTable("preproduction");
        }

    }
}
