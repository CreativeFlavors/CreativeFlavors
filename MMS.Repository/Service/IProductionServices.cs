﻿using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IProductionServices
    {
        bool Post(Production arg);
        bool Put(Production arg);
        Production Getproductionid(int? productionid);
    }
}
