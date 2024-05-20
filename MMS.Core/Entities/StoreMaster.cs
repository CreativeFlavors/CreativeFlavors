using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("storemaster", Schema = "public")]
    public class StoreMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("storemasterid")]
        public int StoreMasterId { get; set; }
        [Column("storecode")]
        public string StoreCode { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("location")]
        public string Location { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
