using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class LeatherShoesGradeMaster : BaseEntity
    {
        public int LeatherShoesGradeMasterId { get; set; }
        public string GradeCode { get; set; }
        public string Grade { get; set; }      
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
