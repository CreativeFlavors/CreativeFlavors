using MMS.Core.Entities;
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
    public class OePackingDetailsMap : EntityTypeConfiguration<OePackingDetails>
    {
        public OePackingDetailsMap()
        {
            HasKey(t => t.OePackingDetailsId);
            Property(t => t.OePackingDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PackingType);
            Property(t => t.SizeRangeMasterId);
            Property(t => t.Size);
            Property(t => t.OrderEntryId);
            ToTable("OePackingDetails");
        }
    }
}
