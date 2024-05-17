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
    public class MRPSizeRangeMap : EntityTypeConfiguration<MRPSizeRange>
    {
        public MRPSizeRangeMap()
        {
            HasKey(t => t.MRPSizeRangeID);
            Property(t => t.MRPSizeRangeID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size).IsRequired();
            Property(t => t.Qty);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.BOMMaterialID);
            Property(t => t.MaterialName);       
            ToTable("MRPSizeRange");
        }
    }
}
