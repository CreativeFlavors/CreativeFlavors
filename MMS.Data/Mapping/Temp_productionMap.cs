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
    public class Temp_productionMap : EntityTypeConfiguration<temp_production>
    {
        public Temp_productionMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SalesOrderId);
            Property(t => t.BuyerId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.ProductId);
            Property(t => t.ProductItem); 
            Property(t => t.producttype);
            Property(t => t.MaterialId);
            Property(t => t.Bomid);
            Property(t => t.Probomid);
            Property(t => t.AvailableStock);
            Property(t => t.AvailableUnitId);
            Property(t => t.Consume);
            Property(t => t.ConsumeUnitId);
            Property(t => t.IsActive);
            ToTable("temp_production");
        }
    }
}
