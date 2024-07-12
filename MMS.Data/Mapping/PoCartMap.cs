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
    public class PoCartMap : EntityTypeConfiguration<PoCart>
    {
        public PoCartMap()
        {           
            HasKey(t => t.PocartId);
            Property(t => t.PocartId).HasColumnName("pocartid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.ProductId).HasColumnName("productid");
            Property(t => t.SupplierMaterialId).HasColumnName("suppliermaterialid");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.UnitPrice).HasColumnName("unitprice");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.WithIndentReference).HasColumnName("withindentreference");
            Property(t => t.UomId).HasColumnName("uomid");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.Subtotal).HasColumnName("subtotal");
            Property(t => t.TotalValue).HasColumnName("totalvalue");
            Property(t => t.DiscountPercentage).HasColumnName("discountpercentage");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.TaxPercentage).HasColumnName("taxpercentage");
            Property(t => t.TaxInclusive).HasColumnName("taxinclusive");
            Property(t => t.PoNumber).HasColumnName("ponumber");
            Property(t => t.IndentNumber).HasColumnName("indentnumber");
            Property(t => t.PoDate).HasColumnName("podate");
            ToTable("pocart", "public");
        }
    }
}
