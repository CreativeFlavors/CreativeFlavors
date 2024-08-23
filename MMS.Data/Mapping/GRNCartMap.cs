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
    public class GRNCartMap : EntityTypeConfiguration<GRNCart>
    {
        public GRNCartMap()
        {
            HasKey(t => t.grncartid);
            Property(t => t.grncartid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.grncartid).HasColumnName("grncartid");
            Property(t => t.podetailid).HasColumnName("podetailid");
            Property(t => t.poquantity).HasColumnName("poquantity");
            Property(t => t.ProductCode).HasColumnName("productcode");
            Property(t => t.ProductNameId).HasColumnName("productnameid");
            Property(t => t.UomMasterId).HasColumnName("uommasterid");
            Property(t => t.ExpiryDate).HasColumnName("expirydate");
            Property(t => t.BatchCode).HasColumnName("batchcode");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.TaxPerId).HasColumnName("taxperid");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.unitprice).HasColumnName("unitprice");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.Subtotal).HasColumnName("subtotal");
            Property(t => t.Grandtotal).HasColumnName("grandtotal");
            Property(t => t.DiscountPerId).HasColumnName("discountperid");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.Grndate).HasColumnName("grndate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.GRNNumber).HasColumnName("grnnumber");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.currencyconid).HasColumnName("currencyconid");
            Property(t => t.for_currencyconid).HasColumnName("for_currencyconid");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.ForSubtotalValue).HasColumnName("for_subtotalvalue");
            Property(t => t.ForTaxValue).HasColumnName("for_taxvalue");
            Property(t => t.ForTotalValue).HasColumnName("for_totalvalue");
            Property(t => t.ForDiscountValue).HasColumnName("for_discountvalue");
            Property(t => t.IsDeleted).HasColumnName("isdeleted");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            ToTable("grncart");
        }
    }
}
