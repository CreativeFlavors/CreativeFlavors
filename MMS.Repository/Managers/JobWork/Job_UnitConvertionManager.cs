using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MMS.Repository.Managers.JobWork
{
    public class Job_UnitConvertionManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<UnitConvertionMaster> UnitConvertionMasterRepository_;
        private RepositoryJob<UnitConvertionMaster> UnitConvertionRepository;
        public Job_UnitConvertionManager()
        {
            // Job_ApprovedPriceRepository = unitOfWork.Repository<Job_ApprovedPriceMaster>();
            UnitConvertionRepository = unitOfWork.Repository_<UnitConvertionMaster>();
        }
        #region Add/Update/Delete Operation

        public bool Post(UnitConvertionMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsDeleted = false;

                //arg.UpdatedBy = username;
                UnitConvertionRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }


        public bool Put(UnitConvertionMaster arg)
        {
            bool result = false;
            try
            {
                UnitConvertionMaster model = UnitConvertionRepository.Table.Where(p => p.UC_No_Id == arg.UC_No_Id).FirstOrDefault();
                if (arg != null)
                {

                    //   model.Jw_ApprovedPriceID = arg.Jw_ApprovedPriceID;
                    model.Store_id_from = arg.Store_id_from;
                    model.UC_No_Code = arg.UC_No_Code;
                    model.Group_From = arg.Group_From;
                    model.Category_From = arg.Category_From;
                    model.Material_id_From = arg.Material_id_From;
                    model.Store_id_to = arg.Store_id_to;
                    model.Group_To = arg.Group_To;
                    model.Category_To = arg.Category_To;
                    model.Material_id_To = arg.Material_id_To;
                    model.Noms =  arg.Noms;

                    model.Uom1 = arg.Uom1;
                    model.Uom2 = arg.Uom2;
                    model.No_sheet = arg.No_sheet;
                    model.No_sheet_Uom = arg.No_sheet_Uom;
                    model.Sheet_Value = arg.Sheet_Value;
                    model.Sheet_Value_Uom = arg.Sheet_Value_Uom;
                    model.Value = arg.Value;
                    model.Value_Uom = arg.Value_Uom;

                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    UnitConvertionRepository.Update(arg);
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
                UnitConvertionMaster model = UnitConvertionRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                UnitConvertionRepository.Update(model);
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public bool Delete_Jobname(int id)
        {
            bool result = false;
            try
            {
                List<UnitConvertionMaster> UnitConvertionMaster = new List<UnitConvertionMaster>();

                UnitConvertionMaster = UnitConvertionRepository.Table.Where(x => x.UC_No_Id == id).ToList();
                foreach (var item in UnitConvertionMaster)
                {
                    item.IsDeleted = true;
                    item.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    item.DeletedDate = DateTime.Now;
                    UnitConvertionRepository.Update(item);
                }
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        #endregion

        public List<UnitConvertionMaster> Get()
        {
            List<UnitConvertionMaster> UnitConvertionMaster = new List<UnitConvertionMaster>();
            try
            {
                UnitConvertionMaster = UnitConvertionRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return UnitConvertionMaster;
        }
        public UnitConvertionMaster Get_UC_No_Id(int? UC_No_Id)
        {
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            try
            {
                if (UC_No_Id != 0)
                {
                    UnitConvertionMaster = UnitConvertionRepository.Table.Where(x => x.UC_No_Id == UC_No_Id).SingleOrDefault();
                    return UnitConvertionMaster;
                }
                
            }
            catch (Exception ex)
            {

            }
            return UnitConvertionMaster;
        }
       
    }
}