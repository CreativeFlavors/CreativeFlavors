using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("billofmaterialgrid", Schema = "public")]
    public class BillOfMaterialGrid : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bomid")]
        public int BomId { get; set; }

        [Column("bomno")]
        public string BomNo { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }

        [Column("buyermodel")]
        public string BuyerModel { get; set; }

        [Column("meansize")]
        public string MeanSize { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("parentbomno")]
        public string ParentBomNo { get; set; }

        [Column("lastbomnoentered")]
        public string LastBomNoEntered { get; set; }

        [Column("linkbomno")]
        public string LinkBomNo { get; set; }

        [Column("ammendment")]
        public string Ammendment { get; set; }

        [Column("commonbomno")]
        public string CommonBomNo { get; set; }

        [Column("preparedby")]
        public string PreparedBy { get; set; }

        [Column("verifiedby")]
        public string VerifiedBy { get; set; }

        [Column("checkedby")]
        public string CheckedBy { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }

        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }

        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }

        [Column("colormasterid")]
        public int ColorMasterId { get; set; }

        [Column("substancemasterid")]
        public int SubstanceMasterId { get; set; }

        [Column("samplenorms")]
        public string SampleNorms { get; set; }

        [Column("wastage")]
        public decimal? Wastage { get; set; }

        [Column("suppliermasterid")]
        public int SupplierMasterId { get; set; }

        [Column("uommasterid")]
        public int UomMasterId { get; set; }

        [Column("sizerangemasterid")]
        public int SizeRangeMasterId { get; set; }

        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }

        [Column("wastageqtyuom")]
        public int WastageQtyUOM { get; set; }

        [Column("totalnorms")]
        public decimal? TotalNorms { get; set; }

        [Column("totalnormsuom")]
        public int TotalNormsUOM { get; set; }

        [Column("componentname")]
        public int ComponentName { get; set; }

        [Column("noofsets")]
        public int NoOfSets3 { get; set; }

        [Column("buyernorms")]
        public decimal? BuyerNorms { get; set; }

        [Column("ournorms")]
        public int? OurNorms { get; set; }

        [Column("ournormspercent")]
        public decimal? OurNormsPercent { get; set; }

        [Column("purchasenorms")]
        public int PurchaseNorms { get; set; }

        [Column("purchasenormspercent")]
        public decimal? PurchaseNormsPercent { get; set; }

        [Column("issuenorms")]
        public int IssueNorms { get; set; }

        [Column("issuenormspercent")]
        public decimal? IssueNormsPercent { get; set; }

        [Column("costingnorms")]
        public int CostingNorms { get; set; }

        [Column("costingnormspercent")]
        public decimal? CostingNormsPercent { get; set; }
    }
}
