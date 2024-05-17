using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("tbl_Permission", Schema = "public")]
    public class tbl_Permission : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("permissionid")]
        public int PermissionID { get; set; }
        [Column("permissionname")]
        public string PermissionName { get; set; }
        [Column("permissiondesc")]
        public string PermissionDesc { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
