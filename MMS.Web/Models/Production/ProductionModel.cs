using MMS.Core.Entities;
using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Production
{
    public class ProductionModel
    {
        public int ProductionId { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string ProductionCode { get; set; }
        public decimal ProductionQty { get; set; }
        public int ProductionStatus { get; set; }
        public int ProductId { get; set; }
        public decimal MinQty { get; set; }
        public decimal MaxQty { get; set; }
        public decimal RequiredQty { get; set; }
        public int StoreCode { get; set; }
        public DateTime? ProductionDueDate { get; set; }
        public DateTime? ProductionFullfillDate { get; set; }
        public decimal RefDocNo { get; set; }
        public DateTime? RefDocDate { get; set; }
        public string Status1Code { get; set; }
        public DateTime? Status1Date { get; set; }
        public string Status1By { get; set; }
        public string Status2Code { get; set; }
        public DateTime? Status2Date { get; set; }
        public string Status2By { get; set; }
        public string Status3Code { get; set; }
        public DateTime? Status3Date { get; set; }
        public string Status3By { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool Productions { get; set; }
        public bool SubAssembly { get; set; }
        public bool Inprogress { get; set; }
        public decimal? ProductionPerDay { get; set; }
        public string ProductCode { get; set; }
        public product product { get; set; }
        

    }
}