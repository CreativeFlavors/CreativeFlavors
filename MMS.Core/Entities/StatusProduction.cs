using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("statusproduction", Schema = "public")]
    public class StatusProduction :BaseEntity
    {
        [Column("statusid")]
        public int StatusId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("producttype")]
        public int? ProductType { get; set; }

        [Column("affectissue")]
        public decimal? AffectIssue { get; set; }

        [Column("affectinventory")]
        public decimal? AffectInventory { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
