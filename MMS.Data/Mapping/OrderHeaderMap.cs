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
    public class OrderHeaderMap : EntityTypeConfiguration<orderheader_hd>
    {
        public OrderHeaderMap()
        {
            HasKey(t => t.invoicehd_id);
            Property(t => t.invoicehd_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.invoicedate);
            Property(t => t.currencyid);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CustomerId);
            Property(t => t.invoice_items);
            Property(t => t.Quantity);
            Property(t => t.CustomerId);
            Property(t => t.TotalPrice);
            Property(t => t.IsActive);
            Property(t => t.TotalDisAmount);
            Property(t => t.TotalSubtotal);
            Property(t => t.TotalTaxAmount);
            Property(t => t.Quantity);
            Property(t => t.GrandTotal);
            Property(t => t.TotalPrice);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.IsDeleted);
            Property(t => t.Status);
            ToTable("orderheader");
        }
    }
}
