using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("buyermodel", Schema = "public")]
    public class BuyerModel : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("buyermodelid")]
        public int BuyerModelID { get; set; }
        [Column("buyermodelname")]
        public string BuyerModelName { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
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
        [Column("creditexposure")]
        public string CreditExposure { get; set; }
        [Column("creditdays")]
        public string CreditDays { get; set; }
    }
}
