using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class SupplierSuppliedMaterialPrice
    {
        public string SupplierName { get; set; }
        public decimal ApprovedPrice { get; set; }
        public DateTime? ApprovedDate { get; set; }

    }
}
