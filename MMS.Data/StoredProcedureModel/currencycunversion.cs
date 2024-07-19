using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_currency_conversion", Schema = "public")]
    public class currencycunversion
    {
        [Column("id")]
        public int id { get; set; }
        [Column("conversionvalue")]
        public decimal? conversionvalue { get; set; }
    }
}
