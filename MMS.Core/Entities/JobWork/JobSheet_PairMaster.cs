using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class JobSheet_PairMaster : BaseEntity
    {
        public int jobsheet_pair_id { get; set; }

        public int jobsheet_pair_Code_id { get; set; }

        public bool Exertnal_work { get; set; }
        public DateTime? Date { get; set; }

        public int JW_Name { get; set; }

        public int Process_Name { get; set; }

        public int UC_NO_id { get; set; }

        public int Buyer { get; set; }


        public int Season { get; set; }

        public int Stores { get; set; }

        public int Category { get; set; }

        public int Group_ { get; set; }

        public int Issue_Material { get; set; }

        public int Material { get; set; }

        public int Qty { get; set; }

        public int Qty_Uom { get; set; }

        public double Uc_Noms { get; set; }

        public int Uc_Noms_Uom { get; set; }

        public decimal? Uc_value { get; set; }

        public DateTime Delivery_Date { get; set; }
        public decimal? Jw_Rate { get; set; }

        public decimal? Value { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public decimal? Reqried_Qty { get; set; }

        public decimal? sheet { get; set; }
        public decimal? Extra_Piece { get; set; }

        public decimal Reduce_Piece { get; set; }

        public decimal? GST { get; set; }
        public decimal? Gst_Amt { get; set; }
        public decimal? Total { get; set; }

        //  public IList<JobSheet_PairMaster> jobsheet_pair_code_id_list { get; set; }

    }
}
