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
    public class GRNHeaderMap : EntityTypeConfiguration<GRNHeader>
    {
        public GRNHeaderMap() {
            HasKey(t => t.GrnHeaderId);
            Property(t => t.GrnHeaderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.GrnDate).HasColumnName("grndate");
            Property(t => t.RefInvoiceNumber).HasColumnName("refinvoicenumber");
            Property(t => t.RefInvoiceDate).HasColumnName("refinvoicedate");
            Property(t => t.PoNumber).HasColumnName("ponumber");
            Property(t => t.PoDate).HasColumnName("podate");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.Items).HasColumnName("items");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.SubtotalValue).HasColumnName("subtotalvalue");
            Property(t => t.GRNNumber).HasColumnName("grnnumber");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.TotalValue).HasColumnName("totalvalue");
            Property(t => t.IsFulfilled).HasColumnName("isfulfilled");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.ShipmentDetails).HasColumnName("shipmentdetails");
            Property(t => t.Notes).HasColumnName("notes");
            Property(t => t.OverallWeight).HasColumnName("overallweight");
            Property(t => t.ForDiscountValue).HasColumnName("for_discountvalue");
            Property(t => t.ForSubtotalValue).HasColumnName("for_subtotalvalue");
            Property(t => t.ForTaxValue).HasColumnName("for_taxvalue");
            Property(t => t.ForTotalValue).HasColumnName("for_totalvalue");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            ToTable("grnheader");
        }
    }
}
