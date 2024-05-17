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
    class JobOtherWorkMap : EntityTypeConfiguration<JobOtherWorkMaster>
    {
        public JobOtherWorkMap()
        {
            HasKey(t => t.OtherJobWork_Id);
            Property(t => t.OtherJobWork_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.OtherJobWork_Code);
            Property(t => t.Services);
            Property(t=>t.Department_Id);
            Property(t => t.JobWork_Id);
            Property(t => t.Process_Id);
            Property(t => t.Buyer_Id);
            Property(t => t.Season_Id);
            Property(t => t.Stoeres_Id);
            Property(t => t.Group_Id);
            Property(t => t.Category_Id);
            Property(t => t.Machinery_Id);
            Property(t => t.Spare_Id);
            Property(t => t.Quantity);
            Property(t => t.UOM);
            Property(t => t.ServiceDescription);
            Property(t => t.JobWork_Price);
            Property(t => t.JobWork_Price_Value);
            Property(t => t.GST);
            Property(t => t.GST_Amount);
            Property(t => t.TotalCost);            
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("JobOtherWork");
        }
    }
}
