using MMS.Core;
using MMS.Core.Entities.JobWork;
using MMS.Data.Context;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Data.StoredProcedureModel;
using MMS.Web.Models.StockModel;
using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;

namespace MMS.Data
{
   public class RepositoryJob<T> where T : BaseEntity
    {
        private readonly MMSContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;
        public RepositoryJob(MMSContext context)
        {
            this.context = context;
        }
        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public List<T> GetAll()
        {
            return this.Entities.ToList<T>();
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.entities.Attach(entity);
                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }
        public List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> SearchApprovedPriceList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>("Execute JobWorker_Name ").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> SearchJobwork_Aprovrdlist(int JW_Name)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>("Execute Job_ApprovedPriceListGrid @JW_Name ={0}", JW_Name).ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> Jw_JobSheet_pair(int jobsheet_pair_Code_id)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>("Execute Job_sheet_pair_edit_table_ @jobsheet_pair_Code_id ={0}", jobsheet_pair_Code_id).ToList();
            return ResultList;
        }
     
        public int Jw_JobSheet_pair_delete(int jobsheet_pair_Code_id)
        {
            var ResultList = context.Database.ExecuteSqlCommand("Execute Job_sheet_pair_Delete @jobsheet_pair_Code_id ={0}", jobsheet_pair_Code_id);
            return ResultList;
        }
        public int Update_QcStage_Sp(int stage, string Lot_no,string Io,string type)
        {
            var ResultList = context.Database.ExecuteSqlCommand("Execute QC_Stage_Update @stage ={0}, @Lot_no ={1},@Io ={2},@type={3}", stage, Lot_no, Io, type);
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.JobProductionGrid> JobProductionGrid()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.JobProductionGrid>("Execute Job_production_grid ").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.CuttingSlipModel> CutingSlip_List_Grid (int LotNo, int BuyerName, int  BuyerSeason, int MaterialCategoryMasterId, int MaterialGroupMasterId,int IoNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.CuttingSlipModel>("Execute spCuttingSlip @LotNo={0}, @BuyerName ={1},@BuyerSeason ={2},@MaterialCategoryMasterId ={3},@MaterialGroupMasterId ={4},@IoNo={5}", LotNo, BuyerName, BuyerSeason, MaterialCategoryMasterId, MaterialGroupMasterId, "").ToList();
            return ResultList;
        }
       
        public List<MMS.Data.StoredProcedureModel.JobOtherWorkList> JobOtherWorkList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.JobOtherWorkList>("Execute Job_Other_Work_List ").ToList();
            return ResultList;
        }

    }
}
