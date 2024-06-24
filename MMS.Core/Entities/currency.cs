using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("currency", Schema = "public")]
    public class currency :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int id { get; set; }

        [Column("currencycode")]
        public string currencycode { get; set; }

        [Column("currencyname")]
        public string currencyname { get; set; }

        [Column("currencysymbol")]
        public string currencysymbol { get; set; }
        [Column("createdby")]
        public string createdby { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
