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
    public class OeOtherDetailsMap : EntityTypeConfiguration<OeOtherDetails>
    {
        public OeOtherDetailsMap()
        {
            HasKey(t => t.OeOtherDetailsId);
            Property(t => t.OeOtherDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PaymentTerms);
            Property(t => t.DeliveryTerms);
            Property(t => t.Insurance);
            Property(t => t.DeliverTo);
            Property(t => t.SpecialInstructions);
            Property(t => t.PackingOrLabelling);
            Property(t => t.OrderEntryId);
            ToTable("OeOtherDetails");
        }
    }
}
