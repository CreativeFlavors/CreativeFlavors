using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MMS.Data.Mapping.JobWork
{
    class ProductionJobworkCoderMap:EntityTypeConfiguration<ProductionJobwork_Code_Master>
    {
        public ProductionJobworkCoderMap()
        {
            HasKey(t => t.ProductionJobwork_code_id);
            Property(t => t.ProductionJobwork_code_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductionJobwork_Code);

            Property(t => t.CreatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.Leather_Pairs);
            Property(t => t.component_Pairs);
            Property(t => t.Upper_Fullshoes);
            Property(t => t.Production_Type);
            ToTable("productionjobwork_code_master");
        }
    }
}
