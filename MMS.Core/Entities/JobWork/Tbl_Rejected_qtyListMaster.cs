using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class Tbl_Rejected_qtyListMaster : BaseEntity
    {
        public int RejectedQty_id { get; set; }
        public int Grn_id { get; set; }
        public decimal Qty { get; set; }
        public int Psc { get; set; }

        public string RejectedQty_Reason { get; set; }

        public int RejectedQty_Inseption { get; set; }

        public string CreatedBY { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletedBy { get; set; }

    }
}
