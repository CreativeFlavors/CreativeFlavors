using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMS.Web.Models.SimpleMRPModel
{
    [Table("simplemrpmodel", Schema = "public")]
    public class SimpleMRPModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("simplemrpid")]
        public int SimpleMRPID { get; set; }
        [Remote("IsMRPNo", "SimpleMRP", ErrorMessage = "MRPNo already in use.")]
        [Column("mrpno")]
        public string MRPNO { get; set; }
        [Column("mrpcode")]
        public string MRPCode { get; set; }
        [Column("mrpdate")]
        public string MRPDate { get; set; }
        [Column("mrptype")]
        public int? MRPType { get; set; }
        [Column("buyernameid")]
        public int? BuyerNameid { get; set; }
        [Column("weekno")]
        public int WeekNO { get; set; }
        [Column("seasonid")]
        public int? SeasonID { get; set; }
        [Column("lotno")]
        public int LotNO { get; set; }
        [Column("sizerangeid")]
        public int? SizeRangeID { get; set; }
        [Column("from")]
        public string From { get; set; }
        [Column("to")]
        public string TO { get; set; }
        [Column("customerplan")]
        public int? CustomerPlan { get; set; }
        [Column("productionplanbasic")]
        public bool ProductionPlanBasic { get; set; }
        [Column("orderbasic")]
        public bool OrderBasic { get; set; }
        [Column("jobwork")]
        public bool JobWork { get; set; }
        [Column("rejectionorreplacement")]
        public bool RejectionorReplacement { get; set; }
        [Column("overconsumption")]
        public bool OverConsumption { get; set; }
        [Column("material")]
        public int Material { get; set; }
        [Column("req_qty")]
        public int? Req_Qty { get; set; }
        [Column("uom")]
        public int? UOM { get; set; }
        [Column("rate")]
        public decimal? Rate { get; set; }
        [Column("totalnorms")]
        public decimal? TotalNorms { get; set; }
        [Column("detailed")]
        public bool Detailed { get; set; }
        [Column("consolidate")]
        public bool Consolidate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("isexit")]
        public bool IsExit { get; set; }
        public List<SimpleMRP> simpleMRPList { get; set; }
        public List<SimpleMRPModel> importIOList { get; set; }
        public IEnumerable<string> SelectedCities { get; set; }
        public IEnumerable<string> MoveSelectedIOS { get; set; }
        public IEnumerable<SelectListItem> IOS { get; set; }
        public IEnumerable<SelectListItem> selectlist { get; set; }
        [Column("totalordercount")]
        public int? TotalOrderCount { get; set; }
    }
}