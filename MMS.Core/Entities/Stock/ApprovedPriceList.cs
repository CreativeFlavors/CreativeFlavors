using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("approvedpricelist", Schema = "public")]
    public class ApprovedPriceList : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("approvedpriceid")]
        public int ApprovedPriceID { get; set; }
        [Column("supplierid")]
        public int SupplierId { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("pricers")]
        public decimal? PriceRs { get; set; }
        [Column("uom")]
        public int Uom { get; set; }
        [Column("categoryid")]
        public int CategoryID { get; set; }
        [Column("taxdetails")]
        public int TaxDetails { get; set; }
        [Column("groupid")]
        public int GroupID { get; set; }
        [Column("currencyid")]
        public int CurrencyID { get; set; }
        [Column("materialid")]
        public int MaterialID { get; set; }
        [Column("colorid")]
        public int ColorID { get; set; }
        [Column("leadtime")]
        public string LeadTime { get; set; }
        [Column("mrpmargin")]
        public string MRPMargin { get; set; }
        [Column("mrpprice")]
        public decimal? MRPPrice { get; set; }
        [Column("accessiblevalue")]
        public decimal? AccessibleValue { get; set; }
        [Column("supplierdescription")]
        public string SupplierDescription { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("poexcesspercentage")]
        public string POExcessPercentage { get; set; }
        [Column("isapproved")]
        public bool IsApproved { get; set; }
        [Column("approvedby")]
        public string ApprovedBy { get; set; }

    }
}
