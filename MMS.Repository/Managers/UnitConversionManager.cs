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
    public class UnitConversionManager : IUnitConversionService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<UnitConversation> UomMasterRepository;
        private Repository<UomMaster> MasterRepository;

        public UnitConversionManager()
        {
            UomMasterRepository = unitOfWork.Repository<UnitConversation>();
        }
        public UnitConversation Post(UnitConversation arg)
        {
            bool result = false;
            UnitConversation unitConversation = new UnitConversation();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                UomMasterRepository.Insert(arg);
                unitConversation = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return unitConversation;

        }
        public UnitConversation GetCategoryName(int Category)
        {
            UnitConversation unitConversation = new UnitConversation();
            if (Category != 0 && Category != null)
            {
                unitConversation = UomMasterRepository.Table.Where(x => x.Category == Category).SingleOrDefault();
            }
            return unitConversation;
        }

        public UnitConversation GetUnitConversionId(int UnitConversionId)
        {
            UnitConversation unitConversation = new UnitConversation();
            if (UnitConversionId != 0)
            {
                unitConversation = UomMasterRepository.Table.Where(x => x.UnitConversionId == UnitConversionId).SingleOrDefault();
            }
            return unitConversation;
        }
        public bool Put(UnitConversation arg)
        {
            bool result = false;
            try
            {

                UnitConversation model = UomMasterRepository.Table.Where(p => p.UnitConversionId == arg.UnitConversionId).FirstOrDefault();
                if (model != null)
                {
                    model.Category = arg.Category;
                    model.UcGroup = arg.UcGroup;
                    model.Material = arg.Material;
                    model.LongUnitNameValue = arg.LongUnitNameValue;
                    model.ShortUnitNameValue = arg.ShortUnitNameValue;
                    model.UpdatedDate = DateTime.Now;
                    model.ShortUnitId = arg.ShortUnitId;
                    model.LongUnitID = arg.LongUnitID;
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
                UnitConversation model = UomMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                UomMasterRepository.Update(model);
                //UomMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public UnitConversation Get(int id)
        {
            return null;
        }

        public List<UnitConversation> Get()
        {
            List<UnitConversation> unitConversation = new List<UnitConversation>();

            try
            {
                unitConversation = UomMasterRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<UnitConversation>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return unitConversation;
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
