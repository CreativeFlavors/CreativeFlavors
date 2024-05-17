using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IOrderTypeService
    {
        OrderType Post(OrderType arg);
        bool Put(OrderType arg);
        bool Delete(int id);
        OrderType Get(int id);
        List<OrderType> Get();
    }
}
