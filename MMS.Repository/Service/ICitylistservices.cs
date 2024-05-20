using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ICitylistservices
    {
        cities Get(int id);
        List<cities> Get();
    }
}
