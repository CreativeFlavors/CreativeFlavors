using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class Job_ApprovedPriceMaster : BaseEntity
    {
        public int Jw_ApprovedPriceID { get; set; }

        public DateTime? Date { get; set; }

        public int JW_Name { get; set; }

        public int Process_Name { get; set; }

        public int Stage_From { get; set; }

        public int Stage_To { get; set; }
    
        public decimal? Rate_For_JW { get; set; }

        public int Rate_For_JW_id { get; set; }

        public int Tax_Details { get; set; }
        public string Lead_Time_Days { get; set; }
        public string Job_ExcessPercentage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public int? Finished_Material { get; set; }
        public int? Issue_Type { get; set; }
        public DateTime? DeletedDate { get; set; }

        public int? Product_BuyerStyle { get; set; }
        public decimal? GSt { get; set; }
    }
}
