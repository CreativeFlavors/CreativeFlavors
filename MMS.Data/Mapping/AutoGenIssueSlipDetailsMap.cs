using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities;
namespace MMS.Data.Mapping
{
  public  class AutoGenIssueSlipDetailsMap : EntityTypeConfiguration<tblautogenissueslipdetails>
    {
        public AutoGenIssueSlipDetailsMap()
        {
            HasKey(t => t.AutoGenerateId);
            Property(t => t.AutoGenerateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IssueSlipDetailsId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("tblAutoGenIssueSlipDetails");
        }
    }
}
