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
    public class SupplierMasterMap : EntityTypeConfiguration<Supplier_master>
    {
        public SupplierMasterMap()
        {
            ToTable("supplier_master");
            HasKey(t => t.SupplierId);
            Property(t => t.SupplierId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Suppliername).HasColumnName("suppliername");
            Property(t => t.suppliercode).HasColumnName("suppliercode");
            Property(t => t.Account).HasColumnName("account");
            Property(t => t.AccountName).HasColumnName("accountname");
            Property(t => t.AccountDescription).HasColumnName("accountdescription");
            Property(t => t.Physical1).HasColumnName("physical1");
            Property(t => t.PhysicalCode).HasColumnName("physicalcode");;
            Property(t => t.Telephone1).HasColumnName("telephone1");
            Property(t => t.Telephone2).HasColumnName("telephone2");
            Property(t => t.EmailContact).HasColumnName("emailcontact");
            Property(t => t.EmailAccounts).HasColumnName("emailaccounts");
            Property(t => t.EmailEmergency).HasColumnName("emailemergency");
            Property(t => t.AccountTypeId).HasColumnName("accounttypeid");
            Property(t => t.VatNumber).HasColumnName("vatnumber");
            Property(t => t.RegNumber).HasColumnName("regnumber");
            Property(t => t.DcBalance).HasColumnName("dcbalance");
            Property(t => t.CreditLimit).HasColumnName("creditlimit");
            Property(t => t.Interest).HasColumnName("interest");
            Property(t => t.TaxTypeId).HasColumnName("taxtypeid");
            Property(t => t.ForeignCurrency).HasColumnName("foreigncurrency");
            Property(t => t.CurrencyID).HasColumnName("currencyid");
            Property(t => t.OnHold).HasColumnName("onhold");
            Property(t => t.createdby).HasColumnName("createdby");
            Property(t => t.updatedby).HasColumnName("updatedby");
            Property(t => t.isActive).HasColumnName("isactive");
            Property(t => t.deletedby).HasColumnName("deletedby");
            Property(t => t.isdeleted).HasColumnName("isdeleted");
            Property(t => t.deleteddate).HasColumnName("deleteddate");
        }
    }
}
