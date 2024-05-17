using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class JobOtherWorkList
    {
        public int OtherJobWork_Id { get; set; }
        public string OtherJobWork_Code { get; set; }
        public string JobWorkName { get; set; }
        public string ProcessName { get; set; }
        public string MachineName { get; set; }        
        public int Quantity { get; set; }        
    }
}
