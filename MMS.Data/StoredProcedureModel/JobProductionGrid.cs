using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class JobProductionGrid
    {
        public int Production_Order_id { get; set; }
        public int Prod_code_id { get; set; }
        public string  ProductionJobwork_Code { get; set;}
        public string Job_Name { get; set; }

        public string ProcessName { get; set; }

        public string StageFrom { get; set; }

        public string StageTo { get; set; }

        public int Lot_no { get; set; }
        public bool Production_Type { get; set; }

    }
}
