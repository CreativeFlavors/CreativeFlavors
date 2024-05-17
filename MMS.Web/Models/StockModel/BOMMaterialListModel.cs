using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class BOMMaterialListModel
    {
        public int MaterilBomID { get; set; }
        public int BomID { get; set; }
        public string BomNo { get; set; }
        public DateTime? Date { get; set; }
        public string ParentBomNo { get; set; }
        public string CommnBOM1 { get; set; }
        public string CommnBOM2 { get; set; }
        public string CommnBOM3 { get; set; }
        public string CommnBOM4 { get; set; }
        public string CommnBOM5 { get; set; }
        public int MaterialMasterId { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public string SampleNorms { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int ColorMasterId { get; set; }
        public decimal Wastage { get; set; }
        public decimal WastageQty { get; set; }
        public int WastageQtyUOM { get; set; }
        public int SubstanceMasterId { get; set; }
        public decimal TotalNorms { get; set; }
        public int TotalNormsUOM { get; set; }
        public decimal? NoOfSet { get; set; }
        public int? MaterialSuppliedBY { get; set; }
        public int? SubtanceMasterID { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [Column("suppliermasterid")]
        public int? SupplierMasterID { get; set; }

        [Column("sno")]
        public string SNO { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deletedon")]
        public DateTime? Deletedon { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }

        [Column("ismrp")]
        public bool IsMRP { get; set; }

        [Column("simplemrpid")]
        public int? SimpleMRPID { get; set; }
        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("sizerangemasterid")]
        public int SizeRangeMasterID { get; set; }

        [Column("buyernorms")]
        public string BuyerNorms { get; set; }

        [Column("noofsets")]
        public decimal? NoofSets { get; set; }

        [Column("orderno")]
        public string OrderNo { get; set; }

        [Column("conversion")]
        public string Conversion { get; set; }

        [Column("parentbomid")]
        public int? ParentBOMID { get; set; }

        [Column("parentcommonbomid")]
        public int? ParentCommonBOMID { get; set; }

        [Column("ournorms")]
        public int? OurNorms { get; set; }

        [Column("ournormspercent")]
        public decimal? OurNormsPercent { get; set; }

        [Column("purchasenorms")]
        public int? PurchaseNorms { get; set; }

        [Column("purchasenormspercent")]
        public decimal? PurchaseNormsPercent { get; set; }

        [Column("issuenorms")]
        public int? IssueNorms { get; set; }

        [Column("issuenormspercent")]
        public decimal? IssueNormsPercent { get; set; }

        [Column("costingnorms")]
        public int? CostingNorms { get; set; }

        [Column("costingnormspercent")]
        public decimal? CostingNormsPercent { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        public MaterialOpeningModel MaterialOpeningMaster { get; set; }

    }
}