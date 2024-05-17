using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class FloorRetMaterialManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<FloorMaterial> floorMaterialRepository;
      

        #region Add/Update/Delete Operations

        public int Post(FloorMaterial arg)
        {
            int result = 0;
            try
            {               
                FloorMaterial model = floorMaterialRepository.Table.Where(p => p.FrmNo == arg.FrmNo).FirstOrDefault();
                if (model != null)
                { 
                    model.FrmNo = arg.FrmNo;
                    model.FloorDate = arg.FloorDate;
                    model.Remarks = arg.Remarks;
                    model.Is_IssueNo = arg.Is_IssueNo;
                    model.IssueNo = arg.IssueNo;                
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    floorMaterialRepository.Update(model);
                    result = model.FloorReturnMaterialId;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    floorMaterialRepository.Insert(arg);
                    result = arg.FloorReturnMaterialId;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                
            }
            return result;
        }

        public bool Put(FloorMaterial arg)
        {
            bool result = false;
            try
            {
                FloorMaterial model = floorMaterialRepository.Table.Where(p => p.FloorReturnMaterialId == arg.FloorReturnMaterialId).FirstOrDefault();
                if (model != null)
                {
                    model.FloorReturnMaterialId = arg.FloorReturnMaterialId;
                    model.FrmNo = arg.FrmNo;
                    model.FloorDate = arg.FloorDate;
                    model.Remarks = arg.Remarks;                  
                    model.IssueNo = arg.IssueNo;                    
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    floorMaterialRepository.Update(model);
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
                FloorMaterial model = floorMaterialRepository.GetById(id);
                floorMaterialRepository.Delete(model);
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

        public FloorRetMaterialManager()
        {
            floorMaterialRepository = unitOfWork.Repository<FloorMaterial>();
        }

        public FloorMaterial GetFloorReturnMaterialId(int FloorReturnMaterialId)
        {
            FloorMaterial floorMaterial = new FloorMaterial();
            if (FloorReturnMaterialId != 0)
            {
                floorMaterial = floorMaterialRepository.Table.Where(x => x.FloorReturnMaterialId == FloorReturnMaterialId).SingleOrDefault();
            }
            return floorMaterial;
        }

        public FloorMaterial Get(int id)
        {
            return null;
        }

        public List<FloorMaterial> Get()
        {
           List<FloorMaterial> floorMaterial = new List<FloorMaterial>();
           try
           {
               floorMaterial = floorMaterialRepository.Table.ToList<FloorMaterial>();
           }
           catch (Exception ex)
           {
               Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
           }
           return floorMaterial;
        }
        public List<FloorReturnMaterialGrid> GetFloorReturnGrid(string IssueNo)
        {
            List<FloorReturnMaterialGrid> floorreturnlist = new List<FloorReturnMaterialGrid>();
            try
            {
                floorreturnlist = floorMaterialRepository.SearchFloorReturnList(IssueNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }
        public List<FloorReturnMaterial> GetFloorReturnMaterial(string MaterialName,int IssuplipID)
        {
            List<FloorReturnMaterial> floorreturnlist = new List<FloorReturnMaterial>();
            try
            {
                floorreturnlist = floorMaterialRepository.GetFloorReturnMaterial(MaterialName, IssuplipID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }
        public List<FloorReturnMaterial_Withoutissue_No> GetFloorReturnMaterial_withoutissueno(string Materialname_id)
        {
            List<FloorReturnMaterial_Withoutissue_No> floorreturnlist = new List<FloorReturnMaterial_Withoutissue_No>();
            try
            {
                floorreturnlist = floorMaterialRepository.GetFloorReturnMaterial_withoutissueno(Materialname_id);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }
        public List<FloorReturnMaterialGrid> GetFloorReturnDetails(int FloorReturnMaterialId)
        {
            List<FloorReturnMaterialGrid> floorreturnlist = new List<FloorReturnMaterialGrid>();
            try
            {
                floorreturnlist = floorMaterialRepository.FloorReturnDetails(FloorReturnMaterialId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public List<SizeRangeIssueModelcs> MaterialOpeningSizeRange_withoutissueno(int MaterialNameID)
        {
            List<SizeRangeIssueModelcs> PendingList = new List<SizeRangeIssueModelcs>();
            try
            {
                PendingList = floorMaterialRepository.GetIssueSizerangeWithMaterial(MaterialNameID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        #endregion
    }
}
