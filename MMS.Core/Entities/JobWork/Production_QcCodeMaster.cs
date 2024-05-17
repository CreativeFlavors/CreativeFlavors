using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class Production_QcCodeMaster:BaseEntity
    {
        public int ProductionQc_ID { get; set; }

        public string Production_Code { get; set; }

        public DateTime Date { get; set; }

        public int Buyer { get; set; }

        public int Season { get; set; }

        public int Stage { get; set; }

        public int Lot_No { get; set; }

        public string Moved_lot { get; set; }

        public int Io { get; set; }

        public string Moved_Io { get; set; }

        public string Style { get; set; }

        public int QC_Io { get; set; }

        public int Total_Pairs { get; set; }

        public int Size { get; set; }


        public decimal IO_Size { get; set; }

        public decimal Qty { get; set; }

        public string Side { get; set; }

        public string CreatedBy { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        

        public DateTime? UpdatedBy { get; set; }

    }
}
