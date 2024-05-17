using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.BasicUnitMasterModel
{
    public class BasicUnitMasterModel
    {
        public int BasicUnitMasterID { get; set; }
        public int CategoryId { get; set; }
        public int GroupID { get; set; }
        public int MaterialID { get; set; }
        public int ShortUnitValue { get; set; }
        public int ShortUnitID { get; set; }
        public int LongUnitValue { get; set; }
        public int LongUnitID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UomMasterId { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string ShortUnitName { get; set; }
        public string LongUnitName { get; set; }
        public List<UomMaster> BasicUnitMasterList { get; set; }

        //public int UomMasterId { get; set; }
        //public string ShortUnitName { get; set; }
        //public string LongUnitName { get; set; }
        //public int Material { get; set; }
        //public int UOMUnitmasteID { get; set; }
        //public int BUGroup { get; set; }
        //public int Category { get; set; }
        //public decimal ShortUnitNameValue { get; set; }
        //public decimal LongUnitNameValue { get; set; }
        //public int UnitConversionId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public List<UomMaster> UomMasterList { get; set; }
        //public List<UnitConversation> UnitConversationList { get; set; }

    }
}