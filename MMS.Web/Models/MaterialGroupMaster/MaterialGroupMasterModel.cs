using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.MaterialGroupMaster
{
    public class MaterialGroupMasterModel
    {
        public int MaterialGroupMasterId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
        public string CateogoryCode { get; set; }
        public string SubGroup { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public bool IsSubstance { get; set; }
        public bool IsSize { get; set; }
        public bool IsColor { get; set; }
        public bool IsConsumption { get; set; }
        public bool IsInspection { get; set; }
        public bool IsReservation { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsBatchcode { get; set; }
        public bool IsMultipleUnits { get; set; }
        public bool IsLeatherType { get; set; }
        public int StoreName { get; set; }
        public string SubGroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int MaterialNameID { get; set; }

        public IEnumerable<SelectListItem> SubGroupList { get; set; }
        public IEnumerable<string> SelectedItemId { get; set; }
        
        public List<MaterialGroupMaster_> MaterialGrouplist { get; set; }
      
    }
}