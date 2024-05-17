using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class FloorMaterial : BaseEntity
    {
        public int FloorReturnMaterialId { get; set; }
        public string FrmNo { get; set; }
        public DateTime? FloorDate { get; set; }
        public string Remarks { get; set; }
        public bool Is_IssueNo { get; set; }
        public string IssueNo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
