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
    public class FloorReturnMaterialDetailsMap : EntityTypeConfiguration<FloorReturnMaterialDetails>
    {
        public FloorReturnMaterialDetailsMap()
        {
            HasKey(t => t.FloorReturnMaterialDetailsId);
            Property(t => t.FloorReturnMaterialDetailsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FloorReturnMaterialId);           
            Property(t => t.StoreMasterId);
            Property(t => t.GroupMasterId);           
            Property(t => t.MaterialMasterId);
            Property(t => t.IoNo);
            Property(t => t.Uom);
            Property(t => t.Style);
            Property(t => t.Category);
            Property(t => t.Piecies);
            Property(t => t.PiecesCurrentType);
            Property(t => t.IssuedQuantity);
            Property(t => t.ReturnQuantity);
            Property(t => t.Rate);
            Property(t => t.LotNo);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.MaterialType);

            ToTable("FloorReturnMaterialDetails");

        }
    }
}
