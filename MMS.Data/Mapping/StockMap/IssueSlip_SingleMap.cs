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
    public class IssueSlip_SingleMap : EntityTypeConfiguration<singleissueslip>
    {
        public IssueSlip_SingleMap()
        {
            HasKey(t => t.IssueSlipID);
            Property(t => t.IssueSlipID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IssueSlipNo);
            Property(t => t.InternalOderID);
            Property(t => t.StyleNo); 
            Property(t => t.ConveyorID);
            Property(t => t.MachineName);           
            Property(t => t.SubtanceID);          
            Property(t => t.CurrentStock);
            Property(t => t.CurrentStockType);          
            Property(t => t.FreeStock);
            Property(t => t.FreeStockType);         
            Property(t => t.ReserverQty);
            Property(t => t.ReserverQtyType);  
            Property(t => t.ClosingStockinAllStores);
            Property(t => t.ClosingStockinAllStoredType);
            Property(t => t.InTransitValue);
            Property(t => t.InTransitType);         
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("SingleIssueSlip");
        }
    }
}
