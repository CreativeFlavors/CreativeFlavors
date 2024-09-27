using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class PoHeaderMap : EntityTypeConfiguration<PoHeader>
    {
        public PoHeaderMap() 
        {            
            HasKey(t => t.PoheaderId);
            Property(t => t.PoheaderId).HasColumnName("poheaderid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PoDate).HasColumnName("podate");
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.items).HasColumnName("items");
            Property(t => t.qty).HasColumnName("qty");
            Property(t => t.TotalDiscountValue).HasColumnName("total_discountvalue");
            Property(t => t.TotalSubtotalValue).HasColumnName("total_subtotalvalue");
            Property(t => t.TotalTaxValue).HasColumnName("total_taxvalue");
            Property(t => t.TotalTotalValue).HasColumnName("total_totalvalue");
            Property(t => t.ExpDeliveryDate).HasColumnName("expdeliverydate");
            Property(t => t.PayByDate).HasColumnName("paybydate");
            Property(t => t.FulfillDate).HasColumnName("fulfilldate");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.ShipmentDetails).HasColumnName("shipmentdetails");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.TotalPrice).HasColumnName("total_price");
            Property(t => t.PoNumber).HasColumnName("ponumber");
            ToTable("poheader", "public");
        }
    }
}
