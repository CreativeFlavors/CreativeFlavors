using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.ViewModel;
using MMS.Web.Models.Production;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace MMS.Web.Controllers
{
    public class ProductionController : Controller
    {
        public ActionResult ProductionMaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductionGrid(int page = 1, int pageSize = 9)
        {
            try
            {
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                ProductionSubassemblyManager productionSubassemblyManager = new ProductionSubassemblyManager();
                StatusProductionManager statusProductionManager = new StatusProductionManager();
                StatusProductionSubassemblyManager statusProductionSubassemblyManager = new StatusProductionSubassemblyManager();
                // Fetch production data
                var productions = (from P in productionManager.GetProductions()
                                   join pr in productManager.Get() on P.ProductId equals pr.ProductId
                                   join sp in statusProductionManager.Get() on P.ProductionStatus equals sp.StatusId
                                   select new ProductionViewModel
                                   {
                                       ProductionId = P.ProductionId,
                                       ProductionCode = P.ProductionCode,
                                       ProductionQty = P.ProductionQty,
                                       RequiredQty = P.RequiredQty,
                                       ProductName = pr.ProductName,
                                       ProductionType = "Production",
                                       ProductionStatus = sp.Status
                                   }).ToList();
                // Fetch production subassembly data
                var productionSubassemblies = (from psa in productionSubassemblyManager.GetProductions()
                                               join pr in productManager.Get() on psa.ProductId equals pr.ProductId
                                               join sps in statusProductionSubassemblyManager.Get() on psa.ProductionSubassemblyStatus equals sps.StatusId
                                               select new ProductionViewModel
                                               {
                                                   ProductionId = psa.ProductionId,
                                                   ProductionCode = psa.ProductionCode,
                                                   ProductionQty = psa.ProductionQty,
                                                   RequiredQty = psa.RequiredQty,
                                                   ProductName = pr.ProductName,
                                                   ProductionType = "Subassembly",
                                                   ProductionStatus = sps.Status
                                               }).ToList();
                // Combine both lists
                productions.AddRange(productionSubassemblies);
                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.ProductionId)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToList();

                ViewBag.Productions = productions;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;

                return PartialView("~/Views/Production/Partial/ProductionGrid.cshtml");
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }


        [HttpGet]
        public ActionResult ProductionDetails(int? productionId)
        {
            ProductionModel model = new ProductionModel();

            // Check if productionId is null, indicating a new entry
            if (productionId == null)
            {               // Generate production code for new entry
                string productionCode = GenerateBatchCode();
                model.ProductionCode = productionCode;
                model.ProductionDate = DateTime.Today;
            }

            return View(model);
        }

        #region Curd Operation
        [HttpPost]
        public ActionResult SaveProduction(ProductionModel model)
        {
            try
            {
                Production production = new Production();
                ProductionMaterial productionMaterial = new ProductionMaterial();
                string status = "";
                ProductionManager productionManager = new ProductionManager();
                ProductionMaterialManager productionMaterialManager = new ProductionMaterialManager();
                production = productionManager.Getproductionid(model.ProductionId);
                ProductManager productManager = new ProductManager();
                product products = new product();
                products = productManager.GetId(model.ProductId);

                if (model.ProductionId == 0)
                {
                    production.ProductionId = model.ProductionId;
                    production.ProductionDate = model.ProductionDate;
                    production.ProductCode = model.ProductCode;
                    production.ProductionCode = model.ProductionCode;
                    production.ProductionQty = model.ProductionQty;
                    production.ProductionStatus = model.ProductionStatus;
                    production.ProductId = model.ProductId;
                    production.MinQty = model.MinQty;
                    production.MaxQty = model.MaxQty;
                    production.RequiredQty = model.RequiredQty;
                    production.StoreCode = model.StoreCode;
                    production.ProductionPerDay = model.ProductionPerDay;
                    production.ProductionDueDate = model.ProductionDueDate;
                    production.ProductionFullfillDate = model.ProductionFullfillDate;
                    production.RefDocNo = model.RefDocNo;
                    production.RefDocDate = model.RefDocDate;
                    production.Status1Code = model.Status1Code;
                    production.Status1Date = model.Status1Date;
                    production.Status1By = model.Status1By;
                    production.Status2Code = model.Status2Code;
                    production.Status2Date = model.Status2Date;
                    production.Status2By = model.Status2By;
                    production.Status3Code = model.Status3Code;
                    production.Status3Date = model.Status3Date;
                    production.Status3By = model.Status3By;
                    production.Productions = model.Productions;
                    production.SubAssembly = model.SubAssembly;
                    production.Inprogress = model.Inprogress;

                    productionManager.Post(production);
                    status = "Inserted";
                }
                else
                {

                    production.ProductionId = model.ProductionId;
                    production.ProductionDate = model.ProductionDate;
                    production.ProductionCode = model.ProductionCode;
                    production.ProductionQty = model.ProductionQty;
                    production.ProductionStatus = model.ProductionStatus;
                    production.ProductId = model.ProductId;
                    production.MinQty = model.MinQty;
                    production.MaxQty = model.MaxQty;
                    production.RequiredQty = model.RequiredQty;
                    production.StoreCode = model.StoreCode;
                    production.ProductionPerDay = model.ProductionPerDay;
                    production.ProductionDueDate = model.ProductionDueDate;
                    if (model.ProductionStatus == 7)
                    {
                        production.ProductionFullfillDate = DateTime.Now;
                    }
                    else
                    {
                        production.ProductionFullfillDate = model.ProductionFullfillDate;
                    }
                    production.RefDocNo = model.RefDocNo;
                    production.RefDocDate = model.RefDocDate;
                    production.Status1Code = model.Status1Code;
                    production.Status1Date = model.Status1Date;
                    production.Status1By = model.Status1By;
                    production.Status2Code = model.Status2Code;
                    production.Status2Date = model.Status2Date;
                    production.Status2By = model.Status2By;
                    production.Status3Code = model.Status3Code;
                    production.Status3Date = model.Status3Date;
                    production.Status3By = model.Status3By;
                    production.Productions = model.Productions;
                    production.SubAssembly = model.SubAssembly;
                    production.Inprogress = model.Inprogress;

                    productionManager.Put(production);
                    status = "Updated";
                }
                // Retrieve JSON data from AJAX request & production material consume values save to productionmaterial table
                string jsonData = HttpContext.Request.Form["tableData"];

                // Deserialize JSON data into array of objects
                List<ProductionMaterial> tableData = JsonConvert.DeserializeObject<List<ProductionMaterial>>(jsonData);

                // Save ProductionMaterial records
                foreach (var productionMaterials in tableData)
                {
                    ProductionMaterial productionMaterial1 = new ProductionMaterial();
                    productionMaterial1.ProductsId = production.ProductId;
                    productionMaterial1.ProductsMatId = productionMaterials.ProductsMatId;
                    productionMaterial1.ConsumeQty = productionMaterials.ConsumeQty;
                    productionMaterial1.StockOnHand = productionMaterials.StockOnHand;
                    productionMaterial1.ProductionId = production.ProductionId;
                    productionMaterial1.StoreCode = production.StoreCode;
                    productionMaterial1.ProductionDate = production.ProductionDate;
                    productionMaterial1.IsActive = true;
                    productionMaterial1.ProductType = productionMaterials.ProductType;
                    productionMaterialManager.Post(productionMaterial1);
                }
                // Check if the production status has changed to "packing" means update the batchstock table
                if (model.ProductionStatus == 7)
                {
                   // Update the batch table with relevant data from Production table
                    BatchStockManager batchStockManager = new BatchStockManager();
                    //BatchStock existingBatchStock = batchStockManager.GetByProductCode(model.ProductCode);
                    //if(existingBatchStock != null)
                    //{
                    //    //already quantity is there Update the stock quantity increased to batchstock table
                    //    existingBatchStock.Quantity += model.ProductionQty;
                    //    existingBatchStock.AltBatchCode = model.ProductionCode;
                    //    batchStockManager.Put(existingBatchStock);
                    //    status = "Packing";
                    //}
                    //else
                    //{
                        //insert new record to batchstock table
                        BatchStock batchStock = new BatchStock
                        {
                            StoreCode = production.StoreCode,
                            productid = products.ProductId,
                            BatchCode = production.ProductionCode,
                            Quantity = production.ProductionQty,
                            Price = products.Price,
                            TaxCode = products.TaxMasterId,
                            UomId = products.UomMasterId,
                            producttype = products.ProductType,
                            ProductCode = production.ProductCode,
                        };
                        batchStockManager.POST(batchStock);
                        status = "Packing";
                    //}
                    // Update the FinishedGood table with relevant data from Production table
                    //FinishedGoodManager finishedGoodManager = new FinishedGoodManager();
                    //FinishedGood existingFinishedGood = finishedGoodManager.GetByProductCode(model.ProductCode);
                    //if (existingFinishedGood != null)
                    //{
                    //    //already quantity is there Update the stock quantity increased to finishedgood table
                      // existingFinishedGood.Quantity += model.ProductionQty;
                        //finishedGoodManager.Put(existingFinishedGood);
                    //  /  status = "Packing";
                   // }
                    //else
                    //{
                    //    //insert new record to finishedgood table
                    //    FinishedGood finishedGood = new FinishedGood
                    //    {
                    //        Batchcode = production.ProductionCode,
                    //        StoreCode = production.StoreCode,
                    //        Quantity = production.ProductionQty,
                    //        ProductCode = production.ProductCode,
                    //        UpdatedDate = DateTime.Now,
                    //        Price = products.Price,
                    //        ProductType = products.ProductType,
                    //        ProductId = products.ProductId,

                    //    };

                    //    finishedGoodManager.Post(finishedGood);
                    //    status = "Packing";
                    //}

                }
                // Update status history table and productmaterial stock decreased when status changes from 2 Inprogress
                else if (model.ProductionStatus == 2)
                {
                    List<ProductionMaterial> productionMaterial1 = productionMaterialManager.GetProductionMaterial(production.ProductionId);
                    foreach (var productionMaterials in productionMaterial1)
                    {
                        BatchStockManager batchStockManager = new BatchStockManager();
                        List<BatchStock> batchStocks = batchStockManager.GetBatchProductMaterialStocks(productionMaterials.ProductsMatId);
                        foreach (var batchStock in batchStocks)
                        {
                            batchStock.Quantity -= productionMaterials.ConsumeQty;
                            //batchStockManager.Put(batchStock);
                        }
                    }
                    StatusHistoryManager statusHistoryManager = new StatusHistoryManager();
                    StatusHistory statusHistory = new StatusHistory
                    {
                        ProductCode = production.ProductCode,
                        InProgressCode = production.ProductionStatus,
                        InProgressDate = DateTime.Now,
                        InProgressBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "Inprogress";
                }
                // Update status history table when status changes from 3 pending
                else if (model.ProductionStatus == 3)
                {
                    StatusHistoryManager statusHistoryManager = new StatusHistoryManager();
                    StatusHistory statusHistory = new StatusHistory
                    {
                        ProductCode = production.ProductCode,
                        PendingCode = production.ProductionStatus,
                        PendingDate = DateTime.Now,
                        PendingBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "Pending";
                }
                // Update status history table when status changes from 4 qualitychecking
                else if (model.ProductionStatus == 4)
                {
                    StatusHistoryManager statusHistoryManager = new StatusHistoryManager();
                    StatusHistory statusHistory = new StatusHistory
                    {
                        ProductCode = production.ProductCode,
                        QualityCheckingCode = production.ProductionStatus,
                        QualityCheckingDate = DateTime.Now,
                        QualityCheckingBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "qualitychecking";
                }
                // Update status history table when status changes from 6 sequence
                else if (model.ProductionStatus == 6)
                {
                    StatusHistoryManager statusHistoryManager = new StatusHistoryManager();
                    StatusHistory statusHistory = new StatusHistory
                    {
                        ProductCode = production.ProductCode,
                        SequenceCode = production.ProductionStatus,
                        SequenceDate = DateTime.Now,
                        SequenceBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "Sequence";
                }


                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveSubassembly(ProductionModel model)
        {
            try
            {
                Productionsubassembly production = new Productionsubassembly();
                string status = "";
                ProductionMaterialManager productionMaterialManager = new ProductionMaterialManager();
                ProductionSubassemblyManager productionSubassemblyManager = new ProductionSubassemblyManager();
                production = productionSubassemblyManager.Getproductionid(model.ProductionId);
                ProductManager productManager = new ProductManager();
                product products = new product();
                products = productManager.GetId(model.ProductId);

                if (model.ProductionId == 0)
                {
                    production.ProductionId = model.ProductionId;
                    production.ProductionDate = model.ProductionDate;
                    production.ProductCode = model.ProductCode;
                    production.ProductionCode = model.ProductionCode;
                    production.ProductionQty = model.ProductionQty;
                    production.ProductionSubassemblyStatus = model.ProductionSubassemblyStatus;
                    production.ProductId = model.ProductId;
                    production.MinQty = model.MinQty;
                    production.MaxQty = model.MaxQty;
                    production.RequiredQty = model.RequiredQty;
                    production.StoreCode = model.StoreCode;
                    production.ProductionPerDay = model.ProductionPerDay;
                    production.ProductionDueDate = model.ProductionDueDate;
                    production.ProductionFullfillDate = model.ProductionFullfillDate;
                    production.RefDocNo = model.RefDocNo;
                    production.RefDocDate = model.RefDocDate;
                    production.Status1Code = model.Status1Code;
                    production.Status1Date = model.Status1Date;
                    production.Status1By = model.Status1By;
                    production.Status2Code = model.Status2Code;
                    production.Status2Date = model.Status2Date;
                    production.Status2By = model.Status2By;
                    production.Status3Code = model.Status3Code;
                    production.Status3Date = model.Status3Date;
                    production.Status3By = model.Status3By;
                    production.Productions = model.Productions;
                    production.SubAssembly = model.SubAssembly;
                    production.Inprogress = model.Inprogress;

                    productionSubassemblyManager.Post(production);
                    status = "Inserted";
                }
                else
                {

                    production.ProductionId = model.ProductionId;
                    production.ProductionDate = model.ProductionDate;
                    production.ProductionCode = model.ProductionCode;
                    production.ProductionQty = model.ProductionQty;
                    production.ProductionSubassemblyStatus = model.ProductionSubassemblyStatus;
                    production.ProductId = model.ProductId;
                    production.MinQty = model.MinQty;
                    production.MaxQty = model.MaxQty;
                    production.RequiredQty = model.RequiredQty;
                    production.StoreCode = model.StoreCode;
                    production.ProductionPerDay = model.ProductionPerDay;
                    production.ProductionDueDate = model.ProductionDueDate;
                    if (model.ProductionSubassemblyStatus == 6)
                    {
                        production.ProductionFullfillDate = DateTime.Now;
                    }
                    else
                    {
                        production.ProductionFullfillDate = model.ProductionFullfillDate;
                    }
                    production.RefDocNo = model.RefDocNo;
                    production.RefDocDate = model.RefDocDate;
                    production.Status1Code = model.Status1Code;
                    production.Status1Date = model.Status1Date;
                    production.Status1By = model.Status1By;
                    production.Status2Code = model.Status2Code;
                    production.Status2Date = model.Status2Date;
                    production.Status2By = model.Status2By;
                    production.Status3Code = model.Status3Code;
                    production.Status3Date = model.Status3Date;
                    production.Status3By = model.Status3By;
                    production.Productions = model.Productions;
                    production.SubAssembly = model.SubAssembly;
                    production.Inprogress = model.Inprogress;

                    productionSubassemblyManager.Put(production);
                    status = "Updated";
                }

                // Retrieve JSON data from AJAX request & production material consume values save to productionmaterial table
                string jsonData = HttpContext.Request.Form["tableData"];

                // Deserialize JSON data into array of objects
                List<ProductionMaterial> tableData = JsonConvert.DeserializeObject<List<ProductionMaterial>>(jsonData);

                // Save ProductionMaterial records
                foreach (var productionMaterials in tableData)
                {
                    ProductionMaterial productionMaterial1 = new ProductionMaterial();
                    productionMaterial1.ProductsId = production.ProductId;
                    productionMaterial1.ProductsMatId = productionMaterials.ProductsMatId;
                    productionMaterial1.ConsumeQty = productionMaterials.ConsumeQty;
                    productionMaterial1.StockOnHand = productionMaterials.StockOnHand;
                    productionMaterial1.ProductionId = production.ProductionId;
                    productionMaterial1.StoreCode = production.StoreCode;
                    productionMaterial1.ProductionDate = production.ProductionDate;
                    productionMaterial1.IsActive = true;
                    productionMaterial1.ProductType = productionMaterials.ProductType;
                    productionMaterialManager.Post(productionMaterial1);
                }
                // Check if the production status has changed to "Released for Assembly" means update the subassemblyinventory table
                if (model.ProductionSubassemblyStatus == 6)
                {
                    // Update the batch table with relevant data from Production table
                    BatchStockManager batchStockManager = new BatchStockManager();
                    //BatchStock existingBatchStock = batchStockManager.GetByProductCode(model.ProductCode);
                    //if (existingBatchStock != null)
                    //{
                    //    //already quantity is there Update the stock quantity increased to batchstock table
                    //    existingBatchStock.Quantity += model.ProductionQty;
                    //    existingBatchStock.AltBatchCode = model.ProductionCode;
                    //    batchStockManager.Put(existingBatchStock);
                    //    status = "ReleasedForAssembly";
                    //}
                    //else
                    //{
                        //insert new record to batchstock table
                        BatchStock batchStock = new BatchStock
                        {
                            StoreCode = production.StoreCode,
                            productid = products.ProductId,
                            BatchCode = production.ProductionCode,
                            Quantity = production.ProductionQty,
                            Price = products.Price,
                            TaxCode = products.TaxMasterId,
                            UomId = products.UomMasterId,
                            producttype = products.ProductType,
                            ProductCode = production.ProductCode,
                        };
                        batchStockManager.POST(batchStock);
                        status = "ReleasedForAssembly";
                    //}
                    //// Update the subassemblyinventory table with relevant data from Productionsubassembly table
                    //SubassemblyinventoryManager subassemblyinventoryManager = new SubassemblyinventoryManager();
                    //Subassemblyinventory existingsubassembly = subassemblyinventoryManager.GetByProductCode(model.ProductCode);
                    //if (existingsubassembly != null)
                    //{
                    //    //already quantity is there Update the stock quantity increased to subassemblyinventory table
                    //    existingsubassembly.Quantity += model.ProductionQty;
                    //    subassemblyinventoryManager.Put(existingsubassembly);
                    //    status = "Released for Assembly";
                    //}
                    //else
                    //{
                    //    //insert new record to subassemblyinventory table
                    //    Subassemblyinventory subassemblyinventory = new Subassemblyinventory
                    //    {
                    //        Batchcode = production.ProductionCode,
                    //        StoreCode = production.StoreCode,
                    //        Quantity = production.ProductionQty,
                    //        ProductCode = production.ProductCode,
                    //        UpdatedDate = DateTime.Now,
                    //        Price = products.Price,
                    //        ProductType = products.ProductType,
                    //        ProductId = products.ProductId,

                    //    };

                    //    subassemblyinventoryManager.Post(subassemblyinventory);
                    //    status = "Released for Assembly";
                    //}

                }
                // Update status history table and subassembly materials decreased when status changes from 2 Inprogress
                else if (model.ProductionSubassemblyStatus == 2)
                {
                    List<ProductionMaterial> productionMaterial1 = productionMaterialManager.GetProductionMaterial(production.ProductionId);
                    foreach (var productionMaterials in productionMaterial1)
                    {
                        BatchStockManager batchStockManager = new BatchStockManager();
                        List<BatchStock> batchStocks = batchStockManager.GetBatchProductMaterialStocks(productionMaterials.ProductsMatId);
                        foreach (var batchStock in batchStocks)
                        {
                            batchStock.Quantity -= productionMaterials.ConsumeQty;
                            //batchStockManager.Put(batchStock);
                        }
                    }
                    StatusHistorySubassemblyManager statusHistoryManager = new StatusHistorySubassemblyManager();
                    StatusHistorySubassembly statusHistory = new StatusHistorySubassembly
                    {
                        ProductCode = production.ProductCode,
                        InProgressCode = production.ProductionSubassemblyStatus,
                        InProgressDate = DateTime.Now,
                        InProgressBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "Inprogress";
                }
                // Update status history table when status changes from 3 QualityInspection
                else if (model.ProductionSubassemblyStatus == 3)
                {
                    StatusHistorySubassemblyManager statusHistoryManager = new StatusHistorySubassemblyManager();
                    StatusHistorySubassembly statusHistory = new StatusHistorySubassembly
                    {
                        ProductCode = production.ProductCode,
                        QualityInspectionCode = production.ProductionSubassemblyStatus,
                        QualityInspectionDate = DateTime.Now,
                        QualityInspectionBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "QualityInspection";
                }
                // Update status history table when status changes from 4 Completed
                else if (model.ProductionSubassemblyStatus == 4)
                {
                    StatusHistorySubassemblyManager statusHistoryManager = new StatusHistorySubassemblyManager();
                    StatusHistorySubassembly statusHistory = new StatusHistorySubassembly
                    {
                        ProductCode = production.ProductCode,
                        CompletedCode = production.ProductionSubassemblyStatus,
                        CompletedDate = DateTime.Now,
                        CompletedBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "Completed";
                }
                // Update status history table when status changes from 6 PendingApproval
                else if (model.ProductionSubassemblyStatus == 5)
                {
                    StatusHistorySubassemblyManager statusHistoryManager = new StatusHistorySubassemblyManager();
                    StatusHistorySubassembly statusHistory = new StatusHistorySubassembly
                    {
                        ProductCode = production.ProductCode,
                        PendingApprovalCode = production.ProductionSubassemblyStatus,
                        PendingApprovalDate = DateTime.Now,
                        PendingApprovalBy = production.CreatedBy,
                        ProductId = production.ProductId

                    };

                    statusHistoryManager.Post(statusHistory);
                    status = "PendingApproval";
                }


                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ProductionEdit(int productionid)
        {
            ProductionModel model = new ProductionModel();
            ProductionManager manager = new ProductionManager();
            var data = manager.Getproductionid(productionid);
            model.ProductionId = data.ProductionId;
            model.ProductionDate = data.ProductionDate;
            model.ProductCode = data.ProductCode;
            model.ProductionCode = data.ProductionCode;
            model.ProductionQty = data.ProductionQty;
            model.ProductionStatus = data.ProductionStatus;
            model.ProductId = data.ProductId;
            model.MinQty = data.MinQty;
            model.MaxQty = data.MaxQty;
            model.RequiredQty = data.RequiredQty;
            model.StoreCode = data.StoreCode;
            model.ProductionPerDay = data.ProductionPerDay;
            model.ProductionDueDate = data.ProductionDueDate;
            model.ProductionFullfillDate = data.ProductionFullfillDate;
            model.RefDocNo = data.RefDocNo;
            model.RefDocDate = data.RefDocDate;
            model.Status1Code = data.Status1Code;
            model.Status1Date = data.Status1Date;
            model.Status1By = data.Status1By;
            model.Status2Code = data.Status2Code;
            model.Status2Date = data.Status2Date;
            model.Status2By = data.Status2By;
            model.Status3Code = data.Status3Code;
            model.Status3Date = data.Status3Date;
            model.Status3By = data.Status3By;
            model.Productions = data.Productions;
            model.SubAssembly = data.SubAssembly;
            model.Inprogress = data.Inprogress;
            return View("~/Views/Production/ProductionDetails.cshtml", model);
        }

        [HttpGet]
        public ActionResult ProductionSubassemblyEdit(int productionid)
        {
            ProductionModel model = new ProductionModel();
            ProductionSubassemblyManager manager = new ProductionSubassemblyManager();
            var data = manager.Getproductionid(productionid);
            model.ProductionId = data.ProductionId;
            model.ProductionDate = data.ProductionDate;
            model.ProductCode = data.ProductCode;
            model.ProductionCode = data.ProductionCode;
            model.ProductionQty = data.ProductionQty;
            model.ProductionSubassemblyStatus = data.ProductionSubassemblyStatus;
            model.ProductId = data.ProductId;
            model.MinQty = data.MinQty;
            model.MaxQty = data.MaxQty;
            model.RequiredQty = data.RequiredQty;
            model.StoreCode = data.StoreCode;
            model.ProductionPerDay = data.ProductionPerDay;
            model.ProductionDueDate = data.ProductionDueDate;
            model.ProductionFullfillDate = data.ProductionFullfillDate;
            model.RefDocNo = data.RefDocNo;
            model.RefDocDate = data.RefDocDate;
            model.Status1Code = data.Status1Code;
            model.Status1Date = data.Status1Date;
            model.Status1By = data.Status1By;
            model.Status2Code = data.Status2Code;
            model.Status2Date = data.Status2Date;
            model.Status2By = data.Status2By;
            model.Status3Code = data.Status3Code;
            model.Status3Date = data.Status3Date;
            model.Status3By = data.Status3By;
            model.Productions = data.Productions;
            model.SubAssembly = data.SubAssembly;
            model.Inprogress = data.Inprogress;
            return View("~/Views/Production/ProductionDetails.cshtml", model);
        }
        public string GenerateBatchCode()
        {
            ProductionManager productionManager = new ProductionManager();
            ProductionSubassemblyManager productionSubassemblyManager = new ProductionSubassemblyManager();

            string yearMonth = DateTime.Now.ToString("yyMM");
            // Retrieve the last generated batch code from both sources
            string lastBatchCodeFromManager = productionManager.GetLatestBatchNumberFromDatabase();
            string lastBatchCodeFromSubassemblyManager = productionSubassemblyManager.GetLatestBatchNumberFromDatabase();
            // Determine the latest sequential number across both sources
            int nextSequentialNumber = 1;

            if (!string.IsNullOrEmpty(lastBatchCodeFromManager) || !string.IsNullOrEmpty(lastBatchCodeFromSubassemblyManager))
            {
                // Extract and compare the sequential numbers
                int lastSequentialNumberManager = ExtractSequentialNumber(lastBatchCodeFromManager);
                int lastSequentialNumberSubassemblyManager = ExtractSequentialNumber(lastBatchCodeFromSubassemblyManager);
                // Determine the maximum sequential number
                nextSequentialNumber = Math.Max(lastSequentialNumberManager, lastSequentialNumberSubassemblyManager) + 1;
            }

            // Format the next sequential number
            string formattedSequentialNumber = nextSequentialNumber.ToString("0000");

            // Combine yearMonth and formattedSequentialNumber to create the batch code
            string productionCode = $"{yearMonth}/{formattedSequentialNumber}";

            // Return the generated batch code
            return productionCode;
        }

        // Helper method to extract sequential number from batch code
        private int ExtractSequentialNumber(string batchCode)
        {
            if (string.IsNullOrEmpty(batchCode))
            {
                return 0;
            }

            int lastIndex = batchCode.LastIndexOf('/');
            string sequentialPart = batchCode.Substring(lastIndex + 1);

            if (int.TryParse(sequentialPart, out int sequentialNumber))
            {
                return sequentialNumber;
            }

            return 0; // Default to 0 if parsing fails
        }


        [HttpGet]
        public ActionResult ProductionSearch(string filter)
        {
            try
            {
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                // Get all productions and products
                List<Production> productions = productionManager.GetProductions();
                List<product> products = productManager.Get();
                List<ProductionModel> productionModels = new List<ProductionModel>();
                // Join productions with products to get additional information
                var totaldata = from P in productions
                                join pr in products on P.ProductId equals pr.ProductId
                                select new ProductionModel
                                {
                                    ProductionCode = P.ProductionCode,
                                    ProductionQty = P.ProductionQty,
                                    product = new product
                                    {
                                        ProductName = pr.ProductName,
                                    }
                                };
                // Filter productions based on the provided filter parameter
                productionModels = totaldata.Where(x => x.product.ProductName.ToLower().Contains(filter.ToLower()) || x.ProductionCode.ToLower().Contains(filter.ToLower())).ToList();

                return Json(productionModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion
        public ActionResult GetBomMaterialForProduction(int productid)
            {
            // Initialize variables
            decimal? minstock = 0;
            decimal? maxstock = 0;
            decimal? productionperday = 0;
            string productcode = null;
            decimal? quantity = 0;

            ProductManager productManager = new ProductManager();
            product product = productManager.GetId(productid);
            if (product != null)
            {
                minstock = product.MinStock;
                maxstock = product.MaxStock;
                productionperday = product.ProductionPerDay;
                productcode = product.ProductCode;
            }

        // for totalquantity ordered list
            Temp_productionManager temp_preproductionManager = new Temp_productionManager();
            preproduction preproduction = temp_preproductionManager.Getproductionqty(productid);
            if (preproduction != null)
            {
                quantity = preproduction.Qty;
            }

            // Listout productmaterial list for production start
            List<int> materialid = new List<int>();
            List<string> materialNames = new List<string>();
            List<int> producttype = new List<int>();
            List<decimal> consumes = new List<decimal>();
            List<decimal?> stockformaterial = new List<decimal?>();
            Temp_productionManager temp_ProductionManager = new Temp_productionManager();
            List<temp_production> temp_Productions = temp_ProductionManager.GetbomproductionMaterial(productid);
            foreach (var tempProductionItem in temp_Productions)
            {
                ProductManager productManager1 = new ProductManager();
                product materialInfo = productManager1.GetId(tempProductionItem.MaterialId);

                if (materialInfo != null)
                {
                    materialid.Add(materialInfo.ProductId);
                    materialNames.Add(materialInfo.ProductName);
                    producttype.Add(materialInfo.ProductType);
                    consumes.Add((decimal)tempProductionItem.Consume);                   
                }

                BatchStockManager batchStockManager = new BatchStockManager();
                List<BatchStock> batchStock = batchStockManager.GetBatchProductMaterialStocks(tempProductionItem.MaterialId);
                if (batchStock != null && batchStock.Any())
                {
                    decimal? totalStockQuantity = batchStock.Sum(bs => bs.Quantity);
                    stockformaterial.Add(totalStockQuantity);
                }
            }

        //Listout the productmaterial list for available to manufacture
            List<string> Bommaterialname = new List<string>();
            List<decimal?> requiredqty = new List<decimal?>();
            List<string> uom = new List<string>();
            List<decimal?> stockqty = new List<decimal?>();
            ProductManager productManagers = new ProductManager();
            product products = productManager.GetId(productid);
            Parentbom_materialManager parentbom_MaterialManager1 = new Parentbom_materialManager();
            //parentbom parentbom = parentbom_MaterialManager1.GetParentBomMaterialId(products.BomNo.ToString());
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            List<parentbom_material> parentbom_Material = parentbom_MaterialManager.GetMaterialList(products.BomNo);

            foreach (var parentBomMaterial in parentbom_Material)
            {
                ProductManager productManager1 = new ProductManager();
                product materialname = productManager1.GetId(parentBomMaterial.ProductId);

                if (materialname != null)
                {
                    Bommaterialname.Add(materialname.ProductName);
                    requiredqty.Add(parentBomMaterial.RequiredQty);
                }

                UOMManager uOMManager = new UOMManager();
                UomMaster uomname = uOMManager.GetUomMasterId(parentBomMaterial.UomId);
                if (uomname != null)
                {
                    uom.Add(uomname.ShortUnitName);
                }
                BatchStockManager batchStockManager = new BatchStockManager();
                List<BatchStock> batchStock = batchStockManager.GetBatchProductMaterialStocks(parentBomMaterial.ProductId);
                if (batchStock != null && batchStock.Any())
                {
                    decimal? totalStockQuantity = batchStock.Sum(bs => bs.Quantity);
                    stockqty.Add(totalStockQuantity);
                }
            }

        //Listout the subassembly manufacturing list
            List<string> Subassmblyname = new List<string>();
            List<decimal?> Subassmblyrequiredqty = new List<decimal?>();
            //List<string> Subassmblyuom = new List<string>();
            List<decimal?> Subassmblystockqty = new List<decimal?>();
            subassemblyManager subassemblyManager = new subassemblyManager();
            List<subassembly> subassemblylist = subassemblyManager.GetsubassemblyList(products.BomNo);

            foreach (var subassemblyitem in subassemblylist)
            {
                product product1 = productManager.GetId(subassemblyitem.ProductId);
                if (product1 != null)
                {
                    Subassmblyname.Add(product1.ProductName);
                    Subassmblyrequiredqty.Add(subassemblyitem.RequiredQty);
                }

                //UOMManager uOMManager = new UOMManager();
                //UomMaster uomname = uOMManager.GetUomMasterId(subassemblyitem.uomid);
                //if (uomname != null)
                //{
                //    uom.Add(uomname.ShortUnitName);
                //}
                BatchStockManager batchStockManager = new BatchStockManager();
                List<BatchStock> batchStock = batchStockManager.GetBatchProductMaterialStocks(subassemblyitem.ProductId);
                if (batchStock != null && batchStock.Any())
                {
                    decimal? totalStockQuantity = batchStock.Sum(bs => bs.Quantity);
                    Subassmblystockqty.Add(totalStockQuantity);
                }
            }
            return Json(new
            {
                Productsid = materialid,
                ProductType = producttype,
                bomdata = materialNames,
                Consumeqty = consumes,
                StockforMaterial = stockformaterial,
                Minstock = minstock,
                Maxstock = maxstock,
                producttotalitems = quantity,
                ProductionperDay = productionperday,
                ProductCode = productcode,
                BomMaterialNames = Bommaterialname,
                BomRequiredQtys = requiredqty,
                UOMs = uom,
                StockOnHand = stockqty,
                subassemblyname = Subassmblyname,
                subassemblyrequiredqty = Subassmblyrequiredqty,
                subassemblystockqty = Subassmblystockqty
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
