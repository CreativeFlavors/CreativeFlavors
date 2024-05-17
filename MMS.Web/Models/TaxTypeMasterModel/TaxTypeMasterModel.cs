using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.TaxTypeMasterModel
{
    public class TaxTypeMasterModel
    {
        public int TaxMasterID { get; set; }
        public string TaxName { get; set; }
        public string TaxMode { get; set; }
        public string TaxValue { get; set; }
        public string TaxCode { get; set; }
        public string TaxRef { get; set; }
        public string Form { get; set; }
        public string TaxOn { get; set; }
        public string CostOfTheProduct { get; set; }
        public string TaxValueMode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<TaxTypeMaster> TaxTypeMasterList { get; set; }
    }
}