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
     
    class GatePassGRNMasterMap:   EntityTypeConfiguration<GatePassGRNMaster>
    {
        public GatePassGRNMasterMap()
        {
            HasKey(t => t.GatePassInvwardId);
            Property(t => t.GatePassInvwardId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ReceiptNo).IsRequired();
            
            Property(t => t.Date).IsRequired();
            Property(t => t.IsSupplier).IsRequired();
            Property(t => t.PartyName).IsRequired();

            Property(t => t.RefGatePassNo).IsRequired();
            Property(t => t.Material).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.UOM).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.Amount).IsRequired();
            Property(t => t.Instructions).IsRequired();
         
            Property(t => t.IsPrintWithRateValue);
            Property(t => t.IsPrintWithoutCompanyAddress);      

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("GatePassDCInward");
        }
    }
}
