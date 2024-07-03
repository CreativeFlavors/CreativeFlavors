using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Web.Models;

namespace MMS.Repository.ViewModel
{
    public class ProductionDetailsViewModel
    {
        public ProductionModels Production { get; set; }
        public ProductionSubassemblyModels Subassembly { get; set; }
    }
}
