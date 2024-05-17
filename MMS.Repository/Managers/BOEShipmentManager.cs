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
    public class BOEShipmentManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BOEShipmentMaster> BOEMasterRepository;

        public BOEShipmentManager()
        {
            BOEMasterRepository = unitOfWork.Repository<BOEShipmentMaster>();
        }

        public bool Post(BOEShipmentMaster arg)
        {
            bool result = false;
            try
            {

                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                BOEMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public BOEShipmentMaster GetBOEShipmentID(int BOEId)
        {
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            boeMaster = BOEMasterRepository.Table.Where(x => x.BOEId == BOEId).SingleOrDefault();
            return boeMaster;
        }
        public BOEShipmentMaster GetCountryStamp(string CountryStamp)
        {
            BOEShipmentMaster boeMaster = new BOEShipmentMaster();
            boeMaster = BOEMasterRepository.Table.Where(x => x.CountryStamp == CountryStamp).SingleOrDefault();
            return boeMaster;
        }
        public bool Put(BOEShipmentMaster arg)
        {
            bool result = false;
            try
            {
                BOEShipmentMaster model = BOEMasterRepository.Table.Where(p => p.BOEId == arg.BOEId).FirstOrDefault();
                if (model != null)
                {
                    model.BOEId = arg.BOEId;
                    model.CountryStamp = arg.CountryStamp;
                    model.ShipmentFrom = arg.ShipmentFrom;
                    model.ShipmentTo = arg.ShipmentTo;
                    model.OtherInstructionFromBuyer = arg.OtherInstructionFromBuyer;                     
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;   
                    BOEMasterRepository.Update(model);
                    result = true;
                }

                else
                {
                    result = false;
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
                BOEShipmentMaster model = BOEMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                BOEMasterRepository.Update(model);
              //  BOEMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public GradeMaster Get(int id)
        {
            return null;
        }
        public List<BOEShipmentMaster> Get()
        {
            List<BOEShipmentMaster> gradeMaster = new List<BOEShipmentMaster>();
            try
            {
                gradeMaster = BOEMasterRepository.Table.Where(x=>x.IsDeleted==false || x.IsDeleted == null).ToList<BOEShipmentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gradeMaster;
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
