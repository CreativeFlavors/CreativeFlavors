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
    public class ComponentManager : IComponentService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ComponentMaster> ComponentRepository;

        public ComponentManager()
        {
            ComponentRepository = unitOfWork.Repository<ComponentMaster>();
        }
        public bool Post(ComponentMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                ComponentRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public ComponentMaster GetComponetName(string ComponentName)
        {
            ComponentMaster componentMaster = new ComponentMaster();
            componentMaster = ComponentRepository.Table.Where(x => x.ComponentName == ComponentName).SingleOrDefault();
            return componentMaster;
        }
        public ComponentMaster GetComponentMasterId(int ComponentMasterId)
        {
            ComponentMaster componentMaster = new ComponentMaster();
            if (ComponentMasterId != 0)
            {
                componentMaster = ComponentRepository.Table.Where(x => x.ComponentMasterId == ComponentMasterId).SingleOrDefault();
            }
            return componentMaster;
        }
        public bool Put(ComponentMaster arg)
        {
            bool result = false;
            try
            {

                ComponentMaster model = ComponentRepository.Table.Where(p => p.ComponentMasterId == arg.ComponentMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.ComponentName = arg.ComponentName;
                    model.Image = arg.Image;
                    model.UsedIn = arg.UsedIn;
                    model.ComponentMasterId = arg.ComponentMasterId; ;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    arg.UpdatedBy = username;
                    ComponentRepository.Update(model);
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
                ComponentMaster model = ComponentRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                ComponentRepository.Update(model);
                //ComponentRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public ComponentMaster Get(int id)
        {
            return null;
        }

        public List<ComponentMaster> Get()
        {
            List<ComponentMaster> componentMaster = new List<ComponentMaster>();

            try
            {
                componentMaster = ComponentRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ComponentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return componentMaster;
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
