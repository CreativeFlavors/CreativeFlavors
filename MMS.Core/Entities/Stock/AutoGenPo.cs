using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
  public  class AutoGenPo: BaseEntity
    {
        public int AutoGenerateId { get; set; }
        public string PoId { get; set; }
    }
}
