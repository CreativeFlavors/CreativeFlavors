using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MMS.Repository.Managers
{
    public class UOMManager : IUOMService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<UomMaster> UomMasterRepository;

        public UOMManager()
        {
            UomMasterRepository = unitOfWork.Repository<UomMaster>();
        }
        public UomMaster Post(UomMaster arg)
        {
            bool result = false;
            UomMaster uomMaster = new UomMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
             //   arg.UpdatedBy = username;
                UomMasterRepository.Insert(arg);
                uomMaster=arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return uomMaster;

        }
        public UomMaster GetUnitName(string ShortUnitName)
        {
            UomMaster uomMaster = new UomMaster();
            if (ShortUnitName!= ""&&ShortUnitName!=null)
            {
                uomMaster = UomMasterRepository.Table.Where(x => x.ShortUnitName.ToLower() == ShortUnitName.ToLower().Trim()).FirstOrDefault();
            }
            return uomMaster;
        }
        public UomMaster GetLongUnitName(string LongUnitName)
        {
            UomMaster uomMaster = new UomMaster();
            if (LongUnitName != "" && LongUnitName != null)
            {
                uomMaster = UomMasterRepository.Table.Where(x => x.LongUnitName.ToLower() == LongUnitName.ToLower()).FirstOrDefault();
            }
            return uomMaster;
        }
        public UomMaster GetUomMasterId(int? UomMasterId)
        {
            UomMaster componentMaster = new UomMaster();
            if (UomMasterId != 0)
            {
                componentMaster = UomMasterRepository.Table.Where(x => x.UomMasterId == UomMasterId).SingleOrDefault();
            }
            return componentMaster;
        }
        public bool Put(UomMaster arg)
        {
            bool result = false;
            try
            {

                UomMaster model = UomMasterRepository.Table.Where(p => p.UomMasterId == arg.UomMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.ShortUnitName = arg.ShortUnitName;
                    model.LongUnitName = arg.LongUnitName;                  
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    UomMasterRepository.Update(model);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }




        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                UomMaster model = UomMasterRepository.GetById(id);
                UomMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public UomMaster Get(int id)
        {
            return null;
        }

        public List<UomMaster> Get()
        {
            List<UomMaster> uomMaster = new List<UomMaster>();

            try
            {
                uomMaster = UomMasterRepository.Table.ToList<UomMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return uomMaster;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

    }
}
