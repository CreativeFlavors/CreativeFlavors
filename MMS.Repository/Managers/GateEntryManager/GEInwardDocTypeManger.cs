using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
   public class GEInwardDocTypeManger: IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryDocTypeMaster> gateEntryDocTypeRepository;
        public GEInwardDocTypeManger()
        {
            gateEntryDocTypeRepository = unitOfWork.Repository<GateEntryDocTypeMaster>();

        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryDocTypeMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.InwardDocumentTypeId == 0)
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
                GateEntryDocTypeMaster model = gateEntryDocTypeRepository.GetById(InwardDocumentTypeId);
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
        public GateEntryDocTypeMaster GetGEDocTypeById(int InwardDocumentTypeId)
        {
            GateEntryDocTypeMaster gateEntryDocType = new GateEntryDocTypeMaster();
            if (InwardDocumentTypeId != 0)
            {
                gateEntryDocType = gateEntryDocTypeRepository.Table.Where(x => x.InwardDocumentTypeId == InwardDocumentTypeId).SingleOrDefault();
            }
            return gateEntryDocType;
        }

        public List<GateEntryDocTypeMaster> Get()
        {
            List<GateEntryDocTypeMaster> gateEntryDocTypelist = new List<GateEntryDocTypeMaster>();
            try
            {
                gateEntryDocTypelist = gateEntryDocTypeRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryDocTypeMaster>();
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

