using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("product", Schema = "public")]
    public class product : BaseEntity
    {
        [Column("productid")]
        public int ProductId { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("productname")]
        public string ProductName { get; set; }

        [Column("productdesc")]
        public string ProductDesc { get; set; }

        [Column("taxmasterid")]
        public string TaxMasterId { get; set; }

        [Column("uommasterid")]
        public string UomMasterId { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("bomno")]
        public string BomNo { get; set; }

        //[Column("imagename")]
        //public string ImageName { get; set; }

        //[Column("imagepath")]
        //public string ImagePath { get; set; }

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

        [Column("materialcategorymasterid")]
        public string MaterialCategoryMasterId { get; set; }
    }
}
