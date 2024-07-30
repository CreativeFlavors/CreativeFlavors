using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("suppliermaterial", Schema = "public")]
    public class SupplierMaterial : BaseEntity
    {
        [Key]
        [Column("suppliermaterialid")]
        public int SupplierMaterialId { get; set; }

        [Column("supplierid")]
        public int SupplierId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("uomid")]
        public int UomId { get; set; } 
        [Column("categoryid")]
        public int Categoryid { get; set; }

        [Column("taxid")]
        public int TaxId { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }
    }
}
