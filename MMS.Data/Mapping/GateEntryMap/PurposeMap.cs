using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
   public class PurposeMap : EntityTypeConfiguration<Purpose>
    {
        public PurposeMap()
        {
            HasKey(t => t.PurposeId);
            Property(t => t.PurposeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PurposeDetails);
           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);


            ToTable("Purpose");
        }
    }
}
