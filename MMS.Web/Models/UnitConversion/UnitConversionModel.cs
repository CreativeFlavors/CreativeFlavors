using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.UnitConversion
{
    public class UnitConversionModel
    {
        public int UnitConversionId { get; set; }
        public int UOMUnitmasteID { get; set; }
        public int Category { get; set; }
        public int UcGroup { get; set; }
        public int Material { get; set; }
        public decimal ShortUnitNameValue { get; set; }
        public decimal LongUnitNameValue { get; set; }
        public int ShortUnitID { get; set; }
        public int LongUnitValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<UnitConversation> UnitConversionList { get; set; }
    }
}