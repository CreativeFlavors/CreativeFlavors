using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("order_details", Schema = "public")]
    public class Order_Details : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customerdocsid")]
        public int CustomerDocsId { get; set; }
        [Column("lineid")]
        public int LineId { get; set; }
        [Column("materialid")]
        public int MaterialId { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("displayname")]
        public string DisplayName { get; set; }
        [Column("poname")]
        public string PoName { get; set; }
        [Column("latcost")]
        public decimal LatCost { get; set; }
        [Column("avecost")]
        public decimal AveCost { get; set; }
        [Column("uomidbuy")]
        public int UomIdBuy { get; set; }

        [Column("uomidsell")]
        public int UomIdSell { get; set; }

        [Column("uomfactor")]
        public string UomFactor { get; set; }

        [Column("taxtypeid")]
        public int TaxTypeId { get; set; }
        [Column("taxrate")]
        public decimal TaxRate { get; set; }
        [Column("qtyrequired")]
        public decimal QtyRequired { get; set; }
        [Column("qtyprocessed")]
        public decimal QtyProcessed { get; set; }
        [Column("qtylastprocessed")]
        public decimal QtyLastProcessed { get; set; }
        [Column("qtyreserved")]
        public decimal QtyReserved { get; set; }
        [Column("note")]
        public string Note { get; set; }
        [Column("discount")]
        public decimal Discount { get; set; }
        [Column("priceexcl")]
        public decimal PriceExcl { get; set; }
        [Column("priceincl")]
        public decimal PriceIncl { get; set; }

        [Column("linetotalexcl")]
        public decimal LineTotalExcl { get; set; }

        [Column("linetotalincl")]
        public decimal LineTotalIncl { get; set; }

        [Column("linetotaltax")]
        public decimal LineTotalTax { get; set; }

        [Column("processedlinetotalexcl")]
        public decimal ProcessedLineTotalExcl { get; set; }

        [Column("processedlinetotalincl")]
        public decimal ProcessedLineTotalIncl { get; set; }

        [Column("processedlinetotaltax")]
        public decimal ProcessedLineTotalTax { get; set; }

        [Column("foreignpriceexcl")]
        public decimal ForeignPriceExcl { get; set; }

        [Column("foreignpriceincl")]
        public decimal ForeignPriceIncl { get; set; }

        [Column("foreignlinetotalexcl")]
        public decimal ForeignLineTotalExcl { get; set; }

        [Column("foreignlinetotalincl")]
        public decimal ForeignLineTotalIncl { get; set; }

        [Column("foreignlinetotaltax")]
        public decimal ForeignLineTotalTax { get; set; }

        [Column("processedforeignlinetotalexcl")]
        public decimal ProcessedForeignLineTotalExcl { get; set; }

        [Column("processedforeignlinetotalincl")]
        public decimal ProcessedForeignLineTotalIncl { get; set; }

        [Column("processedforeignlinetotaltax")]
        public decimal ProcessedForeignLineTotalTax { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("createdby")]
        public int CreatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
