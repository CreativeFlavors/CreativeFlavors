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
   public class GEVisitReasonMap : EntityTypeConfiguration<GateEntryVisitReasonMaster>
    {
        public GEVisitReasonMap()
        {
            HasKey(t => t.GEVisitReasonId);
            Property(t => t.GEVisitReasonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Reason);

            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);


            ToTable("GEVisitReason");
        }
    }
}
