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
using Npgsql;
using MMS.Data.Mapping;

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
        public List<MMS.Data.StoredProcedureModel.Buyeraddress> Get_Buyeraddress_Grid()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Buyeraddress>("SELECT * FROM Buyeraddress()").ToList();
            return ResultList;
        }  
        public List<MMS.Data.StoredProcedureModel.supplieraddress> Get_supplieraddress_Grid()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.supplieraddress>("SELECT * FROM supplieraddress()").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.ProductGrid> Get_ProductGrid()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.ProductGrid>("SELECT * FROM get_product_details()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<MMS.Data.StoredProcedureModel.FineshedgoodsReport> Fineshedgoods()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.FineshedgoodsReport>("SELECT * FROM Fineshedgoods_report()").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.SupplierMaterialgrid> suppliermaterial()
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.SupplierMaterialgrid>("SELECT * FROM suppliermaterial()").ToList();
            return ResultList;
        }
        public List<MMS.Data.StoredProcedureModel.GRNCarlist> GRNCartlist(int poheader)
        {
            var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.GRNCarlist>("SELECT * FROM get_grn_cart(@p0)", poheader).ToList();
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
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.Parentbommatertial>("SELECT * FROM get_bom_materials(@p0)", bom_id).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.SalesorderCart> SearchSalesordercart(int customerid, int salesid)
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.SalesorderCart>("SELECT * FROM get_salesorders_cart(@p0,@p1)", customerid, salesid).ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MMS.Data.StoredProcedureModel.get_indent> Getindentcart()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.get_indent>("SELECT * FROM get_indent()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.get_indentcart> Getindentcarts()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.get_indentcart>("SELECT * FROM get_indentcart()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MMS.Data.StoredProcedureModel.IndentPoMappingsp> GetindentPoMapping()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.IndentPoMappingsp>("SELECT * FROM get_indentpomapping()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<MMS.Data.StoredProcedureModel.PODetails> GetPODetailsList()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.PODetails>("SELECT * FROM PODetails()").ToList();
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
        public List<MMS.Data.StoredProcedureModel.GRNGrids> GetGRNGridList()
        {
            try
            {
                var ResultList = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.GRNGrids>("SELECT * FROM get_grn_Grid()").ToList();
                return ResultList;
            }
            catch (Exception ex)
            {
                throw;
            }
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
        public MMS.Data.StoredProcedureModel.currencycunversion Getcurrencyconversion(string p_primarycurrency, string p_secondarycurrency, string todaydate)
        {
            var val = context.Database.SqlQuery<MMS.Data.StoredProcedureModel.currencycunversion>("SELECT * FROM get_currency_conversion(@p0,@p1,@p2)", p_primarycurrency, p_secondarycurrency, todaydate).FirstOrDefault();
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


        //for buyer master 

        //get the list of values
        public List<MMS.Data.StoredProcedureModel.BuyerModel_SP> GetBuyerModels()
        {
            var result = context.Database.SqlQuery<BuyerModel_SP>("select * from buyer_model_get_sp()").ToList();
            return result;
        }


        //get single buyermaster by id

        public MMS.Data.StoredProcedureModel.BuyerModel_SP GetBuyerById(int id)
        {
            var result = context.Database.SqlQuery<BuyerModel_SP>("SELECT * FROM public.buyer_model_get_by_id_sp(@P0)", id).FirstOrDefault();
            return result;
        }

        //post buyer maste rusing sp

        public bool InsertBuyerModel(BuyerModel_SP buyerModel_SP)
        {
            try
            {
                var parameter = new[]
                {
                  
                    new NpgsqlParameter("_customer_name", buyerModel_SP.CustomerName),
                    new NpgsqlParameter("_account", buyerModel_SP.Account),
                    new NpgsqlParameter("_account_name", buyerModel_SP.AccountName),
                    new NpgsqlParameter("_account_description", buyerModel_SP.AccountDescription),
                     new NpgsqlParameter("_swift_code", buyerModel_SP.SwiftCode),
                    new NpgsqlParameter("_physical1", buyerModel_SP.Physical1),
                 
                    new NpgsqlParameter("_physical_code", buyerModel_SP.PhysicalCode),
                  
                     new NpgsqlParameter("_currency_id", buyerModel_SP.CurrencyId),
                    
                    new NpgsqlParameter("_telephone1", buyerModel_SP.Telephone1),
                    new NpgsqlParameter("_telephone2", buyerModel_SP.Telephone2),
                    new NpgsqlParameter("_email_contact", buyerModel_SP.EmailContact),
                    new NpgsqlParameter("_email_accounts", buyerModel_SP.EmailAccounts),
                    new NpgsqlParameter("_email_emergency", buyerModel_SP.EmailEmergency),
                    new NpgsqlParameter("_account_type_id", buyerModel_SP.AccountTypeId),
                    new NpgsqlParameter("_vat_number", buyerModel_SP.VatNumber),
                    new NpgsqlParameter("_reg_number", buyerModel_SP.RegNumber),
                     new NpgsqlParameter("_credit_limit", buyerModel_SP.CreditLimit),
                     new NpgsqlParameter("_charge_interest", buyerModel_SP.ChargeInterest),
                    new NpgsqlParameter("_interest", buyerModel_SP.Interest),
                    new NpgsqlParameter("_tax_type_id", buyerModel_SP.TaxTypeId),                   
              
                    new NpgsqlParameter("_foreign_currency", buyerModel_SP.ForeignCurrency),
                    new NpgsqlParameter("_on_hold", buyerModel_SP.OnHold),
                    new NpgsqlParameter("_active", buyerModel_SP.Active),                  
                new NpgsqlParameter("_created_date", buyerModel_SP.CreatedDate),
                      new NpgsqlParameter("_updated_date",DBNull.Value),
                        new NpgsqlParameter("_created_by", buyerModel_SP.CreatedBy),
                        new NpgsqlParameter("_updated_by", DBNull.Value),
                        new NpgsqlParameter("_is_deleted", buyerModel_SP.IsDeleted),
                            new NpgsqlParameter("_buyer_code", buyerModel_SP.BuyerCode),
                    new NpgsqlParameter("_buyer_short_name", buyerModel_SP.BuyerShortName),
                        new NpgsqlParameter("_deleted_by", DBNull.Value),
                        new NpgsqlParameter("_deleted_date", DBNull.Value),
                              new NpgsqlParameter("_date_added", buyerModel_SP.DateAdded),
                            new NpgsqlParameter("_dc_balance", buyerModel_SP.DcBalance),
                                new NpgsqlParameter("_foreign_dc_balance", 1),
                                new NpgsqlParameter("_website",buyerModel_SP.Website),
                                new NpgsqlParameter("_onholddate",buyerModel_SP.OnHoldDate)
                };
                var result = context.Database.SqlQuery<bool>("SELECT public.buyer_model_post_sp1 (@_customer_name,@_account,@_account_name,@_account_description,@_swift_code,@_physical1,@_physical_code,@_currency_id,@_telephone1,@_telephone2,@_email_contact,@_email_accounts,@_email_emergency,@_account_type_id,@_vat_number,@_reg_number,@_credit_limit,@_charge_interest,@_interest,@_tax_type_id,@_foreign_currency,@_on_hold,@_active,@_created_date,@_updated_date,@_created_by,@_updated_by,@_is_deleted,@_buyer_code,@_buyer_short_name,@_deleted_by,@_deleted_date,@_date_added,@_dc_balance,@_foreign_dc_balance,@_website,@_onholddate)", parameter).FirstOrDefault();
                return result;
            }

            catch (Exception)
            {

                throw;
            }
        }

        //update buyermaster using sp


        public bool Updatebuyer(BuyerMaster1 buyerModel_SP)
        {
            try
            {
                var parameter = new[]
               {
                      new NpgsqlParameter("_buyermasterid", buyerModel_SP.BuyerMasterId),
                    new NpgsqlParameter("_customer_name", buyerModel_SP.CustomerName),
                    new NpgsqlParameter("_account", buyerModel_SP.Account),
                    new NpgsqlParameter("_account_name", buyerModel_SP.AccountName),
                    new NpgsqlParameter("_account_description", buyerModel_SP.AccountDescription),
                     new NpgsqlParameter("_swift_code", buyerModel_SP.SwiftCode),
                    new NpgsqlParameter("_physical1", buyerModel_SP.Physical1),
                  
                    new NpgsqlParameter("_physical_code", buyerModel_SP.PhysicalCode),
                  
                     new NpgsqlParameter("_currency_id", buyerModel_SP.CurrencyId),

                    new NpgsqlParameter("_telephone1", buyerModel_SP.Telephone1),
                    new NpgsqlParameter("_telephone2", buyerModel_SP.Telephone2),
                    new NpgsqlParameter("_email_contact", buyerModel_SP.EmailContact),
                    new NpgsqlParameter("_email_accounts", buyerModel_SP.EmailAccounts),
                    new NpgsqlParameter("_email_emergency", buyerModel_SP.EmailEmergency),
                    new NpgsqlParameter("_account_type_id", buyerModel_SP.AccountTypeId),
                    new NpgsqlParameter("_vat_number", buyerModel_SP.VatNumber),
                    new NpgsqlParameter("_reg_number", buyerModel_SP.RegNumber),
                     new NpgsqlParameter("_credit_limit", buyerModel_SP.CreditLimit),
                     new NpgsqlParameter("_charge_interest", buyerModel_SP.ChargeInterest),
                    new NpgsqlParameter("_interest", buyerModel_SP.Interest),
                    new NpgsqlParameter("_tax_type_id", buyerModel_SP.TaxTypeId),

                    new NpgsqlParameter("_foreign_currency", buyerModel_SP.ForeignCurrency),
                    new NpgsqlParameter("_on_hold", buyerModel_SP.OnHold),
                    new NpgsqlParameter("_active", buyerModel_SP.Active),
                new NpgsqlParameter("_created_date", buyerModel_SP.CreatedDate),
                      new NpgsqlParameter("_updated_date", buyerModel_SP.UpdatedDate),
                        new NpgsqlParameter("_created_by", buyerModel_SP.CreatedBy),
                        new NpgsqlParameter("_updated_by", buyerModel_SP.UpdatedBy),
                        new NpgsqlParameter("_is_deleted", buyerModel_SP.IsDeleted),
                            new NpgsqlParameter("_buyer_code", buyerModel_SP.BuyerCode),
                    new NpgsqlParameter("_buyer_short_name", buyerModel_SP.BuyerShortName),
                        new NpgsqlParameter("_deleted_by", DBNull.Value),
                        new NpgsqlParameter("_deleted_date", DBNull.Value),
                              new NpgsqlParameter("_date_added", buyerModel_SP.DateAdded),
                            new NpgsqlParameter("_dc_balance", buyerModel_SP.DcBalance),
                                new NpgsqlParameter("_foreign_dc_balance", buyerModel_SP.ForeignDcBalance),
                                new NpgsqlParameter("_website",buyerModel_SP.Website),
                                new NpgsqlParameter("_onholddate",buyerModel_SP.OnHoldDate)
                };
                var result = context.Database.SqlQuery<bool>("SELECT public.buyer_model_update_sp1 (@_buyermasterid,@_customer_name,@_account,@_account_name,@_account_description,@_swift_code,@_physical1,@_physical_code,@_currency_id,@_telephone1,@_telephone2,@_email_contact,@_email_accounts,@_email_emergency,@_account_type_id,@_vat_number,@_reg_number,@_credit_limit,@_charge_interest,@_interest,@_tax_type_id,@_foreign_currency,@_on_hold,@_active,@_created_date,@_updated_date,@_created_by,@_updated_by,@_is_deleted,@_buyer_code,@_buyer_short_name,@_deleted_by,@_deleted_date,@_date_added,@_dc_balance,@_foreign_dc_balance,@_website,@_onholddate)", parameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //delete supplier master using sp
        public bool deletebuyer(BuyerModel_SP buyerModel_SP)
        {
            var paramter = new[]
            {
                 new NpgsqlParameter("_buyermasterid",buyerModel_SP.BuyerMasterId),
                new NpgsqlParameter("_is_deleted",buyerModel_SP.IsDeleted),
                new NpgsqlParameter("_active",buyerModel_SP.Active),
                new NpgsqlParameter("_deleted_by",buyerModel_SP.DeletedBy),
                new NpgsqlParameter("_deleted_date",buyerModel_SP.DeletedDate)
            };
            var result= context.Database.SqlQuery<bool>("SELECT public.buyer_model_delete_sp1 (@_buyermasterid,@_is_deleted,@_active,@_deleted_by,@_deleted_date)", paramter).FirstOrDefault();
            return result;
        }


        public List<MMS.Core.Entities.ProductionBOM> GetProductionbom()
        {
            var ResultList = context.Database.SqlQuery<MMS.Core.Entities.ProductionBOM>("SELECT * FROM get_productionbom_data()").ToList();
            return ResultList;
        }


        public bool InsertSupplier(Supplier_master spPostSupplier)
        {
            var parameters = new[]
            {
       new NpgsqlParameter("@p_suppliername", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.Suppliername ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_suppliercode", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.suppliercode ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_account", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.Account ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_accountname", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.AccountName ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_accountdescription", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.AccountDescription ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_physical1", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.Physical1 ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_physicalcode", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.PhysicalCode ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_telephone1", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.Telephone1 ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_telephone2", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.Telephone2 ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_emailcontact", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.EmailContact ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_emailaccounts", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.EmailAccounts ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_emailemergency", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.EmailEmergency ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_accounttypeid", NpgsqlTypes.NpgsqlDbType.Integer) { Value = spPostSupplier.AccountTypeId },
        new NpgsqlParameter("@p_vatnumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.VatNumber ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_regnumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.RegNumber ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_dcbalance", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = spPostSupplier.DcBalance ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_creditlimit", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = spPostSupplier.CreditLimit ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_interest", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = spPostSupplier.Interest ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_taxtypeid", NpgsqlTypes.NpgsqlDbType.Integer) { Value = spPostSupplier.TaxTypeId },
        new NpgsqlParameter("@p_foreigncurrency", NpgsqlTypes.NpgsqlDbType.Integer) { Value = spPostSupplier.ForeignCurrency },
        new NpgsqlParameter("@p_currencyid", NpgsqlTypes.NpgsqlDbType.Integer) { Value = spPostSupplier.CurrencyID },
        new NpgsqlParameter("@p_onhold", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = spPostSupplier.OnHold },
        new NpgsqlParameter("@p_active", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = spPostSupplier.isActive },
        new NpgsqlParameter("@p_createdby", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = spPostSupplier.createdby ?? (object)DBNull.Value },
        new NpgsqlParameter("@p_createddate", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = spPostSupplier.CreatedDate ?? (object)DBNull.Value }
    };

            var result = context.Database.SqlQuery<bool>(
                "SELECT public.insert_supplier_master(@p_suppliername, @p_suppliercode, @p_account, @p_accountname, @p_accountdescription, @p_physical1, @p_physicalcode, @p_telephone1, @p_telephone2, @p_emailcontact, @p_emailaccounts, @p_emailemergency, @p_accounttypeid, @p_vatnumber, @p_regnumber, @p_dcbalance, @p_creditlimit, @p_interest, @p_taxtypeid, @p_foreigncurrency, @p_currencyid, @p_onhold, @p_active, @p_createdby, @p_createddate)",
            parameters).FirstOrDefault();

            return result;
        }

    }
}
