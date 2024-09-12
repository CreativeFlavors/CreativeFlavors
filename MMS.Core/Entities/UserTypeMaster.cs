using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("usertypemaster", Schema = "public")]
    public class UserTypeMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("usertypeid")]
        public int UserTypeID { get; set; }
        [Column("usertype")]
        public string UserType { get; set; }
        [Column("usertypedesc")]
        public string UserTypeDesc { get; set; }
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
    }
}
