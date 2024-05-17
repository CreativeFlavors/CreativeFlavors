using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class JobLeather_leatherMaster : BaseEntity
    {
        public int Job_Lether_to_lether_id { get; set; }

        public string Job_Lether_to_lether_Code { get; set; }

        public bool Jobwork_raw_material { get; set; }

        public bool Encho_Raw_Material { get; set; }

        public DateTime? Date_from { get; set; }

        public int Jw_Name { get; set; }

        public int Process_Name { get; set; }

        public int Buyer { get; set; }


        public int Season { get; set; }

        public int Stores { get; set; }

        public int Category { get; set; }

        public int Group_ { get; set; }

        public int Material { get; set; }

        public int Finished_Material { get; set; }


        public decimal Qty { get; set; }

        public int Qty_Uom { get; set; }

        public decimal Rate { get; set; }

        public decimal GST { get; set; }

        public decimal Value { get; set; }

        public decimal Gst_Amt { get; set; }


        public decimal Total { get; set; }

        public DateTime? Delivery_Date { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }

}