using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("indent", Schema = "public")]
    public class Indent : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("indentid")]
        public int IndentId { get; set; }

        [Column("ammendmentno")]
        public string AmmendmentNo { get; set; }

        [Column("ammendmentdate")]
        public DateTime? AmmendmentDate { get; set; }

        [Column("indenttype")]
        public string IndentType { get; set; }

        [Column("indentno")]
        public string IndentNo { get; set; }

        [Column("iono")]
        public string IoNo { get; set; }

        [Column("selectedindentno")]
        public string SelectedIndentNO { get; set; }

        [Column("unitid")]
        public Guid? UnitId { get; set; }

        [Column("indendingfor")]
        public string Indendingfor { get; set; }

        [Column("materialcatid")]
        public int? MaterialCatId { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }

        [Column("seasonid")]
        public int? SeasonId { get; set; }

        [Column("materialgrpid")]
        public int? MaterialGrpId { get; set; }

        [Column("storeid")]
        public int? StoreId { get; set; }

        [Column("buyerid")]
        public int? BuyerId { get; set; }

        [Column("requestedby")]
        public string RequestedBy { get; set; }

        [Column("uomid")]
        public int? UOMId { get; set; }

        [Column("reqqty")]
        public string ReqQty { get; set; }

        [Column("storestock")]
        public string StoreStock { get; set; }

        [Column("materialid")]
        public int? MaterialId { get; set; }

        [Column("rate")]
        public string Rate { get; set; }

        [Column("intendqty")]
        public string IntendQty { get; set; }

        [Column("freestock")]
        public string FreeStock { get; set; }

        [Column("colourid")]
        public int? ColourId { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("supplierid")]
        public int? SupplierId { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("preparedby")]
        public string PreparedBy { get; set; }

        [Column("checkedby")]
        public string CheckedBy { get; set; }

        [Column("approvedby")]
        public string ApprovedBy { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("mrpno")]
        public string MRPNO { get; set; }

        [Column("ordertypeid")]
        public int? OrderTypeID { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
