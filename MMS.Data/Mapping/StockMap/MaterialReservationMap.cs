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
    public class MaterialReservationMap : EntityTypeConfiguration<MaterialReservation>
    {
        public MaterialReservationMap()
        {
            HasKey(t => t.MaterialReservationId);
            Property(t => t.MaterialReservationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DocNo).IsRequired();
            Property(t => t.InternalOrder).IsRequired();
            Property(t => t.PlanWise).IsRequired();
            Property(t => t.MaterialCategoryId).IsRequired();
            Property(t => t.MaterialGroupId).IsRequired();
            Property(t => t.MaterialMasterId).IsRequired();
            Property(t => t.UomId).IsRequired();
            Property(t => t.ColourMasterId).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.FreeStock).IsRequired();
            Property(t => t.MaterialWise);
            Property(t => t.Summary);
            Property(t => t.MultipleIO).IsRequired();
            Property(t => t.DisplayIO).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("MaterialReservation");
        }
    }
}
