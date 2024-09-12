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
    public class BuyerMasterMap : EntityTypeConfiguration<BuyerMaster1>
    {
        public BuyerMasterMap()
        {
            ToTable("buyermaster1", "public");

            HasKey(t => t.BuyerMasterId);

            Property(t => t.BuyerMasterId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.CustomerName).IsRequired();
            Property(t => t.Account).IsRequired();
            Property(t => t.AccountName).IsRequired();
            Property(t => t.AccountDescription).IsRequired();
            Property(t => t.SwiftCode).IsRequired();
            Property(t => t.Physical1).IsRequired();
            
            Property(t => t.PhysicalCode).IsRequired();           
            Property(t => t.CurrencyId).IsRequired();
            Property(t => t.Telephone1).IsRequired();
            Property(t => t.Telephone2).IsRequired();
            Property(t => t.EmailContact).IsRequired();
            Property(t => t.EmailAccounts).IsRequired();
            Property(t => t.EmailEmergency).IsRequired();
            Property(t => t.AccountTypeId).IsRequired();
            Property(t => t.VatNumber).IsRequired();
            Property(t => t.RegNumber).IsRequired();     
     
            Property(t => t.CreditLimit).IsRequired();
            Property(t => t.ChargeInterest).IsRequired();
            Property(t => t.Interest).IsRequired(); 
            Property(t => t.TaxTypeId).IsRequired();
            Property(t => t.ForeignCurrency).IsRequired();      
          
            Property(t => t.OnHold);
            Property(t => t.Active);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.BuyerCode).IsRequired();
            Property(t => t.BuyerShortName).IsRequired();
      
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.DateAdded).IsRequired();
            Property(t=>t.DcBalance).IsRequired();
            Property(t => t.ForeignDcBalance).IsRequired();

        }
    }
}
