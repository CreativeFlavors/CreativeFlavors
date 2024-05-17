using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class Production_QcMaster: BaseEntity
    {
        public int Qc_id { get; set; }

        public int ProductionQc_ID { get; set; }

        public string From_Stage { get; set; }

        public decimal Size { get; set; }

        public string Qty { get; set; }
        public int QC_Io { get; set; }
        public string Side { get; set; }

        public string Reason { get; set; }

        public string Type { get; set; }

        public int Component { get; set; }

        public string Style { get; set; }

        public string To_stage { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
