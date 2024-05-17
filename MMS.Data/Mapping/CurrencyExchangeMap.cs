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
    public class CurrencyExchangeMap : EntityTypeConfiguration<CurrencyExchangeMaster>
    {
         public CurrencyExchangeMap()
        {
            HasKey(t => t.CurrencyExchangeMasterId);
            Property(t => t.CurrencyExchangeMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Currency).IsRequired();
            Property(t => t.Date).IsRequired();
            Property(t => t.ValueInRupees).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("CurrencyExchangeMaster");
        }
    }
}
