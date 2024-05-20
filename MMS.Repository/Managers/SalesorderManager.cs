using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class SalesorderManager : ISalesorder, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<salesorder> salesorderrep;
        public SalesorderManager()
        {
            salesorderrep = unitOfWork.Repository<salesorder>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public bool Post(salesorder arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.createdby = username;
                arg.CreatedDate = DateTime.Now;
                salesorderrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public List<salesorder> Get()
        {
            List<salesorder> obj = new List<salesorder>();
            obj = salesorderrep.Table.ToList<salesorder>();
            return obj;
        }
        public salesorder GetSO(int? id)
        {
            salesorder salesorder = new salesorder();
            if (id != null)
            {
                salesorder = salesorderrep.Table.Where(x => x.SalesorderId == id).FirstOrDefault();
            }
            return salesorder;
        }
        public salesorder GettypeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
