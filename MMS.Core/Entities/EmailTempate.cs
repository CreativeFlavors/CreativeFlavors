using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("emailtemplate", Schema = "public")]
    public class EmailTemplate:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("emailtemplateid")]
        public int EmailTemplateID { get; set; }
        [Column("templatename")]
        public string TemplateName { get; set; }
        [Column("subject")]
        public string Subject { get; set; }
        [Column("body")]
        public string Body { get; set; }
        [Column("fromaddress")]
        public string FromAddress { get; set; }
        [Column("toaddress")]
        public string ToAddress { get; set; }
        [Column("ccaddress")]
        public string CCAddress { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("createddate")]
        public string CreatedDate { get; set; }
        [Column("updateddate")]
        public string UpdatedDate { get; set; }
    }
}
