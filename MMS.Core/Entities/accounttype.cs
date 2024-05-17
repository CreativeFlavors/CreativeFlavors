using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("accounttype", Schema = "public")]
    public class accounttype : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accountabbr")]
        public string AccountAbbr { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("termdays")]
        public int TermDays { get; set; }
        [Column("fromstatement")]
        public bool FromStatement { get; set; }
        [Column("frominvoice")]
        public bool FromInvoice { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isactive")]
        public bool? IsActive { get; set; }
    }
}
