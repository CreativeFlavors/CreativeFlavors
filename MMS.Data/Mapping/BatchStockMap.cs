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
    public class BatchStockMap : EntityTypeConfiguration<BatchStock>
    {
        public BatchStockMap() {
            HasKey(t => t.BatchStockId);
            Property(t => t.BatchStockId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.productid).HasColumnName("productid");
            Property(t => t.BatchCode).HasColumnName("batchcode");
            Property(t => t.AltBatchCode).HasColumnName("altbatchcode");
            Property(t => t.ExpiryDate).HasColumnName("expirydate");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.GrnNumber).HasColumnName("grnnumber");
            Property(t => t.GrnDate).HasColumnName("grndate");
            Property(t => t.GrnDetailId).HasColumnName("grndetailid");
            Property(t => t.Price).HasColumnName("price");
            Property(t => t.Cost).HasColumnName("cost");
            Property(t => t.TaxCode).HasColumnName("taxcode");
            Property(t => t.UomId).HasColumnName("uomid");
            Property(t => t.ReservedQty).HasColumnName("reservedqty");
            Property(t => t.LastTransMode).HasColumnName("lasttransmode");
            Property(t => t.LastTransNumber).HasColumnName("lasttransnumber");
            Property(t => t.LastTransDate).HasColumnName("lasttransdate");
            Property(t => t.LastTransQty).HasColumnName("lasttransqty");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.status).HasColumnName("status");
            Property(t => t.producttype).HasColumnName("producttype");
            Property(t => t.ProductCode).HasColumnName("productcode");
            ToTable("batchstock");
        }
    }
}
