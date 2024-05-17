using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface IManufactureService
    {
      ManufacturerMaster Post(ManufacturerMaster arg);
        bool Put(ManufacturerMaster arg);
        bool Delete(int id);
        ManufacturerMaster Get(int id);
        List<ManufacturerMaster> Get();
    }
}
