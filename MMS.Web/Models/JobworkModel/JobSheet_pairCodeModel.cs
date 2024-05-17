using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.JobWork;

namespace MMS.Web.Models.JobworkModel
{
    public class JobSheet_pairCodeModel
    {
        public int jobsheet_pair_code_id { get; set; }

        public string jobsheet_pair_Code { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public List<JobSheet_pairCodeMaster> JobSheet_pairCodeMasterlist { get; set; }
    }
}