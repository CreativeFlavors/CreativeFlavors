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
    public class EmailTemplateManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<EmailTempate> emailTempateRepository;
        #region Add/Update/Delete Operation
        public bool Post(EmailTempate arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now.ToString();
                emailTempateRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(EmailTempate arg)
        {
            bool result = false;
            try
            {

                EmailTempate model = emailTempateRepository.Table.Where(p => p.EmailTemplateID == arg.EmailTemplateID && p.IsActive == true && p.IsDeleted == false).FirstOrDefault();
                if (model != null)
                {
                    model.TemplateName = arg.TemplateName;
                    model.IsActive = arg.IsActive;
                    model.Subject = arg.Subject;
                    model.Body = arg.Body;
                    model.ToAddress = arg.ToAddress;
                    model.CCAddress = arg.CCAddress;
                    model.FromAddress = arg.FromAddress;
                    model.UpdatedDate = DateTime.Now.ToString();
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    emailTempateRepository.Update(model);
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
                EmailTempate model = emailTempateRepository.GetById(id);
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now.ToString();
                emailTempateRepository.Update(model);
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

        public EmailTemplateManager()
        {
            emailTempateRepository = unitOfWork.Repository<EmailTempate>();
        }

        public EmailTempate GetTemplateName(string TemplateName)
        {
            EmailTempate emailTempate = new EmailTempate();
            if (TemplateName != "" && TemplateName != null)
            {
                emailTempate = emailTempateRepository.Table.Where(x => x.TemplateName == TemplateName && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            }
            return emailTempate;
        }

        public EmailTempate GetEmailTemplateID(int EmailTemplateID)
        {
            EmailTempate emailTempate = new EmailTempate();
            if (EmailTemplateID != 0)
            {
                emailTempate = emailTempateRepository.Table.Where(x => x.EmailTemplateID == EmailTemplateID && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            }
            return emailTempate;
        }
        public List<EmailTempate> Get()
        {
            List<EmailTempate> emailTempateList = new List<EmailTempate>();
            try
            {
                emailTempateList = emailTempateRepository.Table.Where(x => x.IsDeleted == false && x.IsActive == true).ToList<EmailTempate>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return emailTempateList;
        }

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
