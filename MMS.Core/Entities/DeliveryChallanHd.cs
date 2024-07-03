using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class DeliveryChallanHd
    {
        public int DCid_hd { get; set; }

        public DateTime DeliveryChallandate { get; set; }

        public int CustomerId { get; set; }

        public int ItemsDc { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDisAmount { get; set; }

        public decimal TotalSubtotal { get; set; }

        public decimal TotalTaxAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int Status { get; set; }
    }
}
