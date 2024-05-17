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
    public  class GateEntryInwardDocumentManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryInwardDocumentMaster> gateEntryInwardDocumentRepository;
        private Repository<GateEntryInwardDocumentUploadMaster> gateEntryInwardDocumentUploadRepository;
        private Repository<GateEntryInwardMaterialsMaster> gateEntryInwardRepository;

        private UnitOfWork2 unitOfWork2 = new UnitOfWork2();
        private Repository2<EmployNameMaster> empNameRepository;
        private Repository2<EmployDepartmentMaster> empDeptRepository;

        public GateEntryInwardDocumentManager()
        {
            gateEntryInwardDocumentRepository = unitOfWork.Repository<GateEntryInwardDocumentMaster>();
            gateEntryInwardDocumentUploadRepository = unitOfWork.Repository<GateEntryInwardDocumentUploadMaster>();
            gateEntryInwardRepository = unitOfWork.Repository<GateEntryInwardMaterialsMaster>();
            empNameRepository = unitOfWork2.Repository<EmployNameMaster>();
            empDeptRepository = unitOfWork2.Repository<EmployDepartmentMaster>();
        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryInwardDocumentMaster arg,List<string> listOfAttachment)
        {
            bool result = false;
            try
            {
                if (arg.GateEntryDocumentId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryInwardDocumentRepository.Insert(arg);

                    foreach(var list in listOfAttachment)
                    {
                        
                        GateEntryInwardDocumentUploadMaster insertUpload = new GateEntryInwardDocumentUploadMaster();
                        insertUpload.CreatedBy = username;
                        insertUpload.CreatedDate = DateTime.Now;
                        insertUpload.IsDeleted = false;
                        insertUpload.UploadPath = list;
                        insertUpload.GateEntryDocumentId = arg.GateEntryDocumentId;
                        gateEntryInwardDocumentUploadRepository.Insert(insertUpload);
                    }
                    




                    result = true;
                    return result;
                }
                else
                {
                    GateEntryInwardDocumentMaster model = gateEntryInwardDocumentRepository.Table.Where(p => p.GateEntryDocumentId == arg.GateEntryDocumentId).FirstOrDefault();
                    if (model != null)
                    {
                        model.GateEntryDocumentId = arg.GateEntryDocumentId;
                        model.GateEntryNo = arg.GateEntryNo;
                        model.EntryDateTime = arg.EntryDateTime;
                        model.InwardDocTypeId = arg.InwardDocTypeId;
                        model.Mode = arg.Mode;
                        model.Company = arg.Company;
                        model.FromWhere = arg.FromWhere;
                        model.Unit = arg.Unit;
                        model.PersonName = arg.PersonName;
                        model.ModeofTransport = arg.ModeofTransport;
                        model.VehicleNo = arg.VehicleNo;
                        model.AddressToWhom = arg.AddressToWhom;
                        model.HandOverTo = arg.HandOverTo;
                        model.ReceivedBy = arg.ReceivedBy;
                        model.Remarks = arg.Remarks;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        model.UpdatedDate = DateTime.Now;
                        model.TagName = arg.TagName;

                        gateEntryInwardDocumentRepository.Update(model);



                        foreach (var list in listOfAttachment)
                        {

                            GateEntryInwardDocumentUploadMaster insertUpload = new GateEntryInwardDocumentUploadMaster();
                            insertUpload.CreatedBy = username;
                            insertUpload.CreatedDate = DateTime.Now;
                            insertUpload.IsDeleted = false;
                            insertUpload.UploadPath = list;
                            insertUpload.GateEntryDocumentId = arg.GateEntryDocumentId;
                            gateEntryInwardDocumentUploadRepository.Insert(insertUpload);
                        }





                        result = true;
                    }
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

        public bool Delete(int GateEntryDocumentId)
        {
            bool result = false;
            try
            {
                GateEntryInwardDocumentMaster model = gateEntryInwardDocumentRepository.GetById(GateEntryDocumentId);
                model.IsDeleted = true;
                gateEntryInwardDocumentRepository.Update(model);
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
        public GateEntryInwardDocumentMaster GetGateEntryDocumentById(int GateEntryDocumentId)
        {
            GateEntryInwardDocumentMaster gateEntryDocument = new GateEntryInwardDocumentMaster();
            if (GateEntryDocumentId != 0)
            {
                gateEntryDocument = gateEntryInwardDocumentRepository.Table.Where(x => x.GateEntryDocumentId == GateEntryDocumentId).SingleOrDefault();
            }
            return gateEntryDocument;
        }

        public List<GateEntryInwardDocumentUploadMaster> GetGateEntryDocumentuploadById(int GateEntryDocumentId)
        {
            List <GateEntryInwardDocumentUploadMaster> listgateEntryDocumentupload = new List<GateEntryInwardDocumentUploadMaster>();
            if(GateEntryDocumentId!=0)
            {
                listgateEntryDocumentupload = gateEntryInwardDocumentUploadRepository.Table.Where(x => x.GateEntryDocumentId == GateEntryDocumentId).ToList();
            }
            return listgateEntryDocumentupload;
        }


        public GateEntryInwardDocumentMaster GetGateEntryDocumentByNo(string GateEntryNo)
        {
            GateEntryInwardDocumentMaster gateEntryDocument = new GateEntryInwardDocumentMaster();
            if (GateEntryNo != null)
            {
                gateEntryDocument = gateEntryInwardDocumentRepository.Table.Where(x => x.GateEntryNo == GateEntryNo).SingleOrDefault();
            }
            return gateEntryDocument;
        }

        public List<EmployNameMaster> GetEmployNameList()
        {
            List<EmployNameMaster> employNameList = new List<EmployNameMaster>();
            try
            {
                employNameList = empNameRepository.Table.Where(x => x.IsDeleted == false).ToList<EmployNameMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employNameList;
        }



        public List<EmployDepartmentMaster> GetEmployDeptList()
        {
            List<EmployDepartmentMaster> employDeptList = new List<EmployDepartmentMaster>();
            try
            {
                employDeptList = empDeptRepository.Table.Where(x => x.IsDeleted == false).ToList<EmployDepartmentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.InnerException.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.StackTrace.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.TargetSite.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.HResult.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employDeptList;
        }

        public List<GateEntryInwardDocumentMaster> Get()
        {
            List<GateEntryInwardDocumentMaster> gateentrydocumentlist = new List<GateEntryInwardDocumentMaster>();
            try
            {
                gateentrydocumentlist = gateEntryInwardDocumentRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryInwardDocumentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateentrydocumentlist;
        }

        #endregion
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public List<GateEntryInwardMaterialsMaster> GetMaterials(int gateEntryNo)
        {
            List<GateEntryInwardMaterialsMaster> gateMaterials = new List<GateEntryInwardMaterialsMaster>();
            try
            {
               var  gateMaterials_ = gateEntryInwardRepository.Table.Where(x => x.GateEntryInwardId == gateEntryNo).FirstOrDefault();
                 gateMaterials = gateEntryInwardRepository.Table.Where(x => x.GateEntryNo == gateMaterials_.GateEntryNo).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateMaterials;
        }

        public string GetDCNo(string gateEntryNo)
        {
            string gateMaterials = "";
            try
            {
                gateMaterials = gateEntryInwardRepository.Table.Where(x => x.GateEntryNo == gateEntryNo).Select(x => x.DcRefNo).Distinct().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateMaterials;
        }
        public string GetDCNo_(int gateEntryNo)
        {
            string gateMaterials = "";
            try
            {
                gateMaterials = gateEntryInwardRepository.Table.Where(x => x.GateEntryInwardId == gateEntryNo).Select(x => x.DcRefNo).Distinct().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateMaterials;
        }
        public GateEntryInwardMaterialsMaster Get_Gate_detail(int GateEntryInwardId)
        {
            GateEntryInwardMaterialsMaster Gate_detail = new GateEntryInwardMaterialsMaster();
            try
            {
                Gate_detail = gateEntryInwardRepository.Table.Where(x => x.GateEntryInwardId == GateEntryInwardId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Gate_detail;
        }
        public GateEntryInwardMaterialsMaster GetTotalQty(int gateEntryNo,int materialNameId)
        {
            GateEntryInwardMaterialsMaster gateMaterials=new GateEntryInwardMaterialsMaster();
            try
            {
                var gateMaterials1 = gateEntryInwardRepository.Table.Where(x => x.GateEntryInwardId == gateEntryNo && x.MaterialNameId== materialNameId).Select(x=>new { TotalQty=x.TotalQty, Quantity=x.Quantity, PendingQuantity=x.PendingQuantity,Rate=x.Rate, DcTotal=x.DcTotal, IssueSlipId=x.IssueSlipId, Pcs = x.Pcs }).FirstOrDefault();

                gateMaterials.TotalQty = gateMaterials1.TotalQty;
                gateMaterials.Quantity = gateMaterials1.Quantity;
                gateMaterials.PendingQuantity = gateMaterials1.PendingQuantity;
                gateMaterials.Rate = gateMaterials1.Rate;
                gateMaterials.DcTotal = gateMaterials1.DcTotal;
                gateMaterials.IssueSlipId = gateMaterials1.IssueSlipId;
                gateMaterials.Pcs = gateMaterials1.Pcs;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateMaterials;
        }
    }
}
