using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("oeshipmentdetails", Schema = "public")]
    public class OeShipmentDetails : BaseEntity
    {
        [Column("oeshipmentdetailsid")]
        public int OeShipmentDetailsId { get; set; }
        [Column("countrystamp")]
        public string CountryStamp { get; set; }
        [Column("shipmentfrom")]
        public string ShipmentFrom { get; set; }
        [Column("shipmentto")]
        public string ShipmentTo { get; set; }
        [Column("otherinstruction")]
        public string OtherInstruction { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
