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
    public class MaterialMap : EntityTypeConfiguration<Material>
    {
        public MaterialMap() 
        {
            
            HasKey(t => t.MaterialId);

            Property(t => t.MaterialId)
                .HasColumnName("materialid")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.MaterialCode)
                .HasColumnName("materialcode");

            Property(t => t.MaterialName)
                .HasColumnName("materialname");

            Property(t => t.MaterialDescription)
                .HasColumnName("materialdesc");

            Property(t => t.TaxMasterId)
                .HasColumnName("taxmasterid");

            Property(t => t.UomMasterId)
                .HasColumnName("uommasterid");

            Property(t => t.Price)
                .HasColumnName("price");

            Property(t => t.Cost)
                .HasColumnName("cost");

            Property(t => t.MaterialCategoryId)
                .HasColumnName("materialcategoryid");

            Property(t => t.StoreId)
                .HasColumnName("storeid");

            Property(t => t.PreferredSupplierId)
                .HasColumnName("preferredsupplierid");

            Property(t => t.MinStock)
                .HasColumnName("minstock");

            Property(t => t.MaxStock)
                .HasColumnName("maxstock");

            Property(t => t.IsActive)
                .HasColumnName("isactive");

            Property(t => t.CreatedDate)
                .HasColumnName("createddate");

            Property(t => t.UpdatedDate)
                .HasColumnName("updateddate");

            Property(t => t.CreatedBy)
                .HasColumnName("createdby");

            Property(t => t.UpdatedBy)
                .HasColumnName("updatedby");

            ToTable("material");
        }
    }
}
