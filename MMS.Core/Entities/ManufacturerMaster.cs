using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("manufacturermaster", Schema = "public")]
    public class ManufacturerMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("manufacturermasterid")]
        public int ManufacturerMasterId { get; set; }

        [Column("manufacturercode")]
        public string ManufacturerCode { get; set; }

        [Column("manufacturername")]
        public string ManufacturerName { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
