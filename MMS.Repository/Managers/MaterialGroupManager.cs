using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;  
namespace MMS.Repository.Managers
{
    public class MaterialGroupManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<materialgroupmaster> MaterialGroupMasterRepository;

        #region Add/Update/Delete Operation

        public materialgroupmaster Post(materialgroupmaster arg)
        {
            bool result = false;
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;

                MaterialGroupMasterRepository.Insert(arg);
                result = true;
                materialGroupMaster = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return materialGroupMaster;

        }
        public bool Put(materialgroupmaster arg)
        {
            bool result = false;
            try
            {
                materialgroupmaster model = MaterialGroupMasterRepository.Table.Where(p => p.MaterialGroupMasterId == arg.MaterialGroupMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.GroupCode = arg.GroupCode;
                    model.GroupName = arg.GroupName;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.SubGroup = arg.SubGroup;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.IsSubstance = arg.IsSubstance;
                    model.IsSize = arg.IsSize;
                    model.IsColor = arg.IsColor;
                    model.IsConsumption = arg.IsConsumption;
                    model.IsInspection = arg.IsInspection;
                    model.IsReservation = arg.IsReservation;
                    model.IsDisplay = arg.IsDisplay;
                    model.IsBatchcode = arg.IsBatchcode;
                    model.IsMultipleUnits = arg.IsMultipleUnits;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    model.IsLeatherType = arg.IsLeatherType;                    
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;
                    MaterialGroupMasterRepository.Update(model);
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
                materialgroupmaster model = MaterialGroupMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                MaterialGroupMasterRepository.Update(model);
               // MaterialGroupMasterRepository.Delete(model);
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

        #region Helper Method

        public MaterialGroupManager()
        {
            MaterialGroupMasterRepository = unitOfWork.Repository<materialgroupmaster>();
        }

        public materialgroupmaster GetGroupName(string GroupName)
        {
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            if (GroupName != "" && GroupName != null)
            {
                materialGroupMaster = MaterialGroupMasterRepository.Table.Where(x => x.GroupName == GroupName).FirstOrDefault();
            }
            return materialGroupMaster;
        }

        public materialgroupmaster GetmaterialgroupmasterId(int? MaterialGroupMasterId)
        {
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            if (MaterialGroupMasterId != 0)
            {
                materialGroupMaster = MaterialGroupMasterRepository.Table.Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId).SingleOrDefault();
            }
            return materialGroupMaster;
        }

        public BuyerMaster Get(int id)
        {
            return null;
        }

        public List<materialgroupmaster> Get()
        {
            List<materialgroupmaster> materialGroupMaster = new List<materialgroupmaster>();
            try
            {
                materialGroupMaster = MaterialGroupMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<materialgroupmaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialGroupMaster;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public materialgroupmaster getCategoryCode(int? CategoryId)
        {
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
            if (CategoryId != 0)
            {
                materialgroupmaster = MaterialGroupMasterRepository.Table.Where(x => x.MaterialGroupMasterId == CategoryId).SingleOrDefault();
            }
            return materialgroupmaster;
        }

        public materialgroupmaster GetMaterialGroupName(string MaterialGroup)
        {
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
            if (MaterialGroup !=  "")
            {
                materialgroupmaster = MaterialGroupMasterRepository.Table.Where(x => x.GroupName == MaterialGroup).SingleOrDefault();
            }
            return materialgroupmaster;
        }

        #endregion
    }
}
