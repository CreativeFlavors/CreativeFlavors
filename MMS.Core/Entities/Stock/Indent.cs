using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class Indent : BaseEntity
    {
        public int IndentId { get; set; }
        public string AmmendmentNo { get; set; }
        public DateTime? AmmendmentDate { get; set; }
        public string IndentType { get; set; }
        public string IndentNo { get; set; }
        public string IoNo { get; set; }
        public string SelectedIndentNO { get; set; }
        public Guid? UnitId { get; set; }
        public string Indendingfor { get; set; }
        public int? MaterialCatId { get; set; }
        public DateTime? Date { get; set; }
          public int? MaterialType { get; set; }
        public int? SeasonId { get; set; }
        public int? MaterialGrpId { get; set; }
        public int? StoreId { get; set; }
        public int? BuyerId { get; set; }
        public string RequestedBy { get; set; }
        public int? UOMId { get; set; }
        public string ReqQty { get; set; }
        public string StoreStock { get; set; }
        public int? MaterialId { get; set; }
        public string Rate { get; set; }
        public string IntendQty { get; set; }
        public string FreeStock { get; set; }
        public int? ColourId { get; set; }
        public string Value { get; set; }
        public int? SupplierId { get; set; }
        public string Remarks { get; set; }
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string MRPNO { get; set; }
        public int? OrderTypeID { get; set; }
    }
}
