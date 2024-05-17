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
    public class InvoiceDetailMap : EntityTypeConfiguration<orderheader>
    {
        public InvoiceDetailMap() {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OrderId);
            Property(t => t.CCode);
            Property(t => t.OrderDate);
            Property(t => t.OrderFullFillDate);
            Property(t => t.Notes);
            Property(t => t.AddCode);
            Property(t => t.ShipAddCode);
            Property(t => t.BillAddCode);
            Property(t => t.TotalAmount);
            Property(t => t.DiscAmount);
            Property(t => t.TaxAmount);
            Property(t => t.BillAmount);
            Property(t => t.FreightAmount);
            Property(t => t.IsQuote);
            Property(t => t.IsActive);
            Property(t => t.TotalQty);
            Property(t => t.ShipCode);
            Property(t => t.ExpectedDeliveryDate);
            ToTable("orderheader");
        }
    }
}
