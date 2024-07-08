using MMS.Core;
using MMS.Core.Entities.Stock;
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
using System.Diagnostics;
using System.Security.Cryptography;

//using MMS.Web.Models;

namespace MMS.Data
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly MMSContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(MMSContext context)
        {
            this.context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public List<T> GetAll()
        {
            //return this.Entities.ToList<T>();
            var entities = this.Entities.ToList<T>();

            // Update the UpdatedDate property for each entity retrieved
            foreach (var entity in entities)
            {
                // Check if the entity has an UpdatedDate property
                var updatedDateProperty = typeof(T).GetProperty("UpdatedDate");
                if (updatedDateProperty != null)
                {
                    // Set UpdatedDate to current date and time
                    updatedDateProperty.SetValue(entity, DateTime.Now);
                }
            }

            return entities;
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
        public virtual int UpdateHierarchy(string storeProcedure, string query, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(storeProcedure, parameters);
        }
        public List<MMS.Web.Models.SPBomMaterialList> BOMMaterial_delete(int BOMNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC BOMMaterial_delete @BOMID={0}", BOMNo).ToList();

            return ResultList;
        }

        public List<MMS.Web.Models.SPBomMaterialList> BOMMaterial_Insert_parentmaterial(int BOMNo, int Parent)
        {
            string query = $"SELECT * FROM BOMMaterial_Insert(@p0, @p1)";
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>(query, BOMNo, Parent).ToList();

            return ResultList;
        }
        //public List<MMS.Web.Models.SPBomMaterialList> BOMMaterial_Insert_parentmaterial(int BOMNo, int Parent)
        //{
        //    string query = $"EXEC BOMMaterial_Insert @BOMID={BOMNo},@Parent={Parent}";
        //    var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>(query).ToList();

        //    return ResultList;
        //}
        public List<MMS.Data.StoredProcedureModel.Job_LetherCode> Jw_LetherGet(string Job_Lether_to_lether_id)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Job_LetherCode>("Execute Job_Order_report @Job_Lether_to_lether_id ={0}", Job_Lether_to_lether_id).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> BOMMaterial_Insert_parentmaterial_common(int BOMNo, int Parent)
        {
            //string query = $"EXEC BOMMaterial_common_Insert @BOMID={BOMNo},@Parent={Parent}";
            //var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>(query).ToList();
            string query = $"select * from BOMMaterial_Insert(@p0, @p1)";

            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>(query, BOMNo, Parent).ToList();
            return ResultList;
        }
        public List<SpResult> ExecWithStoreProcedure(string query, int ApprovedMasterId)
        {
            var ResultList = context.Database.SqlQuery<SpResult>("EXEC spSupplierWithMaterials @MaterialMasterID={0}", ApprovedMasterId).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> BomMaterialList(string MRPNO)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC RaisePO @MRPNO={0}", MRPNO).ToList();

            return ResultList;
        }
        public List<MMS.Web.Models.subspbommateriallist> subBomMaterialList(string MRPNO)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.subspbommateriallist>("SELECT * FROM RaisePO(@p0)", MRPNO).ToList();
            return ResultList;
        }
        //public List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> GetMaterialDropDown()
        //{
        //    try
        //    {
        //        var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.MaterialDropDownmodel>("EXEC MaterialDropDown").ToList();
        //        return ResultList;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        public List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> GetMaterialDropDown()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.MaterialDropDownmodel>("select * from materialdropdown()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int Delete_TempSize(int GateEntryInwardId, int MaterialId)
        {
            try
            {

                var ResultList = context.Database.ExecuteSqlCommand("EXEC Delete_TempSize @GateEntryInwardId={0},@MaterialId={1}", GateEntryInwardId, MaterialId);
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.MaterialSearch> SearchMaterial(string MaterialDescription)
        {

            //var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.MaterialSearch>("Execute MaterialSearch @MaterialDescrption={0}", MaterialDescription).ToList();
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.MaterialSearch>("select * from materialsearch(@p0)", MaterialDescription).ToList();
            return ResultList;

        }
        public MMS.Data.StoredProcedureModel.ItemMaterial GetMaterial(int MaterialID)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.ItemMaterial>("select * from GetMaterialName(@p0)", MaterialID).FirstOrDefault();

                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<MMS.Core.Entities.Stock.Bom> SearchBomMaterialList(string MaterialDescription)
        {
            //var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.Bom>("Execute spSearchBom @Bom ={0}", MaterialDescription).ToList();
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.Bom>("SELECT * FROM spSearchBom(@p0)", MaterialDescription).ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.MRP_Details> MRP_DetailsList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.MRP_Details>("SELECT * FROM get_mrp_details()").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.Salesorder_Grid> Get_Salesorder_Grid()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Salesorder_Grid>("SELECT * FROM salesorder_Grid()").ToList();
            return ResultList;
        }
        public MMS.Data.StoredProcedureModel.Salesorder_Grid salesorder_gridSearch(int SOid)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Salesorder_Grid>("SELECT * FROM salesorder_gridSearch(@p0)", SOid).FirstOrDefault();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.Parentbommatertial> SearchBomMaterialList1(int bom_id)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Parentbommatertial> ("SELECT * FROM get_bom_materials(@p0)",bom_id).ToList();
                return ResultList;
            }
          catch(Exception ex)   
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.SalesorderCart> SearchSalesordercart(int customerid)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.SalesorderCart>("SELECT * FROM get_salesorders_cart(@p0)", customerid).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MMS.Data.StoredProcedureModel.IndentCartsp> Getindentcart()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.IndentCartsp>("SELECT * FROM get_indentcart()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.mrp_material_list> mrpMaterialList1(int? productid)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.mrp_material_list>("SELECT * FROM get_mrp_material_list(@p0)", productid).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.mrp_subassembly_list> get_mrp_subassembly_list(int? productid)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.mrp_subassembly_list>("SELECT * FROM get_mrp_subassembly_list(@p0)", productid).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.subassemblydata> SearchsubassemblyList(int bom_id)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.subassemblydata>("SELECT * FROM get_subassembly_materials(@p0)", bom_id).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Core.Entities.Stock.StockAdjustmentGridDetails> SearchBookStockList(int Store, DateTime From, int Category, int Group, int MaterialType)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.StockAdjustmentGridDetails>("Execute spStockStatementwithSizeRange28 @StoreNo={0}, @From ={1},@Category ={2},@Group ={3},@MaterialType ={4}", Store, From, Category, Group, MaterialType).ToList();
            return ResultList;
        }

        public List<MMS.Data.StoredProcedureModel.IssueSlip_SingleModel> SearchMultipleIssueSlip(string IssueSlipNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.IssueSlip_SingleModel>("select * from MultipleIssueSlipGrid(@P0)", IssueSlipNo).ToList();
            return ResultList;
        }

        public List<MMS.Data.StoredProcedureModel.IssueSlip_SingleModel> SearchJobWorkMultipleIssueSlip(string IssueSlipNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.IssueSlip_SingleModel>("Execute MultipleIssueSlipGrid @IssueSlipNo ={0},@IsJobWork=1", IssueSlipNo).ToList();
            return ResultList;
        }

        public List<MMS.Data.StoredProcedureModel.ContactDetailsSupplierModel> SearchContactSupplierList(string suppliername)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.ContactDetailsSupplierModel>("Execute ContactDetailsCaptureGrid @suppliername ={0}", suppliername).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> SearchInternalOrderList(string BuyerOrderSNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM internalordergrid(@p0)", BuyerOrderSNo).ToList();
            return ResultList;
        }
        public MMS.Core.Entities.Stock.OrderEntry GetOrderEntryId(int OrderEntryId)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_order_entry_data_by_id(@p0)", OrderEntryId).SingleOrDefault();
            return ResultList;
        }
        public MMS.Core.Entities.Stock.OrderEntry GetOrderEntryIdbyFilter(int OrderEntryId, int ProcessStatus)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_order_entry_data_by_id_filter(@p0,@p1)", OrderEntryId, ProcessStatus).SingleOrDefault();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> SearchBuyerOrderList(string BuyerOrderSNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM buyerordergrid(@p0)", BuyerOrderSNo).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.GetBuyerOrderlist> getbuyOrderList(int buyername)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.GetBuyerOrderlist>("SELECT * FROM get_order_details(@p0)", buyername).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.GetBuyerOrderlist> getbuyOrderListdropdown()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.GetBuyerOrderlist>("SELECT * FROM get_order_details_dropdown()").ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.getbuyerorderaddressdetails> getbuyerorderaddressdetails(int orderid)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.getbuyerorderaddressdetails>("SELECT * FROM getBuyerAddressDetails(@p0)", orderid).ToList();
            return ResultList;
        }
        //public List<MMS.Core.Entities.Stock.InternalOrderForm> SearchBuyerOrderList(string BuyerOrderSNo)
        //{
        //    var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.InternalOrderForm>("Execute BuyerOrderGrid @BuyerOrderSNo ={0}", BuyerOrderSNo).ToList();
        //    return ResultList;
        //}
        public List<MMS.Core.Entities.Stock.GRN_EntityModel> SearchGrnList(string GrnNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.GRN_EntityModel>("Execute GRNGrid @GrnNo ={0}", GrnNo).ToList();
            return ResultList;
        }

        public List<MMS.Core.Entities.Stock.ApprovedPriceListMasterGrid> SearchApprovedPriceList(string SupplierName)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.ApprovedPriceListMasterGrid>("select * from ApprovedPriceListGrid(@p0)", SupplierName).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<MMS.Core.Entities.SupplierMaster> SearchSupplierList(string SupplierName)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.SupplierMaster>("Execute SupplierMasterGrid @SupplierName ={0}", SupplierName).ToList();
            return ResultList;
        }

        public List<FloorReturnMaterialGrid> SearchFloorReturnList(string IssueNo)
        {
            var ResultList = context.Database.SqlQuery<FloorReturnMaterialGrid>("Execute FloorReturnGrid @IssueNo ={0}", IssueNo).ToList();
            return ResultList;
        }
        public List<FloorReturnMaterialGrid> FloorReturnDetails(int FloorReturnMaterialId)
        {
            var ResultList = context.Database.SqlQuery<FloorReturnMaterialGrid>("Execute FloorReturnDetails @FloorReturnMaterialId ={0}", FloorReturnMaterialId).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> IssueSlipMaterialList(int BomNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC Materiallist_Issue @BomNo={0}", BomNo).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.InteranalBuyerNo> GetInternalOrderList(string LotNo, string SeasonID)
        {

            var ResultList = context.Database.SqlQuery<MMS.Web.Models.InteranalBuyerNo>("Select * from getinternalorderlist(@p0,@p1)", LotNo, Convert.ToInt32(SeasonID)).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> BOMMaterialSearch(string Materialname, string BOMNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC BOMMMaterialSearch @MaterialDescrption={0},@bomNo={1}", Materialname, BOMNo).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> IssueSlipMaterialList_(string BomNo)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC Materiallist_MultipleIssue @BomNo={0}", BomNo).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.SPBomMaterialList> IssueSlipMaterialListforDetails(string BomNo, string MaterialName, string Color)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC spMultipleIssuesDetails @Bom={0} ,@MaterialName ={1}, @Color={2}  ", BomNo, MaterialName, Color).ToList();
            return ResultList;
        }
        public List<SPMultipleIssueCount> MultipleIssuesCount(string OrderNo, string MaterialName)
        {
            var ResultList = context.Database.SqlQuery<SPMultipleIssueCount>("Execute spMultipleIssueCount @OrderNo={0},@MaterialName= {1}", OrderNo, MaterialName).ToList();
            return ResultList;
        }
        public List<OpeningStockPinkModel> OpeningStockPink()
        {
            var ResultList = context.Database.SqlQuery<OpeningStockPinkModel>("Execute PinCard").ToList();
            return ResultList;
        }
        public List<OpeningStockPinkModel> OpeningMaterialStockPink(int Grn_MaterialID)
        {
            var ResultList = context.Database.SqlQuery<OpeningStockPinkModel>("Execute MaterialPinCard @Grn_MaterialID={0}", Grn_MaterialID).ToList();
            return ResultList;
        }
        public List<MaterialMaster> MaterialList()
        {
            var ResultList = context.Database.SqlQuery<MaterialMaster>("Execute GetMaterialAllList").ToList();
            return ResultList;
        }
        public List<SizeRangeQtyRate> GetAllOrderSize()
        {
            var ResultList = context.Database.SqlQuery<SizeRangeQtyRate>("Execute GetAllOrderSize").ToList();
            return ResultList;
        }
        public List<SizeRangeQtyRate> GetAllOrderSize_ProductionJob(string @OrderEntryId)
        {
            var ResultList = context.Database.SqlQuery<SizeRangeQtyRate>("Execute Job_GetAllOrderSize_Production @OrderEntryId={0}", OrderEntryId).ToList();
            return ResultList;
        }
        public List<IndentMaterial> GetBOMMaterialbySupplier(int SupplierMasterID, int IndentID)
        {

            if (SupplierMasterID == 0 && IndentID != 0)
            {
                var ResultList = context.Database.SqlQuery<IndentMaterial>("Execute PurchaseOrderByIndentMaterial @IndentID={0}", IndentID).ToList();
                return ResultList;
            }
            else
            {
                var ResultList = context.Database.SqlQuery<IndentMaterial>("Execute PurchaseOrderByBOMMaterial @SupplierMasterID={0},@IndentID={1}", SupplierMasterID, IndentID).ToList();
                return ResultList;
            }

        }

        public List<IndentMaterial> GetBOMMaterialbySupplier(int SupplierMasterID, string IndentID)
        {
            List<IndentMaterial> list = new List<IndentMaterial>();

            if (SupplierMasterID == 0 && IndentID != "")
            {
                list = context.Database.SqlQuery<IndentMaterial>("Execute PurchaseOrderByIndentMaterial @IndentID={0}", IndentID).ToList();
                return list;
            }
            else
            {
                list = context.Database.SqlQuery<IndentMaterial>("Execute PurchaseOrderByBOMMaterial @SupplierMasterID={0},@IndentID={1}", SupplierMasterID, IndentID).ToList();
                return list;
            }

        }
        public List<IndentMaterial> GetIndentIDWithSupplierPurchaseOrder(int SupplierMasterID, string IndentID)
        {
            try
            {
                List<IndentMaterial> list = new List<IndentMaterial>();

                if (SupplierMasterID == 0 && IndentID != "")
                {

                    list = context.Database.SqlQuery<IndentMaterial>("SELECT * FROM PurchaseOrderByIndentMaterial(@p0)", IndentID).ToList();
                    return list;
                }
                else
                {
                    list = context.Database.SqlQuery<IndentMaterial>("SELECT * FROM PurchaseOrderByBOMMaterial(@p0,@p1)", SupplierMasterID, IndentID).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStockValue(int materialMasterID)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("SELECT * FROM OpeningStockCounts(@p0)", materialMasterID).ToList();
                return ResultList;

            }
            catch (Exception ex)
            {
                throw;
            }
            //var ResultList = context.Database.SqlQuery<MMS.Web.Models.SPBomMaterialList>("EXEC OpeningStockCounts  @MaterialMasterId={0} ", materialMasterID).ToList();
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStock(int materialMasterID)
        {
            //var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("EXEC OpeningStockCounts @MaterialMasterId={0} ", materialMasterID).ToList();
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("SELECT * FROM OpeningStockCounts(@p0) ", materialMasterID).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.PendingQty> IssueMaterialOpeningStock(int materialMasterID, DateTime IssueDate)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("EXEC IssueOpeningStock @MaterialMasterId={0},@IssueDate={1} ", materialMasterID, IssueDate).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.PendingQty> IssueMaterialOpeningStock_Wetblue(int materialMasterID, DateTime IssueDate, int LOTNo, int SupplierNameId)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("EXEC IssueOpeningStock_LotWetblue @MaterialMasterId={0},@IssueDate={1},@LOTNo={2},@SupplierNameId={3} ", materialMasterID, IssueDate, LOTNo, SupplierNameId).ToList();
            return ResultList;
        }
        public List<MMS.Web.Models.PendingQty> GetCurrentStock(int materialMasterID, int MaterialType)
        {
            var ResultList = context.Database.SqlQuery<MMS.Web.Models.PendingQty>("EXEC GetCurrentStockl @MaterialMasterId={0},@MaterialType={1} ", materialMasterID, MaterialType).ToList();
            return ResultList;
        }

        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaseOrderGrid(int? Supplier, string IndentNO, string PONO)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>("SELECT * FROM OrderGrid(@p0,@p1,@p2)", Supplier, IndentNO, PONO).ToList();
                return ResultList;

            }
            catch (Exception ex)
            {
                throw;
            }
            //var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>("EXEC OrderGrid  @SupplierMasterID={0},@IndentID={1},@Pono={2} ", Supplier, IndentNO, PONO).ToList();

        }
        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaesOrderedwithNeedToIndent(int? Supplier, string IndentNO, string PONO)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>("SELECT * FROM PurchaesOrderedwithNeedToIndent(@p0,@p1,@p2)", Supplier, IndentNO, PONO).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaseOrderGrid1(string PONO)
        {
            try
            {
                //var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>("EXEC OrderGridDirectPo  @PONo={0} ", PONO).ToList();
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>("SELECT * FROM OrderGridDirectPo(@p0)", PONO).ToList();

                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SupplierSuppliedMaterialPrice> SupplierSuppliedMaterialPrice(int MaterialId, int SupplierId)
        {
            var ResultList = context.Database.SqlQuery<SupplierSuppliedMaterialPrice>("EXEC Sp_SupplierSuppliedMaterialPriceDifferanceList  @MaterialId={0},@SupplierId={1} ", MaterialId, SupplierId).ToList();
            return ResultList;
        }

        public List<ClubIndentSizeRange> GetIndentSizerange(string Indentid)
        {
            var ResultList = context.Database.SqlQuery<ClubIndentSizeRange>("Execute GetIndentSizerange @IndentMaterialid ={0}", Indentid).ToList();
            return ResultList;
        }
        public List<ClubIndentSizeRange> FromMRPToGetIndentSizerange(string SimpleMRPID, int? MaterialID)
        {
            var ResultList = context.Database.SqlQuery<ClubIndentSizeRange>("Execute MRPTOGetIndentSizeRange @SimpleMRPID ={0},@MaterialID ={1}", SimpleMRPID, MaterialID).ToList();
            return ResultList;
        }
        public List<ClubIndentSizeRange> GetIndentSizerangeWithMaterial(string IndentID, int? MaterialID)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<ClubIndentSizeRange>("SELECT * FROM GetIndentSizerangeWithMaterial(@p0,@p1)", IndentID, MaterialID).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<ApprovedPrice> getApprovedPrice(string SupplierID, string MaterialID)
        {
            var ResultList = context.Database.SqlQuery<ApprovedPrice>("Execute GetCurrentMaterialPrice @SupplierID={0}, @MaterialID={1}", SupplierID, MaterialID).ToList();
            return ResultList;
        }
        public List<FloorReturnMaterial> GetFloorReturnMaterial(string Materialname, int IssueSlpID)
        {
            var ResultList = context.Database.SqlQuery<FloorReturnMaterial>("Execute GetFloorReturnMaterial @MaterialName ={0}, @IssueSlipId ={1}", Materialname, IssueSlpID).ToList();
            return ResultList;
        }
        public List<FloorReturnMaterial_Withoutissue_No> GetFloorReturnMaterial_withoutissueno(string Materialname)
        {
            var ResultList = context.Database.SqlQuery<FloorReturnMaterial_Withoutissue_No>("Execute GetFloorReturnMaterial_WithoutissueNo @MaterialName ={0}", Materialname).ToList();
            return ResultList;
        }
        public GateEntryInwardMaterialsMaster GateEntryPending(int Materialname, int PoOrderID)
        {
            var ResultList = context.Database.SqlQuery<GateEntryInwardMaterialsMaster>("Execute GateEntryPending @Materialid ={0},@PoOrderID={1}", Materialname, PoOrderID).FirstOrDefault();
            return ResultList;
        }
        //public List<PurchaseOrderGrid> GetPurchaseOrderGrid()
        //{
        //    var ResultList = context.Database.SqlQuery<PurchaseOrderGrid>("SELECT * FROM purchase_order_grid()").ToList();
        //    return ResultList;
        //}
        public List<PurchaseOrderGrid> GetPurchaseOrderGrid()
        {
            var ResultList = context.Database.SqlQuery<PurchaseOrderGrid>("SELECT * FROM purchaseordergrid()").ToList();
            return ResultList;
        }
        public List<GRNGrid> GetPurchaseOrderGrid(string Filter)
        {
            var ResultList = context.Database.SqlQuery<GRNGrid>("Execute GrnGrid @Search={0}", Filter).ToList();
            return ResultList;
        }
        public List<ApprovedPriceMail> ChangepriceinApprovedPrice(string MaterialID, string SupplierID)
        {
            var ResultList = context.Database.SqlQuery<ApprovedPriceMail>("Execute ChangepriceinApprovedPrice @MaterialID={0},@SupplierName={1}", MaterialID, SupplierID).ToList();
            return ResultList;
        }
        public List<StockAdjustMent> isExistsStockAdjustment(int Store, DateTime? From, int Category, int Group, int MaterialType, int MaterialID)
        {
            var ResultList = context.Database.SqlQuery<StockAdjustMent>("Execute IsExistsStockAdjustmentMaterial @Store={0},@From={1},@Category={2},@Group={3},@MaterialType={4},@MaterialID={5}", Store, From, Category, Group, MaterialType, MaterialID).ToList();
            return ResultList;
        }
        public List<SizeRangeIssueModelcs> GetIssueSizerangeWithMaterial(int? MaterialID)
        {
            try
            {
                //   this.context.Database.CommandTimeout = 0;
                var ResultList = context.Database.SqlQuery<SizeRangeIssueModelcs>("Execute SizeRangeMaster_stock @StoreNo ={0},@From ={1},@To ={2},@Category ={3},@Group ={4},@MaterialType ={5},@MaterialMaster ={6}", "", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), "", "", "", MaterialID).ToList();
                // var ResultList = context.Database.SqlQuery<SizeRangeIssueModelcs>("Execute SizeRangeMaster_stock @StoreNo ={0},@From ={1},@To ={2},@Category ={3},@Group ={4},@MaterialType ={5},@MaterialMaster ={6}", "", "", "", "", "", "", MaterialID).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<string> IsExistsIssuewithMaterial(string IssueSlipNo, string MaterialMasterID, string OrderNo)
        {
            var ResultList = context.Database.SqlQuery<string>("EXEC IsExistsIssueWithMaterial @IssueSlipNO={0} ,@MaterialName ={1}, @OrderNo={2}  ", IssueSlipNo, MaterialMasterID, OrderNo).ToList();
            return ResultList;
        }
        public List<string> IsExistsIssuewithMaterial_jobwork(string IssueSlipNo, string MaterialMasterID, string OrderNo)
        {
            var ResultList = context.Database.SqlQuery<string>("EXEC IsExistsIssueWithMaterial_jobwork @IssueSlipNO={0} ,@MaterialName ={1}, @OrderNo={2}", IssueSlipNo, MaterialMasterID, OrderNo).ToList();
            return ResultList;
        }
        public List<string> IsExistsIssuewithMaterial_jobwork_Jobworktype_Id(string IssueSlipNo, string MaterialMasterID, string OrderNo, string Jobworktype_Id)
        {
            var ResultList = context.Database.SqlQuery<string>("EXEC IsExistsIssueWithMaterial_jobwork_jobsheet_pair_Code_id @IssueSlipNO={0} ,@MaterialName ={1}, @OrderNo={2} ,@Jobworktype_Id={3} ", IssueSlipNo, MaterialMasterID, OrderNo, Jobworktype_Id).ToList();
            return ResultList;
        }


        public List<OutwardGateEntryCheck> GetOutwardGateEntry(string IssueSlipNo)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<OutwardGateEntryCheck>("Execute GetOutwardGateEntry @IssueSlipNo={0}", IssueSlipNo).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetOutwardGateEntryCheck(int IssueSlipId, bool isGateChecked, string GateEntryNo)
        {
            try
            {
                var ResultList = context.Database.ExecuteSqlCommand("Execute ISGateChecked @IssueSlipId={0},@IsChecked={1},@GateEntryNo={2}", IssueSlipId, isGateChecked, GateEntryNo);
                return ResultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetOutwardSizeRange(int issueslipId)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<string>("Execute GetGateOutwardSizeRange @IssueSlipId={0}", issueslipId).ToList();
                if (ResultList.Count != 0)
                {
                    return ResultList[0].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MMS.Core.Entities.countries> getcountriesList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.countries>("SELECT Id, Name, Country_Code FROM public.countries").ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.states> getstatesList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.states>("SELECT id,statename , countryid FROM public.get_states_data(); ").ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.cities> getcitiesList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.cities>("SELECT id, cityname,stateid FROM public.get_cities_list();").ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> BuyerOrderforInvoiceList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_valid_orders()").ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> BuyerOrderforBuyerList(int buerid)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_valid_ordersforbuyerid(@p0)", buerid).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> BuyerOrderforProcessList(int Process)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_valid_ordersforprocess_status(@p0)", Process).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> BuyerOrderforbuyerandProcessList(int buerid, int Process)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_valid_ordersforbuyerandprocess_status(@p0,@p1)", buerid, Process).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> GetOrderSingleEntryId(int OrderEntryId)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_order_entry_data_by_id(@p0)", OrderEntryId).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> GetbuyerOrderprocessEntryList(int buerid, int Process, int OrderEntryId, DateTime? from_date, DateTime? to_date)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_order_entry_data_by_Filter(@p0,@p1,@p2,@p3,@p4)", buerid, Process, OrderEntryId, from_date, to_date).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.Stock.OrderEntry> GetOrderListEntryId(int OrderEntryId)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_order_entry_data_by_id(@p0)", OrderEntryId).ToList();
            return ResultList;
        }
        public List<MMS.Core.Entities.order_header> GetOrderheaderListEntryId(int buerid, int Process, int OrderEntryId, int orderheaderid, DateTime? from_date, DateTime? to_date)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.order_header>("SELECT * FROM get_orderheader_data_by_filter(@p0,@p1,@p2,@p3,@p4,@p5)", buerid, Process, OrderEntryId, orderheaderid, from_date, to_date).ToList();
            return ResultList;
        }
        public decimal Getcurrencyconversion(string p_primarycurrency, string p_secondarycurrency, string todaydate)
        {
            decimal val = 0;
            val = context.Database.SqlQuery<decimal>("SELECT * FROM get_currency_conversion(@p0,@p1,@p2)", p_primarycurrency, p_secondarycurrency, todaydate).SingleOrDefault();
            return val;
        }
        public MMS.Core.Entities.Stock.OrderEntry BuyerOrderstyleList()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.Stock.OrderEntry>("SELECT * FROM get_valid_orders()").SingleOrDefault();
            return ResultList;
        }
        public MMS.Core.Entities.CustAddress GetBuyerAddress(int buyerid)
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.CustAddress>("SELECT * FROM get_custaddress_by_buyerid(@p0)", buyerid).SingleOrDefault();
            return ResultList;
        }
        public decimal GetProductqty(string bomno)
        {
            var ResultList = context.Database.SqlQuery<decimal>("SELECT * FROM get_material_qty(@p0)", bomno).SingleOrDefault();
            return ResultList;
        }
        public decimal GetsingleProductqty(int bomid)
        {
            var ResultList = context.Database.SqlQuery<decimal>("SELECT * FROM get_qty_from_bom(@p0)", bomid).SingleOrDefault();
            return ResultList;
        }
        public List<MMS.Core.Entities.SubAssemblyMaster> GetSubAssemblyMaster()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.SubAssemblyMaster>("SELECT * FROM get_subassembly_master()").ToList();
            return ResultList;
        }
        public void UpdateProductionBOMID(int pbomid, int materialid)
        {
            context.Database.SqlQuery<Int32>("SELECT update_probomid(@p0,@p1)", pbomid, materialid).SingleOrDefault();
        }
        public List<MMS.Core.Entities.ProductionBOM> GetProductionbom()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.ProductionBOM>("SELECT * FROM get_productionbom_data()").ToList();
            return ResultList;
        }
    }
}
