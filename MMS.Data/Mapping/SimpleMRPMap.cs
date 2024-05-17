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
    public class SimpleMRPMap : EntityTypeConfiguration<SimpleMRP>
    {
        public SimpleMRPMap()
        {
            HasKey(t => t.SimpleMRPID);
            Property(t => t.SimpleMRPID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MRPNO);
            Property(t => t.MRPDate);
            Property(t => t.MRPType);
            Property(t => t.BuyerNameid);
            Property(t => t.WeekNO);
            Property(t => t.SeasonID);
            Property(t => t.LotNO);
            Property(t => t.SizeRangeID);
            Property(t => t.From);
            Property(t => t.TO);
            Property(t => t.CustomerPlan);
            Property(t => t.ProductionPlanBasic);
            Property(t => t.OrderBasic);
            Property(t => t.JobWork);
            Property(t => t.RejectionorReplacement);
            Property(t => t.OverConsumption);
            //Property(t => t.OverConsumption);
            Property(t => t.Req_Qty);
            Property(t => t.UOM);
            Property(t => t.Rate);
            Property(t => t.TotalNorms);
            Property(t => t.Detailed);
            Property(t => t.Consolidate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            Property(t => t.TotalOrderCount);            
            ToTable("SimpleMRP");
        }
    }
}
