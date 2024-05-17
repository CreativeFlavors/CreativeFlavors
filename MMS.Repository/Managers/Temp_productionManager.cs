using MMS.Common;
using MMS.Core.Entities;
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
    public class Temp_productionManager : ITemp_productionServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<temp_production> tempproductionrep;
        public Temp_productionManager()
        {
            tempproductionrep = unitOfWork.Repository<temp_production>();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Post(temp_production arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                tempproductionrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public temp_production GetPro(int? id)
        {
            temp_production tempro = new temp_production();
            if (id != null)
            {
                tempro = tempproductionrep.Table.Where(x => x.SalesOrderId == id).FirstOrDefault();
            }
            return tempro;
        }
        public List<temp_production> Getmaterial()
        {
            List<temp_production> list = new List<temp_production>();
            list = tempproductionrep.Table.Where(x => x.IsActive == true).ToList<temp_production>();
            return list;
        }
        public void UpdateProductionBOMID(int pbomid, int materialid)
        {
            temp_production model = tempproductionrep.Table.Where(p => p.MaterialId == materialid).FirstOrDefault();
            if (model != null)
            {
                model.Probomid = pbomid;
                tempproductionrep.Update(model);
            }
        }
    }
}
