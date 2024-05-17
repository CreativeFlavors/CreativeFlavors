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
    public class SupplierTransactionMap : EntityTypeConfiguration<supplierTransaction>
    {
        public SupplierTransactionMap()
        {

            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierId);
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.SupplierId).HasColumnName("supplierid");
            Property(t => t.paymentid).HasColumnName("paymentid");
            Property(t => t.Cash).HasColumnName("cash");
            //Property(t => t.PoNo).HasColumnName("pono");
            //Property(t => t.PoDate).HasColumnName("podate");
            Property(t => t.PaymentDate).HasColumnName("paymentdate");
            Property(t => t.PaymentAmount).HasColumnName("paymentamount");
            Property(t => t.GrnRefNumber).HasColumnName("grnrefnumber");
            //Property(t => t.Supplier).HasColumnName("supplier");
            //Property(t => t.Grnqty).HasColumnName("grnqty");
            //Property(t => t.SupplierCode).HasColumnName("suppliercode");
            Property(t => t.GrnDate).HasColumnName("grndate");
            Property(t => t.GrnDueDate).HasColumnName("grnduedate");
            Property(t => t.GrnAmount).HasColumnName("grnamount");
            Property(t => t.GrnPaidAmount).HasColumnName("grnpaidamount");
            Property(t => t.GrnBalanceAmount).HasColumnName("grnbalanceamount");
            Property(t => t.IsTransactionOnHold).HasColumnName("istransactiononhold");
            Property(t => t.PaymentRefNo).HasColumnName("paymentrefno");
            Property(t => t.CreditNoteRef).HasColumnName("creditnoteref");
            Property(t => t.CreditNoteDate).HasColumnName("creditnotedate");
            Property(t => t.CreditNoteValue).HasColumnName("creditnotevalue");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            //Property(t => t.DeletedOn).HasColumnName("deletedon");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            //Property(t => t.DeletedBy).HasColumnName("deletedby");
            //Property(t => t.IsDeleted).HasColumnName("isdeleted");
            ToTable("suppliertransaction");
        }
    }
}

