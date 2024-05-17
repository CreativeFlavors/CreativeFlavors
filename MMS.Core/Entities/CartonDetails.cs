using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class CartonDetails : BaseEntity
    {
        public int CartonDetailsId { get; set; }
        public string PackType { get; set; }
        public string NoOfCarton { get; set; }
        public int OrderEntryId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
