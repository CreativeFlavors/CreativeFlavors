using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class SalesorderHD_Manager : ISalesorderHD_Services, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Salesorder_hd> salesorderhdrep;
        public SalesorderHD_Manager()
        {
            salesorderhdrep = unitOfWork.Repository<Salesorder_hd>();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Salesorder_hd> Get()
        {
            List<Salesorder_hd> obj = new List<Salesorder_hd>();
            obj = salesorderhdrep.Table.ToList<Salesorder_hd>();
            return obj;
        }
        public Salesorder_Grid salesorder_get(int SOid)
        { 
            Salesorder_Grid Salesorder_Grid = new Salesorder_Grid();
            Salesorder_Grid = salesorderhdrep.salesorder_gridSearch(SOid);
            return Salesorder_Grid;
        }
        public List<Salesorder_hd> GettypeId(int id)
        {
            List<Salesorder_hd> salesorder = new List<Salesorder_hd>();
            salesorder = salesorderhdrep.Table.Where(x => x.customerid == id).ToList();
            return salesorder;
        }

        public Salesorder_hd POST(Salesorder_hd arg)
        {
            Salesorder_hd salesorder = new Salesorder_hd();
            try
            {
                string username = "admin";
                arg.createdby = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = true;
                salesorderhdrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }

       
    }
}
