﻿using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class GRNHeaderManager : IGRNHeaderServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRNHeader> GRNHeaderrep;
        public GRNHeaderManager() {
            GRNHeaderrep = unitOfWork.Repository<GRNHeader>();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public GRNHeader POST(GRNHeader arg)
        {
            GRNHeader salesorder = new GRNHeader();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = "1";
                GRNHeaderrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<GRNHeader> Get()
        {
            List<GRNHeader> obj = new List<GRNHeader>();
            obj = GRNHeaderrep.Table.ToList<GRNHeader>();
            return obj;
        }
        public int GetNextGRNNumberFromDatabase()
        {
            int latestNumber = 0; 
            try
            {
                latestNumber = GRNHeaderrep.Table.Max(p => p.GrnHeaderId);
                latestNumber++;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return latestNumber;
        }
    }
}