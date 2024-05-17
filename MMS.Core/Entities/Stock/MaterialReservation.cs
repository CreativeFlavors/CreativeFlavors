using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class MaterialReservation : BaseEntity
    {
        public int MaterialReservationId { get; set; }
        public string DocNo { get; set; }
        public string InternalOrder { get; set; }
        public string PlanWise { get; set; }
        public int MaterialCategoryId { get; set; }
        public int MaterialGroupId { get; set; }
        public int MaterialMasterId { get; set; }
        public int UomId { get; set; }
        public int ColourMasterId { get; set; }
        public string Quantity { get; set; }
        public string FreeStock { get; set; }
        public bool MaterialWise { get; set; }
        public bool Summary { get; set; }
        public string MultipleIO { get; set; }
        public string DisplayIO { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
