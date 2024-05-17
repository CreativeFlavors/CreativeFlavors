using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("oepackingdetails", Schema = "public")]
    public class OePackingDetails : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("oepackingdetailsid")]
        public int OePackingDetailsId { get; set; }
        [Column("packingtype")]
        public string PackingType { get; set; }
        [Column("sizerangemasterid")]
        public int SizeRangeMasterId { get; set; }
        [Column("size")]
        public string Size { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        // public date
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
