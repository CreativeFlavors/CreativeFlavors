using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Data.Mapping
{
    public class Product_BuyerStyleMap : EntityTypeConfiguration<Product_BuyerStyleMaster>
    {
        public Product_BuyerStyleMap()
        {
            HasKey(t => t.ProductOrBuyerStyleId);
            Property(t => t.ProductOrBuyerStyleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerModel_ProductType);
            Property(t => t.BuyerStyle);
            Property(t => t.Last);
            Property(t => t.ProductColour);
            Property(t => t.OurStyle);
            Property(t => t.SizeRange);

            Property(t => t.BomNo);
            Property(t => t.LeatherName_1);
            Property(t => t.LeatherName_2);
            Property(t => t.LeatherName_3);
            Property(t => t.LeatherName_4);
            Property(t => t.UOM);
            Property(t => t.Width_Fit);
            Property(t => t.FinishedProductType);
            Property(t => t.StandardPrice);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
             Property(t => t.Weight);
            Property(t => t.Destination);
            ToTable("Product_BuyerStyleMaster");
        }
    }
}
