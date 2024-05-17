using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class FloorReturnMaterialModel
    {
        public int FloorReturnMaterialId { get; set; }
        public string FrmNo { get; set; }
        public DateTime? FloorDate { get; set; }
        public string Remarks { get; set; }
        public int StoreMasterId { get; set; }
        public int GroupMasterId { get; set; }
        public string IssueNo { get; set; }
        public int MaterialMasterId { get; set; }
        public int IoNo { get; set; }
        public int Uom { get; set; }
        public string Style { get; set; }
        public string  Category { get; set; }
        public decimal Piecies { get; set; }
        public int PiecesCurrentType { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public string Rate { get; set; }
        public string LotNo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<FloorMaterial> FloorMaterialList { get; set; }
    }
}