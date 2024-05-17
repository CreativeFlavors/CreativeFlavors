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
    public class ImirGridMap : EntityTypeConfiguration<ImirGridDetails>
    {
        public ImirGridMap()
        {
            HasKey(t => t.ImirGridId);
            Property(t => t.ImirGridId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SlNo).IsRequired();
            Property(t => t.ParameterId).IsRequired();
            Property(t => t.Parameter).IsRequired();
            Property(t => t.InspectionFrequency).IsRequired();
            Property(t => t.RejectionQty).IsRequired();
            Property(t => t.GridRemarks).IsRequired();
            Property(t => t.ImirId).IsRequired();

            ToTable("ImirGrid");
        }
    }
}
