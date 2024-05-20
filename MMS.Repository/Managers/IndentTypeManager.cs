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
        private Repository<IndentMaster> indentMasterRepository;

        public IndentTypeManager()
        {
            indentMasterRepository = unitOfWork.Repository<IndentMaster>();
        }


        public bool Post(IndentMaster arg)
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

        public IndentMaster GetIndentMasterID(int IndentMasterID)
        {
            IndentMaster indentMaster = new IndentMaster();
            indentMaster = indentMasterRepository.Table.Where(x => x.IndentMasterID == IndentMasterID).SingleOrDefault();
            return indentMaster;
        }
        public IndentMaster GetIndentTypeName(string IndentTypeName)
        {
            IndentMaster indentMaster = new IndentMaster();
            indentMaster = indentMasterRepository.Table.Where(x => x.IndentTypeName == IndentTypeName).SingleOrDefault();
            return indentMaster;
        }
        public bool Put(IndentMaster arg)
        {
            bool result = false;
            try
            {
                IndentMaster model = indentMasterRepository.Table.Where(p => p.IndentMasterID == arg.IndentMasterID).FirstOrDefault();
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
                IndentMaster model = indentMasterRepository.GetById(id);
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

        public IndentMaster Get(int id)
        {
            return null;
        }

        public List<IndentMaster> Get()
        {
            List<IndentMaster> indentMaster = new List<IndentMaster>();
            try
            {
                indentMaster = indentMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<IndentMaster>();
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
