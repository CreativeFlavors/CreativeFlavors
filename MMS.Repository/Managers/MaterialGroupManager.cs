using MMS.Common;
using MMS.Core.Entities;
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
        private Repository<MaterialGroupMaster_> MaterialGroupMasterRepository;

        #region Add/Update/Delete Operation

        public MaterialGroupMaster_ Post(MaterialGroupMaster_ arg)
        {
            bool result = false;
            MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
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
        public bool Put(MaterialGroupMaster_ arg)
        {
            bool result = false;
            try
            {
                MaterialGroupMaster_ model = MaterialGroupMasterRepository.Table.Where(p => p.MaterialGroupMasterId == arg.MaterialGroupMasterId).FirstOrDefault();
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
                MaterialGroupMaster_ model = MaterialGroupMasterRepository.GetById(id);
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
            MaterialGroupMasterRepository = unitOfWork.Repository<MaterialGroupMaster_>();
        }

        public MaterialGroupMaster_ GetGroupName(string GroupName)
        {
            MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
            if (GroupName != "" && GroupName != null)
            {
                materialGroupMaster = MaterialGroupMasterRepository.Table.Where(x => x.GroupName == GroupName).FirstOrDefault();
            }
            return materialGroupMaster;
        }

        public MaterialGroupMaster_ GetMaterialGroupMaster_Id(int? MaterialGroupMasterId)
        {
            MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
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

        public List<MaterialGroupMaster_> Get()
        {
            List<MaterialGroupMaster_> materialGroupMaster = new List<MaterialGroupMaster_>();
            try
            {
                materialGroupMaster = MaterialGroupMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<MaterialGroupMaster_>();
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

        public MaterialGroupMaster_ getCategoryCode(int? CategoryId)
        {
            MaterialGroupMaster_ materialGroupMaster_ = new MaterialGroupMaster_();
            if (CategoryId != 0)
            {
                materialGroupMaster_ = MaterialGroupMasterRepository.Table.Where(x => x.MaterialGroupMasterId == CategoryId).SingleOrDefault();
            }
            return materialGroupMaster_;
        }

        public MaterialGroupMaster_ GetMaterialGroupName(string MaterialGroup)
        {
            MaterialGroupMaster_ materialGroupMaster_ = new MaterialGroupMaster_();
            if (MaterialGroup !=  "")
            {
                materialGroupMaster_ = MaterialGroupMasterRepository.Table.Where(x => x.GroupName == MaterialGroup).SingleOrDefault();
            }
            return materialGroupMaster_;
        }

        #endregion
    }
}
