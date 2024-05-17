using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class BomGridDetailsModel
    {

        public int BomGridId { get; set; }
        public List<int> Sno { get; set; }
        public int BomId { get; set; }
        public List<string> Component { get; set; }
        public List<string> Length { get; set; }
        public List<string> Width { get; set; }
        public List<int> Nos { get; set; }
        public List<decimal?> SampleNormsGrid { get; set; }
        public List<int> WastagePercent { get; set; }
        public List<decimal?> WastageQtyGrid { get; set; }

        public bool? IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime DeletedDate { get; set; }


    }
    public class GridModel
    {
        public int BomGridId { get; set; }
        public int BOMMaterialID { get; set; }
        public int Sno { get; set; }
        public int BomId { get; set; }
        public string Component { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public int Nos { get; set; }
        public decimal? SampleNorms { get; set; }
        public int WastagePercent { get; set; }
        public decimal? WastageQtyGrid { get; set; }
        public decimal? TotalNormsQty { get; set; }
        public bool IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}