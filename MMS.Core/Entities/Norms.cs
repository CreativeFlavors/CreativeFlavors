using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("norms", Schema = "public")]
    public class Norms : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("normsid")]
        public int Normsid { get; set; }

        [Column("groupid")]
        public int GroupId { get; set; }

        [Column("buyeroption")]
        public int BuyerOption { get; set; }

        [Column("ournorms")]
        public int OurNorms { get; set; }

        [Column("buyernameid")]
        public int BuyerNameid { get; set; }

        [Column("purchasenormsid")]
        public int PurchaseNormsid { get; set; }

        [Column("issuenormsid")]
        public int IssueNormsid { get; set; }

        [Column("costingnorms")]
        public int CostingNorms { get; set; }

        [Column("normspercentage")]
        public decimal? NormsPercentage { get; set; }

        [Column("normspercentage1")]
        public decimal? NormsPercentage1 { get; set; }

        [Column("normspercentage2")]
        public decimal? NormsPercentage2 { get; set; }

        [Column("normspercentage3")]
        public decimal? NormsPercentage3 { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
