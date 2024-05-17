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
  public  class IndentTypeManager :IIndentTypeService,IDisposable
    {
          private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<IndentTypeMaster> indentMasterRepository;

        public IndentTypeManager()
        {
            indentMasterRepository = unitOfWork.Repository<IndentTypeMaster>();
        }


        public bool Post(IndentTypeMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;

                indentMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public IndentTypeMaster GetIndentMasterID(int IndentMasterID)
        {
            IndentTypeMaster indentMaster = new IndentTypeMaster();
            indentMaster = indentMasterRepository.Table.Where(x => x.IndentMasterID == IndentMasterID).SingleOrDefault();
            return indentMaster;
        }
        public IndentTypeMaster GetIndentTypeName(string IndentTypeName)
        {
            IndentTypeMaster indentMaster = new IndentTypeMaster();
            indentMaster = indentMasterRepository.Table.Where(x => x.IndentTypeName == IndentTypeName).SingleOrDefault();
            return indentMaster;
        }
        public bool Put(IndentTypeMaster arg)
        {
            bool result = false;
            try
            {
                IndentTypeMaster model = indentMasterRepository.Table.Where(p => p.IndentMasterID == arg.IndentMasterID).FirstOrDefault();
                if (model != null)
                {
                    model.IndentTypeCode = arg.IndentTypeCode;
                    model.IndentMasterID = arg.IndentMasterID;
                    model.IndentTypeName = arg.IndentTypeName;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
         
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;

                    indentMasterRepository.Update(model);
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
                IndentTypeMaster model = indentMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                indentMasterRepository.Update(model);
                // indentMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public IndentTypeMaster Get(int id)
        {
            return null;
        }

        public List<IndentTypeMaster> Get()
        {
            List<IndentTypeMaster> indentMaster = new List<IndentTypeMaster>();
            try
            {
                indentMaster = indentMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<IndentTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return indentMaster;
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
