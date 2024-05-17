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
   //public class GradeManager :IGradeService,IDisposable
   // {
   //       private UnitOfWork unitOfWork = new UnitOfWork();
   //    // private Repository<GradeMaster> gradeMasterRepository;

   //     //public GradeManager()
   //     //{
   //     //    gradeMasterRepository = unitOfWork.Repository<GradeMaster>();
   //     //}

   //     //public bool Post(GradeMaster arg)
   //     //{
   //     //    bool result = false;
   //     //    try
   //     //    {
   //     //        string username = HttpContext.Current.Session["UserName"].ToString();
   //     //        arg.CreatedBy = username;
   //     //        arg.UpdatedBy = username;

   //     //        gradeMasterRepository.Insert(arg);
   //     //        result = true;
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //     //        result = false;
   //     //    }
   //     //    return result;
   //     //}
   //     //public GradeMaster GetGradeMasterID(int gradeMasterID)
   //     //{
   //     //    GradeMaster gradeMaster = new GradeMaster();
   //     //    gradeMaster = gradeMasterRepository.Table.Where(x => x.GradeMasterID == gradeMasterID).SingleOrDefault();
   //     //    return gradeMaster;
   //     //}
   //     //public GradeMaster GetCodeNo(string CodeNo)
   //     //{
   //     //    GradeMaster gradeMaster = new GradeMaster();
   //     //    gradeMaster = gradeMasterRepository.Table.Where(x => x.CodeNo == CodeNo).SingleOrDefault();
   //     //    return gradeMaster;
   //     //}
   //     //public bool Put(GradeMaster arg)
   //     //{
   //     //    bool result = false;
   //     //    try
   //     //    {
   //     //        GradeMaster model = gradeMasterRepository.Table.Where(p => p.GradeMasterID == arg.GradeMasterID).FirstOrDefault();
   //     //        if (model != null)
   //     //        {
   //     //            model.GradeMasterID = arg.GradeMasterID;
   //     //            model.CodeNo = arg.CodeNo;
   //     //            model.Category = arg.Category;
   //     //            model.Designation = arg.Designation;
   //     //            model.Grade = arg.Grade;
   //     //            model.Efficiency = arg.Efficiency;
   //     //            model.CreatedDate = model.CreatedDate;
   //     //            model.UpdatedDate = DateTime.Now;
   //     //            //model.CreatedBy = arg.CreatedBy;
   //     //            string username = HttpContext.Current.Session["UserName"].ToString();                     
   //     //            model.UpdatedBy = username;
   //     //            gradeMasterRepository.Update(model);
   //     //            result = true;
   //     //        }

   //     //        else
   //     //        {
   //     //            result = false;
   //     //        }
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //     //        result = false;
   //     //    }

   //     //    return result;
   //     //}




   //     //public bool Delete(int id)
   //     //{
   //     //    bool result = false;
   //     //    try
   //     //    {
   //     //        GradeMaster model = gradeMasterRepository.GetById(id);
   //     //        gradeMasterRepository.Delete(model);
   //     //        result = true;
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //     //        result = false;
   //     //    }
   //     //    return result;
   //     //}

   //     //public GradeMaster Get(int id)
   //     //{
   //     //    return null;
   //     //}
   //     //public List<GradeMaster>Get()
   //     //{
   //     //    List<GradeMaster> gradeMaster = new List<GradeMaster>();
   //     //    try
   //     //    {
   //     //        gradeMaster = gradeMasterRepository.Table.ToList<GradeMaster>();
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //     //    }
   //     //    return gradeMaster;
   //     //}

   //     public void Dispose()
   //     {
   //         using (MMSContext context = new MMSContext())
   //         {
   //             context.Dispose();
   //         }
   //     }
   // }
}
