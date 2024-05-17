using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface InvoiceService
    {
        bool Delete(int id);
            List<order_header> Get();
        order_header Get(int id);
    }
}
