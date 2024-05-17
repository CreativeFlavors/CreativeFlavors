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
    public class order_details : EntityTypeConfiguration<Order_Details>
    {
        public order_details()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CustomerDocsId).IsRequired();
            Property(t => t.LineId);
            Property(t => t.MaterialId);
            Property(t => t.Code);
            Property(t => t.DisplayName);
            Property(t => t.PoName);
            Property(t => t.LatCost);
            Property(t => t.AveCost);
            Property(t => t.UomIdBuy);
            Property(t => t.UomIdSell);
            Property(t => t.UomFactor);
            Property(t => t.TaxTypeId);
            Property(t => t.TaxRate);
            Property(t => t.QtyRequired);
            Property(t => t.QtyProcessed);
            Property(t => t.QtyLastProcessed);
            Property(t => t.QtyReserved);
            Property(t => t.Note);
            Property(t => t.Discount);
            Property(t => t.PriceExcl);
            Property(t => t.PriceIncl);
            Property(t => t.LineTotalExcl);
            Property(t => t.LineTotalIncl);
            Property(t => t.LineTotalTax);
            Property(t => t.ProcessedLineTotalExcl);
            Property(t => t.ProcessedLineTotalIncl);
            Property(t => t.ProcessedLineTotalTax);
            Property(t => t.ForeignPriceExcl);
            Property(t => t.ForeignPriceIncl);
            Property(t => t.ForeignLineTotalExcl);
            Property(t => t.ForeignLineTotalIncl);
            Property(t => t.ForeignLineTotalTax);
            Property(t => t.ProcessedForeignLineTotalExcl);
            Property(t => t.ProcessedForeignLineTotalIncl);
            Property(t => t.ProcessedForeignLineTotalTax);
            Property(t => t.IsActive);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            ToTable("order_details");
        }
    }
}
