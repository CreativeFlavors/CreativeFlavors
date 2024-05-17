using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.JobWork;
using MMS.Data.StoredProcedureModel;

namespace MMS.Web.Models.JobworkModel
{
    public class ProductionJobworkModel
    {
        public int Production_Order_id { get; set; }

        public int Prod_code_id { get; set; }
        public String Prod_code_id_code { get; set; }

        public DateTime? date { get; set; }

        public bool Leather_Pairs { get; set; }


        public bool component_Pairs { get; set; }

        public bool Upper_Fullshoes { get; set; }

        public int Jw_Name { get; set; }

        public int Process_Name { get; set; }


        public int Stage_From { get; set; }

        public int Stage_To { get; set; }

        public int Lot_no { get; set; }

        public string Io_based { get; set; }

        public int Buyer { get; set; }

        public int Season { get; set; }

        public int Style { get; set; }
        public int Group { get; set; }
        public string JW_BOM_linked { get; set; }
        public int Material_Name { get; set; }
        public int Size_range { get; set; }
        public string ComponentId { get; set; }
        public DateTime Delivery_date { get; set; }
        public int Totalpair { get; set; }
        public decimal Qty { get; set; }
        public int Qty_Uom { get; set; }

        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public int GST { get; set; }
        public decimal GST_AMT { get; set; }
        public decimal Total_Cost { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
         public bool Production_Type { get; set; }
        public string DeletedBy { get; set; }
        public bool Io { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? Fg_Material_Name { get; set; }
        public string Fg_ComponentId { get; set; }
        public string Qty_ { get; set; }
        public string size { get; set; }
        public List<ProductionJobworkMaster> ProductionJobworkMasterlist { get; set; }
        public List<ProductionJobwork_Code_Master> ProductionJobwork_Code_list { get; set; }
        public List<JobProductionGrid> JobProductionGrid_list { get; set; }
        public ProductionJobwork_Code_Master code { get; set; }
    }
}