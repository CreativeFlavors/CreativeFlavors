using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace MMS.Web.Models.SimpleMRPModel
{
    public class SimpleMRPModel
    {
        public int SimpleMRPID { get; set; }

        [Remote("IsMRPNo", "SimpleMRP", ErrorMessage = "MRPNo already in use.")]
        public string MRPNO { get; set; }
        public string MRPCode { get; set; }
        public string MRPDate { get; set; }
        public int? MRPType { get; set; }
        public int? BuyerNameid { get; set; }
        public int WeekNO { get; set; }
        public int? SeasonID { get; set; }
        public int LotNO { get; set; }
        public int? SizeRangeID { get; set; }
        public string From { get; set; }
        public string TO { get; set; }
        public int? CustomerPlan { get; set; }
        public bool ProductionPlanBasic { get; set; }
        public bool OrderBasic { get; set; }
        public bool JobWork { get; set; }
        public bool RejectionorReplacement { get; set; }
        public bool OverConsumption { get; set; }
        public int Material { get; set; }
        public int? Req_Qty { get; set; }
        public int? UOM { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalNorms { get; set; }
        public bool Detailed { get; set; }
        public bool Consolidate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsExit { get; set; }
        public List<SimpleMRP> simpleMRPList { get; set; }
        public List<SimpleMRPModel> importIOList { get; set; }
        public IEnumerable<string> SelectedCities { get; set; }
        public IEnumerable<string> MoveSelectedIOS { get; set; }
        public IEnumerable<SelectListItem> IOS { get; set; }
        public IEnumerable<SelectListItem> selectlist { get; set; }
        public int? TotalOrderCount { get; set; }
    }
}