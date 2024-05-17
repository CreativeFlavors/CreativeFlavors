using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class ProductionPlanMap : EntityTypeConfiguration<ProductionPlanning> 
    {
        public ProductionPlanMap()
        {
            HasKey(t => t.ProductionPlanId);
            Property(t => t.ProductionPlanId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PlanNo).IsRequired();
            Property(t => t.CustomerName).IsRequired();
            Property(t => t.WeekPlan).IsRequired();
            Property(t => t.From).IsRequired();
            Property(t => t.To).IsRequired();
            Property(t => t.InHouseCapacity).IsRequired();
            Property(t => t.OrderQty).IsRequired();
            Property(t => t.PlanQty).IsRequired();
            //Property(t => t.MultipleIO).IsRequired();
            //Property(t => t.DisplayIO).IsRequired();
            Property(t => t.ShipDate).IsRequired();
            Property(t => t.ShipTo).IsRequired();
            Property(t => t.IsStyleDurea);
            Property(t => t.IsStyle);
            Property(t => t.IsSelectAll);
            Property(t => t.Remarks).IsRequired();
            Property(t => t.PlanQtyInMultiples).IsRequired();
            Property(t => t.IsAllocateStyleProp);
            Property(t => t.IsAllocateSizeProp);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("ProductionPlan");

        }
    }
}
