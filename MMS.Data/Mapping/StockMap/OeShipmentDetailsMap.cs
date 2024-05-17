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
    public class OeShipmentDetailsMap : EntityTypeConfiguration<OeShipmentDetails>
    {
        public OeShipmentDetailsMap()
        {
            HasKey(t => t.OeShipmentDetailsId);
            Property(t => t.OeShipmentDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CountryStamp);
            Property(t => t.ShipmentFrom);
            Property(t => t.ShipmentTo);
            Property(t => t.OtherInstruction);
            Property(t => t.OrderEntryId);
            ToTable("OeShipmentDetails");
        }
    }
}
