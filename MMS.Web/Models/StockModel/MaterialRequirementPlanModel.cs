﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Models.StockModel
{
    public class MaterialRequirementPlanModel
    {
        public int MaterialRequirementPlanId { get; set; }
        public bool? IsProductionPlanBasis { get; set; }
        public bool? IsOrderBasis { get; set; }
        public bool? IsRejectionOrReplacement { get; set; }
        public bool? IsOverConsumption { get; set; }
        public string MrpNo { get; set; }
        public int Buyer { get; set; }
        public int SizeRangeMasterId { get; set; }
        public string Date { get; set; }
        public string MrpType { get; set; }
        public string WeekNo { get; set; }
        public int SeasonMasterId { get; set; }
        public string LotNo { get; set; }
        public string FROM { get; set; }
        public string TO { get; set; }
        public string CustPlan { get; set; }
        public int ListOfCategory { get; set; }
        public int ListOfGroup { get; set; }
        public bool? IsCritical { get; set; }
        public bool? IsCustomerSupplied { get; set; }
        public bool? IsGeneral { get; set; }
        public bool? IsImported { get; set; }
        public bool? IsReOrder { get; set; }
        public bool? IsSelectAll { get; set; }
        public bool? IsBalToOrder { get; set; }
        public int MaterialList { get; set; }
        public string TotalNoOfIos { get; set; }
        public string TotalNoOfPrs { get; set; }
        public string ETA { get; set; }
        public decimal? Weight { get; set; }
        public decimal? ShinkagePercent { get; set; }
        public decimal? WastagePercent { get; set; }
        public decimal? ShortagePercent { get; set; }
        public int Category { get; set; }
        public int Material { get; set; }
        public string BomQty { get; set; }
        public string ConversionTable { get; set; }
        public decimal? Wastage { get; set; }
        public bool? IsDetailed { get; set; }
        public bool? IsConsolidated { get; set; }
        public string MultipleIO { get; set; }
        public string DisplayIO { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsConversionInhouse { get; set; }

        public List<int> FromGrid { get; set; }
        public List<int> ToGrid { get; set; }

        public List<MaterialRequirementPlanning> materialRequirementPlanningList { get; set; }

    }
}