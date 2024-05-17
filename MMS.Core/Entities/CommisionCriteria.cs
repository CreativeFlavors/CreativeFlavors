using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class CommisionCriteria:BaseEntity
    {
        public int CommisionCriteriaId { get; set; }
        public string CommisionName { get; set; }
        public string Criteria { get; set; }
        public int Value { get; set; }
        public int CommisionPercent { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
