using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.StockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class ImirManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ImirForm> imirFormRepository;
        private Repository<ImirGridDetails> imirGridDetailsRepository;

        #region Add/Update/Delete Operation

        public bool Post(ImirForm arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                imirFormRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(ImirForm arg)
        {
            bool result = false;
            try
            {
                ImirForm model = imirFormRepository.Table.Where(p => p.ImirId == arg.ImirId).FirstOrDefault();
                if (model != null)
                {
                    model.ReportNo = arg.ReportNo;
                    model.Date = arg.Date;
                    model.GateEntryNo = arg.GateEntryNo;
                    model.RefInfrepNo = arg.RefInfrepNo;
                    model.GrnNo = arg.GrnNo;
                    model.DcNo = arg.DcNo;
                    model.SupplierMasterId = arg.SupplierMasterId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.ColourMasterId = arg.ColourMasterId;
                    model.Store = arg.Store;
                    model.Uom = arg.Uom;
                    model.InspectionType = arg.InspectionType;
                    model.DcQty = arg.DcQty;
                    model.ReceivedQty = arg.ReceivedQty;
                    model.DcPcs = arg.DcPcs;
                    model.ReceivedPcs = arg.ReceivedPcs;
                    model.QtyInspected = arg.QtyInspected;
                    model.QtyAccepted = arg.QtyAccepted;
                    model.QtyRejected = arg.QtyRejected;
                    model.QtyRejectionPercent = arg.QtyRejectionPercent;
                    model.QtyExcess = arg.QtyExcess;
                    model.PcsInspected = arg.PcsInspected;
                    model.PcsAccepted = arg.PcsAccepted;
                    model.PcsRejected = arg.PcsRejected;
                    model.PcsRejectionPercent = arg.PcsRejectionPercent;
                    model.PcsExcess = arg.PcsExcess;
                    model.Remarks = arg.Remarks;
                    model.InspectedBy = arg.InspectedBy;
                    model.QcExcecutive = arg.QcExcecutive;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    imirFormRepository.Update(model);
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
                ImirForm model = imirFormRepository.GetById(id);
                imirFormRepository.Delete(model);
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

        public ImirManager()
        {
            imirFormRepository = unitOfWork.Repository<ImirForm>();
            imirGridDetailsRepository = unitOfWork.Repository<ImirGridDetails>();
        }

        public ImirForm GetImirId(int ImirId)
        {
            ImirForm imirForm = new ImirForm();
            if (ImirId != 0)
            {
                imirForm = imirFormRepository.Table.Where(x => x.ImirId == ImirId).SingleOrDefault();
            }
            return imirForm;
        }

        public ImirForm Get(int id)
        {
            return null;
        }

        public List<ImirForm> Get()
        {
            List<ImirForm> imirForm = new List<ImirForm>();
            try
            {
                imirForm = imirFormRepository.Table.ToList<ImirForm>();
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return imirForm;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        #endregion

        #region ImirGrid

        public bool Post(ImirGridDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.ImirGridId == 0)
                {
                    imirGridDetailsRepository.Insert(arg);
                    result = true;
                }
                else if (arg.ImirGridId != 0)
                {
                    ImirGridDetails model = imirGridDetailsRepository.Table.Where(x => x.ImirGridId == arg.ImirGridId).FirstOrDefault();
                    model.ImirGridId = arg.ImirGridId;
                    model.SlNo = arg.SlNo;
                    model.ParameterId = arg.ParameterId;
                    model.Parameter = arg.Parameter;
                    model.InspectionFrequency = arg.InspectionFrequency;
                    model.RejectionQty = arg.RejectionQty;
                    model.GridRemarks = arg.GridRemarks;
                    model.ImirId = arg.ImirId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    imirGridDetailsRepository.Update(arg);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public ImirGridDetails GetImirGridId(int ImirGridId)
        {
            ImirGridDetails imirGridDetails = new ImirGridDetails();
            if (ImirGridId != 0)
            {
                imirGridDetails = imirGridDetailsRepository.Table.Where(x => x.ImirGridId == ImirGridId).SingleOrDefault();
            }
            return imirGridDetails;
        }

        public List<ImirGridDetails> GridList()
        {
            List<ImirGridDetails> imirGridDetails = new List<ImirGridDetails>();
            try
            {
                imirGridDetails = imirGridDetailsRepository.Table.ToList<ImirGridDetails>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return imirGridDetails;
        }


        public bool ImirGridDelete(int id)
        {
            bool result = false;
            try
            {
                ImirGridDetails model = imirGridDetailsRepository.GetById(id);
                imirGridDetailsRepository.Delete(model);
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
    }
}
