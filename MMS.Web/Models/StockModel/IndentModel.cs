using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Models.StockModel
{
    public class IndentModel
    {
        public int IndentId { get; set; }
        public string AmmendmentNo { get; set; }
        public int? OrderTypeID { get; set; }
        public string AmmendmentDate { get; set; }
        public string IndentType { get; set; }
        public string IndentNo { get; set; }
        public string IoNo { get; set; }
        public string SelectedIndentNO { get; set; }
        public Guid? UnitId { get; set; }
        public string Indendingfor { get; set; }
        public int? MaterialCatId { get; set; }
        public string Date { get; set; }
        public int? SeasonId { get; set; }
        public int? MaterialGrpId { get; set; }
        public int? StoreId { get; set; }
        public int? BuyerId { get; set; }
        public string RequestedBy { get; set; }
        public int? UOMId { get; set; }
        public string ReqQty { get; set; }
        public string StoreStock { get; set; }
        public int? MaterialName { get; set; }
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
        public List<Indent> IndentList { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string[] From { get; set; }
        public string[] To { get; set; }
        public int? MaterialType { get; set; }
        public string MRPNO { get; set; }
        public string MRPNO_txtValue { get; set; }
        public List<IndentMaterials> IndentMaterialsList { get; set; }
        public List<MMS.Web.Models.SPBomMaterialList> MRPbomMateriallist { get; set; }
        public List<Indent> IndentMateriallist { get; set; }
        public int BOMMaterialID { get; set; }
        public int BOMID { get; set; }
        public List<SimpleMRP> MRPList { get; set; }
        public int IndentMaterialID { get; set; }
        public int? IssueType { get; set; }
        public string OrderType { get; set; }
        public string SizeRangeDetails { get; set; }
        public int? SizeRangeID { get; set; }
        public string SizeRangeName { get; set; }
        public int? SubstanceMasterID { get; set; }
        public string SubstanceRangeName { get; set; }
        public List<PurchaseOrderSizeRangeQuantity> PurchaseOrderSizeRangeQuantityList { get; set; }
        public List<PurchaseOrderDelierySchedule> PurchaseOrderDelierySchedulelist { get; set; }
        public List<DelieryScheduleObject> DelieryScheduleObjectlist { get; set; }
        //public string MaterialName { get; set; }
        //public string MaterialCategoryName { get; set; }
        //public string MaterialGroupName { get; set; }
        //public string ColorName { get; set; }
        //public string UOMName { get; set; }
        //public string SupplierName { get; set; }                                                             
        //public List<string> ListOfIONo { get; set; }
        //public List<int> From { get; set; }        
        //public int[] AvailableSelected { get; set; }
        //public int[] RequestedSelected { get; set; }
        public string MRPSelectedValues { get; set; }



    }
}