using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.JobWork;

namespace MMS.Web.Models.JobworkModel
{
    public class ProductionQcModel
    {
        public int Qc_id { get; set; }

        public string Production_Code_new { get; set; }

        public int ProductionQc_ID { get; set; }

        public string From_Stage { get; set; }

        public decimal Size { get; set; }

        public string Qty { get; set; }

        public string Side { get; set; }

        public string Reason { get; set; }

        public string Type { get; set; }

        public int Component { get; set; }

        public string Style { get; set; }

        public string To_stage { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }


        //Code
        public DateTime Date { get; set; }

        public int Buyer { get; set; }

        public int Season { get; set; }

        public int Stage { get; set; }

        public int Lot_No { get; set; }

        public string Moved_lot { get; set; }

        public int Io { get; set; }

        public string Moved_Io { get; set; }

        public int QC_Io { get; set; }

        public int Total_Pairs { get; set; }


        public decimal IO_Size { get; set; }

        public string Production_Code { get; set; }
        public string Qty_ { get; set; }
        public string size_ { get; set; }


        public List<Production_QcCodeMaster> Production_QcCodeMaster { get; set; }
        public Production_QcCodeMaster Production_Code_ { get; set; }
        public List<Production_QcMaster> Production_QcMaster { get; set; }
    }
}