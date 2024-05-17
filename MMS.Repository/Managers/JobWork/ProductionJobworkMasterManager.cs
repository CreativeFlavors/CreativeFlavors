using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MMS.Repository.Managers.JobWork
{
    public class ProductionJobworkMasterManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private RepositoryJob<ProductionJobworkMaster> JobRepositoryProductionJobworkMaster;
        private RepositoryJob<ProductionJobwork_Code_Master> JobRepositoryProductionJobwork_Code_Master;
        private RepositoryJob<ProductionJobSizerangeMaster> JobProductionJobSizerang_Master;
        
        public ProductionJobworkMasterManager()
        {
            JobRepositoryProductionJobworkMaster = unitOfWork.Repository_<ProductionJobworkMaster>();
            JobRepositoryProductionJobwork_Code_Master = unitOfWork.Repository_<ProductionJobwork_Code_Master>();
            JobProductionJobSizerang_Master = unitOfWork.Repository_<ProductionJobSizerangeMaster>();
        }
        #region Add/Update/Delete Operation

        public int Post(ProductionJobworkMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsDeleted = false;

                //arg.UpdatedBy = username;
                JobRepositoryProductionJobworkMaster.Insert(arg);
                result = arg.Production_Order_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int Post_Code(ProductionJobwork_Code_Master arg)
        {
            int result = 0;
            try
            {
                ProductionJobwork_Code_Master model = JobRepositoryProductionJobwork_Code_Master.Table.Where(p => p.ProductionJobwork_Code == arg.ProductionJobwork_Code).FirstOrDefault();
                if (model != null)
                {
                    model.ProductionJobwork_Code = arg.ProductionJobwork_Code;
                    model.CreatedDate = arg.CreatedDate;

                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();

                    result = model.ProductionJobwork_code_id;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    JobRepositoryProductionJobwork_Code_Master.Insert(arg);
                    result = arg.ProductionJobwork_code_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }
        public int Post_Code_size(ProductionJobSizerangeMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                JobProductionJobSizerang_Master.Insert(arg);
                result = arg.Job_Production_pair_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }

        public int Put(ProductionJobworkMaster arg)
        {
            int result = 0;
            try
            {
                ProductionJobworkMaster model = JobRepositoryProductionJobworkMaster.Table.Where(p => p.Production_Order_id == arg.Production_Order_id).FirstOrDefault();
                if (arg != null)
                {

                    //   model.Jw_ApprovedPriceID = arg.Jw_ApprovedPriceID;
                    model.Production_Order_id = arg.Production_Order_id;
                  //  model.Prod_code_id = arg.Prod_code_id;
                    model.date = arg.date;
                    model.Leather_Pairs = arg.Leather_Pairs;
                    model.component_Pairs = arg.component_Pairs;
                    model.Upper_Fullshoes = arg.Upper_Fullshoes;
                    model.Jw_Name = arg.Jw_Name;
                    model.Process_Name = arg.Process_Name;
                    model.Stage_From = arg.Stage_From;

                    model.Stage_To = arg.Stage_To;
                    model.Lot_no = arg.Lot_no;
                    model.Io_based = arg.Io_based;
                    model.Buyer = arg.Buyer;
                    model.Season = arg.Season;
                    model.Style = arg.Style;
                    model.JW_BOM_linked = arg.JW_BOM_linked;
                    model.Material_Name = arg.Material_Name;

                    model.Size_range = arg.Size_range;
                    model.Delivery_date = arg.Delivery_date;
                    model.Totalpair = arg.Totalpair;
                    model.Qty = arg.Qty;
                    model.Qty_Uom = arg.Qty_Uom;
                    model.Rate = arg.Rate;
                    model.Value = arg.Value;
                    model.GST = arg.GST;


                    model.GST_AMT = arg.GST_AMT;
                    model.Total_Cost = arg.Total_Cost;

                    model.Fg_ComponentId = arg.Fg_ComponentId;
                    model.Fg_Material_Name = arg.Fg_Material_Name;

                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    JobRepositoryProductionJobworkMaster.Update(arg);
                    result = arg.Production_Order_id;
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }

            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                ProductionJobworkMaster model = JobRepositoryProductionJobworkMaster.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobRepositoryProductionJobworkMaster.Update(model);
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public bool Delete_ProductionName(int id)
        {
            bool result = false;
            try
            {
                ProductionJobwork_Code_Master model = JobRepositoryProductionJobwork_Code_Master.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobRepositoryProductionJobwork_Code_Master.Update(model);
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        //public bool Delete_size(Jobsheet_pairSizerangeMaster arg)
        //{
        //    Jobsheet_pairSizerangeMaster Jobsheet_pairSizerangeMaster = new Jobsheet_pairSizerangeMaster();
        //    bool result = false;
        //    List<Jobsheet_pairSizerangeMaster> model = Getjobsheet_SizeList(arg.jobsheet_pair_Code_id, arg.jobsheet_pair_id);
        //    try
        //    {
        //        foreach (var item in model)
        //        {
        //            Jobsheet_pairSizerangeRepository.Delete(item);
        //        }
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

        //    }
        //    return result;
        //}
        #endregion

        public List<ProductionJobworkMaster> Get()
        {
            List<ProductionJobworkMaster> ProductionJobworkMaster = new List<ProductionJobworkMaster>();
            try
            {
                ProductionJobworkMaster = JobRepositoryProductionJobworkMaster.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ProductionJobworkMaster;
        }

        //public List<JobSheet_PairMaster> save_check_material(int Material, int jobsheet_pair_Code_id)
        //{
        //    List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
        //    try
        //    {
        //        JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(X => X.Material == Material && X.jobsheet_pair_Code_id == jobsheet_pair_Code_id && X.IsDeleted == false || X.IsDeleted == null).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return JobSheet_PairMaster;
        //}
        public List<ProductionJobwork_Code_Master> Get_byCode(int jobsheet_pair_Code_id)
        {
            List<ProductionJobwork_Code_Master> ProductionJobwork_Code_Master = new List<ProductionJobwork_Code_Master>();
            try
            {
                ProductionJobwork_Code_Master = JobRepositoryProductionJobwork_Code_Master.Table.Where(X => X.IsDeleted == true || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ProductionJobwork_Code_Master;
        }
        public List<ProductionJobwork_Code_Master> Get_production_code_Tbl()
        {
            List<ProductionJobwork_Code_Master> ProductionJobwork_Code_Master = new List<ProductionJobwork_Code_Master>();
            try
            {
                ProductionJobwork_Code_Master = JobRepositoryProductionJobwork_Code_Master.Table.Where(y => y.IsDeleted == false || y.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ProductionJobwork_Code_Master;
        }

        public ProductionJobworkMaster GetproductionMaster_id(int? Production_Order_id)
        {
            ProductionJobworkMaster ProductionJobworkMaster = new ProductionJobworkMaster();
            try
            {
                ProductionJobworkMaster = JobRepositoryProductionJobworkMaster.Table.Where(x => x.Production_Order_id == Production_Order_id && x.IsDeleted == false).FirstOrDefault();
                return ProductionJobworkMaster;
            }
            catch (Exception ex)
            {

            }
            return ProductionJobworkMaster;
        }


        public List<ProductionJobSizerangeMaster> Get_Size(int? Production_Order_id)
        {
            List<ProductionJobSizerangeMaster> ProductionJobSizerangeMaster = new List<ProductionJobSizerangeMaster>();
            try
            {
                ProductionJobSizerangeMaster = JobProductionJobSizerang_Master.Table.Where(x => x.Job_Production_pair_id == Production_Order_id).ToList();

                 


                return ProductionJobSizerangeMaster;
            }
            catch (Exception ex)
            {

            }
            return ProductionJobSizerangeMaster;
        }
        public IEnumerable<ProductionJobSizerangeMaster> Get_Size_IEnumerable(int? Production_Order_id)
        {
            IEnumerable<ProductionJobSizerangeMaster> ProductionJobSizerangeMaster = ProductionJobSizerangeMaster = JobProductionJobSizerang_Master.Table.Where(x => x.Job_Production_pair_id == Production_Order_id).ToList(); ;
            try
            {
                ProductionJobSizerangeMaster = JobProductionJobSizerang_Master.Table.Where(x => x.Job_Production_pair_id == Production_Order_id).ToList();




                return ProductionJobSizerangeMaster;
            }
            catch (Exception ex)
            {

            }
            return ProductionJobSizerangeMaster;
        }
        public List<MMS.Data.StoredProcedureModel.JobProductionGrid> Get_ProductionGrid()
        {
            List<MMS.Data.StoredProcedureModel.JobProductionGrid> JobProductionGrid = new List<MMS.Data.StoredProcedureModel.JobProductionGrid>();
            try
            {
                JobProductionGrid = JobRepositoryProductionJobwork_Code_Master.JobProductionGrid();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobProductionGrid;
        }
        public ProductionJobwork_Code_Master Getjobsheet_pair_code_id(int? ProductionJobwork_code_id)
        {
            ProductionJobwork_Code_Master ProductionJobwork_Code_Master = new ProductionJobwork_Code_Master();
            try
            {
                ProductionJobwork_Code_Master = JobRepositoryProductionJobwork_Code_Master.Table.Where(x => x.ProductionJobwork_code_id == ProductionJobwork_code_id).FirstOrDefault();
                return ProductionJobwork_Code_Master;
            }
            catch (Exception ex)
            {

            }
            return ProductionJobwork_Code_Master;
        }
        public List<MMS.Data.StoredProcedureModel.CuttingSlipModel> Get_CutingSlip_List_Grid(int LotNo, int BuyerName, int BuyerSeason, int MaterialCategoryMasterId, int MaterialGroupMasterId, int IoNo)
        {
            List<MMS.Data.StoredProcedureModel.CuttingSlipModel> JobProductionGrid = new List<MMS.Data.StoredProcedureModel.CuttingSlipModel>();
            try
            {
                JobProductionGrid = JobRepositoryProductionJobwork_Code_Master.CutingSlip_List_Grid(LotNo, BuyerName, BuyerSeason, MaterialCategoryMasterId, MaterialGroupMasterId, IoNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobProductionGrid;
        }
        //public List<Jobsheet_pairSizerangeMaster> Getjobsheet_SizeList(int? jobsheet_pair_Code_id, int? jobsheet_pair_id)
        //{
        //    List<Jobsheet_pairSizerangeMaster> Jobsheet_pairSizerangeMaster = new List<Jobsheet_pairSizerangeMaster>();
        //    try
        //    {
        //        Jobsheet_pairSizerangeMaster = Jobsheet_pairSizerangeRepository.Table.Where(x => x.jobsheet_pair_Code_id == jobsheet_pair_Code_id && x.jobsheet_pair_id == jobsheet_pair_id).ToList();
        //        return Jobsheet_pairSizerangeMaster;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Jobsheet_pairSizerangeMaster;
        //}
    }
}