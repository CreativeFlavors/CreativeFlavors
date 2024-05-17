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
    public class GateEntryInwardMaterialsManager : IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryInwardMaterialsMaster> gateEntryInwardMaterialsRepo;
        private Repository<GateEntryInwardMaterialsUploadMaster> gateEntryInwardMaterialsUploadRepo;
        public GateEntryInwardMaterialsManager()
        {
            gateEntryInwardMaterialsRepo = unitOfWork.Repository<GateEntryInwardMaterialsMaster>();
            gateEntryInwardMaterialsUploadRepo = unitOfWork.Repository<GateEntryInwardMaterialsUploadMaster>();
        }


        #region Add/Update/Delete Operation
        public int Post(GateEntryInwardMaterialsMaster arg)
        {
            int result = 0;
            try
            {
                if (arg.GateEntryInwardId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryInwardMaterialsRepo.Insert(arg);
                    result = arg.GateEntryInwardId;
                    
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryInwardMaterialsRepo.Update(arg);
                    result = arg.GateEntryInwardId;
                    return result;

                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int PostUpload(GateEntryInwardMaterialsMaster arg, List<string> listOfAttachment)
        {
            int result = 0;
            try
            {
                if (arg.GateEntryInwardId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryInwardMaterialsRepo.Insert(arg);
                    result = arg.GateEntryInwardId;

                    foreach (var list in listOfAttachment)
                    {

                        GateEntryInwardMaterialsUploadMaster insertUpload = new GateEntryInwardMaterialsUploadMaster();
                        insertUpload.CreatedBy = username;
                        insertUpload.CreatedDate = DateTime.Now;
                        insertUpload.IsDeleted = false;
                        insertUpload.UploadPath = list;
                        insertUpload.GateEntryInwardId = arg.GateEntryInwardId;
                        gateEntryInwardMaterialsUploadRepo.Insert(insertUpload);
                    }




                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryInwardMaterialsRepo.Update(arg);
                    result = arg.GateEntryInwardId;
                    

                    

                    foreach(var list in listOfAttachment)
                    {

                        GateEntryInwardMaterialsUploadMaster insertUpload = new GateEntryInwardMaterialsUploadMaster();
                        insertUpload.CreatedBy = username;
                        insertUpload.CreatedDate = DateTime.Now;
                        insertUpload.IsDeleted = false;
                        insertUpload.UploadPath = list;
                        insertUpload.GateEntryInwardId = arg.GateEntryInwardId;
                        gateEntryInwardMaterialsUploadRepo.Insert(insertUpload);
                    }



                    return result;



                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }

        public int Post_sheet(GateEntryInwardMaterialsMaster arg)
        {
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                
                   
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryInwardMaterialsRepo.Insert(arg);
                   // result = arg.GateEntryInwardId;
                    return arg.GateEntryInwardId;
              
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int Post_sheet_Update(GateEntryInwardMaterialsMaster arg)
        {
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                 GateEntryInwardMaterialsMaster model = gateEntryInwardMaterialsRepo.Table.Where(p => p.GateEntryInwardId == arg.GateEntryInwardId).FirstOrDefault();
                    model.HSNCode = arg.HSNCode;
                    model.UpdatedBy = username;
                    model.UpdatedDate = DateTime.Now;

                    model.InwardEntryDateTime = System.DateTime.Now;
                    model.IsReturned = arg.IsReturned;
                    model.IsNewInward = arg.IsNewInward;
                    model.IsJobWork = arg.IsJobWork;
                    model.GateEntryNo = arg.GateEntryNo;
                    model.InwardMaterialType = arg.InwardMaterialType;
                    model.DcRefNo = arg.DcRefNo;
                    model.DcRefDate = arg.DcRefDate;
                    model.Company = arg.Company;
                    model.MaterialName = arg.MaterialName;
                    model.UnitDivision = arg.UnitDivision;
                    model.ReturnType = arg.ReturnType;
                    model.MaterialType = arg.MaterialType;
                    model.DcNo = arg.DcNo;
                    model.DcDate = arg.DcDate;
                    model.InvoiceNo = arg.InvoiceNo;
                    model.InvoiceDate = arg.InvoiceDate;
                    model.ModeofTransport = arg.ModeofTransport;
                    model.InvoiceCurrency = arg.InvoiceCurrency;
                    model.InvoiceValue = arg.InvoiceValue;
                    model.VehicleNo = arg.VehicleNo;
                    model.PoNoId = arg.PoNoId;
                    //  model.SupplierId = arg.SupplierId;
                    //  model.StoresId = arg.StoresId;
                    model.MaterialNameId = arg.MaterialNameId;
                    model.HSNCode = arg.HSNCode;
                    model.SizeRangeId = arg.SizeRangeId;
                    model.UomId = arg.UomId;
                    model.Pcs = arg.Pcs;
                    model.ReceivedBy = arg.ReceivedBy;
                    model.AcknowledgedBy = arg.AcknowledgedBy;
                    model.Remarks = arg.Remarks;
                    model.Quantity = arg.Quantity;
                    //model.TotalQty = arg.TotalQty;

                    model.Value = arg.Value;
                    model.DcTotal = arg.DcTotal;
                    model.ArrivedTotal = arg.ArrivedTotal;
                    model.InwardPo = arg.InwardPo;
                    model.GateEntryInwardId = arg.GateEntryInwardId;
                    model.PoTotal = arg.PoTotal;
                    model.PendingQuantity = arg.PendingQuantity;
                    //    model.GateEntryInwardId = arg.GateEntryInwardId;

                    gateEntryInwardMaterialsRepo.Update(model);
                    result = arg.GateEntryInwardId;
                    return result;
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }

        public bool Delete(int GateEntryInwardId)
        {
            bool result = false;
            try
            {
                GateEntryInwardMaterialsMaster model = gateEntryInwardMaterialsRepo.GetById(GateEntryInwardId);
                model.IsDeleted = true;
                gateEntryInwardMaterialsRepo.Update(model);
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
        public GateEntryInwardMaterialsMaster GetGateEntryInMaterialById(int GateEntryInwardId)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            if (GateEntryInwardId != 0)
            {
                gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(x => x.GateEntryInwardId == GateEntryInwardId).SingleOrDefault();
            }
            return gateEntryInward;
        }
      
        public GateEntryInwardMaterialsMaster GetGateEntryPendingMaterial(int MaterialID, int PoorderID)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(x => x.MaterialNameId == MaterialID && x.PoNoId == PoorderID && x.PendingQuantity != "0").FirstOrDefault();

            return gateEntryInward;
        }
        public GateEntryInwardMaterialsMaster GetGateEntryPendinglist(int MaterialID, int PoorderID)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            gateEntryInward= gateEntryInwardMaterialsRepo.GateEntryPending(MaterialID, PoorderID);
           // gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(x => x.MaterialNameId == MaterialID && x.PoNoId == PoorderID).OrderByDescending(x=>x.CreatedDate).Take(1).FirstOrDefault();

            return gateEntryInward;
        }

        public List<GateEntryInwardMaterialsMaster> GetGateEntry()
        {
            List<GateEntryInwardMaterialsMaster> inwardMateriallist = new List<GateEntryInwardMaterialsMaster>();
            try
            {
                inwardMateriallist = gateEntryInwardMaterialsRepo.Table.Where(X => X.IsDeleted == false && X.IsJobWork == true).ToList<GateEntryInwardMaterialsMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return inwardMateriallist;
        }


        public List<GateEntryInwardMaterialsMaster> Get()
        {
            List<GateEntryInwardMaterialsMaster> gateEntryInwardlist = new List<GateEntryInwardMaterialsMaster>();
            try
            {
                gateEntryInwardlist = gateEntryInwardMaterialsRepo.Table.Where(x => x.IsDeleted == false).ToList<GateEntryInwardMaterialsMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateEntryInwardlist;
        }

        public List<GateEntryInwardMaterialsMaster> GEMaterialListGridBasedOnPoNo(string GateEntryNo)
        {
            List<GateEntryInwardMaterialsMaster> ApprovedPriceList = new List<GateEntryInwardMaterialsMaster>();

            try
            {
                ApprovedPriceList = gateEntryInwardMaterialsRepo.Table.Where(x => x.GateEntryNo == GateEntryNo && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }

        
        public List<GateEntryInwardMaterialsUploadMaster> GetGateEntryInwordMaterialuploaddatalist(int GateEntryInwardId)
        {
            List<GateEntryInwardMaterialsUploadMaster> list = new List<GateEntryInwardMaterialsUploadMaster>();
            try
            {
                list = gateEntryInwardMaterialsUploadRepo.Table.Where(x => x.GateEntryInwardId == GateEntryInwardId && x.IsDeleted == false).ToList();
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return list;
        }

        public List<GateEntryInwardMaterialsMaster> GEMaterialListGridBasedOnGateEntryNo_(string GateEntryNo)
        {
            List<GateEntryInwardMaterialsMaster> ApprovedPriceList = new List<GateEntryInwardMaterialsMaster>();

            try
            {
                ApprovedPriceList = gateEntryInwardMaterialsRepo.Table.Where(x => x.GateEntryNo == GateEntryNo && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }
        public List<GateEntryInwardMaterialsMaster> GEMaterialListGridBasedOnGateEntryNo(int GateEntryNo)
        {
            List<GateEntryInwardMaterialsMaster> ApprovedPriceList = new List<GateEntryInwardMaterialsMaster>();

            try
            {
                ApprovedPriceList = gateEntryInwardMaterialsRepo.Table.Where(x => x.GateEntryInwardId == GateEntryNo && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }
        public GateEntryInwardMaterialsMaster GetGateEntryInMaterialById_pendinglist(int MaterialNameId,string DcRefNo)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            if (MaterialNameId != 0)
            {
                gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.MaterialNameId == MaterialNameId && X.DcRefNo == DcRefNo).ToList().OrderBy(X=>X.GateEntryInwardId).ToList().LastOrDefault();
            }
            return gateEntryInward;
        }
        public GateEntryInwardMaterialsMaster GetGateEntryInMaterialById_pendinglist_jobsheet_pair_Code_id(int MaterialNameId, string DcRefNo, int Process_Name, int jobsheet_pair_Code_id)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            if (MaterialNameId != 0)
            {
                try
                {
                    gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.MaterialNameId == MaterialNameId && X.DcRefNo == DcRefNo && X.Process_Name ==
                    (Process_Name) && X.jobsheet_pair_Code_id == (jobsheet_pair_Code_id)).ToList().OrderBy(X => X.GateEntryInwardId).ToList().LastOrDefault();
                }
                catch (Exception ex)
                {

                }
            }
            return gateEntryInward;
        }
        public GateEntryInwardMaterialsMaster Getpendinglist_jobsheet_pair_Code_id_Production(int MaterialNameId, string DcRefNo)
        {
            GateEntryInwardMaterialsMaster gateEntryInward = new GateEntryInwardMaterialsMaster();
            if (MaterialNameId != 0)
            {
                try
                {
                    gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.ComponentId == MaterialNameId && X.DcRefNo == DcRefNo ).ToList().OrderBy(X => X.GateEntryInwardId).ToList().LastOrDefault();
                }
                catch (Exception ex)
                {

                }
            }
            return gateEntryInward;
        }
        public List<GateEntryInwardMaterialsMaster> GetGateEntryInMaterialById_pendinglist_sumqty(int MaterialNameId, string DcRefNo)
        {
            List <GateEntryInwardMaterialsMaster> gateEntryInward = new List<GateEntryInwardMaterialsMaster>();
            if (MaterialNameId != 0)
            {
                gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.MaterialNameId == MaterialNameId && X.DcRefNo == DcRefNo).OrderBy(X => X.GateEntryInwardId).ToList();
            }
            return gateEntryInward;
        }
        public List<GateEntryInwardMaterialsMaster> GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(int MaterialNameId, string DcRefNo, int Process_Name, int jobsheet_pair_Code_id)
        {
            List<GateEntryInwardMaterialsMaster> gateEntryInward = new List<GateEntryInwardMaterialsMaster>();

            try
            {
                if (MaterialNameId != 0)
                {
                    gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.MaterialNameId == MaterialNameId && X.DcRefNo == DcRefNo && X.Process_Name == (Process_Name) && X.jobsheet_pair_Code_id == (jobsheet_pair_Code_id)).ToList().OrderBy(X => X.GateEntryInwardId).ToList();
                }
               
            }
            catch (Exception ex)
            {

                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return gateEntryInward;
        }
        public List<GateEntryInwardMaterialsMaster> Getpendinglist_sumqty_jobsheet_pair_Code_id_Production(int MaterialNameId, string DcRefNo)
        {
            List<GateEntryInwardMaterialsMaster> gateEntryInward = new List<GateEntryInwardMaterialsMaster>();
            if (MaterialNameId != 0)
            {
                gateEntryInward = gateEntryInwardMaterialsRepo.Table.Where(X => X.MaterialNameId == MaterialNameId && X.DcRefNo == DcRefNo ).ToList().OrderBy(X => X.GateEntryInwardId).ToList();
            }
            return gateEntryInward;
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
