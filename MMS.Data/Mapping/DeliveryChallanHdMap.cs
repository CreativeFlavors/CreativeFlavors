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
    public class DeliveryChallanHdMap : EntityTypeConfiguration<DeliveryChallan_hd>
    {
        public DeliveryChallanHdMap()
        {
            HasKey(t => t.DCid_hd);
            Property(t => t.DCid_hd).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DeliveryChallandate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CustomerId);
            Property(t => t.ItemsDc);
            Property(t => t.Quantity);
            Property(t => t.CustomerId);
            Property(t => t.TotalPrice);
            Property(t => t.IsActive);
            Property(t => t.TotalDisAmount);
            Property(t => t.TotalSubtotal);
            Property(t => t.TotalTaxAmount);
            Property(t => t.Quantity);
            Property(t => t.GrandTotal);
            Property(t => t.TotalPrice);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.IsDeleted);
            Property(t => t.Status);
            ToTable("DeliveryChallan_dt");
        }
    }
}
