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
    public class CurrencyMap : EntityTypeConfiguration<currency>
    {
        public CurrencyMap() {
            HasKey(t => t.id);
            Property(t => t.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.currencycode);
            Property(t => t.currencyname);
            Property(t => t.currencysymbol);
            Property(t => t.createdby);
            ToTable("currency");
        }
    }
}
