using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("currencymaster", Schema = "public")]
    public class CurrencyMaster: BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("currencymasterid")]
        public int CurrencyMasterId { get; set; }

        [Column("symbol")]
        public string Symbol { get; set; }

        [Column("shortform")]
        public string ShortForm { get; set; }

        [Column("longform")]
        public string LongForm { get; set; }

        [Column("lowerdenomination")]
        public string LowerDenomination { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
