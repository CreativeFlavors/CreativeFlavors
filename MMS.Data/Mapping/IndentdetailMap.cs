using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class IndentdetailMap : EntityTypeConfiguration<Indentdetail>
    {
        public IndentdetailMap()
        {
            HasKey(t => t.IndentDetailId);
            Property(t => t.IndentDetailId).HasColumnName("indentdetailid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IndentHeaderId).HasColumnName("indentheaderid");
            Property(t => t.IndentDate).HasColumnName("indentdate");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.SupplierCode).HasColumnName("suppliercode");
            Property(t => t.MaterialId).HasColumnName("materialid");
            Property(t => t.UomId).HasColumnName("uomid");
            Property(t => t.TaxId).HasColumnName("taxid");
            Property(t => t.UnitPrice).HasColumnName("unitprice");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.ExtendedPrice).HasColumnName("extendedprice");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.DiscountPerc).HasColumnName("discountperc");
            Property(t => t.Subtotal).HasColumnName("subtotal");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.Total).HasColumnName("total");
            Property(t => t.Weight).HasColumnName("weight");
            Property(t => t.ForUnitPrice).HasColumnName("for_unitprice");
            Property(t => t.ForExtendedPrice).HasColumnName("for_extendedprice");
            Property(t => t.ForDiscountValue).HasColumnName("for_discountvalue");
            Property(t => t.ForSubtotal).HasColumnName("for_subtotal");
            Property(t => t.ForTaxValue).HasColumnName("for_taxvalue");
            Property(t => t.ForTotal).HasColumnName("for_total");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.IndentQty).HasColumnName("indentqty");
            ToTable("indentdetail");
        }
    }
}
