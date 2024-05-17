using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.JobWork
{
    class JobworkMap : EntityTypeConfiguration<JobworkMaster>
    {
        public JobworkMap()
        {
            HasKey(t => t.JW_Id);
            Property(t => t.JW_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.JW_Code);
            Property(t => t.JW_Name);
            Property(t => t.Currency);
            Property(t => t.Address);
            Property(t => t.Country);
            Property(t => t.Contact_Person);
            Property(t => t.Designation);
            Property(t => t.Mobile);
            Property(t => t.Phone);
            Property(t => t.Fax);
            Property(t => t.Email);
            Property(t => t.St_No_Head);
            Property(t => t.Cst_No_Head);
            Property(t => t.Range);
            Property(t => t.Division);
            Property(t => t.PLA_No);
            Property(t => t.EOC_No);
            Property(t => t.Regn_No);
            Property(t => t.Payment_Terms);
            Property(t => t.Delivery_Terms);
            Property(t => t.Packing_Details);
            Property(t => t.Insurance);
            Property(t => t.Port_Of_Loading);
            Property(t => t.Despatch_Through);
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
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("Job_JobworkMaster");
        }
    }
}
