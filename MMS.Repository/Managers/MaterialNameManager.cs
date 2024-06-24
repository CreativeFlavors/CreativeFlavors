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
    public class MaterialNameManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<tbl_materialnamemaster> MaterialNameMasterRepository;
        //private Repository<MaterialNameMaster> MaterialNameMasterRepository;

        #region Add/Update/Delete Operation

        public tbl_materialnamemaster Post(tbl_materialnamemaster arg)
        {
            bool result = false;
            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                //arg.UpdatedBy = username;
                MaterialNameMasterRepository.Insert(arg);
                materialNameMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return materialNameMaster;
        }
        public bool Put(tbl_materialnamemaster arg)
        {
            bool result = false;
            try
            {
                tbl_materialnamemaster model = MaterialNameMasterRepository.Table.Where(p => p.MaterialMasterID == arg.MaterialMasterID).FirstOrDefault();
                if (model != null)
                {
                    model.MaterialMasterID = arg.MaterialMasterID;
                    model.MaterialDescription = arg.MaterialDescription;
                    model.CreatedDate = model.CreatedDate;
                    model.LeatherMaterialName = arg.LeatherMaterialName;
                    model.LeatherMaterilFirstValue = arg.LeatherMaterilFirstValue;
                    model.LeatherMaterilLastValue = arg.LeatherMaterilLastValue;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    MaterialNameMasterRepository.Update(model);
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
                tbl_materialnamemaster model = MaterialNameMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                MaterialNameMasterRepository.Update(model);
                //  MaterialNameMasterRepository.Delete(model);
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

        public MaterialNameManager()
        {
            MaterialNameMasterRepository = unitOfWork.Repository<tbl_materialnamemaster>();
            //MaterialNameMasterRepository = unitOfWork.Repository<MaterialNameMaster>();
        }

        public tbl_materialnamemaster GetMaterialNameMasterId(int? MaterialNameMasterId)
        {
            tbl_materialnamemaster MaterialNameMaster = new tbl_materialnamemaster();
            if (MaterialNameMasterId != 0)
            {
                MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialMasterID == MaterialNameMasterId &&x.IsDeleted==false).FirstOrDefault();
            }
            return MaterialNameMaster;
        }

        public tbl_materialnamemaster GetMaterialNameMaterial(int? MaterialMasterID)
        {
            tbl_materialnamemaster MaterialNameMaster = new tbl_materialnamemaster();
            if (MaterialMasterID != 0)
            {
                MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialMasterID == MaterialMasterID).SingleOrDefault();
            }
            return MaterialNameMaster;
        }

        public List<tbl_materialnamemaster> GetMaterialName(int? MaterialMasterID)
        {
            List<tbl_materialnamemaster> MaterialNameMaster = new List<tbl_materialnamemaster>();
            if (MaterialMasterID != 0)
            {
                MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialMasterID == MaterialMasterID).ToList();
            }
            return MaterialNameMaster;
        }

        public tbl_materialnamemaster GetMaterialName(string MaterialDescription)
        {
            tbl_materialnamemaster MaterialNameMaster = new tbl_materialnamemaster();
            if (MaterialDescription != "")
            {
                MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialDescription.Trim() == MaterialDescription.Trim() && x.IsDeleted==false).FirstOrDefault();

               
            }
            return MaterialNameMaster;
        }
        public tbl_materialnamemaster GetLeatherMaterialName(string LeatherMaterilFirstValue, string LeatherMaterilLastValue, int? LeatherMaterialName, int MaterialGroupMasterId)
        {
            tbl_materialnamemaster MaterialNameMaster = new tbl_materialnamemaster();
            if (LeatherMaterilLastValue != null && LeatherMaterilFirstValue != null && LeatherMaterialName != null)
            {
                MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.LeatherMaterilFirstValue == LeatherMaterilFirstValue && x.LeatherMaterilLastValue == LeatherMaterilLastValue && x.LeatherMaterialName == LeatherMaterialName && x.MaterialGroupMasterId == MaterialGroupMasterId).SingleOrDefault();
            }
            return MaterialNameMaster;
        }
        public string GetMaxMaterialCode()
        {

            var MaterialNameMaster = (from t in MaterialNameMasterRepository.Table orderby t.MaterialMasterID descending select t.MaterialCode).FirstOrDefault();

            return MaterialNameMaster;
        }
        public List<tbl_materialnamemaster> Get()
        {
            List<tbl_materialnamemaster> MaterialNameMasterList = new List<tbl_materialnamemaster>();
            try
            {
                MaterialNameMasterList = MaterialNameMasterRepository.Table.Where(X => X.IsDeleted == false).ToList<tbl_materialnamemaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialNameMasterList;
        }
        //public List<MaterialNameMaster> GetGridMaterialName()
        //{
        //    List<MaterialNameMaster> MaterialNameMasterList = new List<MaterialNameMaster>();
        //    try
        //    {

        //     var B = (from x in MaterialNameMaster 
        //             where x.Contains(pre)
        //             select x);


        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return MaterialNameMasterList;
        //}
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        #endregion
    }
}
