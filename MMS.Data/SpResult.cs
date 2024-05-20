using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    [Table("spresult", Schema = "public")]
    public class SpResult
    {
        [Column("suppliername")]
        public string SupplierName { get; set; }
        [Column("price")]
        public string  Price { get; set; }
        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
    }
    [Table("interanalbuyerno", Schema = "public")]
    public class InteranalBuyerNo
    {
        [Column("buyerpono")]
        public string BuyerPoNo { get; set; }
        [Column("orderentryid")]
        public int? OrderEntryId { get; set; }      
    }
}