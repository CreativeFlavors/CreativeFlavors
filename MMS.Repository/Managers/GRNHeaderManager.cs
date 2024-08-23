using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
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

        public GRNHeader Get(int id)
        {
            GRNHeader GRNEntityModelNew = new GRNHeader();
            if (id != 0)
            {
                try
                {
                    GRNEntityModelNew = GRNHeaderrep.Table.Where(x => x.GRNNumber == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return GRNEntityModelNew;
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
        public List<GRNGrids> GetgrnList()
        {
            List<GRNGrids> MRP_Details = new List<GRNGrids>();
            try
            {
                MRP_Details = GRNHeaderrep.GetGRNGridList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MRP_Details;
        }
        public List<GRNHeader> GetGRNId(int id)
        {
            List<GRNHeader> PoHeader = new List<GRNHeader>();
            PoHeader = GRNHeaderrep.Table.Where(x => x.PoNumber == id).ToList();
            return PoHeader;
        }     
        public List<GRNHeader> GetsupplierId(int id)
        {
            List<GRNHeader> PoHeader = new List<GRNHeader>();
            PoHeader = GRNHeaderrep.Table.Where(x => x.SupplierId == id).ToList();
            return PoHeader;
        }
        public int GetNextGRNNumberFromDatabase()
        {
            int latestNumber = 0; 
            try
            {
                latestNumber = GRNHeaderrep.Table.Max(p => p.GRNNumber);
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
