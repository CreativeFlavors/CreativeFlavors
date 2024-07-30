using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("categorymaster", Schema = "public")]
    public class CategoryMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("categoryid")]
        public int CategoryId { get; set; }
        [Column("categorycode")]
        public string CategoryCode { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; } = true;
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("categorytype")]
        public int? CategoryType { get; set; }
    }
}
