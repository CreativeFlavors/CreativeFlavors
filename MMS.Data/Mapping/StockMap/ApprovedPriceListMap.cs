using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class ApprovedPriceListMap : EntityTypeConfiguration<ApprovedPriceList>
    {
       // modelBuilder.Entity<ApprovedPriceList>().Property(x => x.PriceRs);
        public ApprovedPriceListMap()
        {
            HasKey(t => t.ApprovedPriceID);
            Property(t => t.ApprovedPriceID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierId);
            Property(t => t.Date);
            Property(t => t.PriceRs).HasPrecision(18, 4);
            Property(t => t.Uom);
            Property(t => t.CategoryID);
            Property(t => t.TaxDetails);
            Property(t => t.GroupID);
            Property(t => t.MaterialID);
            Property(t => t.CurrencyID);
            Property(t => t.ColorID);
            Property(t => t.LeadTime);
            Property(t => t.MRPMargin);
            Property(t => t.MRPPrice);
            Property(t => t.AccessibleValue);
            Property(t => t.SupplierDescription);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBY);
            Property(t => t.IsApproved);
            Property(t => t.ApprovedBy);
            ToTable("ApprovedPriceList");
        }
    }
}
