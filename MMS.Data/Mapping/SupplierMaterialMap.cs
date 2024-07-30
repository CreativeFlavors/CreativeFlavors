using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class SupplierMaterialMap : EntityTypeConfiguration<SupplierMaterial>
    {
        public SupplierMaterialMap()
        {
            HasKey(e => e.SupplierMaterialId);
            Property(e => e.SupplierMaterialId).HasColumnName("suppliermaterialid");
            Property(e => e.SupplierId).HasColumnName("supplierid");
            Property(e => e.ProductId).HasColumnName("productid");
            Property(e => e.UomId).HasColumnName("uomid");
            Property(e => e.Categoryid).HasColumnName("categoryid");
            Property(e => e.TaxId).HasColumnName("taxid");          
            Property(e => e.CreatedDate).HasColumnName("createddate");
            Property(e => e.UpdatedDate).HasColumnName("updateddate");
            Property(e => e.DeletedDate).HasColumnName("deleteddate");
            Property(e => e.IsActive).HasColumnName("isactive");
            Property(e => e.CreatedBy).HasColumnName("createdby");
            Property(e => e.UpdatedBy).HasColumnName("updatedby");
            Property(e => e.DeletedBy).HasColumnName("deletedby");
            ToTable("suppliermaterial");
        }
    }
}
