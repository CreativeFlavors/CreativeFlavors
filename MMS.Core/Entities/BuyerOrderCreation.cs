using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("buyerordercreation", Schema = "public")]
    public class BuyerOrderCreation : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("buyerordercreationid")]
        public int BuyerOrderCreationID { get; set; }

        [Column("buyerid")]
        public int BuyerId { get; set; }

        // [Column("groupid")]
        // public int GroupID { get; set; }

        [Column("materialcode")]
        public string MaterialCode { get; set; }

        [Column("materialname")]
        public string MaterialName { get; set; }

        [Column("stockunit")]
        public int StockUnit { get; set; }

        [Column("sizerange")]
        public int SizeRange { get; set; }

        [Column("standardprice")]
        public decimal StandardPrice { get; set; }

        [Column("complexityfactor")]
        public int ComplexitityFactor { get; set; }

        [Column("sketchno")]
        public string SketchNo { get; set; }

        [Column("leathermainrawmaterial")]
        public int LeatherMainRawMaterial { get; set; }

        [Column("productcolor")]
        public string ProductColor { get; set; }

        [Column("buyerstyleno")]
        public string BuyerStyleNo { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deletedon")]
        public DateTime? Deletedon { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
