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
    class ProductionJobworkMasterMap:EntityTypeConfiguration<ProductionJobworkMaster>
    {
        public ProductionJobworkMasterMap()
        {
            HasKey(t => t.Production_Order_id);
            Property(t => t.Production_Order_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Prod_code_id);
            Property(t => t.date);
            Property(t => t.Leather_Pairs);
            Property(t => t.component_Pairs);
            Property(t => t.Upper_Fullshoes);
            Property(t => t.Jw_Name);
            Property(t => t.Process_Name);
            Property(t => t.Stage_From);
            Property(t => t.Stage_To);

            Property(t => t.Lot_no);
            Property(t => t.Io_based);
            Property(t => t.Buyer);
            Property(t => t.Season);
            Property(t => t.Style);
            Property(t => t.Jw_Name);
            Property(t => t.JW_BOM_linked);
            Property(t => t.Material_Name);
            Property(t => t.Size_range);


            Property(t => t.Delivery_date);
            Property(t => t.Delivery_date);
            Property(t => t.Qty);
            Property(t => t.Qty_Uom);
            Property(t => t.Rate);
            Property(t => t.Value);
            Property(t => t.GST);
            Property(t => t.GST_AMT);
            Property(t => t.Total_Cost);

            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.Io);
            Property(t => t.ComponentId);
            Property(t => t.Fg_Material_Name);
            Property(t => t.Fg_ComponentId);
            ToTable("Job_ProductionJobwork");
        }
    }
}

