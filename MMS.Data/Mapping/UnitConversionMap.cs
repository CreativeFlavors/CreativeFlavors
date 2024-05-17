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
    public class UnitConversionMap : EntityTypeConfiguration<UnitConversation>
    {
        public UnitConversionMap()
        {
            HasKey(t => t.UnitConversionId);
            Property(t => t.UnitConversionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          //  Property(t => t.UOMUnitmasteID);
            Property(t => t.Category);
            Property(t => t.UcGroup);
            Property(t => t.Material);
            Property(t => t.ShortUnitNameValue);
            Property(t => t.LongUnitNameValue);
            Property(t => t.ShortUnitId);
            Property(t => t.LongUnitID);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("UnitConversion");
        }
    }
}
