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
    public class MultipleIssueSlipMap : EntityTypeConfiguration<MultipleIssueSlip>
    {
        public MultipleIssueSlipMap()
        {
            HasKey(t => t.MultipleIssueSlipID);
            Property(t => t.MultipleIssueSlipID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IssueSlipNo);
            Property(t => t.InternalOderID);
            Property(t => t.StyleNo);       
             
            Property(t => t.ConveyorID);
            Property(t => t.LotNo);
            Property(t => t.MachineName);           
            Property(t => t.SubtanceID);             
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsJobWork);
            Property(t => t.Jobworktype_Id);
            Property(t => t.GateOut_No);
            Property(t => t.GateOut_No_Datetime);
            ToTable("MultipleIssueSlip");
        }
    }
}
