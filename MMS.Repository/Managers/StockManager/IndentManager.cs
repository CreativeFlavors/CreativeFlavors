using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Common;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Web.Models;

namespace MMS.Repository.Managers.StockManager
{
    public class IndentManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Indent> IndentRepository;

        private UnitOfWork2 unitOfWork2 = new UnitOfWork2();
        private Repository2<Unit_Division> UnitDivisionRepository;
        private Repository2<EmpGrade> GradeMasterRepository;
        private Repository2<tbl_Company> CompanyRepository;

        #region Helper Method
        public IndentManager()
        {
            IndentRepository = unitOfWork.Repository<Indent>();
            UnitDivisionRepository = unitOfWork2.Repository<Unit_Division>();
            GradeMasterRepository = unitOfWork2.Repository<EmpGrade>();
            CompanyRepository = unitOfWork2.Repository<tbl_Company>();
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStock(int materialMasterID)
        {
            List<MMS.Web.Models.PendingQty> bomMaterialList = new List<MMS.Web.Models.PendingQty>();
            if (materialMasterID != 0)
            {
                bomMaterialList = IndentRepository.MaterialOpeningStock(materialMasterID);

            }
            return bomMaterialList;
          
        }
        public List<MMS.Web.Models.SPBomMaterialList> GetBOMMaterial(string MRPNO)
        {
            List<MMS.Web.Models.SPBomMaterialList> bomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            if (MRPNO != "")
            {
                bomMaterialList = IndentRepository.BomMaterialList(MRPNO);

            }
            return bomMaterialList;
        }
        public List<MMS.Web.Models.subspbommateriallist> subGetBOMMaterial(string MRPNO)
        {
            List<MMS.Web.Models.subspbommateriallist> bomMaterialList = new List<MMS.Web.Models.subspbommateriallist>();
            if (MRPNO != "")
            {
                bomMaterialList = IndentRepository.subBomMaterialList(MRPNO);

            }
            return bomMaterialList;
        }
        public List<IndentMaterials> IndentMaterialLists(string MRPNO)
        {
            List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            indentMaterials = indentMaterialManager.Get(MRPNO);
            return indentMaterials;
        }
        public List<Indent> Get()
        {
            List<Indent> IndentList = new List<Indent>();
            try
            {
                IndentList = IndentRepository.Table.ToList<Indent>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public Indent GetIndentMasterId(int IndentId)
        {
            Indent IndentMaster = new Indent();
            if (IndentId != 0)
            {
                IndentMaster = IndentRepository.Table.Where(x => x.IndentId == IndentId).FirstOrDefault();
            }
            return IndentMaster;
        }
        public Indent GetIndentNO(int IndentId)
        {
            Indent IndentMaster = new Indent();
            if (IndentId != 0)
            {
                IndentMaster = IndentRepository.Table.Where(x => x.IndentNo == IndentId.ToString()).FirstOrDefault();
            }
            return IndentMaster;
        }
        public Indent GetIndentID(int IndentId)
        {
            Indent IndentMaster = new Indent();
            if (IndentId != 0)
            {
                IndentMaster = IndentRepository.Table.Where(x => x.IndentId == IndentId).FirstOrDefault();
            }
            return IndentMaster;
        }
        public Indent IsExistIndent(int? StoredID, string MRPNO, int? MaterialType)
        {
            Indent IndentMaster = new Indent();
            IndentMaster = IndentRepository.Table.Where(x => x.StoreId == StoredID && x.MRPNO == MRPNO && x.MaterialType == MaterialType).FirstOrDefault();

            return IndentMaster;
        }
        public List<Indent> GetIndentNOWithSupplierID(int SupplierID)
        {
            List<Indent> IndentMaster = new List<Indent>();
            if (SupplierID != 0)
            {
                IndentMaster = IndentRepository.Table.Where(x => x.SupplierId == SupplierID).ToList();
            }
            return IndentMaster;
        }
        #endregion

        #region Curd Operation
        public Indent IndentSave(Indent indent)
        {
            Indent indent_ = new Indent();
            try
            {
                if (indent.IndentId == 0)
                {
                    indent.CreatedDate = DateTime.Now;
                    indent.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
                    indent.UpdatedDate = null;
                    indent.UpdatedBy = string.Empty;
                    IndentRepository.Insert(indent);
                    indent_ = indent;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return indent_;
        }
        public bool Post(Indent arg, string MRPNO)
        {
            bool result = false;
            try
            {
                Indent IsExistIndent = new Indent();
                IndentManager indentManager = new IndentManager();
                IsExistIndent = indentManager.IsExistIndent(arg.StoreId, arg.MRPNO, arg.MaterialType);
                if (arg.IndentId == 0&& (IsExistIndent==null|| IsExistIndent.IndentId==0))
                {
                    arg.CreatedDate = DateTime.Now;
                    arg.AmmendmentDate = null;
                    arg.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
                    IndentRepository.Insert(arg);
                    result = true;
                    List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                    IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                    List<IndentMaterialsList> BOMMaterialList = new List<IndentMaterialsList>().ToList();
                    if (HttpContext.Current.Session["MRPMaterial"] != null)
                    {
                        var myList = (List<IndentMaterialsList>)HttpContext.Current.Session["MRPMaterial"];
                        foreach (var item in myList)
                        {
                            if (item.MaterialMasterID == 548)
                            {
                                string message = "";
                            }
                            IndentMaterials indentMaterial = new IndentMaterials();
                            indentMaterial.IndentID = arg.IndentId;
                            indentMaterial.CreatedDate = DateTime.Now;
                            indentMaterial.UpdatedDate = null;
                            indentMaterial.TotalIndentPairs = item.TotalIndentPairs;
                            indentMaterial.CategoryName = item.CategoryName;
                            indentMaterial.GroupName = item.GroupName;
                            indentMaterial.MaterialDescription = item.MaterialDescription;
                            indentMaterial.Color = item.Color;
                            indentMaterial.WastageName = item.WastageName;
                            indentMaterial.TotalNormsName = item.TotalNormsName;
                            indentMaterial.SampleNorms = item.SampleNorms;
                            indentMaterial.Wastage = item.Wastage;
                            indentMaterial.WastageQty = item.WastageQty;
                            indentMaterial.WastageQtyUOM = item.WastageQtyUOM;
                            indentMaterial.TotalNorms = item.TotalNorms;
                            indentMaterial.MRPNO = arg.MRPNO;
                            indentMaterial.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                            indentMaterial.MaterialGroupMasterId = item.MaterialGroupMasterId;
                            indentMaterial.MaterialMasterID = item.MaterialMasterID;
                            indentMaterial.SubstanceRange = item.SubstanceRange;
                            indentMaterial.SubstanceMasterId = item.SubstanceMasterId;
                            indentMaterial.BOMMaterialID = item.BOMMaterialID;
                            indentMaterial.BOMID = item.BOMID;
                            indentMaterial.ColorMasterID = item.ColorMasterID;
                            indentMaterial.BuyerSeason = item.BuyerSeason;
                            indentMaterial.SeasonName = item.SeasonName;
                            indentMaterial.RequiredQty = item.RequiredQty;
                            indentMaterial.StoreStock = item.StoreStock;
                            if (item.IndentQTY == null || item.IndentQTY == 0)
                            {
                                indentMaterial.IndentQTY = item.RequiredQty;
                            }
                            else
                            {
                                indentMaterial.IndentQTY = item.IndentQTY;
                            }
                            indentMaterial.OrderEntryId = item.OrderEntryId;
                            indentMaterial.UomMasterId = item.UomMasterId;
                            indentMaterial.BuyerFullName = item.BuyerFullName;
                            indentMaterial.TotalPairs = item.TotalPairs;
                            indentMaterial.SupplierId = item.SupplierId;
                            indentMaterial.BuyerPoNo = item.BuyerPoNo;
                            indentMaterial.Price = item.Price;
                            indentMaterial.SupplierMasterName = item.SupplierMasterName;
                            indentMaterial.Value = item.Value;
                            indentMaterial.BuyerMasterId = item.BuyerMasterId;
                            indentMaterialManager.Post(indentMaterial);
                        }
                    }

                }
                else
                {
                    if (arg.AmmendmentDate.ToString() == "1/1/0001 12:00:00 AM")
                    {
                        arg.AmmendmentDate = null;
                    }
                    arg.UpdatedDate = DateTime.Now;
                    arg.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                    arg.IndentId = Convert.ToInt32( IsExistIndent.IndentId);
                    IndentRepository.Update(arg);
                    List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                    IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                    // indentMaterials = indentMaterialManager.GetMRPID(Convert.ToInt32(MRPNO));
                    indentMaterials = indentMaterialManager.GetIndentMaterilDeails(arg.MRPNO);
                    foreach (var item in indentMaterials)
                    {
                        item.IndentID = arg.IndentId;
                        indentMaterialManager.Post(item);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(Indent arg)
        {
            bool result = false;
            try
            {
                IndentRepository.Update(arg);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Indent model = IndentRepository.GetById(id);
                IndentRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        #endregion

        #region Unit Division
        public List<Unit_Division> GetUnitDivisionList()
        {
            List<Unit_Division> unitList = new List<Unit_Division>();
            try
            {
                unitList = UnitDivisionRepository.Table.ToList<Unit_Division>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return unitList;
        }
        public List<EmpGrade> GetGradeMasterList()
        {
            List<EmpGrade> gradeMaster = new List<EmpGrade>();
            try
            {
                gradeMaster = GradeMasterRepository.Table.ToList<EmpGrade>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gradeMaster;
        }


        public List<tbl_Company> GetCompanyName()
        {
            List<tbl_Company> company = new List<tbl_Company>();
            try
            {
                company = CompanyRepository.Table.ToList<tbl_Company>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return company;
        }

        #endregion

    }
}
