using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class IndentcartMap : EntityTypeConfiguration<IndentCart>
    {
        public IndentcartMap()
        {
           
            HasKey(e => e.IndentCartId);
            Property(e => e.IndentCartId).HasColumnName("indentcartid");
            Property(e => e.MaterialNameId).HasColumnName("materialnameid");
            Property(e => e.StoreNameId).HasColumnName("storenameid");
            Property(e => e.TaxNameId).HasColumnName("taxnameid");
            Property(e => e.UomNameId).HasColumnName("uomnameid");
            Property(e => e.Price).HasColumnName("price");
            Property(e => e.IndentRequiredQty).HasColumnName("indentrequiredqty");
            Property(e => e.RequiredQty).HasColumnName("requiredqty");
            Property(e => e.Status).HasColumnName("status");
            Property(e => e.CreatedBy).HasColumnName("createdby");
            Property(e => e.DeletedBy).HasColumnName("deletedby");
            Property(e => e.CreatedDate).HasColumnName("createddate");
            Property(e => e.UpdatedDate).HasColumnName("updateddate");
            Property(e => e.DeletedDate).HasColumnName("deleteddate");
            Property(e => e.IsActive).HasColumnName("isactive");
            Property(e => e.IndentNumber).HasColumnName("indentnumber");
            Property(e => e.IndentDate).HasColumnName("indentdate");
            ToTable("indentcart");
        }
    }
}
