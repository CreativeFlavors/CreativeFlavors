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
    public class CustomertransactionMap : EntityTypeConfiguration<customertransaction>
    {
        public CustomertransactionMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CustomerId);
            Property(t => t.PaymentDate);
            Property(t => t.PaymentAmount);
            Property(t => t.InvRefNumber);
            Property(t => t.InvDate);
            Property(t => t.InvDueDate);
            Property(t => t.InvAmount);
            Property(t => t.InvPaidAmount);
            Property(t => t.InvBalanceAmount);
            Property(t => t.IsCustomerOnHold);
            Property(t => t.PaymentRefNo);
            Property(t => t.CreditNoteRef);
            Property(t => t.CreditNoteDate);
            Property(t => t.CreditNoteValue);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.PaymentId);
            Property(t => t.Cash);
            ToTable("customertransaction");
        }

    }
}
