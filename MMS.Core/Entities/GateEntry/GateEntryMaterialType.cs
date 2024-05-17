using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Core.Entities.GateEntry
{
    public class GEMaterialTypeMaster : BaseEntity
    {
        public int GEMaterialTypeId { get; set; }
        public string MaterialType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}