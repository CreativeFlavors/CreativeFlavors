using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("salesorder_hd", Schema = "public")]
    public class Salesorder_hd : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("salesorderid_hd")]
        public int salesorderid_hd { get; set; }
        [Column("customerid")]
        public int customerid { get; set; }
        [Column("items")]
        public int items { get; set; }
        [Column("quantity")]
        public decimal? quantity { get; set; }
        [Column("salesorderdate")]
        public DateTime Salesorderdate { get; set; }
        [Column("total_price")]
        public decimal? Total_price { get; set; }
        [Column("total_disamount")]
        public decimal? Total_disamount { get; set; }
        [Column("total_subtotal")]
        public decimal? Total_subtotal { get; set; }
        [Column("total_taxamount")]
        public decimal? Total_taxamount { get; set; }
        [Column("grand_total")]
        public decimal? grand_total { get; set; }
        [Column("isactive")]
        public bool isactive { get; set; } = true;
        [Column("createdby")]
        public string createdby { get; set; }
        [Column("updatedby")]
        public string Updatedby { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("deletedby")]
        public string deletedby { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted{ get; set; } = true;

    }
}
