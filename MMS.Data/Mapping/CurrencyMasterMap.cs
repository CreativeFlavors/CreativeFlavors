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
    public class CurrencyMasterMap : EntityTypeConfiguration<CurrencyMaster>
    {
        public CurrencyMasterMap()
        {
            HasKey(t => t.CurrencyMasterId);
            Property(t => t.CurrencyMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Symbol);
            Property(t => t.ShortForm);
            Property(t => t.LongForm);
            Property(t => t.LowerDenomination);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("CurrencyMaster");
        }
    }
}
