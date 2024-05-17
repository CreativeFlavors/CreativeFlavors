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
    public class Invoice : EntityTypeConfiguration<order_header>
    {
        public Invoice() {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CustomerId).IsRequired();
            Property(t => t.OrderId).IsRequired();
            Property(t => t.DoctypeId).IsRequired();
            Property(t => t.Revision);
            Property(t => t.DocStateId);
            Property(t => t.QuoteRef);
            Property(t => t.SoRef);
            Property(t => t.RefDate);
            Property(t => t.RefItems);
            Property(t => t.RefQuantity);
            Property(t => t.RefDueDate);
            Property(t => t.RefExtendedValue);
            Property(t => t.RefDiscountValue);
            Property(t => t.RefGrossValue);
            Property(t => t.RefTaxValue);
            Property(t => t.RefNetValue);
            Property(t => t.DocDate);
            Property(t => t.DocItems);
            Property(t => t.DocQuantity);
            Property(t => t.DocDueDate);
            Property(t => t.DocExtendedValue);
            Property(t => t.DocDiscountValue);
            Property(t => t.DocGrossValue);
            Property(t => t.DocTaxValue);
            Property(t => t.DocNetValue);
            Property(t => t.CustAddCode);
            Property(t => t.CustShipCode);
            Property(t => t.CustBillCode);
            Property(t => t.HeaderDiscountPerc);
            Property(t => t.ShippingNotes);
            Property(t => t.Status);
            Property(t => t.DocDescription);
            Property(t => t.OriginalQuoteDate);
            Property(t => t.QuoteDate);
            Property(t => t.DueDate);
            Property(t => t.TaxInclusive);
            Property(t => t.EmailSent);
            Property(t => t.ExternalRef);
            Property(t => t.CreditLimit);
            Property(t => t.DCBalance);
            Property(t => t.ForeignDCBalance);
            Property(t => t.CurrencyId);
            Property(t => t.IsActive);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            ToTable("order_header");
        }
    }
}
