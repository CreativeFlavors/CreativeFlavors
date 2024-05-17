using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.ColorMasterModel
{
    public class ColorMasterModel
    {
        public int ColorMasterId { get; set; }
        public string Color { get; set; }
        public string BuyerColorCode { get; set; }
        public byte[] ColorImage { get; set; }
        public string ColorImages { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<ColorMaster> ColorMasterList { get; set; }
    }
}