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
    public class PoDetailMap : EntityTypeConfiguration<PoDetail>
    {
        public PoDetailMap()
        {
            HasKey(t => t.PodetailId);
            Property(t => t.PodetailId).HasColumnName("podetailid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PoheaderId).HasColumnName("poheaderid");
            Property(t => t.PoDate).HasColumnName("podate");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.ProductId).HasColumnName("productid");
            Property(t => t.UomId).HasColumnName("uomid"); 
            Property(t => t.UnitPrice).HasColumnName("unitprice");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.DiscountPercentage).HasColumnName("discountpercentage");
            Property(t => t.Subtotal).HasColumnName("subtotal");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.TotalValue).HasColumnName("totalvalue");
            Property(t => t.Weight).HasColumnName("weight");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.TaxInclusive).HasColumnName("taxinclusive");
            Property(t => t.PoNumber).HasColumnName("ponumber");
            Property(t => t.IndentNumber).HasColumnName("indentnumber");
            Property(t => t.TaxPercentage).HasColumnName("taxpercentage");
            ToTable("podetail", "public");
        }
    }
}
