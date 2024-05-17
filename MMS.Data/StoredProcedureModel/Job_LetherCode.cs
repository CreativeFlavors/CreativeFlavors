using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public  class Job_LetherCode
    {
        public string Job_Lether_to_lether_Code { get; set; }

        public string FINISHED_M { get; set; }

        public string MaterialDescription { get; set; }

        public string Finished_MAterial { get; set; }
        public decimal qty { get; set; }

        public string StoreName { get; set; }

        public string Date_from { get; set; }
        // [NotMapped]
        public DateTime Delivery_Date { get; set; }

        public string CategoryName { get; set; }

        public int Material { get; set; }
        
        public int Job_Lether_to_lether_id { get; set; }
        public int issue_Material { get; set; }

      
        public decimal Value { get; set; }
    }
}
