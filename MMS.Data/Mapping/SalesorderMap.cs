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
    public class SalesorderMap : EntityTypeConfiguration<salesorder>
    {
        public SalesorderMap()
        {
            HasKey(t => t.SalesorderId);
            Property(t => t.SalesorderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductCode).HasColumnName("productcode");
            Property(t => t.ProductNameid).HasColumnName("productnameid");
            Property(t => t.Taxperid).HasColumnName("taxperid");
            Property(t => t.UomMasterId).HasColumnName("uommasterid");
            Property(t => t.Price).HasColumnName("price");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.isactive).HasColumnName("isactive");
            Property(t => t.quantity).HasColumnName("quantity");
            Property(t => t.Subtotal).HasColumnName("subtotal");
            Property(t => t.Grandtotal).HasColumnName("grandtotal");
            Property(t => t.Discountperid).HasColumnName("discountperid");
            Property(t => t.Discountvalue).HasColumnName("discountvalue");
            Property(t => t.Specialinstruction).HasColumnName("specialinstruction");
            Property(t => t.Additionalcommends).HasColumnName("additionalcommends");
            Property(t => t.quoteref).HasColumnName("quoteref");
            Property(t => t.quotedate).HasColumnName("quotedate");
            Property(t => t.salesorderdate).HasColumnName("salesorderdate");
            Property(t => t.custaddcode).HasColumnName("custaddcode");
            Property(t => t.custbillcode).HasColumnName("custbillcode");
            Property(t => t.originalquotedate).HasColumnName("originalquotedate");
            Property(t => t.taxinclusive).HasColumnName("taxinclusive");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.createdby).HasColumnName("createdby");
            Property(t => t.Updatedby).HasColumnName("updatedby");
            ToTable("salesorder");
        }
    }
}
