using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models;
using MMS.Web.Models.ColorMasterModel;
using MMS.Web.Models.JobworkModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using System.Configuration;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class ProductionJobwork_Controller : Controller
    {
        //
        // GET: /ProductionJobwork_/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductionJobworkMaster()
        {
            MMS.Web.Models.JobworkModel.ProductionJobworkModel Pm = new ProductionJobworkModel();
            return View("~/Views/Jobwork/Job_Master/ProductionJobWork/Production_JobMaster.cshtml", Pm);
        }
        ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();
        ProductionJobwork_Code_Master ProductionJobwork_Code_Master = new ProductionJobwork_Code_Master();
        ProductionJobworkMaster ProductionMaster = new ProductionJobworkMaster();
        ProductionJobworkMasterManager ProductionJobworkMasterManager = new ProductionJobworkMasterManager();

        public ActionResult ProductionJobworkGrid()
        {
            List<MMS.Data.StoredProcedureModel.JobProductionGrid> JobProductionGrid = new List<MMS.Data.StoredProcedureModel.JobProductionGrid>();
            ProductionJobworkModel.ProductionJobwork_Code_list = ProductionJobworkMasterManager.Get_production_code_Tbl();

            return PartialView("~/Views/Jobwork/Job_Master/ProductionJobWork/Partial/Production_JobGrid.cshtml", ProductionJobworkModel);
        }
        [HttpPost]
        public ActionResult EditProductionJobworkList(int ProductionJobwork_code_id)
        {
            //Get_ProductionGrid
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
      

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.GetJob_production_code_id();

            ProductionMaster = ProductionJobworkMasterManager_.GetproductionMaster_id(ProductionJobwork_code_id);

            //  jobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_Code_id);
            if (ProductionMaster != null  && ProductionMaster.Production_Order_id !=0)
            {
                ProductionJobworkModel.Production_Order_id = ProductionMaster.Production_Order_id;
                ProductionJobworkModel.date = ProductionMaster.date;
                ProductionJobworkModel.Leather_Pairs = ProductionMaster.Leather_Pairs;
                ProductionJobworkModel.component_Pairs = ProductionMaster.component_Pairs;
                ProductionJobworkModel.Upper_Fullshoes = ProductionMaster.Upper_Fullshoes;
                ProductionJobworkModel.Jw_Name = ProductionMaster.Jw_Name;
                ProductionJobworkModel.Process_Name = ProductionMaster.Process_Name;
                ProductionJobworkModel.Stage_From = ProductionMaster.Stage_From;
                ProductionJobworkModel.Stage_To = ProductionMaster.Stage_To;
                ProductionJobworkModel.Lot_no = ProductionMaster.Lot_no;
                ProductionJobworkModel.Io_based = ProductionMaster.Io_based;
                ProductionJobworkModel.Buyer = ProductionMaster.Buyer;
                ProductionJobworkModel.Season = ProductionMaster.Season;
                ProductionJobworkModel.Style = ProductionMaster.Style;
                ProductionJobworkModel.JW_BOM_linked = ProductionMaster.JW_BOM_linked;
                ProductionJobworkModel.Material_Name = ProductionMaster.Material_Name;
                ProductionJobworkModel.Size_range = ProductionMaster.Size_range;
                ProductionJobworkModel.Delivery_date = ProductionMaster.Delivery_date;
                ProductionJobworkModel.Totalpair = ProductionMaster.Totalpair;
                ProductionJobworkModel.Qty = ProductionMaster.Qty;
                ProductionJobworkModel.Qty_Uom = ProductionMaster.Qty_Uom;
                ProductionJobworkModel.Rate = ProductionMaster.Rate;
                ProductionJobworkModel.Value = ProductionMaster.Value;
                ProductionJobworkModel.GST = ProductionMaster.GST;
                ProductionJobworkModel.GST_AMT = ProductionMaster.GST_AMT;
                ProductionJobworkModel.Total_Cost = ProductionMaster.Total_Cost;

                ProductionJobworkModel.Fg_ComponentId = ProductionMaster.Fg_ComponentId;
                ProductionJobworkModel.Fg_Material_Name = ProductionMaster.Fg_Material_Name;
                //Size
            }

            else
            {
                ID++;
                string year = DateTime.Now.Year.ToString();
           //     Jw_ApprovedPriceModel.code = new JobSheet_pairCodeModel() { jobsheet_pair_Code = "JBO" + ID };
                ProductionJobworkModel.code=new ProductionJobwork_Code_Master() { ProductionJobwork_Code = "PROD" + '/' + ID + '/' + year };
                //  JobSheet_pairModel JobSheet_pairModel = new JobSheet_pairModel();
                //  JobSheet_pairModel.code.jobsheet_pair_Code = "JBO" + ID;
            }
            return PartialView("~/Views/Jobwork/Job_Master/ProductionJobWork/Partial/Production_JobEdit.cshtml", ProductionJobworkModel);
        }
        [HttpPost]
        public ActionResult EditProductionJobworkList_Code(int ProductionJobwork_code_id)
        {
            string Type = "";
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();
            List<MMS.Data.StoredProcedureModel.JobProductionGrid> JobProductionGrid = new List<MMS.Data.StoredProcedureModel.JobProductionGrid>();
            ProductionJobworkModel.JobProductionGrid_list =   ProductionJobworkMasterManager_.Get_ProductionGrid().Where(x=>x.Prod_code_id== ProductionJobwork_code_id).ToList();
            if (ProductionJobworkModel.JobProductionGrid_list != null)
            {
                ProductionJobworkModel.code = new ProductionJobwork_Code_Master() { ProductionJobwork_Code = ProductionJobworkModel.JobProductionGrid_list[0].ProductionJobwork_Code ,
                Production_Type= ProductionJobworkModel.JobProductionGrid_list[0].Production_Type
                };
                if (ProductionJobworkModel.JobProductionGrid_list[0].Production_Type== true)
                {
                    Type = "1";
                }
                else {
                    Type = "0";
                }
                //Size
            }
            //  return PartialView("~/Views/Jobwork/Job_Master/ProductionJobWork/Partial/Production_JobEdit.cshtml", ProductionJobworkModel);
            var Prod_code_id = ProductionJobworkMasterManager_.Get_production_code_Tbl().Where(x=> x.ProductionJobwork_code_id == ProductionJobworkModel.JobProductionGrid_list[0].Prod_code_id).FirstOrDefault();

            var partialView = RenderRazorViewToString("~/Views/Jobwork/Job_Master/ProductionJobWork/Partial/Production_JobEdit.cshtml", ProductionJobworkModel);
            return Json(new { PartialView = partialView, items = Prod_code_id, Type= Type }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditProductionJobworkList_Code_ID(int Production_Order_id)
        {
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();

           var Model  = ProductionJobworkMasterManager_.GetproductionMaster_id(Production_Order_id);
            if (Model != null)
            {
                //Size
                ProductionJobworkModel.Production_Order_id = Model.Production_Order_id;
                ProductionJobworkModel.date = Model.date;
                ProductionJobworkModel.Leather_Pairs = Model.Leather_Pairs;
                ProductionJobworkModel.component_Pairs = Model.component_Pairs;
                ProductionJobworkModel.Upper_Fullshoes = Model.Upper_Fullshoes;
                ProductionJobworkModel.Jw_Name = Model.Jw_Name;
                ProductionJobworkModel.Process_Name = Model.Process_Name;
                ProductionJobworkModel.Stage_From = Model.Stage_From;
                ProductionJobworkModel.Stage_To = Model.Stage_To;
                ProductionJobworkModel.Lot_no = Model.Lot_no;
                ProductionJobworkModel.Io_based = Model.Io_based;
                ProductionJobworkModel.Buyer = Model.Buyer;
                ProductionJobworkModel.Season = Model.Season;
                ProductionJobworkModel.Style = Model.Style;
                ProductionJobworkModel.JW_BOM_linked = Model.JW_BOM_linked;
                ProductionJobworkModel.Material_Name = Model.Material_Name;
                ProductionJobworkModel.Size_range = Model.Size_range;
                ProductionJobworkModel.Delivery_date = Model.Delivery_date;
                ProductionJobworkModel.Totalpair = Model.Totalpair;
                ProductionJobworkModel.Qty = Model.Qty;
                ProductionJobworkModel.Qty_Uom = Model.Qty_Uom;
                ProductionJobworkModel.Rate = Model.Rate;
                ProductionJobworkModel.Value = Model.Value;
                ProductionJobworkModel.GST = Model.GST;
                ProductionJobworkModel.GST_AMT = Model.GST_AMT;
                ProductionJobworkModel.Total_Cost = Model.Total_Cost;

                ProductionJobworkModel.Fg_ComponentId = Model.Fg_ComponentId;
                ProductionJobworkModel.Fg_Material_Name = Model.Fg_Material_Name;
            }
           string[] Io_based= ProductionJobworkModel.Io_based.Split(',');
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
            var list = items.Where(x => Io_based.Contains(x.Text)).ToList();
            string io_val = "";
            foreach (var item in list)
            {
                io_val += item.Value + ',' ;
            }
            return Json(new { ProductionJobworkModel=ProductionJobworkModel, list=list , io_val = io_val.Substring(0, io_val.Length - 1) }, JsonRequestBehavior.AllowGet);
        }
        #region Curd Operation
        [HttpPost]
        public ActionResult saveProductionJobworkModel(ProductionJobworkModel ProductionJobworkModel)
        {
            try
            {
               
                ProductionJobwork_Code_Master ProductionJobwork_Code_Master = new ProductionJobwork_Code_Master();
                string SizeQty = ProductionJobworkModel.Qty_.Remove(ProductionJobworkModel.Qty_.Length-1);
                string size = ProductionJobworkModel.size.Remove(ProductionJobworkModel.size.Length - 1);
                string[] qty = SizeQty.Split(';');
                string[] Size_array = size.Split(',');

                ProductionMaster.Production_Order_id = ProductionJobworkModel.Production_Order_id;

                ProductionJobwork_Code_Master.Upper_Fullshoes = ProductionJobworkModel.Upper_Fullshoes;
                ProductionJobwork_Code_Master.Leather_Pairs = ProductionJobworkModel.Leather_Pairs;
                ProductionJobwork_Code_Master.component_Pairs = ProductionJobworkModel.component_Pairs;
                ProductionJobwork_Code_Master.ProductionJobwork_Code = ProductionJobworkModel.Prod_code_id_code;

                ProductionJobwork_Code_Master.Production_Type = ProductionJobworkModel.Production_Type;
                ProductionMaster.Prod_code_id = ProductionJobworkMasterManager.Post_Code(ProductionJobwork_Code_Master);

                //     JobSheet_PairMaster.jobsheet_pair_Code_id = JobSheet_PairManager.Post_Code(JobSheet_pairCodeMaster);
                ProductionMaster.date = ProductionJobworkModel.date;
                ProductionMaster.Leather_Pairs = ProductionJobworkModel.Leather_Pairs;
                ProductionMaster.component_Pairs = ProductionJobworkModel.component_Pairs;
                ProductionMaster.Upper_Fullshoes = ProductionJobworkModel.Upper_Fullshoes;
                ProductionMaster.Jw_Name = ProductionJobworkModel.Jw_Name;
                ProductionMaster.Process_Name = ProductionJobworkModel.Process_Name;
                ProductionMaster.Stage_From = ProductionJobworkModel.Stage_From;
                ProductionMaster.Stage_To = ProductionJobworkModel.Stage_To;
                ProductionMaster.Lot_no = ProductionJobworkModel.Lot_no;
                ProductionMaster.Io_based = ProductionJobworkModel.Io_based;
                ProductionMaster.Buyer = ProductionJobworkModel.Buyer;
                ProductionMaster.Season = ProductionJobworkModel.Season;
                ProductionMaster.Style = ProductionJobworkModel.Style;
                ProductionMaster.JW_BOM_linked = ProductionJobworkModel.JW_BOM_linked;
                ProductionMaster.Material_Name = ProductionJobworkModel.Material_Name;
                ProductionMaster.Size_range = ProductionJobworkModel.Size_range;
                ProductionMaster.Delivery_date = ProductionJobworkModel.Delivery_date;
                ProductionMaster.Totalpair = ProductionJobworkModel.Totalpair;
                ProductionMaster.Qty = ProductionJobworkModel.Qty;
                ProductionMaster.Qty_Uom = ProductionJobworkModel.Qty_Uom;
                ProductionMaster.Rate = ProductionJobworkModel.Rate;
                ProductionMaster.Value = ProductionJobworkModel.Value;
                ProductionMaster.GST = ProductionJobworkModel.GST;
                ProductionMaster.GST_AMT = ProductionJobworkModel.GST_AMT;
                ProductionMaster.Total_Cost = ProductionJobworkModel.Total_Cost;
                ProductionMaster.Leather_Pairs = ProductionJobworkModel.Leather_Pairs;
                ProductionMaster.component_Pairs = ProductionJobworkModel.component_Pairs;
                ProductionMaster.ComponentId = ProductionJobworkModel.ComponentId;

                ProductionMaster.Fg_ComponentId = ProductionJobworkModel.Fg_ComponentId;
                ProductionMaster.Fg_Material_Name = ProductionJobworkModel.Fg_Material_Name;
               int id= ProductionJobworkMasterManager.Post(ProductionMaster);
                int count = 0;
                foreach (var itemss in qty)
                {
                    ProductionJobSizerangeMaster ProductionJobSizerangeMaster = new ProductionJobSizerangeMaster();
                    ProductionJobSizerangeMaster.Sizerange = Convert.ToDecimal( Size_array[count]);
                    ProductionJobSizerangeMaster.Qty = Convert.ToDecimal(itemss);
                    ProductionJobSizerangeMaster.Job_Production_pair_id = id;
                    ProductionJobworkMasterManager.Post_Code_size(ProductionJobSizerangeMaster);
                    count++;
                }

                return Json(ProductionMaster, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(ProductionMaster, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult UpdateProductionJobworkModel(ProductionJobworkModel ProductionJobworkModel)
        {

            //ProductionMaster = ProductionJobworkMasterManager.GetproductionMaster_id(ProductionJobworkModel.Production_Order_id);
            if (ProductionJobworkModel != null)
            {
                ProductionMaster.Production_Order_id = ProductionJobworkModel.Production_Order_id;
               // ProductionMaster.Prod_code_id = ProductionJobworkModel.Prod_code_id;
                ProductionMaster.date = ProductionJobworkModel.date;
                ProductionMaster.Leather_Pairs = ProductionJobworkModel.Leather_Pairs;
                ProductionMaster.component_Pairs = ProductionJobworkModel.component_Pairs;
                ProductionMaster.Upper_Fullshoes = ProductionJobworkModel.Upper_Fullshoes;
                ProductionMaster.Jw_Name = ProductionJobworkModel.Jw_Name;
                ProductionMaster.Process_Name = ProductionJobworkModel.Process_Name;
                ProductionMaster.Stage_From = ProductionJobworkModel.Stage_From;
                ProductionMaster.Stage_To = ProductionJobworkModel.Stage_To;
                ProductionMaster.Lot_no = ProductionJobworkModel.Lot_no;
                ProductionMaster.Io_based = ProductionJobworkModel.Io_based;
                ProductionMaster.Buyer = ProductionJobworkModel.Buyer;
                ProductionMaster.Season = ProductionJobworkModel.Season;
                ProductionMaster.Style = ProductionJobworkModel.Style;
                ProductionMaster.JW_BOM_linked = ProductionJobworkModel.JW_BOM_linked;
                ProductionMaster.Material_Name = ProductionJobworkModel.Material_Name;
                ProductionMaster.Size_range = ProductionJobworkModel.Size_range;
                ProductionMaster.Delivery_date = ProductionJobworkModel.Delivery_date;
                ProductionMaster.Totalpair = ProductionJobworkModel.Totalpair;
                ProductionMaster.Qty = ProductionJobworkModel.Qty;
                ProductionMaster.Qty_Uom = ProductionJobworkModel.Qty_Uom;
                ProductionMaster.Rate = ProductionJobworkModel.Rate;
                ProductionMaster.Value = ProductionJobworkModel.Value;
                ProductionMaster.GST = ProductionJobworkModel.GST;
                ProductionMaster.GST_AMT = ProductionJobworkModel.GST_AMT;
                ProductionMaster.Total_Cost = ProductionJobworkModel.Total_Cost;

                ProductionMaster.Fg_ComponentId = ProductionJobworkModel.Fg_ComponentId;
                ProductionMaster.Fg_Material_Name = ProductionJobworkModel.Fg_Material_Name;
                ProductionJobworkMasterManager.Put(ProductionMaster);
            }
            else
            {
                return Json(ProductionMaster, JsonRequestBehavior.AllowGet);
            }


            return Json(ProductionMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete_ProductionJobworkModel(int Production_Order_id)
        {
            string status = "";

            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();

            List<ProductionJobworkMaster> ProductionJobworkMaster = new List<ProductionJobworkMaster>();

            ProductionJobworkMaster = ProductionJobworkMasterManager_.Get().Where(x=>x.Prod_code_id== Production_Order_id).ToList();
            ProductionJobworkMasterManager.Delete_ProductionName(Production_Order_id);
            foreach (var item in ProductionJobworkMaster)
            {
                if (ProductionJobworkMaster != null)
                {
                    status = "Success";
                    ProductionJobworkMasterManager.Delete(item.Production_Order_id);
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_IoListBystyle(string Style)
        {
            string Message = "";
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<Bom> billOfMaterial = new List<Bom>();
            List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
            List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();


            List<System.Web.Mvc.SelectListItem> items = buyerOrderEntryManager.GetBuyer_Production_style().Where(X => X.OurStyle == Style).Select(   item => new System.Web.Mvc.SelectListItem()
                                                 {
                Text = item.BuyerOrderSlNo,
                Value = Convert.ToString(item.OurStyle)
            }).ToList();
            var Ordername = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            return Json(new { item = items }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Getbom_id(string LinkBomNo,string BuyerPo)
        {
            string Message = "";
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
           List<Bom> billOfMaterial = new List<Bom>();
            List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
            List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();

            string OrderEntryId = "";
            string[] LinkBom_No = LinkBomNo.Split(',');
            string[] BuyerPo_ = BuyerPo.Split(',');
            //   billOfMaterial = billOfMaterialManager.Get().Where(X => X.LinkBomNo == LinkBomNo && X.IsDeleted== false).FirstOrDefault();

            billOfMaterial = billOfMaterialManager.Get_Arraylist(LinkBom_No);
               var BomNo = billOfMaterial.FirstOrDefault().BomNo;
            string Style = BomNo.Substring(0, 4);
          //  var BomNo_ = billOfMaterial.Select();
            foreach (var items in billOfMaterial)
            {
                if (Style != items.BomNo.Substring(0, 4))
                {
                    Message = "Style cant be diffrent";
                }
            }
            foreach (var items in BuyerPo_)
            {
                var Id = buyerOrderEntryManager.Get().Where(X => X.BuyerOrderSlNo == items && X.IsInternal == true && X.IsInternal == true ).FirstOrDefault();
                OrderEntryId += Id.OrderEntryId + ",";
              }
            // var result = (BomNo_.Count == BomNo_.Distinct().Count());
            //  var i    = BomNo_.Contains(false);
            listOfSizeRange = buyerOrderEntryManager.GetAllOrderSize_OrderEntryId(OrderEntryId );
            decimal? Totalqty = listOfSizeRange.Sum(x => x.Qty);
            return Json(new { Style = Style , BillOfMaterial= billOfMaterial, Message= Message, listOfSizeRange= listOfSizeRange, Totalqty= Totalqty },JsonRequestBehavior.AllowGet);
        }
        // GEtBom_Material
        public ActionResult GEtBom_Material(string JW_BOM_linked)
        {
            //GetBomDetails_Arraylist
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<Bom> billOfMaterial = new List<Bom>();
            List<BOMMaterial> bomGrid = new List<BOMMaterial>();
            MaterialManager MaterialManager = new MaterialManager();
            MaterialNameManager MaterialNameManager = new MaterialNameManager();

            int[] BomGridId = JW_BOM_linked.Split(',').Select(int.Parse).ToArray();

            //   billOfMaterial = billOfMaterialManager.Get().Where(X => X.LinkBomNo == LinkBomNo && X.IsDeleted== false).FirstOrDefault();

            bomGrid = billOfMaterialManager.GetBomDetails_Arraylist(BomGridId);
            var items = (from G in bomGrid
                        join X in MaterialManager.Get()
                        on G.MaterialName equals X.MaterialMasterId
                        join y in MaterialNameManager.Get()
                        on X.MaterialName equals y.MaterialMasterID
                        select new System.Web.Mvc.SelectListItem() { Value = G.MaterialName+"_"+G.BOMMaterialID , Text=y.MaterialDescription }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return Json(new { BomGrid = bomGrid,Material_Desctription= items }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Material_NameDetail(string Material_Name)
        {
            //GetBomDetails_Arraylist
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<Bom> billOfMaterial = new List<Bom>();
            Bom bomGrid = new Bom();
            MaterialManager MaterialManager = new MaterialManager();
            MaterialNameManager MaterialNameManager = new MaterialNameManager();
            List<BOMMaterial> bOMMateriallist = new List<BOMMaterial>();
            MRPRequirement mrpRequirement = new MRPRequirement();
            BuyerOrderEntryManager BuyerOrderEntryManager = new BuyerOrderEntryManager();

            int[] Material = Material_Name.Split('_').Select(int.Parse).ToArray();
            int Material_id = Material[0];
            int BOMMaterialID = Material[1];
            //   billOfMaterial = billOfMaterialManager.Get().Where(X => X.LinkBomNo == LinkBomNo && X.IsDeleted== false).FirstOrDefault();
            bOMMateriallist = billOfMaterialManager.getBomMaterialID_List(BOMMaterialID);
            bomGrid = billOfMaterialManager.GetbomId(bOMMateriallist[0].BOMID);
            var Io = BuyerOrderEntryManager.Get().Where(x => x.OurStyle == bomGrid.LinkBomNo && x.IsDeleted == false && x.IsInternal==true).FirstOrDefault(); 
            mrpRequirement = IOSelectList(Io.BuyerOrderSlNo, "", bOMMateriallist);
            
            return Json(new { mrpRequirement= mrpRequirement }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Material_NameDetail_CuttingSlip(int LotNo, int BuyerName, int BuyerSeason, int MaterialGroupMasterId, int Material_Name,string Io)
        {
            decimal? RequiredQty=Convert.ToDecimal( 0.0);
            //GetBomDetails_Arraylist
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
           var cuttingSplit = ProductionJobworkMasterManager_.Get_CutingSlip_List_Grid(LotNo, BuyerName, BuyerSeason, 2, MaterialGroupMasterId,0);
          string[] Iovalue=  Io.Split(',');
            foreach (var items in Iovalue)
            {
                Decimal? qty = cuttingSplit.Where(x => x.BuyerPoNo == items.ToString() && x.MaterialMasterId == Material_Name).Sum(x => x.TotalNorms);
                Decimal? SizeQty = cuttingSplit.Where(x => x.SizeBuyerPoNumber == items).Sum(x => x.SizeQty);
                 RequiredQty = RequiredQty+(qty * SizeQty);
            }
            return Json(new { CuttingSplit = cuttingSplit, RequiredQty= RequiredQty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_IoDetailBased(int Production_Order_id)
        {
            string Message = "";
            ProductionJobworkMaster materaildesc = new ProductionJobworkMaster();
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();
            MMS.Web.Models.JobworkModel.JobLeather_leatherModel JobLeather_leatherModel = new JobLeather_leatherModel();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            int count = 0;

            var Balance_stock = Convert.ToDecimal(0);

            var Pro_Model =   ProductionJobworkMasterManager_.GetproductionMaster_id(Production_Order_id);
            var size = ProductionJobworkMasterManager_.Get_Size(Production_Order_id);
            var size_ = ProductionJobworkMasterManager_.Get_Size_IEnumerable(Production_Order_id);
            materaildesc = ProductionJobworkMasterManager_.GetproductionMaster_id(Convert.ToInt32(Production_Order_id));
            if (materaildesc != null)
            {
                Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x => x.Jobsheet_pair_Code_id == Production_Order_id.ToString() && x.IssueType == "17").Sum(x => x.CurrentIssue));

                count = issueSlipManager.Get().Where(x => x.Jobsheet_pair_Code_id == Production_Order_id.ToString()).Count();

                var ProductionJobworkMaster_ = ProductionJobworkMasterManager_.GetproductionMaster_id(Production_Order_id);

                JobLeather_leatherModel.JobApproved_list = (from y in Job_ApprovedPriceManager.Get()
                                                            where y.JW_Name == ProductionJobworkMaster_.Jw_Name && y.Process_Name == ProductionJobworkMaster_.Process_Name && y.Rate_For_JW == ProductionJobworkMaster_.Rate && y.Stage_From == ProductionJobworkMaster_.Stage_From && y.Stage_To == ProductionJobworkMaster_.Stage_To
                                                            select new JobApproved_list { Date = y.Date, Job_ExcessPercentage = y.Job_ExcessPercentage }).OrderByDescending(y => y.Date).FirstOrDefault();
            }
                return Json(new { item = Pro_Model, size= size, Excess_pecent = JobLeather_leatherModel.JobApproved_list.Job_ExcessPercentage, Balance_stock= Balance_stock

                }, JsonRequestBehavior.AllowGet);
        }
        #region MRP calculation 
        public MRPRequirement IOSelectList(string SelectText, string SimpleMRPID, List<BOMMaterial> bOMMateriallist_)
        {

            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry order = new OrderEntry();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            Bom billOfMaterial = new Bom();
            List<bomgriddetail> listMaterial = new List<bomgriddetail>();
            var SizeQuantityRate_ = SelectText.Split(',');
            List<BOMMaterial> bomMaterialList = new List<BOMMaterial>();
            BOMMaterial bomMaterial = new BOMMaterial();
            string OrderMessage = "";
            string BOMMessage = "";
            string SaveBOMMessage = "";
            List<SelectListItem> listBoxItem = new List<SelectListItem>();
        //    SimpleMRPModel model = new SimpleMRPModel();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            List<OrderEntry> listOfOrders_ = new List<OrderEntry>();
            List<Bom> listBillOfMaterial = new List<Bom>();
            List<MaterialMaster> listOfMaterial = new List<MaterialMaster>();
            MaterialManager materialManager = new MaterialManager();
            List<materialgroupmaster> listOfMaterialGroupMaster = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();
            string orderExited = "";
            string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
            List<MRPRequirement> listOfMRPRequirement = new List<MRPRequirement>();
            MRPRequirement mrpRequirement = new MRPRequirement();
            try
            {
                listOfOrders_ = buyerOrderEntryManager.GetInternalIO();
                listBillOfMaterial = billOfMaterialManager.Get();
                listOfMaterial = materialManager.MaterialList();
                listOfMaterialGroupMaster = materialGroupManager.Get();
                listOfSizeRange = buyerOrderEntryManager.GetAllOrderSize();
                foreach (var item in SizeQuantityRate_)
                {
                    MRPSelectedValues selectedValues = new MRPSelectedValues();
                    BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
                    order = listOfOrders_.Where(x => x.BuyerOrderSlNo == item).FirstOrDefault();
                    if (order == null)
                    {
                        OrderMessage = "Please make a entry on internal order for this:" + item;
                        //return Json(new { InternalStatus = OrderMessage, status = SaveBOMMessage, orderStatus = orderExited, BOMStatus = BOMMessage }, JsonRequestBehavior.AllowGet);
                        return mrpRequirement;
                    }
                    if (order != null && order.OrderEntryId != 0)
                    {
                        billOfMaterial = listBillOfMaterial.Where(x => x.LinkBomNo == order.OurStyle).FirstOrDefault();

                        if (billOfMaterial == null)
                        {
                            Product_BuyerStyleManager productBuyerStyleManager = new Product_BuyerStyleManager();
                            Product_BuyerStyleMaster productBuyerStyle = new Product_BuyerStyleMaster();
                            productBuyerStyle = productBuyerStyleManager.GetProductOrBuyerStyleId(Convert.ToInt32(order.OurStyle));
                            BOMMessage = "Please make a entry BOM for this Style:" + productBuyerStyle.OurStyle;
                            //return Json(new { InternalStatus = OrderMessage, status = SaveBOMMessage, orderStatus = orderExited, BOMStatus = BOMMessage }, JsonRequestBehavior.AllowGet);
                            return mrpRequirement;
                        }
                    }
                    MRPSelectedValues mrpselectedValues = new MRPSelectedValues();
                    mrpselectedValues = simpleMRPManager.checkOrderNumber(item);
                    if (mrpselectedValues == null)
                    {
                       // orderExited += item + " ";
                    }
                    else
                    {
                        if (billOfMaterial != null)
                        {
                            bomMaterialList = billOfMaterialManager.GetBomMaterialBOMID(billOfMaterial.BomId);
                        }
                        selectedValues.IONumberID = item;
                        selectedValues.SimpleMRPID = 1;
                        SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                        if (order != null && billOfMaterial != null)
                        {
                          //  simpleMRPManager.SimpleMRPSelectedPost(selectedValues);
                            foreach (var each in bOMMateriallist_)
                            {
                               
                                mrpRequirement.RequiredQty = 0;
                                decimal? Amount = 0;
                                decimal? qty = 0;
                                BOMMaterial bomMaterial_ = new BOMMaterial();
                                materialgroupmaster materialGroupMaster = new materialgroupmaster();
                                SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
                                MaterialMaster materialMaster = new MaterialMaster();
                                materialMaster = listOfMaterial.Where(x => x.MaterialMasterId == each.MaterialName).FirstOrDefault();
                                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                                materialGroupMaster = listOfMaterialGroupMaster.Where(x => x.MaterialGroupMasterId == each.MaterialGroupMasterId).FirstOrDefault();
                                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                                if (materialGroupMaster.IsSize == true)
                                {
                                    mrpRequirement = simpleMRPManager.IsGroupSize(mrpRequirement, each, materialMaster, listOfSizeRange, consString, order);
                                }
                                else
                                {
                                    string endurea = System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString();
                                    string host = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                                    string Hostname = "http://" + host;
                                    if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                                    {
                                        Amount = Convert.ToDecimal(each.BuyerNorms) * order.TotalAmount;
                                    }
                                    else
                                    {
                                        Amount = Convert.ToDecimal(each.TotalNorms) * order.TotalAmount;
                                    }
                                    qty = Amount;
                                    materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                    if (materialMaster.PurchasePacket != 0)
                                    {
                                        mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                    }
                                    else
                                    {
                                        mrpRequirement.RequiredQty = qty;
                                    }
                                    mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                                }
                                mrpRequirement.IsMRP = true;
                                mrpRequirement.ParentCommonBOMID = each.ParentCommonBOMID;
                                mrpRequirement.ParentBOMID = each.ParentBOMID;
                                mrpRequirement.BOMID = each.BOMID;
                                mrpRequirement.MaterialCategoryMasterId = each.MaterialCategoryMasterId;
                                mrpRequirement.MaterialGroupMasterId = each.MaterialGroupMasterId;
                                mrpRequirement.SubstanceMasterId = each.SubstanceMasterId;
                                mrpRequirement.MaterialName = each.MaterialName;
                                mrpRequirement.Conversion = each.Conversion;
                                mrpRequirement.ColorMasterId = each.ColorMasterId;
                                mrpRequirement.SizeScheduleMasterId = each.SizeScheduleMasterId;
                                mrpRequirement.SampleNorms = each.SampleNorms;
                                mrpRequirement.Wastage = each.Wastage;
                                mrpRequirement.SupplierMasterID = each.SupplierMasterID;
                                mrpRequirement.WastageQty = each.WastageQty;
                                mrpRequirement.TotalNorms = each.TotalNorms;
                                mrpRequirement.BuyerNorms = each.BuyerNorms;
                                mrpRequirement.Rate = Convert.ToDecimal(materialMaster.Price);
                                mrpRequirement.SizeRangeMasterID = each.SizeRangeMasterID;
                                mrpRequirement.NoofSets = each.NoofSets;
                                mrpRequirement.BuyerNorms = each.BuyerNorms;
                                mrpRequirement.WastageQtyUOM = each.WastageQtyUOM;
                                mrpRequirement.TotalNormsUOM = each.TotalNormsUOM;
                                mrpRequirement.SimpleMRPID = selectedValues.SimpleMRPID;
                                mrpRequirement.OrderNo = order.BuyerOrderSlNo;
                                //listOfMRPRequirement.Add(mrpRequirement);
                                //SaveBOMMessage = "Saved Successfully";
                            }
                        }
                    }

                }
                //simpleMRPManager.BulkInsertTOMRPRequirement(listOfMRPRequirement, consString);
                // return mrpRequirement;
                return mrpRequirement;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                // Logger.Log(ex.str.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mrpRequirement;
        }
        #endregion
        #region Convert to partial view
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion 
    }
}
