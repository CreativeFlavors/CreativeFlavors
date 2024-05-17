using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities.Stock;
namespace MMS.Data.Mapping.StockMap
{
   public  class AutoGenIndentMap : EntityTypeConfiguration<tblAutoGenIndent>
    {
        public AutoGenIndentMap()
        {
            HasKey(t => t.AutoGenerateId);
            Property(t => t.AutoGenerateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IndentId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("tblAutoGenIndent");
        }
    }
}
