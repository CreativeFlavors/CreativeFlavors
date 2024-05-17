using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("tblautogenissueslipdetails", Schema = "public")]
    public class tblautogenissueslipdetails : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("autogenerateid")]
        public int AutoGenerateId { get; set; }

        [Column("issueslipdetailsid")]
        public string IssueSlipDetailsId { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

    }
}
