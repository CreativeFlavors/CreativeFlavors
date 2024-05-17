using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface IColorService
    {
        bool Post(ColorMaster arg);
        bool Put(ColorMaster arg);
        bool Delete(int id);
        ColorMaster Get(int id);
        List<ColorMaster> Get();
    }
}
