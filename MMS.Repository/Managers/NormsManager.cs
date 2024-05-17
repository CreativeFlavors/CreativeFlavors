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
    public class NormsManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Norms> normsRepository;

        #region Add/Update/Delete Operation

        public Norms Post(Norms arg)
        {
            Norms norms = new Norms();
            try
            {
                if (arg.Normsid == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    normsRepository.Insert(arg);
                    norms = arg;
                }
                else if (arg.Normsid != 0)
                {
                    Norms model = normsRepository.Table.Where(p => p.Normsid == arg.Normsid).FirstOrDefault();
                    if (model != null)
                    {
                        model.Normsid = arg.Normsid;
                        model.BuyerNameid = arg.BuyerNameid;
                        model.GroupId = arg.GroupId;
                        model.BuyerOption = arg.BuyerOption;
                        model.IssueNormsid = arg.IssueNormsid;
                        model.PurchaseNormsid = arg.PurchaseNormsid;
                        model.OurNorms = arg.OurNorms;
                        model.NormsPercentage = arg.NormsPercentage;
                        model.NormsPercentage1 = arg.NormsPercentage1;
                        model.NormsPercentage2 = arg.NormsPercentage2;
                        model.NormsPercentage3 = arg.NormsPercentage3;
                        model.CostingNorms = arg.CostingNorms;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        normsRepository.Update(model);
                        norms = model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return norms;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Norms model = normsRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedOn = DateTime.Now;
                model.IsDeleted = true;
                normsRepository.Update(model);
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

        public NormsManager()
        {
            normsRepository = unitOfWork.Repository<Norms>();
        }

        public Norms GetNormsID(int OrderTypeName)
        {
            Norms norms = new Norms();
            if (OrderTypeName != 0 && OrderTypeName != null)
            {
                norms = normsRepository.Table.Where(x => x.Normsid == OrderTypeName).SingleOrDefault();
            }
            return norms;
        }

        public Norms GetGroupID(int Groupid)
        {
            Norms norms = new Norms();
            if (Groupid != 0)
            {
                norms = normsRepository.Table.Where(x => x.GroupId == Groupid).FirstOrDefault();
            }
            return norms;
        }
        public Norms GetGroupIDWithBuyername(int Groupid,int BuyerName)
        {
            Norms norms = new Norms();
            if (Groupid != 0)
            {
                norms = normsRepository.Table.Where(x => x.GroupId == Groupid&&x.BuyerNameid== BuyerName).FirstOrDefault();
            }
            return norms;
        }
        public Norms Get(int id)
        {
            return null;
        }

        public List<Norms> Get()
        {
            List<Norms> Normslist = new List<Norms>();
            try
            {
                Normslist = normsRepository.Table.Where(X => X.IsDeleted == false).ToList<Norms>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Normslist;
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
