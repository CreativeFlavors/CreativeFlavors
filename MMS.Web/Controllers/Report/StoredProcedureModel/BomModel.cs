using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Controllers.Report.StoredProcedureModel
{
    [Table("bommodel", Schema = "public")]
    public class BomModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bomid")]
        public int BomId { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("buyerfullname")]
        public string BuyerFullName { get; set; }
        [Column("meansize")]
        public string MeanSize { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription  { get; set; }
        [Column("shortunitname")]
        public string ShortUnitName { get; set; }
        [Column("totalnorms")]
        public string TotalNorms { get; set; }
        [Column("noofsets")]
        public decimal NoOfSets { get; set; }
        [Column("sizerangename")]
        public string SizeRangeName { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("lotno")]
        public string LotNo { get; set; }
        [Column("seasonname")]
        public string SeasonName { get; set; }

    }
}