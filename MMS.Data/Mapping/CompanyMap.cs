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
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            HasKey(t => t.CompanyCodePK);
            Property(t => t.CompanyCodePK).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SupplierName);
            Property(t => t.StoreID);            
            Property(t => t.City);
            Property(t => t.Address);
            Property(t => t.DeliverTerms);
            Property(t => t.TermsConditions);
            Property(t => t.CompanyName);
            Property(t => t.Phone);
            Property(t => t.PaymentTerms);
            Property(t => t.OtherTerms);            
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.Createdby);
            Property(t => t.Updatedby);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedOn);
            Property(t => t.Deletedby);
            ToTable("Company");
        }
    }
}
