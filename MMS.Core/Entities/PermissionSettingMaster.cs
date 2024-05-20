using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("tbl_permissionsetting", Schema = "public")]
    public class tbl_PermissionSetting : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("permissionsetid")]
        public int PermissionSetID { get; set; }
        [Column("usertypeid")]
        public int UserTypeID { get; set; }
        [Column("permissionid")]
        public string PermissionID { get; set; }
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
