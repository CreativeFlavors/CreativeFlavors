using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("currencyconversion", Schema = "public")]
    public class CurrencyConversion : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("primarycurrency")]
        public int PrimaryCurrency { get; set; }
        [Column("secondarycurrency")]
        public int SecondaryCurrency { get; set; }
        [Column("conversionvalue")]
        public decimal? ConversionValue { get; set; }
        [Column("fromdate")]
        public DateTime? FromDate { get; set; }
        [Column("todate")]
        public DateTime? ToDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; } = true;
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
