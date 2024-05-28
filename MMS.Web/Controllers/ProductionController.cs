using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.JobWork;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.JobWork;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.JobworkModel;
using MMS.Web.Models.Product;
using MMS.Web.Models.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace MMS.Web.Controllers
{
    public class ProductionController : Controller
    {
        public ActionResult ProductionMaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductionGrid(int page = 1, int pageSize = 8)
        {
            try
            {
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                //List<Production> productions = productionManager.Get();


                var totaldata = from P in productionManager.GetProductions()
                                join pr in productManager.Get() on P.ProductId equals pr.ProductId
                                select new ProductionModel
                                {
                                    ProductionCode=P.ProductionCode,
                                    ProductionQty=P.ProductionQty,
                                    RequiredQty=P.RequiredQty,
                                    product=new product
                                    {
                                        ProductName=pr.ProductName,
                                    }
                                };
                var totalCount = totaldata.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                totaldata = totaldata.OrderByDescending(d => d.ProductionId)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();
                ViewBag.Productions = totaldata;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                return PartialView("partial/ProductionGrid");
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }

        [HttpGet]
        public ActionResult ProductionDetails()
        {
            
            string ProductionCode = GenerateBatchCode(); /// Generate the batch code
            ViewBag.ProductionCode = ProductionCode;
            return View();
        }

        public JsonResult GetProductionCode()
        {
            string productionCode = GenerateBatchCode(); // Generate the batch code
            return Json(new { success = true, productionCode = productionCode }, JsonRequestBehavior.AllowGet);
        }

        #region Curd Operation
        [HttpPost]
        public ActionResult SaveProduction(ProductionModel model)
        {
            try
            {
                Production production = new Production();
                string status = "";
                
                if (model.ProductionId == 0)
                {
                    ProductionManager productionManager = new ProductionManager();    
                    
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

                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public string GenerateBatchCode()
        {
            Production production = new Production();
            ProductionManager productionManager = new ProductionManager();

            // Get current year and month
            string yearMonth = DateTime.Now.ToString("yyMM");

            // Retrieve the last generated batch code from the database or any storage
            string lastBatchCode = productionManager.GetLatestBatchNumberFromDatabase();

            // If there's no last batch code, start from 0001
            int nextSequentialNumber = 1;
            if (!string.IsNullOrEmpty(lastBatchCode))
            {
                // Extract the sequential number part and increment it
                int lastIndex = lastBatchCode.LastIndexOf('/');
                string sequentialPart = lastBatchCode.Substring(lastIndex + 1);
                int lastSequentialNumber = int.Parse(sequentialPart);
                nextSequentialNumber = lastSequentialNumber + 1;
            }
            // Format the next sequential number
            string formattedSequentialNumber = nextSequentialNumber.ToString("0000");

            // Combine yearMonth and formattedSequentialNumber to create the batch code
            string ProductionCode = $"{yearMonth}/{formattedSequentialNumber}";

            // Return the generated batch code
            return ProductionCode;
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


            // Fetch the temp_production data for the given productid
            Temp_productionManager temp_ProductionManager = new Temp_productionManager();
            List<temp_production> temp_Productions = new List<temp_production>();
            temp_Productions = temp_ProductionManager.GetbomproductionMaterial(productid);

            SalesorderManager salesorderManager = new SalesorderManager();
            salesorder salesorder = new salesorder();
            salesorder = salesorderManager.Getproductqty(productid);

            ProductManager productManager = new ProductManager();
            product product = new product();
            product = productManager.GetId(productid);

            Temp_productionManager temp_preproductionManager = new Temp_productionManager();
            preproduction preproduction = new preproduction();
            preproduction = temp_preproductionManager.Getproductionqty(productid);


            int BomData = 0; // Default value in case temp_Productions is null
            decimal? Consume = 0;

            if (temp_Productions != null)
            {
                BomData = temp_Productions[0].Bomid;
                Consume= temp_Productions[0].Consume;
            }

            decimal? minstock = 0;
            decimal? maxstock = 0;
            decimal? productionperday = 0;// Retrieve the productionperday value from your data source

            if (product != null)
            {
                minstock = product.MinStock;
                maxstock = product.MaxStock;
                productionperday= product.ProductionPerDay;
            }
            //ViewBag.ProductionPerDay = productionperday;

            decimal? quantity = 0;

            if(preproduction != null)
            {
                quantity = preproduction.Qty;
            }

            // Fetch BOM material based on Bomid
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            Bom bom = billOfMaterialManager.GetbomId(BomData);

            BillOfMaterialManager billOfMaterialManagers = new BillOfMaterialManager();
            List<BOMMaterial> bOMMaterials = billOfMaterialManagers.Getbom(bom.BomId);

            // Fetch material names based on material ids from bOMMaterials
            MaterialNameManager MaterialNameManager = new MaterialNameManager();

            List<string> materialNames = new List<string>();
            List<decimal> consumes = new List<decimal>();
            foreach (var tempProductionItem in temp_Productions)
            {
                // Call the GetMaterialName method to retrieve the tbl_materialnamemaster object
                tbl_materialnamemaster materialInfo = MaterialNameManager.GetMaterialNameMaterial(tempProductionItem.MaterialId);

                // Extract the material name from the retrieved object
                string materialName = null;
                decimal? consume = tempProductionItem.Consume;
                if (materialInfo != null)
                {
                    materialName = materialInfo.MaterialDescription;
                }

                if (!string.IsNullOrEmpty(materialName))
                {
                    materialNames.Add(materialName);
                    consumes.Add((decimal)consume);
                }
            }

                       
            // materialNames now contains the list of material names corresponding to the material IDs in bOMMaterials


            return Json(new { bomdata = materialNames,Consumeqty= consumes, Minstock= minstock,Maxstock=maxstock,RequiredQty= quantity, ProductionperDay = productionperday }, JsonRequestBehavior.AllowGet);

        }

    
    }
}
