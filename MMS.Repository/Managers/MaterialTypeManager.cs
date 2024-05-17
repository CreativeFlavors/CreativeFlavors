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
    public class MaterialTypeManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OpeningStockPinCard> MaterialTypeMasterRepository;
       
        //private Repository<MaterialNameMaster> MaterialNameMasterRepository;

        //#region Add/Update/Delete Operation

        //public MaterialNameMaster Post(MaterialNameMaster arg)
        //{
        //    bool result = false;
        //    MaterialNameMaster materialNameMaster = new MaterialNameMaster();
        //    try
        //    {
        //        string username = HttpContext.Current.Session["UserName"].ToString();
        //        arg.CreatedBy = username;
        //        arg.CreatedDate = DateTime.Now;
        //        //arg.UpdatedBy = username;
        //        MaterialNameMasterRepository.Insert(arg);
        //        materialNameMaster = arg;
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }
        //    return materialNameMaster;
        //}
        //public bool Put(MaterialNameMaster arg)
        //{
        //    bool result = false;
        //    try
        //    {
        //        MaterialNameMaster model = MaterialNameMasterRepository.Table.Where(p => p.MaterialMasterID == arg.MaterialMasterID).FirstOrDefault();
        //        if (model != null)
        //        {
        //            model.MaterialMasterID = arg.MaterialMasterID;
        //            model.MaterialDescription = arg.MaterialDescription;
        //            model.CreatedDate = model.CreatedDate;
        //            model.LeatherMaterialName = arg.LeatherMaterialName;
        //            model.LeatherMaterilFirstValue = arg.LeatherMaterilFirstValue;
        //            model.LeatherMaterilLastValue = arg.LeatherMaterilLastValue;
        //            model.UpdatedDate = DateTime.Now;
        //            model.CreatedBy = model.CreatedBy;
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            model.UpdatedBy = username;
        //            MaterialNameMasterRepository.Update(model);
        //            result = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}
        //public bool Delete(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        MaterialNameMaster model = MaterialNameMasterRepository.GetById(id);
        //        model.IsDeleted = true;
        //        model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
        //        model.DeletedDate = DateTime.Now;
        //        MaterialNameMasterRepository.Update(model);
        //        //  MaterialNameMasterRepository.Delete(model);
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}
        //#endregion

        //#region Helper Method

        public MaterialTypeManager()
        {
            MaterialTypeMasterRepository = unitOfWork.Repository<OpeningStockPinCard>();
            //MaterialNameMasterRepository = unitOfWork.Repository<MaterialNameMaster>();
        }

       

        public OpeningStockPinCard GetMaterialType(string MaterialType)
        {
            OpeningStockPinCard MaterialTypeName = new OpeningStockPinCard();
            if (MaterialType != null)
            {
                MaterialTypeName = MaterialTypeMasterRepository.Table.Where(x => x.MaterialType == MaterialType).SingleOrDefault();
            }
            return MaterialTypeName;
        }
        //public MaterialNameMaster GetMaterialName(string MaterialDescription)
        //{
        //    MaterialNameMaster MaterialNameMaster = new MaterialNameMaster();
        //    if (MaterialDescription != "")
        //    {
        //        MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialDescription.Trim() == MaterialDescription.Trim() && x.IsDeleted==false).FirstOrDefault();


        //    }
        //    return MaterialNameMaster;
        //}
        //public MaterialNameMaster GetLeatherMaterialName(string LeatherMaterilFirstValue, string LeatherMaterilLastValue, int? LeatherMaterialName, int MaterialGroupMasterId)
        //{
        //    MaterialNameMaster MaterialNameMaster = new MaterialNameMaster();
        //    if (LeatherMaterilLastValue != null && LeatherMaterilFirstValue != null && LeatherMaterialName != null)
        //    {
        //        MaterialNameMaster = MaterialNameMasterRepository.Table.Where(x => x.LeatherMaterilFirstValue == LeatherMaterilFirstValue && x.LeatherMaterilLastValue == LeatherMaterilLastValue && x.LeatherMaterialName == LeatherMaterialName && x.MaterialGroupMasterId == MaterialGroupMasterId).SingleOrDefault();
        //    }
        //    return MaterialNameMaster;
        //}
        //public string GetMaxMaterialCode()
        //{

        //    var MaterialNameMaster = (from t in MaterialNameMasterRepository.Table orderby t.MaterialMasterID descending select t.MaterialCode).FirstOrDefault();

        //    return MaterialNameMaster;
        //}
        public List<OpeningStockPinCard> Get()
        {
            List<OpeningStockPinCard> OpeningStockPinCardList = new List<OpeningStockPinCard>();
           
            try
            {
                OpeningStockPinCardList = MaterialTypeMasterRepository.Table.ToList<OpeningStockPinCard>();
               // OpeningStockPinCardList = MaterialTypeMasterRepository.Table.Where(x => x.MaterialOpeningStockID != null).ToList<MaterialNameMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return OpeningStockPinCardList;
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
        //public void Dispose()
        //{
        //    using (MMSContext context = new MMSContext())
        //    {
        //        context.Dispose();
        //    }
        //}
        //#endregion
    }
}
