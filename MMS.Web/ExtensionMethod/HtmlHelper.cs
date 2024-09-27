using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Repository.Managers;
using MMS.Repository.Managers.JobWork;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using MMS.Repository.Managers.GateEntryManager;
using System.Configuration;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using MMS.Web.Models.GateEntryModel;
using MMS.Web.Models.Product;

namespace MMS.Web.ExtensionMethod
{
    public class HtmlHelper
    {
        private UnitOfWork2 unitOfWork2 = new UnitOfWork2();
        private Repository2<Unit_Division> GradeRepository;

        enum leatherStore { LeatherStore = 1 }
        public enum CategoryName
        {
            Leathers = 2,
            Materials = 1,
            Sole = 3,
            Insole = 4,
            Footbed = 5
        };

        #region Automatic Code Methods
        public static int getAutoGenerateID()
        {
            InspectionTypeManager inspectionManager = new InspectionTypeManager();
            int? CID = inspectionManager.Get().Max(u => (int?)u.InspectionTypeMasterId);
            return Convert.ToInt32(CID);
        }
        public static int getAutoGenerateSupplierID()
        {
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            int? SID = supplierMasterManager.Get().Max(u => (int?)u.SupplierMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEDocID()
        {
            GateEntryInwardDocumentManager gateentrydocManager = new GateEntryInwardDocumentManager();
            int? SID = gateentrydocManager.Get().Max(u => (int?)u.GateEntryDocumentId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEOutwardDocID()
        {
            GateEntryOutwardDocumentManager gateentrydocManager = new GateEntryOutwardDocumentManager();
            int? SID = gateentrydocManager.Get().Max(u => (int?)u.GateEntryOutwardDocumentId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEVisitorID()
        {
            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
            int? SID = visitorManager.Get().Max(u => (int?)u.GateEntryVisitorId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEInwardID()
        {
            GateEntryInwardMaterialsManager inwardManager = new GateEntryInwardMaterialsManager();
            int? SID = inwardManager.Get().Max(u => (int?)u.GateEntryInwardId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEOutwardID()
        {
            GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
            int? SID = outwardManager.Get().Max(u => (int?)u.GateEntryOutwardId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateGEVehicleID()
        {
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            int? SID = vehicleManager.Get().Max(u => (int?)u.VehicleEntryId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoOrderEntryID()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            int? SID = buyerOrderEntryManager.Get().Max(u => (int?)u.OrderEntryId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateConveyorID()
        {
            ConveyorManager conveyorManager = new ConveyorManager();
            int? SID = conveyorManager.Get().Max(u => (int?)u.ConveyorMasterId);
            return Convert.ToInt32(SID);
        }
        public static string getAutoGenerateStoreID()
        {
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            int? SID = storeMasterManager.Get().Max(u => (int?)u.StoreMasterId);
            return Convert.ToString(SID);
        }
        public static string getAutoGenerateMachinaryID()
        {
            MachineryMasterManager machineryMasterManager = new MachineryMasterManager();
            int? SID = machineryMasterManager.Get().Max(u => (int?)u.MachineryMasterId);
            return Convert.ToString(SID);
        }
        public static string getAutoGenerateProductTypeID()
        {
            ProductTypeManager productTypeManager = new ProductTypeManager();
            int? SID = productTypeManager.Get().Max(u => (int?)u.ProductTypeID);
            return Convert.ToString(SID);
        }
        public static int getAutoGenerateBuyerID()
        {
            BuyerManager buyerMasterManager = new BuyerManager();
            int? BID = buyerMasterManager.Get().Max(u => (int?)u.BuyerMasterId);
            return Convert.ToInt32(BID);
        }
        public static int getAutoGenerateBuyerModelID()
        {
            BuyerModelManager buyerMasterManager = new BuyerModelManager();
            int? BID = buyerMasterManager.Get().Max(u => (int?)u.BuyerModelID);
            return Convert.ToInt32(BID);
        }
        public static int getAutoGenerateSubGroupID()
        {
            SubGroupManager subGroupManager = new SubGroupManager();
            int? SID = subGroupManager.Get().Max(u => (int?)u.SubGroupID);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGenerateMaterialGroupID()
        {
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            int? SID = materialGroupManager.Get().Max(u => (int?)u.MaterialCategoryMasterId);
            return Convert.ToInt32(SID);
        }
        public static string getBuyerModelNameById(int BuyerModel_ProductType)
        {
            BuyerModelManager bm = new BuyerModelManager();
            string buyerModelName = bm.Get().Where(x => x.BuyerModelID == BuyerModel_ProductType).Select(y => y.BuyerModelName).FirstOrDefault();
            return buyerModelName;
        }
        public static string getBuyerNameById(int BuyerName_ProductGroup)
        {
            BuyerManager bm = new BuyerManager();
            string buyerName = bm.Get().Where(x => x.BuyerMasterId == BuyerName_ProductGroup).Select(y => y.BuyerFullName).FirstOrDefault();
            return buyerName;
        }
        public static int getMaterialCodeId()
        {
            MaterialManager bm = new MaterialManager();
            int? SID = bm.Get().Max(u => (int?)u.MaterialMasterId);
            return Convert.ToInt32(SID);
        }

        public static int getMaterialNameCodeId()
        {
            MaterialNameManager bm = new MaterialNameManager();
            // int? SID = null;
            int? SID = bm.Get().Max(u => (int?)Convert.ToInt32(u.MaterialCode));
            return Convert.ToInt32(SID);
        }
        public static int getTaxTypeCodeId()
        {
            TaxTypeManager bm = new TaxTypeManager();
            int? SID = bm.Get().Max(u => (int?)u.TaxMasterID);
            return Convert.ToInt32(SID);
        }
        public static int getStageMasterCodeId()
        {
            StageMasterManager bm = new StageMasterManager();
            int? SID = bm.Get().Max(u => (int?)u.StageMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getStageMaster_ProcessId()
        {
            StageMasterManager bm = new StageMasterManager();
            int? SID = bm.Get().Max(u => (int?)u.ProcessCode);
            return Convert.ToInt32(SID);
        }
        public static int getAutoOrginID()
        {
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            int? SID = orgingMasterManager.Get().Max(u => (int?)u.OriginMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoOrderTypeD()
        {
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            int? SID = orderTypeManager.Get().Max(u => (int?)u.OrderTypeID);
            return Convert.ToInt32(SID);
        }
        public static int getAutoOperationMasterD()
        {
            OperationManager operationManager = new OperationManager();
            int? SID = operationManager.Get().Max(u => (int?)u.OperationMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoMaterialCategoryMasterD()
        {
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            int? SID = materialCategoryManager.Get().Max(u => (int?)u.MaterialCategoryMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoManufactureMasterD()
        {
            ManufactureManager manufactureManager = new ManufactureManager();
            int? SID = manufactureManager.Get().Max(u => (int?)u.ManufacturerMasterId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoProductTypeMasterD()
        {
            ProductTypeManager productTypeManager = new ProductTypeManager();
            int? SID = productTypeManager.Get().Max(u => (int?)u.ProductTypeID);
            return Convert.ToInt32(SID);
        }
        public static int getAutoBuyerOrderID()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            int? SID = buyerOrderEntryManager.Get().Max(u => (int?)u.OrderEntryId);
            return Convert.ToInt32(SID);
        }
        public static string getAutoLeatherShoreGradeMasterID()
        {
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            int? SID = leatherShoeGradeManager.Get().Max(u => (int?)u.LeatherShoesGradeMasterId);
            return Convert.ToString(SID);
        }
        public static string getAutoIssueTypeMasterID()
        {
            IssueTypeManager issueTypeManager = new IssueTypeManager();
            int? SID = issueTypeManager.Get().Max(u => (int?)u.IssueTypeMasterId);
            return Convert.ToString(SID);
        }
        public static string getAutoInspectionTypeMasterID()
        {
            InspectionTypeManager inspectionTypeManager = new InspectionTypeManager();
            int? SID = inspectionTypeManager.Get().Max(u => (int?)u.InspectionTypeMasterId);
            return Convert.ToString(SID);
        }
        public static string getAutoGenerateSimpleMRPID()
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            int? SID = simpleMRPManager.GetAutoMRPNO().Max(u => (int?)u.SimpleMRPID);
            return Convert.ToString(SID);
        }
        public static string getAutoGenerateSimpleMRPID_code()
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            var SID = simpleMRPManager.GetAutoMRPNO().Where(x => x.MRPNO != null).LastOrDefault();
            return Convert.ToString(SID.MRPNO);
        }
        public static int getAutoSizeMatchingID()
        {
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            int? SID = sizeMatchingManager.Get().Max(u => (int?)u.SizeMatchingMasterID);
            return Convert.ToInt32(SID);
        }

        public static int getAgentCodeId()
        {
            AgentManager agentManager = new AgentManager();
            int? SID = agentManager.Get().Max(u => (int?)u.AgentMasterId);
            return Convert.ToInt32(SID);
        }

        public static int getColorCodeId()
        {
            ColorManager colorManager = new ColorManager();
            int? SID = colorManager.Get().Max(u => (int?)u.ColorMasterId);
            return Convert.ToInt32(SID);
        }

        public static int getNormsCodeId()
        {
            NormsManager normsManager = new NormsManager();
            int? SID = normsManager.Get().Max(u => (int?)u.Normsid);
            return Convert.ToInt32(SID);
        }
        public static int getLeatherTypeCodeId()
        {
            LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
            int? SID = leatherTypeManager.Get().Max(u => (int?)u.LeatherTypeID);
            return Convert.ToInt32(SID);
        }
        public static string getAutoGenerateProduct_TypeID()
        {
            Product_TypeManger productTypeManager = new Product_TypeManger();
            int? SID = productTypeManager.Get().Max(u => (int?)u.ProductTypeID);
            return Convert.ToString(SID);
        }
        public static SelectList countryListdata()
        {
            countryListmanager Manager = new countryListmanager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Name).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Name,
                                                      Value = item.Id.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList invoisenoListdata()
        {
            InvoiceManager Manager = new InvoiceManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Id).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Id.ToString(),
                                                      Value = item.Id.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList salesorderno()
        {
            SalesorderHD_Manager Manager = new SalesorderHD_Manager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.salesorderid_hd).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.salesorderid_hd.ToString(),
                                                      Value = item.salesorderid_hd.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select SO"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList Paymentmethodlist()
        {
            paymentmethodmanager Manager = new paymentmethodmanager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Paymentstype).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Paymentstype,
                                                      Value = item.Id.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Payment Method"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList StateListdata()
        {
            StatelistManager Manager = new StatelistManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Statename).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Statename,
                                                      Value = item.Id.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList CityListdata()
        {
            CitylistManager Manager = new CitylistManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Id).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Cityname,
                                                      Value = item.Id.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static int getIndentTypeMasterId()
        {
            IndentTypeManager indentTypeManager = new IndentTypeManager();
            int? SID = indentTypeManager.Get().Max(u => (int?)u.IndentMasterID);
            return Convert.ToInt32(SID);
        }

        public static int getAutoGenerateIndentNo()
        {
            IndentManager indentManager = new IndentManager();
            int? SID = indentManager.Get().Max(u => (int?)u.IndentId);
            return Convert.ToInt32(SID);
        }
        public static int getAutoGeneratePoNo()
        {
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            var Orderlist = purchaseOrderManager.Get().ToList();
            List<int> listString = new List<int>().ToList();
            foreach (var item in Orderlist)
            {
                string num = Regex.Replace(item.PoNo, @"\D", "");
                listString.Add(Convert.ToInt32(num));
            }
            int maxid = 0;


            if (Orderlist == null || Orderlist.Count == 0)
            {
                return Convert.ToInt32(maxid);
            }
            else
            {
                maxid = (listString.Max());
                return maxid;
            }
            // return Convert.ToInt32(result);
        }
        public static int getAutoGenerateGRNNO()
        {
            GrnManager grnManager = new GrnManager();
            int? SID = grnManager.Get().Max(u => (int?)u.GrnNo);
            return Convert.ToInt32(SID);
        }

        public static int getAutoGenerateFloorID()
        {
            FloorRetMaterialManager floorManager = new FloorRetMaterialManager();
            int? SID = floorManager.Get().Max(u => (int?)u.FloorReturnMaterialId);
            return Convert.ToInt32(SID);
        }
        public static SelectList ModeofTransportGEDoc()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Two Wheeler"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Four Wheeler"
            };
            var ShotName4 = new System.Web.Mvc.SelectListItem()
            {
                Value = "3",
                Text = "In person"
            };
            //var ShotName4 = new System.Web.Mvc.SelectListItem()
            //{
            //    Value = "3",
            //    Text = "Courier"
            //};
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);
            items.Add(ShotName4);
            return new SelectList(items, "Value", "Text");

        }
        public static int getStockAdjustmentCodeId()
        {
            StockAdjustmentManager bm = new StockAdjustmentManager();
            int? SID = bm.Get().Max(u => (int?)u.StockAdjustmentId);
            return Convert.ToInt32(SID);
        }
        #endregion

        #region DropDown Methods


        public static SelectList ShortUnitName()
        {
            UOMManager uOMManager = new UOMManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.Get().OrderBy(x => x.ShortUnitName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.ShortUnitName,
                                        Value = item.UomMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList OrderTypeName()
        {
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            List<System.Web.Mvc.SelectListItem> items = orderTypeManager.Get().OrderBy(x => x.OrderTypeName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.OrderTypeName,
                                        Value = item.OrderTypeID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }   
        public static SelectList accountypeName()
        {
            AccounttypeManager orderTypeManager = new AccounttypeManager();
            List<System.Web.Mvc.SelectListItem> items = orderTypeManager.Get().OrderBy(x => x.Id).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.AccountAbbr,
                                        Value = item.Id.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList leatherStoreWithMaterialName()
        {
            MaterialManager materialManager = new MaterialManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId
                         where x.StoreMasterId == 1
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, z.Color), x.MaterialMasterId, x.Price, z.ColorMasterId, x.Uom, x.UomUnit, x.SizeRangeMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            IEnumerable<SelectListItem> myCollection = distinctList
                                             .Select(i => new SelectListItem()
                                             {
                                                 Text = i.MaterialDescription.ToString(),
                                                 Value = i.MaterialMasterId.ToString()
                                             });

            return new SelectList(myCollection, "Value", "Text");
        }
        public static SelectList Conveyor()
        {
            ConveyorManager uOMManager = new ConveyorManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.Get().OrderBy(x => x.ConveyorName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.ConveyorName,
                                        Value = item.ConveyorMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList OperationName()
        {
            OperationManager operManager = new OperationManager();
            List<System.Web.Mvc.SelectListItem> items = operManager.Get().OrderBy(x => x.OperationName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.OperationName,
                                        Value = item.OperationMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList LOtNowihSeason()
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<InternalOrderForm> BuyerOrderEntryList = new List<InternalOrderForm>();
            List<SelectListItem> lotNoList = new List<SelectListItem>();
            var items_ = (from x in uOMManager.GetInternalIO()
                          join y in seasonManager.Get()
                           on x.BuyerSeason equals y.SeasonMasterId
                          // where y.SeasonName == SeasonName
                          select new { x.LotNo, x.OrderEntryId });
            foreach (var item in items_)
            {
                SelectListItem selectlist = new SelectListItem()
                {
                    Text = item.LotNo,
                    Value = item.OrderEntryId.ToString()
                };
                lotNoList.Add(selectlist);
            }
            lotNoList = lotNoList.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            lotNoList.Insert(0, ShotName_);
            return new SelectList(lotNoList, "Value", "Text");
        }
        public static SelectList LOtNo()
        {
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();

            items = uOMManager.Get().OrderBy(x => x.BuyerName).Select(
                                        item => new System.Web.Mvc.SelectListItem()
                                        {
                                            Text = item.LotNo,
                                            Value = item.OrderEntryId.ToString()
                                        }
                                        ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList<SelectListItem>();
            items.Insert(0, new SelectListItem { Text = "Please Select", Value = string.Empty });
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList WeekNo()
        {
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.Get().OrderBy(x => x.WeekNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.WeekNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList WeekNo_Internal()
        {
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.GetInternalIO().OrderBy(x => x.WeekNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.WeekNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetTaxRef()
        {
            TaxTypeManager taxManager = new TaxTypeManager();
            List<System.Web.Mvc.SelectListItem> items = taxManager.Get().OrderBy(x => x.TaxCode).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.TaxCode,
                                        Value = item.TaxMasterID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select Taxtype"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetPONO()
        {
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            var query = purchaseOrderManager.Get().GroupBy(x => x.PoNo).Select(g => g.First()).ToList();
            List<System.Web.Mvc.SelectListItem> items = query.OrderBy(x => x.PoNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.PoNo,
                                        Value = item.PoOrderId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetTaxON()
        {
            TaxTypeManager taxManager = new TaxTypeManager();
            List<System.Web.Mvc.SelectListItem> items = taxManager.Get().OrderBy(x => x.TaxName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.TaxName,
                                        Value = item.TaxMasterID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetSizeScheduleMasterRangeName()
        {
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            List<System.Web.Mvc.SelectListItem> items = sizeScheduleMasterManager.Get().OrderBy(x => x.SizeRangeName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SizeRangeName,
                                        Value = item.SizeScheduleMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetSizeRangeMasterDetailsName()
        {
            SizeRangeMasterManager sizeScheduleMasterManager = new SizeRangeMasterManager();
            List<System.Web.Mvc.SelectListItem> items = sizeScheduleMasterManager.Get().OrderBy(x => x.SizeRangeRef).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SizeRangeRef,
                                        Value = item.SizeRangeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList LongUnitName()
        {
            UOMManager uOMManager = new UOMManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.Get().OrderBy(x => x.LongUnitName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.LongUnitName,
                                        Value = item.UomMasterId.ToString()
                                    }
                                    ).ToList();
            var LongName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, LongName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList MaterialCategoryCode()
        {
            MaterialCategoryManager Manager = new MaterialCategoryManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CategoryCode).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = "MC" + " " + item.CategoryCode.ToString(),
                                        Value = item.MaterialCategoryMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList MaterialCategoryName()
        {
            MaterialCategoryManager Manager = new MaterialCategoryManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CategoryName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.CategoryName,
                                        Value = item.MaterialCategoryMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select Category"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }    
        public static SelectList CategoryNames()
        {
            CategorymasterManager Manager = new CategorymasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CategoryName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.CategoryName,
                                        Value = item.CategoryId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select Category"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList MaterialSubGroupName()
        {
            SubGroupManager Manager = new SubGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SubGroupDescription,
                                        Value = item.SubGroupID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList CommisionName()
        {
            CommisionCritiriaManager Manager = new CommisionCritiriaManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CommisionName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.CommisionName,
                                        Value = item.CommisionCriteriaId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList CurrencyName()
        {
            CurrencyManager Manager = new CurrencyManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.ShortForm).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.LongForm,
                                                     Value = item.CurrencyMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }    
        public static SelectList forCurrencyName()
        {
            CurrencyManager Manager = new CurrencyManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Getcurrency().OrderBy(x => x.id).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.currencycode,
                                                     Value = item.id.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList MaterialGroupName()
        {
            MaterialGroupManager Manager = new MaterialGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.GroupName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.GroupName,
                                                     Value = item.MaterialGroupMasterId.ToString()
                                                 }).ToList();
            var MaterialCatName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, MaterialCatName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList leatherTypes()
        {
            LeatherTypeManager Manager = new LeatherTypeManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.LeatherTypeDescription).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.LeatherTypeDescription,
                                                     Value = item.LeatherTypeID.ToString()
                                                 }).ToList();
            var MaterialCatName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, MaterialCatName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList ColourName()
        {
            ColorManager Manager = new ColorManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Color).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Color,
                                                     Value = Convert.ToString(item.ColorMasterId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ColourName_()
        {
            ColorManager Manager = new ColorManager();
            //string Proeeuct = "";
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Color).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {

                                                     Text = item.Color,
                                                     Value = Convert.ToString(item.ColorMasterId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ColourCode()
        {
            ColorManager Manager = new ColorManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.Color).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.BuyerColorCode,
                                                     Value = Convert.ToString(item.ColorMasterId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList CompanyCurrencyAsSelectListItems()
        {
            CurrencyManager am = new CurrencyManager();
            List<System.Web.Mvc.SelectListItem> items = (am.Get().Select(r =>
             new System.Web.Mvc.SelectListItem()
             {
                 Value = r.CurrencyMasterId.ToString(),
                 Text = r.LongForm
             })).ToList();

            var emptyItem = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, emptyItem);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList SupplierName()
        {
            Supplier_masterManager Manager = new Supplier_masterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SupplierId).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Suppliername,
                                                     Value = Convert.ToString(item.SupplierId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select Supplier"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");

        }    
        public static SelectList SupplierfullName()
        {
            Supplier_masterManager Manager = new Supplier_masterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SupplierId).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Suppliername,
                                                     Value = item.Suppliername
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Supplier"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GetPurpose()
        {
            GateEntryOutwardManager Manager = new GateEntryOutwardManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.GetPurpose().OrderBy(x => x.PurposeDetails).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.PurposeDetails,
                                                     Value = Convert.ToString(item.PurposeId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList Store()
        {
            StoreMasterManager Manager = new StoreMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.StoreName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.StoreName,
                                                     Value = item.StoreMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Store"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList ContactCompanyName()
        {
            CompanyManager Manager = new CompanyManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SupplierName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {

                                                     Value = item.CompanyCodePK.ToString(),
                                                     Text = item.SupplierName,

                                                 }).ToList();
            var cname = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, cname);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList DynamicMaterialName(int MaterialID = 0)
        {
            MaterialNameManager Manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = null;
            if (MaterialID == 0)
            {
                items = Manager.Get().OrderBy(x => x.MaterialDescription).Select(
                                                   item => new System.Web.Mvc.SelectListItem()
                                                   {
                                                       Text = item.MaterialDescription,
                                                       Value = item.MaterialMasterID.ToString()
                                                   }).ToList();
            }
            else
            {
                items = Manager.Get().Where(x => x.MaterialGroupMasterId == MaterialID).OrderBy(x => x.MaterialDescription).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterID.ToString()
                                                  }).ToList();
            }
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList DynamicMaterialName()
        {
            MaterialNameManager Manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MaterialDescription).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.MaterialDescription,
                                                     Value = item.MaterialMasterID.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GetStockAdjustmentNO()
        {
            StockAdjustmentManager Manager = new StockAdjustmentManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().Where(x => x.StockNo != null).OrderBy(x => x.StockNo).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.StockNo,
                                                     Value = item.StockAdjustmentId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GEMaterialName()
        {
            BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
            var list_ = billofMaterialManager.GetMaterialList();
            var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.RemoveAll(c => c.Text == "" || c.Text == null);
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList StoreLocation()
        {
            StoreMasterManager Manager = new StoreMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.StoreName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Location,
                                                     Value = item.StoreMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SubGroup()
        {
            MaterialGroupManager Manager = new MaterialGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroup).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SubGroup,
                                                     Value = item.MaterialGroupMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SubGroupName()
        {
            SubGroupManager Manager = new SubGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SubGroupDescription,
                                                     Value = item.SubGroupID.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList Uom()
        {
            UOMManager Manager = new UOMManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.LongUnitName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.LongUnitName,
                                                     Value = item.UomMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select UOM"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList UomUnit()
        {
            UOMManager Manager = new UOMManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.ShortUnitName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ShortUnitName,
                                                     Value = item.UomMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }

        public static SelectList Defect_type()
        {
            InspectionTypeManager Manager = new InspectionTypeManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Parameter,
                                                     Value = item.InspectionTypeMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList MachineName()
        {
            MachineryMasterManager Manager = new MachineryMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MachineName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.MachineName,
                                                     Value = item.MachineryMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SubstanceRange()
        {
            SubstanceMasterManager Manager = new SubstanceMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubstanceRange).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SubstanceRange,
                                                     Value = item.SubstanceMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList LeatherSizeName()
        {
            LeatherSizeManager Manager = new LeatherSizeManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.LeatherSizeMasterId).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Length + "-" + item.Width,
                                                     Value = item.LeatherSizeMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList OriginName()
        {
            OrgingMasterManager Manager = new OrgingMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.OriginName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.OriginName,
                                                     Value = item.OriginMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList ComponentMaster()
        {
            ComponentManager Manager = new ComponentManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.ComponentName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.ComponentName,
                                        Value = item.ComponentMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList SizeRangeValue()
        {
            SizeRangeMasterManager Manager = new SizeRangeMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SizeRangeRefValue).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SizeRangeRefValue,
                                                     Value = item.SizeRangeMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SizeRangeName()
        {
            SizeRangeMasterManager Manager = new SizeRangeMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SizeRangeRefValue).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SizeRangeRef,
                                                     Value = item.SizeRangeMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SizeMatchingName()
        {
            SizeMatchingManager Manager = new SizeMatchingManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SizeMatchingName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SizeMatchingName,
                                                     Value = item.SizeMatchingMasterID.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SizeRangeMaster()
        {
            SizeRangeDetailsManager Manager = new SizeRangeDetailsManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SizeRangeName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SizeNo,
                                                     Value = item.SizeRangeDetailsId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList SizeScheduleMaster()
        {
            SizeScheduleMasterManager Manager = new SizeScheduleMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SizeRangeName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.SizeMatchingNo,
                                                     Value = item.SizeScheduleMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList ManufacturerName()
        {
            ManufactureManager Manager = new ManufactureManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.ManufacturerName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ManufacturerName,
                                                     Value = item.ManufacturerMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList MaterialName()
        {
            MaterialNameManager Manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MaterialDescription).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterID.ToString()
                                                  }).ToList();

            items.RemoveAll(c => c.Text == null);
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList MaterialNameBasedOnFloorIssueNo(string IssueSlipNo)
        {

            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            IssueSlipManager issueslipmanager = new IssueSlipManager();
            MaterialNameManager namemanager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            var MaterialList = (from x in manager.GetMultipleIssueSlip()
                                join y in issueslipmanager.Get() on (x.IssueSlipNo) equals y.IssueSlipNo
                                join w in materialManager.Get() on y.MaterialMasterId equals w.MaterialMasterId
                                join z in namemanager.Get() on w.MaterialName equals z.MaterialMasterID
                                where x.IssueSlipNo == IssueSlipNo
                                select new
                                {
                                    MaterialName = y.MaterialName + "~" + y.IssueSlipId,
                                    MaterialMasterId = z.MaterialMasterID
                                });

            MaterialNameManager Manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = MaterialList.OrderBy(x => x.MaterialName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialName,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList IssueNo()
        {

            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<MultipleIssueSlip> list = new List<MultipleIssueSlip>();
            var list_ = Manager.GetMultipleIssueSlip();

            var distinctList = list_.GroupBy(x => x.IssueSlipNo).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.IssueSlipNo,
                                                      Value = item.IssueSlipNo
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList IssueNo_ID_jOB()
        {

            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<MultipleIssueSlip> list = new List<MultipleIssueSlip>();
            var list_ = Manager.GetMultipleIssueSlip();

            var distinctList = list_.Where(X => X.IsJobWork == true).GroupBy(x => x.IssueSlipNo).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.IssueSlipNo,
                                                      Value = item.MultipleIssueSlipID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList IssueNo_ID_jOBGate()
        {

            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<MultipleIssueSlip> list = new List<MultipleIssueSlip>();
            var list_ = Manager.GetMultipleIssueSlip();

            var distinctList = list_.Where(X => X.IsJobWork == true && X.GateOut_No != "" && X.GateOut_No != null).GroupBy(x => x.IssueSlipNo).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.GateOut_No,
                                                      Value = item.MultipleIssueSlipID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList MaterialMasterName()
        {
            BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
            var list_ = billofMaterialManager.GetMaterialList();
            var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList MaterialMasterNameBasedByGroup(int MaterialGroupMasterId)
        {
            BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
            var list_ = billofMaterialManager.GetMaterialList();
            list_ = list_.Where(x => x.GroupID == MaterialGroupMasterId).ToList();
            var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }

        public static SelectList IsCheckedSizeRangeMaterialName()
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join g in materialGroupManager.Get()
                         on x.MaterialGroupMasterId equals g.MaterialGroupMasterId
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         select new { MaterialDescription = string.Format("{0} {1}", y.MaterialDescription, x.OptionMaterialValue), x.MaterialMasterId, g.IsSize });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.Where(x => x.IsSize == true).ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList StockMaterialName()
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID

                         select new { MaterialDescription = y.MaterialDescription, MaterialMasterId = x.MaterialMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();

            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }
        public static SelectList MaterialSubCategory()
        {
            SubGroupManager Manager = new SubGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.SubGroupDescription,
                                                      Value = item.SubGroupID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList SeasonName()
        {
            SeasonManager Manager = new SeasonManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SeasonName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.SeasonName,
                                                      Value = item.SeasonMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList MaterialCode()
        {
            MaterialManager Manager = new MaterialManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MaterialCode).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialCode,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList MaterialType()
        {
            MaterialTypeManager Manager = new MaterialTypeManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MaterialType).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialType,
                                                      Value = item.MaterialOpeningStockID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BuyerName()
        {
            BuyerMasterManager Manager = new BuyerMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CustomerName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.CustomerName,
                                                      Value = item.BuyerMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Buyer"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }     
        public static SelectList BuyerNames()
        {
            BuyerMasterManager Manager = new BuyerMasterManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.CustomerName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.CustomerName,
                                                      Value = item.CustomerName
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Search By Buyer Name"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList PONo()
        {
            PoManager Manager = new PoManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.PoheaderId).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.PoNumber.ToString(),
                                                      Value = item.PoNumber.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select PONo"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ProductName()
        {
            ProductManager Manager = new ProductManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().Where(m=>m.IsActive ==true).OrderBy(x => x.ProductName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.ProductName,
                                                      Value = item.ProductId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ProductmFINName()
        {
            ProductManager Manager = new ProductManager();
            List<product> products = Manager.Get();
            var filteredProducts = products.Where(p => p.ProductType == 1).Where(m => m.IsActive == true).OrderBy(p => p.ProductName).ToList();
            List<System.Web.Mvc.SelectListItem> items = filteredProducts.Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ProductName,
                                                     Value = item.ProductId.ToString()
                                                 }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product"
            };

            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }      
        public static SelectList ProductmMATName()
        {
            ProductManager Manager = new ProductManager();
            List<product> products = Manager.Get();
            var filteredProducts = products.Where(p => p.ProductType == 3 && p.IsActive == true).OrderBy(p => p.ProductName).ToList();
            List<System.Web.Mvc.SelectListItem> items = filteredProducts.Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ProductName,
                                                     Value = item.ProductId.ToString()
                                                 }).ToList();

            // Insert the "Please Select Product" item
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product"
            };

            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }       
        public static SelectList ProductmSUBName()
        {
            ProductManager Manager = new ProductManager();
            List<product> products = Manager.Get();
            var filteredProducts = products.Where(p => p.ProductType == 2 && p.IsActive == true).OrderBy(p => p.ProductName).ToList();
            List<System.Web.Mvc.SelectListItem> items = filteredProducts.Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ProductName,
                                                     Value = item.ProductId.ToString()
                                                 }).ToList();

            // Insert the "Please Select Product" item
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product"
            };

            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }
        public static SelectList IndentDetailNo()
        {
            IndentNewMaterialManager Manager = new IndentNewMaterialManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().Where(m => m.IsActive == true).OrderBy(x => x.IndentHeaderId).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.IndentNo.ToString(),
                                                      Value = item.IndentNo.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select IndentNo"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BuyerModel()
        {
            BuyerModelManager buyerModelManager = new BuyerModelManager();
            List<System.Web.Mvc.SelectListItem> items = buyerModelManager.Get().OrderBy(x => x.BuyerModelName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BuyerModelName,
                                                      Value = item.BuyerModelID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList AgentName()
        {
            AgentManager Manager = new AgentManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.AgentFullName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.AgentFullName,
                                                      Value = item.AgentMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList OurStyle()
        {
            Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.OurStyle).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.OurStyle,
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BuyerOrderCreationOurStyle()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<Product_BuyerStyleMaster> product_BuyerStyleMasterList = new List<Product_BuyerStyleMaster>();
            product_BuyerStyleMasterList = product_BuyerStyleManager.Get();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleMasterList.OrderBy(x => x.OurStyle).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.OurStyle,
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList CountryName()
        {
            CountryManager Manager = new CountryManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.LongCountryName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.LongCountryName,
                                                      Value = item.CountryMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public IEnumerable<SelectListItem> StateTypeSelectList(Guid stateCodePK, Guid countryCodePK)
        {
            var am = new CompanyManager();
            var CountryTypes = am.GetStates();

            return CountryTypes.Where(c => c.CountryCodeFK == countryCodePK).OrderBy(item => item.StateName)
                    .Select(item =>
                        new SelectListItem
                        {
                            Selected = (item.StateCodePK == stateCodePK),
                            Text = item.StateName,
                            Value = item.StateCodePK.ToString()
                        });
        }

        public static SelectList GetOperation()
        {
            OperationManager taxManager = new OperationManager();
            List<System.Web.Mvc.SelectListItem> items = taxManager.Get().OrderBy(x => x.OperationName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.OperationName,
                                        Value = item.OperationMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetGRNType()
        {
            GRNTypeManager GRNManager = new GRNTypeManager();
            List<System.Web.Mvc.SelectListItem> items = GRNManager.Get().OrderBy(x => x.IssueType).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IssueType,
                                        Value = item.GrnTypeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetGateEntryNo()
        {
            GateEntryInwardMaterialsManager GRNManager = new GateEntryInwardMaterialsManager();
            List<System.Web.Mvc.SelectListItem> items = GRNManager.GetGateEntry().GroupBy(x => x.GateEntryNo).Select(g => g.First()).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GateEntryNo,
                                        Value = item.GateEntryInwardId.ToString()
                                    }
                                    ).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };



            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetJobWorkGRNType()
        {
            GRNTypeManager GRNManager = new GRNTypeManager();
            List<System.Web.Mvc.SelectListItem> items = GRNManager.GetJobWork().OrderBy(x => x.IssueType).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IssueType,
                                        Value = item.GrnTypeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetCurrencySizeRange()
        {
            CurrencyManager currencyManager = new CurrencyManager();
            List<System.Web.Mvc.SelectListItem> items = currencyManager.Get().OrderBy(x => x.ShortForm).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.ShortForm,
                                        Value = item.CurrencyMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetIndentType()
        {
            IndentTypeManager indentTypeManager = new IndentTypeManager();
            List<System.Web.Mvc.SelectListItem> items = indentTypeManager.Get().OrderBy(x => x.IndentTypeName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IndentTypeName,
                                        Value = item.IndentMasterID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetLeatherShoeGrade()
        {
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            List<System.Web.Mvc.SelectListItem> items = leatherShoeGradeManager.Get().OrderBy(x => x.Grade).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.Grade,
                                        Value = item.LeatherShoesGradeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetInternalOrderIONO()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().OrderBy(x => x.BuyerPoNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerPoNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }
        //public static SelectList GetBuyerOrderlist()
        //{
        //    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
        //    List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.GetBuyerOrderlist().OrderBy(x => x.buyerorderslno).Select(
        //                            item => new System.Web.Mvc.SelectListItem()
        //                            {
        //                                Text = item.buyerorderslno.ToString(),
        //                                Value = item.orderid.ToString()
        //                            }
        //                            ).ToList();

        //    var ShotName = new System.Web.Mvc.SelectListItem()
        //    {
        //        Value = "",
        //        Text = "Please Select"
        //    };
        //    items.Insert(0, ShotName);

        //    return new SelectList(items, "Value", "Text");
        //}

        //public static SelectList GetBuyerOrderAddress1list(int orderid)
        //{
        //    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
        //    List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid).OrderBy(x => x.orderid).Select(
        //                            item => new System.Web.Mvc.SelectListItem()
        //                            {
        //                                Text = item.add1.ToString(),
        //                                Value = item.orderid.ToString()
        //                            }
        //                            ).ToList();

        //    var ShotName = new System.Web.Mvc.SelectListItem()
        //    {
        //        Value = "",
        //        Text = "Please Select"
        //    };
        //    items.Insert(0, ShotName);

        //    return new SelectList(items, "Value", "Text");
        //}
        //public static SelectList GetBuyerOrderAddress2list(int orderid)
        //{
        //    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
        //    List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid).OrderBy(x => x.orderid).Select(
        //                            item => new System.Web.Mvc.SelectListItem()
        //                            {
        //                                Text = item.add2.ToString(),
        //                                Value = item.orderid.ToString()
        //                            }
        //                            ).ToList();

        //    var ShotName = new System.Web.Mvc.SelectListItem()
        //    {
        //        Value = "",
        //        Text = "Please Select"
        //    };
        //    items.Insert(0, ShotName);

        //    return new SelectList(items, "Value", "Text");
        //}
        //public static SelectList GetBuyerOrderAddress3list(int orderid)
        //{
        //    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
        //    List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid).OrderBy(x => x.orderid).Select(
        //                            item => new System.Web.Mvc.SelectListItem()
        //                            {
        //                                Text = item.add3.ToString(),
        //                                Value = item.orderid.ToString()
        //                            }
        //                            ).ToList();

        //    var ShotName = new System.Web.Mvc.SelectListItem()
        //    {
        //        Value = "",
        //        Text = "Please Select"
        //    };
        //    items.Insert(0, ShotName);

        //    return new SelectList(items, "Value", "Text");
        //}
        public static SelectList GetInternalOrderIONOForListBox()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().OrderBy(x => x.BuyerPoNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerPoNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();

            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetInternalOrderIONODummy()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().OrderBy(x => x.BuyerPoNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerPoNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();

            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetInternalOrderIONO(List<int> filter)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().OrderBy(x => x.BuyerPoNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerPoNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();
            foreach (var item in filter)
            {
                items.Where(x => x.Value == item.ToString()).ToList();
            }
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList InternalOrderIONO()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerOrderSlNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList OrderIONO()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.BuyerOrderSlNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GRNNO()
        {
            GrnManager grnManager = new GrnManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.GrnNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GrnNo.ToString(),
                                        Value = item.GrnID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ReportGRNNO()
        {
            GrnManager grnManager = new GrnManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.GrnNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GrnNo.ToString(),
                                        Value = item.GrnID.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ReportGRNGSTNO()
        {
            GrnManagerNew grnManager = new GrnManagerNew();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.GrnNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GrnNo.ToString(),
                                        Value = item.GrnID.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList Bom()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select BOM No"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetBomNo()
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<System.Web.Mvc.SelectListItem> items = billOfMaterialManager.Get().Where(x => x.BomNo != null).OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.BomId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select BOM No"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetBomno()
        {
            ParentbomManager parentbom = new ParentbomManager();
            List<System.Web.Mvc.SelectListItem> items = parentbom.Get().Where(x => x.BomNo != null && x.IsDelete == true).OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.BomId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select BOM No"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetBomNovalue()
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<System.Web.Mvc.SelectListItem> items = billOfMaterialManager.Get().Where(x => x.BomNo != null).OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.BomNo
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Select BOM No"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BomStyleNo()
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();


            var ListOfBillOfMaterial = billOfMaterialManager.Get().ToList();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            if (ListOfBillOfMaterial != null)
            {

                items = ListOfBillOfMaterial.Where(x => x.BomNo != null).OrderBy(x => x.BomNo).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.BomNo,
                                                     Value = item.BomId.ToString()
                                                 }).ToList();

            }
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BomStyle()
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<System.Web.Mvc.SelectListItem> items = billOfMaterialManager.Get().Where(x => x.BomNo != null).OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.BomId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BomStyle_Link()
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<System.Web.Mvc.SelectListItem> items = billOfMaterialManager.Get().Where(x => x.BomNo != null).OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo,
                                                      Value = item.BomId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ProductBuyerStyleNo()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BuyerStyle).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BuyerStyle.Substring(0, 5),
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ProductBuyerStyleNo_Name()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BuyerStyle).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BuyerStyle,
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BomLast()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo.Substring(5, 3),
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        //public static SelectList BomColor()
        //{
        //    Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
        //    List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BomNo).Select(
        //                                          item => new System.Web.Mvc.SelectListItem()
        //                                          {
        //                                              Text = item.BomNo.Substring(9, 4),
        //                                              Value = item.ProductOrBuyerStyleId.ToString()
        //                                          }).ToList();
        //    var ShotName = new System.Web.Mvc.SelectListItem()
        //    {
        //        Value = "",
        //        Text = "Please Select"
        //    };
        //    items.Insert(0, ShotName);
        //    return new SelectList(items, "Value", "Text");
        //}
        public static SelectList BomColor()
        {
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<System.Web.Mvc.SelectListItem> items = product_BuyerStyleManager.Get().OrderBy(x => x.BomNo).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BomNo.Length >= 13 ? item.BomNo.Substring(9, 4) : "",
                                                      Value = item.ProductOrBuyerStyleId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetLeather()
        {

            StoreMasterManager storeManager = new StoreMasterManager();
            var result = from x in storeManager.Get()
                         where x.StoreName.Contains("Leather Store")
                         select x.StoreMasterId;
            int storeMasterID = result.FirstOrDefault();

            MaterialCategoryManager materialCategoryMaster = new MaterialCategoryManager();
            var result1 = from x in materialCategoryMaster.Get()
                          where x.CategoryName.Contains("Leathers")
                          select x.MaterialCategoryMasterId;
            int materialCategoryMasterId = result1.FirstOrDefault();


            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId
                         where x.MaterialCategoryMasterId == materialCategoryMasterId && x.StoreMasterId == storeMasterID
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, z.Color), x.MaterialMasterId, x.Price, z.ColorMasterId, x.Uom, x.UomUnit, x.SizeRangeMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");

        }
        public static SelectList ProductType()
        {
            Product_TypeManger productTypeManager = new Product_TypeManger();
            List<System.Web.Mvc.SelectListItem> items = productTypeManager.Get().OrderBy(x => x.ProductTypeName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.ProductTypeName,
                                                      Value = item.ProductTypeID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product Type"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList Material()
        {
            MaterialNewManager materialNewManager = new MaterialNewManager();
            List<System.Web.Mvc.SelectListItem> items = materialNewManager.Get().OrderBy(x => x.MaterialName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialName,
                                                      Value = item.MaterialId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Material Name"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList StatusProduction()
        {
            StatusProductionManager statusProductionManager = new StatusProductionManager();
            List<System.Web.Mvc.SelectListItem> items = statusProductionManager.Get().OrderBy(x => x.StatusId).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Status,
                                                      Value = item.StatusId.ToString()
                                                  }).ToList();
            // Find the index of "initiated" status
            int initiatedIndex = items.FindIndex(item => item.Text.ToLower() == "Initiated");

            // Move the "initiated" status to the beginning of the list
            if (initiatedIndex != -1)
            {
                var initiatedItem = items[initiatedIndex];
                items.RemoveAt(initiatedIndex);
                items.Insert(0, initiatedItem);
            }
          

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList StatusProductionSubassembly()
        {
            StatusProductionSubassemblyManager statusProductionManager = new StatusProductionSubassemblyManager();
            List<System.Web.Mvc.SelectListItem> items = statusProductionManager.Get().OrderBy(x => x.StatusId).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Status,
                                                      Value = item.StatusId.ToString()
                                                  }).ToList();
            // Find the index of "initiated" status
            int initiatedIndex = items.FindIndex(item => item.Text.ToLower() == "Started");

            // Move the "initiated" status to the beginning of the list
            if (initiatedIndex != -1)
            {
                var initiatedItem = items[initiatedIndex];
                items.RemoveAt(initiatedIndex);
                items.Insert(0, initiatedItem);
            }


            return new SelectList(items, "Value", "Text");
        }
        public static SelectList BuyerProductType()
        {
            BuyerModelManager buyerManager = new BuyerModelManager();
            List<System.Web.Mvc.SelectListItem> items = buyerManager.Get().OrderBy(x => x.BuyerModelName).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.BuyerModelName,
                                                      Value = item.BuyerModelID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList InspectionType()
        {
            InspectionTypeManager inspectionTypeManager = new InspectionTypeManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = inspectionTypeManager.Get().OrderBy(x => x.Type).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.Type,
                                        Value = item.InspectionTypeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList Freight()
        {
            List<SelectListItem> rateList = new List<SelectListItem>();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            items.Add(new SelectListItem() { Text = "Paid", Value = "Paid" });
            items.Add(new SelectListItem() { Text = "To Pay", Value = "To Pay" });
            items.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });
            items.Add(new SelectListItem() { Text = "Included in Price", Value = "Included in Price" });

            rateList = (from r in items
                        select new SelectListItem
                        {
                            Text = r.Text,
                            Value = r.Value.ToString(),
                            Selected = false
                        }).ToList<SelectListItem>();

            return new SelectList(rateList, "Value", "Text");
        }
        public static SelectList FreightName()
        {
            List<SelectListItem> rateList = new List<SelectListItem>();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            items.Add(new SelectListItem() { Text = "ENCO SHOES", Value = "ENCO SHOES" });
            items.Add(new SelectListItem() { Text = "Supplier", Value = "Supplier" });

            rateList = (from r in items
                        select new SelectListItem
                        {
                            Text = r.Text,
                            Value = r.Value.ToString(),
                            Selected = false
                        }).ToList<SelectListItem>();

            return new SelectList(rateList, "Value", "Text");
        }
        public static SelectList FormName()
        {
            List<SelectListItem> rateList = new List<SelectListItem>();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            items.Add(new SelectListItem() { Text = "Applicable", Value = "Applicable" });
            items.Add(new SelectListItem() { Text = "Not Applicable", Value = "Not Applicable" });
            items.Add(new SelectListItem() { Text = "Not Issued", Value = "Not Issued" });

            rateList = (from r in items
                        select new SelectListItem
                        {
                            Text = r.Text,
                            Value = r.Value.ToString(),
                            Selected = false
                        }).ToList<SelectListItem>();

            return new SelectList(rateList, "Value", "Text");
        }
        public static SelectList UnitDivision()
        {
            IndentManager indentManager = new IndentManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = indentManager.GetUnitDivisionList().OrderBy(x => x.UnitCode).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.FullName,
                                        Value = item.UnitCodePK.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList IndentCompanyName()
        {
            IndentManager indentManager = new IndentManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = indentManager.GetCompanyName().OrderBy(x => x.CompanyCode).Select(
                item => new System.Web.Mvc.SelectListItem()
                {
                    Text = item.FullName,
                    Value = item.CompanyCodePK.ToString()
                }
                ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static List<tbl_Company> CompanyName()
        {
            IndentManager indentManager = new IndentManager();
            List<tbl_Company> _items = new List<tbl_Company>();
            _items = indentManager.GetCompanyName().ToList();
            string url = System.Configuration.ConfigurationManager.AppSettings["ReportURL"].ToString();
            if (url != null)
            {
                if (url.ToLower().Contains("mms.encoshoesapps.com"))
                {
                    _items = _items.Where(x => x.FullName == "Enco Shoes").ToList();
                }
                else if (url.ToLower().Contains("unit2.encoshoesapps.com"))
                {
                    _items = _items.Where(x => x.FullName == "Enco Shoes Unit - II").ToList();
                }
                else if (url.ToLower().Contains("mms.endurashoes.com") || url.ToLower().Contains("mms.mycipl.com"))
                {
                    _items = _items.Where(x => x.FullName == "Endura Shoes").ToList();
                }
            }
            return _items;
        }
        public static SelectList GetMRPNO()
        {
            IndentManager indentManager = new IndentManager();


            List<System.Web.Mvc.SelectListItem> items = indentManager.Get().Where(x => x.MRPNO != null).OrderBy(x => x.MRPNO).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.MRPNO.ToString(),
                                        Value = ""
                                    }
                                    ).ToList();

            items = items.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList MRPNO()
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            BillOfMaterialManager billOFMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialManager = new BOMMaterialListManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            var items = (from x in simpleMRPManager.Get()
                         join y in bomMaterialManager.GetMRPMaterialList() on x.SimpleMRPID equals y.SimpleMRPID
                         select new { MRPNO = x.MRPNO, x.SimpleMRPID });
            items = items.Where(x => x.MRPNO != null).ToList();
            var distinctList = items.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MRPNO,
                                                      Value = item.MRPNO.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");


        }
        public static SelectList GetIndentNo()
        {
            IndentManager indentManager = new IndentManager();
            List<System.Web.Mvc.SelectListItem> items = indentManager.Get().OrderBy(x => x.IndentNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IndentNo,
                                        Value = item.IndentId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetGrade()
        {
            IndentManager indentManager = new IndentManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = indentManager.GetGradeMasterList().OrderBy(x => x.GradeName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GradeName,
                                        Value = item.EmpGradeCodePK.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList IssueType()
        {
            IssueTypeManager IssueTypeManager = new IssueTypeManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = IssueTypeManager.Get().OrderBy(x => x.IssueType).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IssueType,
                                        Value = item.IssueTypeMasterId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList UserTypeName()
        {
            UserTypeManager Manager = new UserTypeManager();
            List<SelectListItem> items = Manager.Get().OrderBy(x => x.UserType).Select(item => new SelectListItem()
            {
                Text = item.UserType,
                Value = item.UserTypeID.ToString()
            }).ToList();
            var ShotName = new SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static tbl_PermissionSetting GetPermissionIdList()
        {
            int UserTypeID = Convert.ToInt32(HttpContext.Current.Session["UserType"]);
            PermissionSettingManager Manager = new PermissionSettingManager();
            tbl_PermissionSetting PermissionSetList = new tbl_PermissionSetting();
            PermissionSetList = Manager.GetByID(UserTypeID);
            return PermissionSetList;
        }
        public static List<tbl_Permission> GetPermissionList(string PageName, string PermissionListIDs)
        {
            PermissionManager Manager = new PermissionManager();
            List<tbl_Permission> PermissionSetList = new List<tbl_Permission>();
            PermissionSetList = Manager.GetPermissionList(PageName, PermissionListIDs);
            return PermissionSetList;
        }
        public enum Categoryname
        {
            [Description("Leathers")]
            Leathers
        };
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static SelectList GetIndentNoBasedOnSupplierName(int SupplierID)
        {
            IndentManager indentManager = new IndentManager();
            List<System.Web.Mvc.SelectListItem> items = indentManager.Get().OrderBy(x => x.IndentNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IndentNo,
                                        Value = item.IndentId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList ModeGEDoc()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Postal"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Hand Carried"
            };
            var ShotName4 = new System.Web.Mvc.SelectListItem()
            {
                Value = "3",
                Text = "Courier"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);
            items.Add(ShotName4);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GEVisitorToMeet()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Production Manager"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Admin"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);

            return new SelectList(items, "Value", "Text");

        }

        public static SelectList GEVisitorReason()
        {
            GEVisitReasonManager visitorManager = new GEVisitReasonManager();

            List<System.Web.Mvc.SelectListItem> items = visitorManager.Get().OrderBy(x => x.Reason).Select(
                                                             item => new System.Web.Mvc.SelectListItem()
                                                             {
                                                                 Text = item.Reason,
                                                                 Value = item.GEVisitReasonId.ToString()
                                                             }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            items.Insert(0, ShotName);


            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GEVisitorEntryType()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Person"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Staff"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);

            return new SelectList(items, "Value", "Text");

        }

        public static SelectList InwardMaterialPO()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "With Po"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "WithOut Po"
            };

            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GateEntryEmpDesignation()
        {
            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();

            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            var list_ = visitorManager.GetEmployDesignationList().OrderBy(x => x.DesignationName);

            var distinctList = list_.GroupBy(x => x.DesignationName).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.DesignationName,
                                                      Value = item.EmpDesignationCodePK.ToString()
                                                  }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }

        public static SelectList GetDriverName()
        {
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            GateEntryInwardDocumentManager inwardManager = new GateEntryInwardDocumentManager();
            GateEntryVisitorManager visitManager = new GateEntryVisitorManager();
            var result = from x in inwardManager.GetEmployNameList()
                         join y in visitManager.GetEmployDesignationList() on x.EmpDesignationCodeFK equals y.EmpDesignationCodePK
                         where y.DesignationName == "DRIVER"
                         select new SelectListItem { Value = y.EmpDesignationCodePK.ToString(), Text = x.Name };
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            _items = result.ToList();
            _items.Insert(0, ShotName);

            return new SelectList(_items, "Value", "Text");
        }

        public static SelectList GetVehicleName()
        {

            VehicleManager inwardManager = new VehicleManager();

            List<System.Web.Mvc.SelectListItem> items = inwardManager.Get().OrderBy(x => x.VehicleName).Select(
                                                             item => new System.Web.Mvc.SelectListItem()
                                                             {
                                                                 Text = item.VehicleName + " / " + item.VehicleNo,
                                                                 Value = item.VehicleId.ToString()
                                                             }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };


            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetVehicleNo()
        {

            VehicleManager inwardManager = new VehicleManager();

            List<System.Web.Mvc.SelectListItem> items = inwardManager.Get().OrderBy(x => x.VehicleNo).Select(
                                                             item => new System.Web.Mvc.SelectListItem()
                                                             {
                                                                 Text = item.VehicleNo,
                                                                 Value = item.VehicleNo
                                                             }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };


            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }


        public static SelectList GateEntryEmpName()
        {
            GateEntryInwardDocumentManager visitorManager = new GateEntryInwardDocumentManager();

            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            var list_ = visitorManager.GetEmployNameList().OrderBy(x => x.Name);

            var distinctList = list_.GroupBy(x => x.Name).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Name,
                                                      Value = item.EmployeeCodePK.ToString()
                                                  }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }

        public static SelectList GateEntryEmpNameWithCode()
        {
            GateEntryInwardDocumentManager visitorManager = new GateEntryInwardDocumentManager();

            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            var list_ = visitorManager.GetEmployNameList().OrderBy(x => x.Name);

            var distinctList = list_.GroupBy(x => x.Name).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.Name + "--" + item.EmployeeID,
                                                      Value = item.EmployeeID
                                                  }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }

        public static SelectList GateEntryEmpDept()
        {
            GateEntryInwardDocumentManager visitorManager = new GateEntryInwardDocumentManager();

            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            var list_ = visitorManager.GetEmployDeptList().OrderBy(x => x.DepartmentName);

            var distinctList = list_.GroupBy(x => x.DepartmentName).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.DepartmentName,
                                                      Value = item.EmpDepartmentCodePK.ToString()
                                                  }).ToList();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return new SelectList(items_, "Value", "Text");
        }

        public static SelectList GateEntryInwardMaterials()
        {
            GEMaterialTypeManger inwardManager = new GEMaterialTypeManger();
            List<System.Web.Mvc.SelectListItem> items = inwardManager.Get().OrderBy(x => x.GEMaterialTypeId).Select(
                                                item => new System.Web.Mvc.SelectListItem()
                                                {

                                                    Value = item.GEMaterialTypeId.ToString(),
                                                    Text = item.MaterialType,

                                                }).ToList();
            var cname = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, cname);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GateEntryInwardDocType()
        {
            GEInwardDocTypeManger inwardManager = new GEInwardDocTypeManger();
            List<System.Web.Mvc.SelectListItem> items = inwardManager.Get().OrderBy(x => x.DocumentType).Select(
                                                item => new System.Web.Mvc.SelectListItem()
                                                {

                                                    Value = item.InwardDocumentTypeId.ToString(),
                                                    Text = item.DocumentType,

                                                }).ToList();
            var cname = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, cname);


            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GEVisitorDesignation()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Associates"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Manager"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);

            return new SelectList(items, "Value", "Text");

        }

        public static SelectList GEVisitorType()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Visitor"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "staff"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);

            return new SelectList(items, "Value", "Text");

        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static SelectList ReportGateEntryOutwardNO()
        {
            GateEntryOutwardManager grnManager = new GateEntryOutwardManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.GateEntryNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.GateEntryNo.ToString(),
                                        Value = item.GateEntryOutwardId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetGateEntryOutwardDCNO()
        {
            GateEntryOutwardManager grnManager = new GateEntryOutwardManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.DcNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = !string.IsNullOrEmpty(item.DcNo) ? item.DcNo.ToString() : "",
                                        Value = item.GateEntryOutwardId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetGateEntryInwardDCNO()
        {
            GateEntryInwardMaterialsManager grnManager = new GateEntryInwardMaterialsManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.DcNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.DcNo,
                                        Value = item.GateEntryInwardId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetGateEntryInwardMaterials()
        {
            GateEntryInwardMaterialsManager grnManager = new GateEntryInwardMaterialsManager();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = grnManager.Get().OrderBy(x => x.MaterialNameId).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.MaterialNameId.ToString(),
                                        Value = item.GateEntryInwardId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }

        //Get DriverName from DriverMaster Table
        public static SelectList GetDriver()
        {

            GateEntryVehicleManager driverManager = new GateEntryVehicleManager();

            List<System.Web.Mvc.SelectListItem> items = driverManager.GetDriverList().OrderBy(x => x.DriverName).Select(
                                                             item => new System.Web.Mvc.SelectListItem()
                                                             {
                                                                 Text = item.DriverName,
                                                                 Value = item.DriverID.ToString()
                                                             }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList AddressTypebind()
        {
            CustAddressMangers AddressTypeManager = new CustAddressMangers();
            List<System.Web.Mvc.SelectListItem> items = AddressTypeManager.GetAddtype().OrderBy(x => x.AddTypeName).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.AddTypeName,
                                        Value = item.AddTypeID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList ProductProcessTypebind()
        {
            InvoiceManager oManager = new InvoiceManager();
            List<System.Web.Mvc.SelectListItem> items = oManager.GetProductProcess().OrderBy(x => x.process).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.process,
                                        Value = item.ProcessID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList getbuyOrderListdropdown()
        {
            BuyerOrderEntryManager oManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = oManager.getbuyOrderListdropdown().OrderBy(x => x.orderid).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.buyerorderslno,
                                        Value = item.orderid.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList GetProductName()
        {
            SubAssemblyMasterManagers Manager = new SubAssemblyMasterManagers();
            List<System.Web.Mvc.SelectListItem> _items = new List<System.Web.Mvc.SelectListItem>();
            List<System.Web.Mvc.SelectListItem> items = Manager.GetSubAssemblyMaster().Where(m=>m.IsActive == true).OrderBy(x => x.Id).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.ProductName.ToString(),
                                        Value = item.ProductId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select Product"
            };
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        #endregion

        public class DropdownViewModel
        {
            [Display(Name = "Choose indent no")]
            public int? SelectedIndentNO { get; set; }
        }

        #region jobwork

        public static int getProcessCodeId()
        {

            ProcessManager Processmanager = new ProcessManager();
            int? SID = Processmanager.Get().Max(u => (int?)u.ProcessId);
            return Convert.ToInt32(SID);
        }
        public static SelectList ProcessName()
        {
            ProcessManager Processmanager = new ProcessManager();
            List<System.Web.Mvc.SelectListItem> items = Processmanager.Get().OrderBy(x => x.ProcessName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.ProcessName,
                                                     Value = Convert.ToString(item.ProcessId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList UnitConvertionName()
        {
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            List<System.Web.Mvc.SelectListItem> items = Job_UnitConvertionManager.Get().OrderBy(x => x.UC_No_Id).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.UC_No_Code,
                                                     Value = Convert.ToString(item.UC_No_Id)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList JobworkName()
        {
            JobworkManager JobworkManager = new JobworkManager();
            List<System.Web.Mvc.SelectListItem> items = JobworkManager.Get().OrderBy(x => x.JW_Name).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.JW_Name,
                                                     Value = Convert.ToString(item.JW_Id)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList StageName()
        {
            StageMasterManager StageMasterManager = new StageMasterManager();
            List<System.Web.Mvc.SelectListItem> items = StageMasterManager.Get().OrderBy(x => x.Sequence).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.StageName,
                                                     Value = Convert.ToString(item.StageMasterId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList StageName_code()
        {
            StageMasterManager StageMasterManager = new StageMasterManager();
            List<System.Web.Mvc.SelectListItem> items = StageMasterManager.Get().OrderBy(x => x.Sequence).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.StageCode,
                                                     Value = Convert.ToString(item.StageMasterId)
                                                 }).ToList();
            var ColorName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ColorName);
            return new SelectList(items, "Value", "Text");
        }
        public static SelectList Stage_ordertype()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Full Shoes"
            };

            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Upper",
            };
            listItems.Add(ShotName);
            listItems.Add(ShotName2);
            listItems.Add(ShotName3);

            return new SelectList(listItems, "Value", "Text");
        }
        public static SelectList Io_Based()
        {
            BuyerOrderEntryManager BuyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = BuyerOrderEntryManager.Get().OrderBy(x => x.OurStyle).Where(X => X.IsInternal == true).GroupBy(x => new { x.OurStyle, x.BuyerOrderSlNo }).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.Key.BuyerOrderSlNo,
                                                     Value = Convert.ToString(item.Key.OurStyle)
                                                 }).ToList();
            var Ordername = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, Ordername);
            return new SelectList(items, "Value", "Text");
        }
        public static int getjob_UnitconvertionCodeId()
        {
            Job_UnitConvertionManager bm = new Job_UnitConvertionManager();
            int? SID = bm.Get().Max(u => (int?)u.UC_No_Id);
            return Convert.ToInt32(SID);
        }
        public static SelectList Production_Type()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Production "
            };

            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Jobwork",
            };
            listItems.Add(ShotName);
            listItems.Add(ShotName2);
            listItems.Add(ShotName3);

            return new SelectList(listItems, "Value", "Text");
        }
        public static SelectList Side()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Left "
            };

            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Right",
            };
            listItems.Add(ShotName);
            listItems.Add(ShotName2);
            listItems.Add(ShotName3);

            return new SelectList(listItems, "Value", "Text");
        }
        public static SelectList Type_QcWork()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Recut "
            };

            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Rework",
            };
            listItems.Add(ShotName);
            listItems.Add(ShotName2);
            listItems.Add(ShotName3);

            return new SelectList(listItems, "Value", "Text");
        }
        #region
        public static int getJobCodeId()
        {
            JobworkManager JobworkManager = new JobworkManager();
            int? SID = JobworkManager.Get().Max(u => (int?)u.JW_Id);
            return Convert.ToInt32(SID);
        }
        public static int getJobLeatherCodeId()
        {
            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            int? SID = JobLeather_leatherManager.Get().Max(u => (int?)u.Job_Lether_to_lether_id);
            return Convert.ToInt32(SID);
        }
        public static int Getjobsheet_pair_code_id()
        {
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            int? SID = JobSheet_PairManager.GetJobsheet_paircode_Tbl().Max(u => (int?)u.jobsheet_pair_code_id);
            return Convert.ToInt32(SID);
        }
        public static int GetJob_production_code_id()
        {
            ProductionJobworkMasterManager ProductionJobworkMasterManager = new ProductionJobworkMasterManager();
            int? SID = ProductionJobworkMasterManager.Get_byCode(1).Max(u => (int?)u.ProductionJobwork_code_id);
            return Convert.ToInt32(SID);
        }
        public static int GetJob_production_code_id1()
        {
            ProductionManager productionManager = new ProductionManager();
            int? SID = productionManager.Get_byCodes(1).Max(u => (int?)u.ProductionId);
            return Convert.ToInt32(SID);
        }

        public static int GetJob_production_Qc_code_id()
        {
            ProductionQcManager ProductionQcManager = new ProductionQcManager();
            int? SID = ProductionQcManager.Get_byCode().Max(u => (int?)u.ProductionQc_ID);
            return Convert.ToInt32(SID);
        }
        public static int GetJob_OtherWork_code_id()
        {
            JobOtherWorkManager JobOtherworkManager = new JobOtherWorkManager();
            int? SID = JobOtherworkManager.Get().Max(u => (int?)u.OtherJobWork_Id);
            return Convert.ToInt32(SID);
        }
        public static SelectList Grn_Reason()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Material Shortage"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Pending"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);

            return new SelectList(items, "Value", "Text");

        }
        public static SelectList Qc_Stage()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };
            var ShotName2 = new System.Web.Mvc.SelectListItem()
            {
                Value = "1",
                Text = "Stage 1"
            };
            var ShotName3 = new System.Web.Mvc.SelectListItem()
            {
                Value = "2",
                Text = "Stage 2"
            };

            var ShotName4 = new System.Web.Mvc.SelectListItem()
            {
                Value = "3",
                Text = "Stage 3"
            };
            var ShotName5 = new System.Web.Mvc.SelectListItem()
            {
                Value = "4",
                Text = "Stage 4"
            };

            var ShotName6 = new System.Web.Mvc.SelectListItem()
            {
                Value = "5",
                Text = "Stage 5"
            };
            items.Add(ShotName);
            items.Add(ShotName2);
            items.Add(ShotName3);
            items.Add(ShotName4);
            items.Add(ShotName5);
            items.Add(ShotName6);
            return new SelectList(items, "Value", "Text");

        }
        public static SelectList GET_SupplierName_Lotno()
        {

            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();

            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "0",
                Text = "Please Select"
            };

            items.Add(ShotName);

            return new SelectList(items, "Value", "Text");

        }
        #endregion
        #endregion

    }


}