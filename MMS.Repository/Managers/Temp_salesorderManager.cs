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
    public class Temp_salesorderManager : ITemp_salesorderServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Temp_salesorder> tempsalesorderrep;
        public Temp_salesorderManager()
        {
            tempsalesorderrep = unitOfWork.Repository<Temp_salesorder>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Post(Temp_salesorder arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                tempsalesorderrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public Temp_salesorder GetSO(int? id)
        {
            Temp_salesorder salesorder = new Temp_salesorder();
            if (id != null)
            {
                salesorder = tempsalesorderrep.Table.Where(x => x.SalesOrderId == id).FirstOrDefault();
            }
            return salesorder;
        }

        public Temp_salesorder GetStockRequiredForMaterial(int MaterialNameID)
        {
            Temp_salesorder temp_Salesorder = new Temp_salesorder();
            try
            {
                if (MaterialNameID != 0)
                {
                    temp_Salesorder = tempsalesorderrep.Table.Where(x => x.MaterialId == MaterialNameID).FirstOrDefault();
                    if (temp_Salesorder == null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }

            return temp_Salesorder;
        }
    }
}
