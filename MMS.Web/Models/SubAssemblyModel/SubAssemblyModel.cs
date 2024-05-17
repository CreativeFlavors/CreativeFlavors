using MMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.SubAssemblyModel
{
    public class SubAssemblyModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int SubAssemblyTypeId { get; set; } = 0;
        public string ProductName { get; set; }
        public decimal Qty { get; set; } = 0;
        public DateTime TransactionDate { get; set; }
        public int InProgress_Qty { get; set; } = 0;
        public int Uom { get; set; } = 0;
        public decimal MinStock { get; set; } = 0;
        public decimal MaxStock { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal Cost { get; set; } = 0;
        public bool IsActive { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ProductionTime { get; set; }
        public decimal ProductionPerDay { get; set; } = 0;
        public List<SubAssemblyMaster> SubAssemblydetailslist { get; set; }
        public List<MMS.Web.Models.ProductionBOM.ProductionBOMModel> listProductionBOMModel { get; set; }
    }

}