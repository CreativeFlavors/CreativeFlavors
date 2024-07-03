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
    public class SubassemblyinventoryMap : EntityTypeConfiguration<Subassemblyinventory>
    {
        public SubassemblyinventoryMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductCode)
                .HasColumnName("productcode");

            Property(t => t.Price)
                .HasColumnName("price");

            Property(t => t.Cost)
                .HasColumnName("cost");

            Property(t => t.StoreCode)
                .HasColumnName("storecode");

            Property(t => t.Quantity)
                .HasColumnName("quantity");

            Property(t => t.QuantityLock)
                .HasColumnName("quantitylock");

            Property(t => t.QuantityLockRefTime)
                .HasColumnName("quantitylockreftime");

            Property(t => t.QuantityLockReleaseAt)
                .HasColumnName("quantitylockreleaseat");

            Property(t => t.PurchaseUOM)
                .HasColumnName("purchaseuom");

            Property(t => t.SaleUOM)
                .HasColumnName("saleuom");

            Property(t => t.LastTransNo)
                .HasColumnName("lasttransno");

            Property(t => t.LastTransQty)
                .HasColumnName("lasttransqty");

            Property(t => t.LastTransDate)
                .HasColumnName("lasttransdate");

            Property(t => t.CreatedBy)
                .HasColumnName("createdby");

            Property(t => t.UpdatedBy)
                .HasColumnName("updatedby");

            Property(t => t.CreatedDate)
                .HasColumnName("createddate");

            Property(t => t.UpdatedDate)
                .HasColumnName("updateddate");

            Property(t => t.Batchcode)
                .HasColumnName("batchcode");

            Property(t => t.ProductType)
                .HasColumnName("producttype");

            Property(t => t.ProductId)
                .HasColumnName("productid");

            ToTable("subassemblyinventory");
        }
    }
}
