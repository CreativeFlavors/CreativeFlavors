using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class MaterialOpeningModel
    {
        public int MaterialOpeningId { get; set; }
        public string Store { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int? MaterialName { get; set; }
        public int? ColorMasterId { get; set; }
        public string Date { get; set; }
        public int ColorMasterId_ { get; set; }
        public int PrimaryUomMasterId_ { get; set; }
        public int SecondaryUomMasterId_ { get; set; }
        public int PrimaryUomMasterId { get; set; }
        public int SecondaryUomMasterId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public int? MaterialPcs { get; set; }
        public int? QtyPcs { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? MaterialType { get; set; }
        //public int UOM { get; set; }
        //public decimal? UOMValue { get; set; }
        //public string UOMType { get; set; }
        //public int SizeQuantity { get; set; }
        //public decimal SizeRate { get; set; }
        //public string Size { get; set; }
        public List<MaterialOpeningSizeQtyRate> sizeRangeDetailslist { get; set; }
        public List<MaterialOpeningMaster> MaterialOpeningMasterList { get; set; }

        public string SizeQuantityRate { get; set; }
        public int? SizeRangeMasterId { get; set; }

        public string MaterialCode { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }
        public string SizeRange { get; set; }
        public string Quantity { get; set; }
        public string Rates { get; set; }
        public int MaterialMasterId { get; set; }
        public StoreMaster storeMaster { get; set; }
    }

    public class MaterialNamePopupModel
    {
        public int MaterialMasterId { get; set; }
        public string MaterialDescription { get; set; }
    }


}