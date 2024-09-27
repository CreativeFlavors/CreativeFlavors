using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class purchaceorderheader
    {
        public decimal? qty { get; set; }
        public int? items { get; set; }
        public int? headerid { get; set; }
        public int? ponumber { get; set; }
        public string suppliername { get; set; }
        public string suppliercode { get; set; }
        public decimal? discountvalue { get; set; }
        public decimal? subtotalvalue { get; set; }
        public decimal? taxvalue { get; set; }
        public decimal? totalvalue { get; set; }
        public decimal? totalprice { get; set; }
        public string status { get; set; }
        public DateTime? fullfilldate { get; set; }
        public DateTime? createddate { get; set; }
    }
}
