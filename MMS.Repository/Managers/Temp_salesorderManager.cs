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
        private Repository<Temp_Indent> tempsalesorderrep;
        public Temp_salesorderManager()
        {
            tempsalesorderrep = unitOfWork.Repository<Temp_Indent>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Post(Temp_Indent arg)
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
        public Temp_Indent GetSO(int? id)
        {
            Temp_Indent salesorder = new Temp_Indent();
            if (id != null)
            {
                salesorder = tempsalesorderrep.Table.Where(x => x.SalesOrderId == id).FirstOrDefault();
            }
            return salesorder;
        }

        public Temp_Indent GetStockRequiredForMaterial(int MaterialNameID)
        {
            Temp_Indent temp_Salesorder = new Temp_Indent();
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
        public List<Temp_Indent> GetStockRequiredForMaterials(int MaterialNameID)
        {
            List<Temp_Indent> temp_Salesorder = new List<Temp_Indent>();
            try
            {
                if (MaterialNameID != 0)
                {
                    temp_Salesorder = tempsalesorderrep.Table.Where(x => x.MaterialId == MaterialNameID).ToList();
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
