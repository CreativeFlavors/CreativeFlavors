using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class ProductionJobwork_Code_Master: BaseEntity
    {
        public int ProductionJobwork_code_id { get; set; }

        public string ProductionJobwork_Code { get; set; }

        public bool Leather_Pairs { get; set; }

        public bool component_Pairs { get; set; }

        public bool Upper_Fullshoes { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public bool Production_Type { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
