using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class FloorReturnMaterialDetails : BaseEntity
    {
        public int FloorReturnMaterialDetailsId { get; set; }
        public int FloorReturnMaterialId { get; set; }         
        public int StoreMasterId { get; set; }
        public int GroupMasterId { get; set; }      
        public int MaterialMasterId { get; set; }
        public int IoNo { get; set; }
        public int Uom { get; set; }
        public string Category { get; set; }
        public decimal? Piecies { get; set; }
        public int PiecesCurrentType { get; set; }
        public string Style { get; set; }
        public decimal? ReturnQuantity { get; set; }
        public decimal? IssuedQuantity { get; set; }
        public string Rate { get; set; }
        public string LotNo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
         public int MaterialType { get; set; }
    }
}
