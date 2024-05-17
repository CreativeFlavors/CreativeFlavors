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
    public class AccounttypemMap : EntityTypeConfiguration<accounttype>
    {
        public AccounttypemMap() {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TermDays);
            Property(t => t.FromStatement);
            Property(t => t.FromInvoice);
            Property(t => t.AccountAbbr);
            Property(t => t.Description);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.IsActive);
            ToTable("accounttype");
        }

    }
}
