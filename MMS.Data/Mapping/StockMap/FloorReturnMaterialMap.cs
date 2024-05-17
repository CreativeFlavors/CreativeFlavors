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
    public class FloorReturnMaterialMap : EntityTypeConfiguration<FloorMaterial>
    {
        public FloorReturnMaterialMap()
        {
            HasKey(t => t.FloorReturnMaterialId);
            Property(t => t.FloorReturnMaterialId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FrmNo).IsRequired();
            Property(t => t.FloorDate);
            Property(t => t.Remarks);          
            Property(t => t.IssueNo).IsRequired();           
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.Is_IssueNo);

            ToTable("FloorReturnMaterial");

        }
    }
}
