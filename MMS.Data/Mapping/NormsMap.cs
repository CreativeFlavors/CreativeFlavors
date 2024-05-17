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
    class NormsMap:EntityTypeConfiguration<Norms>
    {
        public NormsMap()
        {
            HasKey(t => t.Normsid);
            Property(t => t.Normsid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GroupId);
            Property(t => t.BuyerOption);
            Property(t => t.OurNorms);
            Property(t => t.BuyerNameid);
            Property(t => t.PurchaseNormsid);
            Property(t => t.IssueNormsid);
            Property(t => t.CostingNorms);
            Property(t => t.NormsPercentage);
            Property(t => t.NormsPercentage1);
            Property(t => t.NormsPercentage2);
            Property(t => t.NormsPercentage3);            
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            ToTable("Norms");
        }
    }
}
