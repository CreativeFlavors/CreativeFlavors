using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("addresstype", Schema = "public")]
    public class AddressType : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("addtypeid")]
        public int AddTypeID { get; set; }
        [Column("addtypename")]
        public string AddTypeName { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
