using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("indentcart",Schema ="public")]
    public class IndentCart :BaseEntity
    {
        [Key]
        [Column("indentcartid")]
        public int IndentCartId { get; set; }

        [Column("materialnameid")]
        public int? MaterialNameId { get; set; }

        [Column("storenameid")]
        public int? StoreNameId { get; set; }

        [Column("taxnameid")]
        public int? TaxNameId { get; set; }

        [Column("uomnameid")]
        public int? UomNameId { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("indentrequiredqty")]
        public decimal? IndentRequiredQty { get; set; }

        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }

        [Column("status")]
        public int? Status { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("indentnumber")]
        public int? IndentNumber { get; set; }

        [Column("indentdate")]
        public DateTime? IndentDate { get; set; }
    }
}
