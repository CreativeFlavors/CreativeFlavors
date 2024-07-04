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
    public class IndentheaderMap : EntityTypeConfiguration<Indentheader>
    {
        public IndentheaderMap()
        {
            // Primary Key
            HasKey(t => t.IndentHeaderId);

            // Properties
            Property(t => t.IndentHeaderId).HasColumnName("indentheaderid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IndentDate).HasColumnName("indentdate");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.SupplierCode).HasColumnName("suppliercode");
            Property(t => t.PoRefNumber).HasColumnName("porefnumber");
            Property(t => t.Items).HasColumnName("items");
            Property(t => t.Quantity).HasColumnName("quantity");
            Property(t => t.ExtendedValue).HasColumnName("extendedvalue");
            Property(t => t.DiscountValue).HasColumnName("discountvalue");
            Property(t => t.SubtotalValue).HasColumnName("subtotalvalue");
            Property(t => t.TaxValue).HasColumnName("taxvalue");
            Property(t => t.TotalValue).HasColumnName("totalvalue");
            Property(t => t.ExpDeliveryDate).HasColumnName("expdeliverydate");
            Property(t => t.PayByDate).HasColumnName("paybydate");
            Property(t => t.FulfillDate).HasColumnName("fulfilldate");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.ShipmentDetails).HasColumnName("shipmentdetails");
            Property(t => t.Notes).HasColumnName("notes");
            Property(t => t.OverallWeight).HasColumnName("overallweight");
            Property(t => t.ForExtendedValue).HasColumnName("for_extendedvalue");
            Property(t => t.ForDiscountValue).HasColumnName("for_discountvalue");
            Property(t => t.ForSubtotalValue).HasColumnName("for_subtotalvalue");
            Property(t => t.ForTaxValue).HasColumnName("for_taxvalue");
            Property(t => t.ForTotalValue).HasColumnName("for_totalvalue");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.IndentQty).HasColumnName("indentqty");
            Property(t => t.IndentNo).HasColumnName("indentno");
            ToTable("indentheader");
        }
    }
}
