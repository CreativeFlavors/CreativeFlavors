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
        public ActionResult ProductionGrid(int page = 1, int pageSize = 5)
        {
            try
            {
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                //List<Production> productions = productionManager.GetProductions();
               
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
            //ProductionManager productionManager = new ProductionManager();
            //Production production = new Production();
            //ProductionModel productionModel = new ProductionModel();

            //int ID = MMS.Web.ExtensionMethod.HtmlHelper.GetJob_production_code_id1();

            //production = productionManager.Getproductionid(Productionid);

           
            //if (production != null && production.ProductionId != 0)
            //{
            //    productionModel.ProductionId = production.ProductionId;
            //    productionModel.ProductionDate = production.ProductionDate;
            //    productionModel.ProductionCode = production.ProductionCode;
            //    productionModel.ProductionQty = production.ProductionQty;
            //    productionModel.ProductionStatus = production.ProductionStatus;
            //    productionModel.ProductId = production.ProductId;
            //    productionModel.MinQty = production.MinQty;
            //    productionModel.MaxQty = production.MaxQty;
            //    productionModel.Temp_ProductionQty = production.Temp_ProductionQty;
            //    productionModel.StoreCode = production.StoreCode;
            //    productionModel.ProductionDueDate = production.ProductionDueDate;
            //    productionModel.ProductionFullfillDate = production.ProductionFullfillDate;
            //    productionModel.RefDocDate = production.RefDocDate;
            //    productionModel.RefDocNo = production.RefDocNo;
            //    productionModel.Status1Code = production.Status1Code;
            //    productionModel.Status1Date = production.Status1Date;
            //    productionModel.Status1By = production.Status1By;
            //    productionModel.Status2Code = production.Status2Code;
            //    productionModel.Status2Date = production.Status2Date;
            //    productionModel.Status2By = production.Status2By;
            //    productionModel.Status3Code = production.Status3Code;
            //    productionModel.Status3Date = production.Status3Date;
            //    productionModel.Status3By = production.Status3By;
            //    productionModel.Productions = production.Productions;
            //    productionModel.SubAssembly = production.SubAssembly;
            //    productionModel.Inprogress = production.Inprogress;
            //}
            //else
            //{
            //    ID++;
            //    string year = DateTime.Now.Year.ToString();
            //    productionModel.code = new ProductionJobwork_Code_Master() 
            //                           { ProductionJobwork_Code = "PROD" + '/' + ID + '/' + year }; 
            //}
            return View();
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
            temp_production temp_Productions = new temp_production();
            temp_Productions = temp_ProductionManager.GetbomproductionMaterial(productid);

            SalesorderManager salesorderManager = new SalesorderManager();
            salesorder salesorder = new salesorder();
            salesorder = salesorderManager.Getproductqty(productid);

            ProductManager productManager = new ProductManager();
            product product = new product();
            product = productManager.GetId(productid);


            int BomData = 0; // Default value in case temp_Productions is null
            decimal? Consume = 0;

            if (temp_Productions != null)
            {
                BomData = temp_Productions.Bomid;
                Consume= temp_Productions.Consume;
            }

            decimal? minstock = 0;
            decimal? maxstock = 0;

            if(product != null)
            {
                minstock = product.MinStock;
                maxstock = product.MaxStock;
            }

            // Fetch BOM material based on Bomid
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            Bom bom = billOfMaterialManager.GetbomId(BomData);

            BillOfMaterialManager billOfMaterialManagers = new BillOfMaterialManager();
            List<BOMMaterial> bOMMaterials = billOfMaterialManagers.Getbom(bom.BomId);

            // Fetch material names based on material ids from bOMMaterials
            MaterialNameManager MaterialNameManager = new MaterialNameManager();

            List<string> materialNames = new List<string>();
            List<decimal> quantities = new List<decimal>();
            foreach (var bomMaterialItem in bOMMaterials)
            {
                // Call the GetMaterialName method to retrieve the tbl_materialnamemaster object
                tbl_materialnamemaster materialInfo = MaterialNameManager.GetMaterialNameMaterial(bomMaterialItem.MaterialName);

                // Extract the material name from the retrieved object
                string materialName = null;
                decimal? quantity = 0;
                if (materialInfo != null)
                {
                    materialName = materialInfo.MaterialDescription;
                    quantity = salesorder.quantity;
                }

                if (!string.IsNullOrEmpty(materialName))
                {
                    materialNames.Add(materialName);
                    quantities.Add((decimal)quantity);
                }
            }

            // materialNames now contains the list of material names corresponding to the material IDs in bOMMaterials


            return Json(new { bomdata = materialNames, requiredqty = quantities,Minstock= minstock,Maxstock=maxstock }, JsonRequestBehavior.AllowGet);

        }

    }
}
