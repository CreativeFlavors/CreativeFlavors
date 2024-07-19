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
    public class GRNDetailsMap : EntityTypeConfiguration<GRNDetails>
    { 
        public GRNDetailsMap()
        {
            HasKey(t => t.GrnDetailId);
            Property(t => t.GrnDetailId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GrnHeaderId).HasColumnName("grnheaderid");
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.PoDetailId).HasColumnName("podetailid");
            Property(t => t.PoQuantity).HasColumnName("poquantity");
            Property(t => t.UnitPrice).HasColumnName("unitprice");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.SubtotalValue).HasColumnName("subtotalvalue");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.TotalValue).HasColumnName("totalvalue");
            Property(t => t.ExpiryDate).HasColumnName("expirydate");
            Property(t => t.BatchCode).HasColumnName("batchcode");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.Weight).HasColumnName("weight");
            Property(t => t.IsFulfilled).HasColumnName("isfulfilled");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.ShipmentDetails).HasColumnName("shipmentdetails");
            Property(t => t.Notes).HasColumnName("notes");
            Property(t => t.OverallWeight).HasColumnName("overallweight");
            Property(t => t.for_totalunitprice).HasColumnName("for_totalunitprice");
            Property(t => t.ForDiscountValue).HasColumnName("for_discountvalue");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.ForSubtotalValue).HasColumnName("for_subtotalvalue");
            Property(t => t.ForTaxValue).HasColumnName("for_taxvalue");
            Property(t => t.ForTotalValue).HasColumnName("for_totalvalue");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby"); 
            Property(t => t.for_currencyconid).HasColumnName("for_currencyconid");
            Property(t => t.currencyconid).HasColumnName("currencyconid");
            Property(t => t.Pounitprice).HasColumnName("Pounitprice");
            Property(t => t.currencyid).HasColumnName("currencyid");
            ToTable("grndetails");

        }
    }
}
