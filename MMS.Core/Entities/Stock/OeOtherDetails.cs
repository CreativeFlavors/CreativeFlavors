using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{

    [Table("oeotherdetails", Schema = "public")]
    public class OeOtherDetails : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("oeotherdetailsid")]
        public int OeOtherDetailsId { get; set; }
        [Column("paymentterms")]
        public string PaymentTerms { get; set; }
        [Column("deliveryterms")]
        public string DeliveryTerms { get; set; }
        [Column("insurance")]
        public string Insurance { get; set; }
        [Column("deliverto")]
        public string DeliverTo { get; set; }
        [Column("specialinstructions")]
        public string SpecialInstructions { get; set; }
        [Column("packingorlabelling")]
        public string PackingOrLabelling { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
