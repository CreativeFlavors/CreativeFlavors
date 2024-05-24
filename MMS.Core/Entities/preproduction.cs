using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("preproduction", Schema = "public")]
    public class preproduction : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("salesorderno")]
        public int SalesOrderNo { get; set; }
        [Column("salesorderdate")]
        public DateTime SalesOrderDate { get; set; }
        [Column("buyerid")]
        public int BuyerId { get; set; }
        [Column("materialid")]
        public int Materialid { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("qty")]
        public decimal? Qty { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; } = true;
    }
}
