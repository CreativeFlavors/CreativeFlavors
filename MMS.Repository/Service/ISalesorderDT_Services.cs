using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ISalesorderDT_Services
    {
        Salesorder_dt GettypeId(int id);
        List<Salesorder_dt> Get();
    }
}
