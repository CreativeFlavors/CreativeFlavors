using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    internal interface ICusAddreess
    {
        bool Post(CustAddress arg);
        bool Put(CustAddress arg);
        bool Delete(int id);
        CustAddress Get(int id);
    }
}
