using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class currencyconversiongrid
    {
        public int id { get; set; }
        public int primaryid { get; set; }
        public int secondaryid { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public bool isactive { get; set; }
        public decimal? conversionvalue { get; set; }
        public DateTime? createddate { get; set; }
        public string primarycurrency { get; set; }
        public string secondarycurrency { get; set; }
    }
}
