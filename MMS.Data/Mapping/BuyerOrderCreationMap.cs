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
 
    public class BuyerOrderCreationMap : EntityTypeConfiguration<BuyerOrderCreation>
    {
        public BuyerOrderCreationMap()
        {
            HasKey(t => t.BuyerOrderCreationID);
            Property(t => t.BuyerOrderCreationID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerId).IsRequired();
            Property(t => t.MaterialCode);
            Property(t => t.MaterialName);
            Property(t => t.StockUnit);
            Property(t => t.SizeRange);
            Property(t => t.StandardPrice);
            Property(t => t.ComplexitityFactor);
            Property(t => t.SketchNo);
            Property(t => t.LeatherMainRawMaterial);
            Property(t => t.ProductColor);
            Property(t => t.BuyerStyleNo);
            Property(t => t.IsDeleted);           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.Deletedon);
            ToTable("BuyerOrderCreation");
        }
    }
}
