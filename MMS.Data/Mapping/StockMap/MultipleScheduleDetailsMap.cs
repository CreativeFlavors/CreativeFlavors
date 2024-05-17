using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class MultipleScheduleDetailsMap : EntityTypeConfiguration<MultipleScheduleDetails>
    {
        public MultipleScheduleDetailsMap()
        {
            HasKey(t => t.MultipleScheduleDetailsId);
            Property(t => t.MultipleScheduleDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Io);
            Property(t => t.Size);
            Property(t => t.Qty);
            Property(t => t.ExFDt);
            Property(t => t.OrderEntryId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("MultipleScheduleDetails");
        }
    }
}
