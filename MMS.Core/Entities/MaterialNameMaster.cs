using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class MaterialNameMaster : BaseEntity
    {
        public int MaterialMasterID { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string LeatherMaterilFirstValue { get; set; }
        public int? LeatherMaterialName { get; set; }
        public string LeatherMaterilLastValue { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
