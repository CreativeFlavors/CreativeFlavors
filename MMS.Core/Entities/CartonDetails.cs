using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("cartondetails", Schema = "public")]
    public class CartonDetails : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("cartondetailsid")]
        public int CartonDetailsId { get; set; }
        [Column("packtype")]
        public string PackType { get; set; }
        [Column("noofcarton")]
        public string NoOfCarton { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
