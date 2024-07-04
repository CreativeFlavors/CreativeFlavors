using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class Material : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("materialid")]
        public int MaterialId { get; set; }

        [Column("materialcode")]
        public string MaterialCode { get; set; }

        [Column("materialname")]
        public string MaterialName { get; set; }

        [Column("materialdesc")]
        public string MaterialDescription { get; set; }

        [Column("taxmasterid")]
        public int? TaxMasterId { get; set; }

        [Column("uommasterid")]
        public int? UomMasterId { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("cost")]
        public decimal? Cost { get; set; }

        [Column("materialcategoryid")]
        public int? MaterialCategoryId { get; set; }

        [Column("storeid")]
        public int StoreId { get; set; }

        [Column("preferredsupplierid")]
        public int? PreferredSupplierId { get; set; }

        [Column("minstock")]
        public decimal? MinStock { get; set; }

        [Column("maxstock")]
        public decimal? MaxStock { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
