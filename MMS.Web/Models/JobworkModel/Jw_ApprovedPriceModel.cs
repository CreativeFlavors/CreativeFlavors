using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.JobWork;
using MMS.Data.StoredProcedureModel;


namespace MMS.Web.Models.JobworkModel
{
    public class Jw_ApprovedPriceModel
    {
        public int Jw_ApprovedPriceID { get; set; }

        public DateTime? Date { get; set; }

        public int JW_Name { get; set; }

        public int Process_Name { get; set; }

        public int Stage_From { get; set; }

        public int Stage_To { get; set; }

        public string Job_Name { get; set; }
        
        public string ProcessName { get; set; }

        public string StageFrom { get; set; }

        public string StageTo { get; set; }


        public decimal? Rate_For_JW { get; set; }

        public int Rate_For_JW_id { get; set; }

        public int Tax_Details { get; set; }
        public string Lead_Time_Days { get; set; }
        public string Job_ExcessPercentage { get; set; }
        public int? Issue_Type { get; set; }

        public int? Finished_Material { get; set; }

        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int? Product_BuyerStyle { get; set; }
        public decimal? GSt { get; set; }
        public List<Jw_Approvedpricelist> Job_ApprovedPriceList { get; set; }
        public List<Job_ApprovedPriceMaster> Job_ApprovedPriceList1 { get; set; }
    }
}