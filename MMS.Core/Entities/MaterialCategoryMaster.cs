using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("materialcategorymaster", Schema = "public")]
    public  class MaterialCategoryMaster:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("categorycode")]
        public string CategoryCode { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
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
