using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.ProductionBOM
{
    public class ProductionBOMModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("materialid")]
        public int MaterialId { get; set; }
        [Column("bomproductcode")]
        public string BOMProductCode { get; set; }
        [Column("bomproductid")]
        public int BOMProductId { get; set; }
        [Column("bomproductname")]
        public string BOMProductName { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterid { get; set; }
        [Column("producttype")]
        public int ProductType { get; set; }
        [Column("uomidmaster")]
        public int UomIdMaster { get; set; }
        [Column("bomid")]
        public int Bomid { get; set; }
        [Column("qty")]
        public decimal Qty { get; set; }
        [Column("consumeunitid")]
        public decimal ConsumeunitId { get; set; }
        [Column("description")]
        public string Description { get; set; }
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
        [NotMapped]
        public string materiallist { get; set; }
        [NotMapped]
        public string consumelist { get; set; }
        [Column("process")]
        public int Process { get; set; }
        [Column("subAssemblyid")]
        public int SubAssemblyId { get; set; }
        public List<ProductionBOMModel> listProductionBOMModel { get; set; }
    }
}