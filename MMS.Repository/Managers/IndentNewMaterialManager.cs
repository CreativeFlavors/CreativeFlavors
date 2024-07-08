using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Data.Mapping.StockMap;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class IndentNewMaterialManager :IIndentMaterialService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Indentheader> indentheaderRepository;
        private Repository<Indentdetail> indentdetailRepository;
        private Repository<IndentCart> indentcartRepository;
        private Repository<Temp_Indent> temp_indentRepository;
        public IndentNewMaterialManager()
        {
            indentheaderRepository = unitOfWork.Repository<Indentheader>();
            indentdetailRepository = unitOfWork.Repository<Indentdetail>();
            indentcartRepository= unitOfWork.Repository<IndentCart>();
            temp_indentRepository = unitOfWork.Repository<Temp_Indent>();
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Postindentheader(Indentheader arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                indentheaderRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Postindentdetail(Indentdetail arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                indentdetailRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Postindentcart(IndentCart arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                indentcartRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool PutIndentheader(Indentheader arg)
        {
            bool result = false;
            try
            {
                Indentheader indentheader = indentheaderRepository.Table.Where(p => p.IndentHeaderId == arg.IndentHeaderId).FirstOrDefault();
                if (indentheader != null)
                {
                    indentheader.IndentHeaderId = arg.IndentHeaderId;
                    indentheader.IndentDate = arg.IndentDate;
                    indentheader.PoRefNumber = arg.PoRefNumber;
                    indentheader.IndentNo = arg.IndentNo;
                    indentheader.IsActive = true;

                    indentheader.CreatedDate = arg.CreatedDate;
                    indentheader.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        indentheader.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    indentheaderRepository.Update(indentheader);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool PutIndentcart(IndentCart arg)
        {
            bool result = false;
            try
            {
                IndentCart indentheader = indentcartRepository.Table.Where(p => p.IndentCartId == arg.IndentCartId).FirstOrDefault();
                if (indentheader != null)
                {
                    indentheader.IndentCartId = arg.IndentCartId;
                    indentheader.MaterialNameId = arg.MaterialNameId;
                    indentheader.StoreNameId = arg.StoreNameId;
                    indentheader.TaxNameId = arg.TaxNameId;
                    indentheader.UomNameId = arg.UomNameId;
                    indentheader.Price = arg.Price;
                    indentheader.IndentRequiredQty = arg.IndentRequiredQty;
                    indentheader.RequiredQty = arg.RequiredQty;
                    indentheader.Status = arg.Status;
                    indentheader.IndentNumber = arg.IndentNumber;
                    indentheader.IsActive = true;

                    indentheader.CreatedDate = arg.CreatedDate;
                    indentheader.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        indentheader.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    indentcartRepository.Update(indentheader);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public Indentheader Getindentheaderid(int? indentheaderid)
        {
            Indentheader Indentheaderlist = new Indentheader();
            if (indentheaderid != 0)
            {
                try
                {
                    Indentheaderlist = indentheaderRepository.Table.Where(x => x.IndentHeaderId == indentheaderid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Indentheaderlist;
        }
        public bool Delete(int? indentcartid)
        {
            bool result = false;
            try
            {
                IndentCart model = indentcartRepository.GetById(indentcartid);
                //model.IsActive = false;

                indentcartRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public IndentCart Getindentcartid(int? indentcartid)
        {
            IndentCart Indentcartlist = new IndentCart();
            if (indentcartid != 0)
            {
                try
                {
                    Indentcartlist = indentcartRepository.Table.Where(x => x.IndentCartId == indentcartid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Indentcartlist;
        }
        public IndentCart Getindentcard(int materialnameid)
        {
            IndentCart Indentcartlist = new IndentCart();
            
                try
                {
                    Indentcartlist = indentcartRepository.Table.Where(i=>i.MaterialNameId== materialnameid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            
            return Indentcartlist;
        }

        public List<IndentCartsp> GetindentcartList()
        {
            List<IndentCartsp> indentlistcart = new List<IndentCartsp>();
            try
            {
                indentlistcart = indentcartRepository.Getindentcart();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return indentlistcart;
        }
        public int? GetNextIndentNumberFromDatabase()
        {
            int? latestNumber = 0; // Initialize with a default value
            try
            {
                latestNumber = indentcartRepository.Table.Max(p => p.IndentNumber);
                latestNumber++;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return latestNumber;
        }
        public List<IndentCart> GetIndentCartsList(int? indentnumber)
        {
            List<IndentCart> indentCarts = new List<IndentCart>();
            indentCarts = indentcartRepository.Table.Where(x => x.IndentNumber == indentnumber).ToList();
            return indentCarts;
        }

        public void UpdateTempIndent(int? materialId, decimal? indentQty)
        {
            var tempIndentRecord = GetTempIndentRecord(materialId.Value);
            var indentnumberRecord = Getindentcard(materialId.Value);
            if (tempIndentRecord != null)
            {
                //tempIndentRecord.Consume -= indentQty.Value;
                tempIndentRecord.stockRequired -= indentQty.Value;
                tempIndentRecord.IndentBy = "Admin";
                tempIndentRecord.IndentOn = DateTime.Now;
                tempIndentRecord.IndentNumber = indentnumberRecord.IndentNumber;
                tempIndentRecord.IndentStoreCode = indentnumberRecord.StoreNameId;
                tempIndentRecord.Status = 0;
                UpdateTempIndentRecord(tempIndentRecord);
            }
        }

        public Temp_Indent GetTempIndentRecord(int materialId)
        {
            Temp_Indent tempIndent = new Temp_Indent();

            try
            {
                tempIndent = temp_indentRepository.Table.Where(t => t.MaterialId == materialId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }

            return tempIndent;
        }

        public void UpdateTempIndentRecord(Temp_Indent tempIndent)
        {
            try
            {

                temp_indentRepository.Update(tempIndent);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating temp_indent record.", ex);
            }
        }


    }
}
