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
    public class SupplierMasterMap : EntityTypeConfiguration<SupplierMaster>
    {
        public SupplierMasterMap()
        {
            HasKey(t => t.SupplierMasterId);
            Property(t => t.SupplierMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierCode);
            Property(t => t.SupplierName);
            Property(t => t.Currency);
            Property(t => t.AddressLine1);
            Property(t => t.AddressLine2);
            Property(t => t.AddressLine3);
            Property(t => t.Country);
            Property(t => t.ContactPerson);
            Property(t => t.Designation);
            Property(t => t.Mobile);
            Property(t => t.LandLine);
            Property(t => t.Fax);
            Property(t => t.Email);
            Property(t => t.StNo);
            Property(t => t.CstNo);
            Property(t => t.Range);
            Property(t => t.Division);
            Property(t => t.PLANo);
            Property(t => t.EOCNo);
            Property(t => t.RegnNo);
            Property(t => t.PaymentTerms);
            Property(t => t.DeliveryTerms);
            Property(t => t.PackingDetails);
            Property(t => t.Insurance);
            Property(t => t.PortOfLoading);
            Property(t => t.DespatchThrough);
            Property(t => t.Freight);
            Property(t => t.FreightName);
            Property(t => t.Form);
            Property(t => t.FormName);
            Property(t => t.Forwarder);
            Property(t => t.BankName);
            Property(t => t.BankAddress);
            Property(t => t.BankCode);
            Property(t => t.SwiftCode);
            Property(t => t.AccountNumber);
            Property(t => t.IBAN);
            Property(t => t.OtherInformation);
            Property(t => t.IsDomestic);
            Property(t => t.IsImport);
            Property(t => t.MaterialMasterID);
            Property(t => t.SupplierGSTIN);
            Property(t => t.MaterialGroupMasterID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreditLimit);
            Property(t => t.CreditDays);
            ToTable("SupplierMaster");
        }
    }
}
