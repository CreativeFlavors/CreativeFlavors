using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("mrprequirement", Schema = "public")]
    public class MRPRequirement : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("mrprequirementid")]
        public int MRPRequirementID { get; set; }

        [Column("bomid")]
        public int BOMID { get; set; }

        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }

        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }

        [Column("substancemasterid")]
        public int SubstanceMasterId { get; set; }

        [Column("materialname")]
        public int MaterialName { get; set; }

        [Column("colormasterid")]
        public int ColorMasterId { get; set; }

        [Column("samplenorms")]
        public string SampleNorms { get; set; }

        [Column("wastage")]
        public decimal? Wastage { get; set; }

        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }

        [Column("wastageqtyuom")]
        public int WastageQtyUOM { get; set; }

        [Column("totalnorms")]
        public string TotalNorms { get; set; }

        [Column("totalnormsuom")]
        public int TotalNormsUOM { get; set; }

        [Column("suppliermasterid")]
        public int? SupplierMasterID { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

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

        [Column("sizeschedulemasterid")]
        public int? SizeScheduleMasterId { get; set; }

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
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
