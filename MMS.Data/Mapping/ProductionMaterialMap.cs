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
    public class ProductionMaterialMap : EntityTypeConfiguration<ProductionMaterial>
    {
        public ProductionMaterialMap() 
        {
            HasKey(t => t.ProductionMaterialId);
            Property(t => t.ProductionMaterialId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductsId).HasColumnName("productsid");
            Property(t => t.ProductsMatId).HasColumnName("productsmatid");
            Property(t => t.ConsumeQty).HasColumnName("consumeqty");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.ProductionId).HasColumnName("productionid");
            Property(t => t.StockOnHand).HasColumnName("stockonhand");
            Property(t => t.ProductionDate).HasColumnName("productiondate");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.ProductType).HasColumnName("producttype");
            ToTable("productionmaterial");
        }
    }
}
