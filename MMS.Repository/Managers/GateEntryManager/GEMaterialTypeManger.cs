using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
    public class GEMaterialTypeManger : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GEMaterialTypeMaster> gateEntryDocTypeRepository;
        public GEMaterialTypeManger()
        {
            gateEntryDocTypeRepository = unitOfWork.Repository<GEMaterialTypeMaster>();

        }

        #region Add/Update/Delete Operation
        public bool Post(GEMaterialTypeMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.GEMaterialTypeId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryDocTypeRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryDocTypeRepository.Update(arg);
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        
        public bool Delete(int InwardDocumentTypeId)
        {
            bool result = false;
            try
            {
                GEMaterialTypeMaster model = gateEntryDocTypeRepository.GetById(InwardDocumentTypeId);
                model.IsDeleted = true;
                gateEntryDocTypeRepository.Update(model);
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
        public GEMaterialTypeMaster GetGEMaterialTypeById(int InwardDocumentTypeId)
        {
            GEMaterialTypeMaster gateEntryDocType = new GEMaterialTypeMaster();
            if (InwardDocumentTypeId != 0)
            {
                gateEntryDocType = gateEntryDocTypeRepository.Table.Where(x => x.GEMaterialTypeId == InwardDocumentTypeId &&  x.IsDeleted == false).SingleOrDefault();
            }
            return gateEntryDocType;
        }

        public List<GEMaterialTypeMaster> Get()
        {
            List<GEMaterialTypeMaster> gateEntryDocTypelist = new List<GEMaterialTypeMaster>();
            try
            {
                gateEntryDocTypelist = gateEntryDocTypeRepository.Table.Where(x => x.IsDeleted == false).ToList<GEMaterialTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateEntryDocTypelist;
        }



        #endregion
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}

