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
    public class ProductMap : EntityTypeConfiguration<product>
    {
        public ProductMap()
        {
            HasKey(t => t.ProductId);
            Property(t => t.ProductId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductCode).HasColumnName("productcode");
            Property(t => t.ProductDesc).HasColumnName("productdesc");
            Property(t => t.TaxMasterId).HasColumnName("taxmasterid");
            Property(t => t.UomMasterId).HasColumnName("uommasterid");
            Property(t => t.Price).HasColumnName("price");
            Property(t => t.BomNo).HasColumnName("bomno");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.MaterialCategoryMasterId).HasColumnName("materialcategorymasterid");
            ToTable("product");
        }

    }
}