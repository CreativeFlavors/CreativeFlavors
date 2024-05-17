using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class CartonDetailsMap: EntityTypeConfiguration<CartonDetails>
    {
        public CartonDetailsMap()
        {
            HasKey(t => t.CartonDetailsId);
            Property(t => t.CartonDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PackType);
            Property(t => t.NoOfCarton);
            Property(t => t.OrderEntryId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("CartonDetails");
        }
    }
}
