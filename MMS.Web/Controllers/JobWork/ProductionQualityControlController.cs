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
    public class ProductionQualityControlController : Controller
    {
        //
        // GET: /ProductionQualityControl/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Qc_Control()
        {
            MMS.Web.Models.JobworkModel.ProductionQcModel Pm = new ProductionQcModel();
            return View("~/Views/Jobwork/Job_Master/QualityControl/Job_QcControl.cshtml", Pm);
        }
        public ActionResult ProductionJobworkGrid()
        {
            ProductionQcManager ProductionQcManager = new ProductionQcManager();
            ProductionQcModel ProductionQcModel = new ProductionQcModel();
                ProductionQcModel.Production_QcCodeMaster = ProductionQcManager.Get_byCode();
            return PartialView("~/Views/Jobwork/Job_Master/QualityControl/Partial/QualityControlGrid.cshtml", ProductionQcModel);
        }
        [HttpPost]
        public ActionResult EditQcList(int ProductionQc_ID)
        {
            //Get_ProductionGrid
            ProductionQcManager ProductionQcManager = new ProductionQcManager();
            Production_QcCodeMaster Production_QcCodeMaster = new Production_QcCodeMaster();
            ProductionQcModel ProductionQcModel = new ProductionQcModel();
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.GetJob_production_Qc_code_id();

            Production_QcCodeMaster = ProductionQcManager.Get_byCode_Id(ProductionQc_ID);

            //  jobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_Code_id);
            if (Production_QcCodeMaster != null && Production_QcCodeMaster.ProductionQc_ID != 0)
            {
                
                //Size
            }

            else
            {
                ID++;
                string year = DateTime.Now.Year.ToString();
                ProductionQcModel.Production_Code_ = new Production_QcCodeMaster() { Production_Code = "PROQC" + '/' + ID + '/' + year };
                //  JobSheet_pairModel JobSheet_pairModel = new JobSheet_pairModel();
                //  JobSheet_pairModel.code.jobsheet_pair_Code = "JBO" + ID;
            }
            return PartialView("~/Views/Jobwork/Job_Master/QualityControl/Partial/QualityControlDetail.cshtml", ProductionQcModel);
        }
        #region Curd
        [HttpPost]
        public ActionResult saveQualityControl(ProductionQcModel ProductionQcModel)
        {
            try
            {
                
                ProductionQcManager ProductionQcManager = new ProductionQcManager();
                Production_QcMaster Production_QcMaster = new Production_QcMaster();
                Production_QcCodeMaster Production_QcCodeMaster = new Production_QcCodeMaster();
                InternalOrderForm BuyerEntry = new InternalOrderForm();
                BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();

                Production_QcCodeMaster.Production_Code = ProductionQcModel.Production_Code_new;

                Production_QcCodeMaster.Date = ProductionQcModel.Date;
                Production_QcCodeMaster.Buyer = ProductionQcModel.Buyer;
                Production_QcCodeMaster.Season = ProductionQcModel.Season;
                Production_QcCodeMaster.Stage = ProductionQcModel.Stage;

                Production_QcCodeMaster.Lot_No = ProductionQcModel.Lot_No;
                Production_QcCodeMaster.Io = ProductionQcModel.Io;

                Production_QcCodeMaster.Moved_lot = ProductionQcModel.Moved_lot;

                Production_QcCodeMaster.Moved_Io = ProductionQcModel.Moved_Io;
                Production_QcCodeMaster.Style = ProductionQcModel.Style;
                Production_QcCodeMaster.QC_Io = ProductionQcModel.QC_Io;

                Production_QcCodeMaster.Total_Pairs = ProductionQcModel.Total_Pairs;
                Production_QcCodeMaster.Size = 0;
                Production_QcCodeMaster.IO_Size = ProductionQcModel.IO_Size;
                Production_QcCodeMaster.Qty = 0;
                Production_QcCodeMaster.Side = "0";
                Production_QcCodeMaster.ProductionQc_ID = ProductionQcManager.Post_Code(Production_QcCodeMaster);
                for (int i = 0; i <= Convert.ToInt32(ProductionQcModel.Stage); i++)
                {
                    if (1 == i)
                    {

                        ProductionQcManager.Update_QcStage(1, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");

                    }
                    else if (2 == i)
                    {
                        ProductionQcManager.Update_QcStage(2, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");
                    }
                    else if (3 == i)
                    {
                        ProductionQcManager.Update_QcStage(3, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");
                    }
                    else if (4 == i)
                    {
                        ProductionQcManager.Update_QcStage(4, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");
                    }
                    else if (5 == i)
                    {
                        ProductionQcManager.Update_QcStage(5, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");
                    }
                    else if (6 == i)
                    {
                        ProductionQcManager.Update_QcStage(6, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "0");
                    }
                }

                //     JobSheet_PairMaster.jobsheet_pair_Code_id = JobSheet_PairManager.Post_Code(JobSheet_pairCodeMaster);
                //if (Production_QcMaster.Qty =="" && Production_QcMaster.To_stage =="")
                //{
                //    Production_QcMaster.ProductionQc_ID = Production_QcCodeMaster.ProductionQc_ID;
                //    Production_QcMaster.From_Stage = ProductionQcModel.From_Stage;
                //    Production_QcMaster.Size = ProductionQcModel.Size;
                //    Production_QcMaster.Qty = ProductionQcModel.Qty;
                //    Production_QcMaster.Side = ProductionQcModel.Side;
                //    Production_QcMaster.Reason = ProductionQcModel.Reason;
                //    Production_QcMaster.Type = ProductionQcModel.Type;
                //    Production_QcMaster.Component = ProductionQcModel.Component;
                //    Production_QcMaster.Style = ProductionQcModel.Style;
                //    Production_QcMaster.To_stage = ProductionQcModel.To_stage;

                //    ProductionQcManager.Post(Production_QcMaster);
                //    for (int i = 0; i < Convert.ToInt32(Production_QcMaster.To_stage); i++)
                //    {
                //        ProductionQcManager.Update_QcStage(ProductionQcModel.Stage, ProductionQcModel.Moved_lot, ProductionQcModel.Moved_Io, "1");
                //    }

                //}

                return Json(ProductionQcManager, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult saveQualityControl_Issue(ProductionQcModel ProductionQcModel)
        {
            try
            {

                ProductionQcManager ProductionQcManager = new ProductionQcManager();
                Production_QcMaster Production_QcMaster = new Production_QcMaster();
                Production_QcCodeMaster Production_QcCodeMaster = new Production_QcCodeMaster();
                InternalOrderForm BuyerEntry = new InternalOrderForm();
                BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
                int Qc_id =0 ;


                Production_QcCodeMaster.Production_Code = ProductionQcModel.Production_Code_new;
                Production_QcCodeMaster.Date = ProductionQcModel.Date;
                Production_QcCodeMaster.Buyer = ProductionQcModel.Buyer;
                Production_QcCodeMaster.Season = ProductionQcModel.Season;
                Production_QcCodeMaster.Stage = ProductionQcModel.Stage;

                Production_QcCodeMaster.Lot_No = ProductionQcModel.Lot_No;
                Production_QcCodeMaster.Io = ProductionQcModel.Io;

                Production_QcCodeMaster.Moved_lot = ProductionQcModel.Moved_lot;

                Production_QcCodeMaster.Moved_Io = ProductionQcModel.Moved_Io;
                Production_QcCodeMaster.Style = ProductionQcModel.Style;
                Production_QcCodeMaster.QC_Io = ProductionQcModel.QC_Io;

                Production_QcCodeMaster.Total_Pairs = ProductionQcModel.Total_Pairs;
                Production_QcCodeMaster.Size = 0;
                Production_QcCodeMaster.IO_Size = ProductionQcModel.IO_Size;
                Production_QcCodeMaster.Qty = 0;
                Production_QcCodeMaster.Side = "0";
                Production_QcCodeMaster.ProductionQc_ID = ProductionQcManager.Post_Code(Production_QcCodeMaster);
              

                //     JobSheet_PairMaster.jobsheet_pair_Code_id = JobSheet_PairManager.Post_Code(JobSheet_pairCodeMaster);
                if (ProductionQcModel.Qty != "" && ProductionQcModel.To_stage != "")
                {
                    Production_QcMaster.ProductionQc_ID = Production_QcCodeMaster.ProductionQc_ID;
                    Production_QcMaster.From_Stage = ProductionQcModel.Stage.ToString();
                    Production_QcMaster.Size = ProductionQcModel.Size;
                    Production_QcMaster.Qty = ProductionQcModel.Qty;
                    Production_QcMaster.Side = ProductionQcModel.Side;
                    Production_QcMaster.Reason = ProductionQcModel.Reason;
                    Production_QcMaster.Type = ProductionQcModel.Type;
                    Production_QcMaster.Component = ProductionQcModel.Component;
                    Production_QcMaster.Style = ProductionQcModel.Style;
                    Production_QcMaster.To_stage = ProductionQcModel.To_stage;
                    Production_QcMaster.QC_Io = ProductionQcModel.QC_Io;
                    Qc_id =ProductionQcManager.Post(Production_QcMaster);
                    for (int i =Convert.ToInt32(Production_QcMaster.To_stage); i <= Convert.ToInt32(Production_QcMaster.From_Stage); i++)
                    {
                        if (i != Convert.ToInt32(Production_QcMaster.To_stage))
                        {
                            ProductionQcManager.Update_QcStage(i, ProductionQcModel.Moved_lot, ProductionQcModel.QC_Io.ToString(), "1");
                        }
                    }

                }
                if (Qc_id != 0)
                {
                    string Size = ProductionQcModel.Qty_.Remove(ProductionQcModel.Qty_.Length - 1);
                    string SizeQty = ProductionQcModel.Qty_.Remove(ProductionQcModel.Qty_.Length - 1);
                    string[] Qc_size = ProductionQcModel.size_.Split(';');
                    string[] qty = ProductionQcModel.Qty_.Split(';');
                    int count = 0;
                    foreach (var itemss in Qc_size)
                    {
                        ProductionSizeQc_Issue ProductionSizeQc_Issue = new ProductionSizeQc_Issue();
                        ProductionSizeQc_Issue.Size = itemss;
                        ProductionSizeQc_Issue.Size_Bool = qty[count] == "true" ? true : false;
                        ProductionSizeQc_Issue.Qc_id = Qc_id;
                        ProductionQcManager.Post_Code_size(ProductionSizeQc_Issue);
                        count++;
                    }
                }
                return Json(ProductionQcManager, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult GEt_Stage_LotNo(int stage)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();

            if (stage == 1)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.Stage1 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }
           else if (stage == 2)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.Stage2 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }
            else if (stage == 3)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.Stage3 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }
            else if (stage == 4)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.IsInternal == true && x.Stage4 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }
            else if (stage == 5)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.Stage5 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }
            else if (stage == 6)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.Stage6 == false).GroupBy(x => x.LotNo).Select(g => g.First()).ToList();
            }



            items = BuyerOrderEntryList.OrderBy(x => x.BuyerName).Select(
                                        item => new System.Web.Mvc.SelectListItem()
                                        {
                                            Text = item.LotNo,
                                            Value = item.OrderEntryId.ToString()
                                        }
                                        ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList<SelectListItem>();
            items.Insert(0, new SelectListItem { Text = "Please Select", Value = string.Empty });
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GEt_Stage_IO(int stage,string Lotno)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();

            if (stage == 1)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo== Lotno && x.Stage1 == false ).ToList();
            }
            else if (stage == 2)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage2 == false).ToList();
            }
            else if (stage == 3)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage3 == false).ToList();
            }
            else if (stage == 4)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage4 == false).ToList();
            }
            else if (stage == 5)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage5 == false).ToList();
            }
            else if (stage == 6)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage5 == false).ToList();
            }


            items = BuyerOrderEntryList.Where(x => x.IsInternal == true).OrderBy(x => x.BuyerName).Select(
                                        item => new System.Web.Mvc.SelectListItem()
                                        {
                                            Text = item.BuyerOrderSlNo,
                                            Value = item.OrderEntryId.ToString()
                                        }
                                        ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList<SelectListItem>();
            items.Insert(0, new SelectListItem { Text = "Please Select", Value = string.Empty });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GEt_Stage_IO_Issue(int stage, string Lotno)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();

            if (stage == 1)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage1 == true).ToList();
            }
            else if (stage == 2)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage2 == true).ToList();
            }
            else if (stage == 3)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage3 == true).ToList();
            }
            else if (stage == 4)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage4 == true).ToList();
            }
            else if (stage == 5)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage5 == true).ToList();
            }
            else if (stage == 6)
            {
                BuyerOrderEntryList = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true && x.LotNo == Lotno && x.Stage6 == true).ToList();
            }



            items = BuyerOrderEntryList.Where(x => x.IsInternal == true).OrderBy(x => x.BuyerName).Select(
                                        item => new System.Web.Mvc.SelectListItem()
                                        {
                                            Text = item.BuyerOrderSlNo,
                                            Value = item.OrderEntryId.ToString()
                                        }
                                        ).ToList();
            items = items.GroupBy(x => x.Text).Select(x => x.First()).ToList<SelectListItem>();
            items.Insert(0, new SelectListItem { Text = "Please Select", Value = string.Empty });
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GEt_IO_Size(string OrderEntryId)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            List<InternalOrderForm> BuyerOrderEntryList = new List<InternalOrderForm>();
            List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();

            listOfSizeRange = buyerOrderEntryManager.GetAllOrderSize_OrderEntryId(OrderEntryId);
            decimal? Totalqty = listOfSizeRange.Sum(x => x.Qty);

            return Json(new { listOfSizeRange = listOfSizeRange, Totalqty = Totalqty }, JsonRequestBehavior.AllowGet);
        }
    }
}
