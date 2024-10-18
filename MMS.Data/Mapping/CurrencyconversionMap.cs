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
    public class CurrencyconversionMap : EntityTypeConfiguration<CurrencyConversion>
    {
        public CurrencyconversionMap() {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PrimaryCurrency);
            Property(t => t.SecondaryCurrency);
            Property(t => t.ConversionValue);
            Property(t => t.FromDate);
            Property(t => t.ToDate);
            Property(t => t.CreatedBy);
            Property(t => t.IsActive);
            Property(t => t.UpdatedBy);
            ToTable("currencyconversion");
        }

    }
}
