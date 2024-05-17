using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
   public class GroupSystemCalculationMap:EntityTypeConfiguration<GroupSystemCalculation>
    {
        public GroupSystemCalculationMap()
        {
            HasKey(t => t.GroupSystemCalculationId);
            Property(t => t.GroupSystemCalculationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductionPercent).IsRequired();
            Property(t => t.Amount).IsRequired();              
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("GroupSystemCalculation");
        }
    }
}
