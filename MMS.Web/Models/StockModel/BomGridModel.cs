using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class BomGridModel
    {
        public int BomGridId { get; set; }
        public int Sno { get; set; }
        public int BomId { get; set; }
        public string Component { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public int Nos { get; set; }
        public decimal? SampleNormsGrid { get; set; }
        public int WastagePercent { get; set; }
        public decimal? WastageQtyGrid { get; set; }

        public bool? IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}