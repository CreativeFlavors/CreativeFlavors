using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class FloorReturnMaterialGrid
    {
        public int FloorReturnMaterialId { get; set; }
        public int FloorReturnMaterialDetailsId { get; set; }
        public string FrmNo { get; set; }
        public string FloorDate { get; set; }
      
        public string Remarks { get; set; }
        public bool Is_IssueNo { get; set; }
        public int StoreMasterId { get; set; }
        public int GroupMasterId { get; set; }
        public string IssueNo { get; set; }
        public int MaterialMasterId { get; set; }
        public int IoNo { get; set; }
        public decimal Piecies { get; set; }
        public int Uom { get; set; }
        public string Style { get; set; }
        public string Category { get; set; }

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
        
        public string Size { get; set; }
        public string SizeId { get; set; }
        public string Quantity { get; set; }
        public string ReturnTotalQuantity { get; set; }
      
        public string LongUnitName { get; set; }
        public string StoreName { get; set; }
        public string GroupName { get; set; }
        public string MaterialName { get; set; }
        public int MaterialType { get; set; }
        public List<FloorReturnMaterialGrid> floormaterialgridlist { get; set; }        
        public List<FloorReturnMaterialDetailsModel> floorReturnMaterialDetails { get; set; }
        public List<SizeItemsIssueSlip> sizeItemsIssueSlipList { get; set; }
        public List<FloorMaterial> floorReturnMaterialList { get; set; }
        public List<FloorReturnSizeRange> floorReturnSizeRange { get; set; }

    }
    public class FloorReturnMaterial
    {
        public string Style { get; set; }
        public int? Groupid { get; set; }
        public int? StoreMasterId { get; set; }
      
        public string Groupname { get; set; }
        public string Lotno { get; set; }
        public string IssueSlipNo { get; set; }
        public string StoreName { get; set; }
        public int? Uom { get; set; }
        public decimal? Piecies { get; set; }
        public decimal Rate { get; set; }
        public string IoNo { get; set; }
        public string LongUnitName { get; set; }
        public int? Category { get; set; }
        public int? Materialmasterid { get; set; }
        public string materialdescription { get; set; }      
        public int? IssueSlipID { get; set; }
        public decimal? Quantity { get; set; }
        public int MaterialType { get; set; }
    }

    public class FloorReturnMaterial_Withoutissue_No
    {
        public string Style { get; set; }
        public int? Groupid { get; set; }
        public int? StoreMasterId { get; set; }

        public string Groupname { get; set; }
        public string Lotno { get; set; }
        public string IssueSlipNo { get; set; }
        public string StoreName { get; set; }
        public int? Uom { get; set; }
        public decimal? Piecies { get; set; }
        public decimal Rate { get; set; }
        public string IoNo { get; set; }
        public string LongUnitName { get; set; }
        public int? Category { get; set; }
        public int? Materialmasterid { get; set; }
        public string materialdescription { get; set; }
        public int? IssueSlipID { get; set; }
        public decimal? Quantity { get; set; }
        public int MaterialType { get; set; }
    }
}
