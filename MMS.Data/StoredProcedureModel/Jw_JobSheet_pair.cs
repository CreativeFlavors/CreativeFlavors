using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class Jw_JobSheet_pair
    {
        public int jobsheet_pair_id { get; set; }

        public int qty { get; set; }

        public double Uc_Noms { get; set; }

        public decimal Req_Qty { get; set; }

        public decimal sheet { get; set; }

        public decimal Jw_Rate { get; set; }
        // [NotMapped]
        public decimal Value { get; set; }

        public string MaterialDescription { get; set; }

        public int Material { get; set; }

          public int Issue_Material { get; set; }
        public int JW_Name { get; set; }
        public int jobsheet_pair_Code_id { get; set; }

        public int? DcRefNo { get; set; }
        public decimal? Pending_qty { get; set; }
        public int? IssueSlipId { get; set; }
        public int? Process_Name { get; set; }
        public int? Buyer { get; set; }
        public int? Season { get; set; }
    }
}
