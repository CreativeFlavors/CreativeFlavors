﻿using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class PoManager : IPoService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<PoHeader> poheaderRepository;
        private Repository<PoDetail> podetailRepository;
        private Repository<PoCart> pocartRepository;
        private Repository<IndentPoMapping> indentpomappingRepository;
        public PoManager()
        {
            poheaderRepository = unitOfWork.Repository<PoHeader>();
            podetailRepository = unitOfWork.Repository<PoDetail>();
            pocartRepository = unitOfWork.Repository<PoCart>();
            indentpomappingRepository = unitOfWork.Repository<IndentPoMapping>();
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool PostIndentPoMapping(IndentPoMapping arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                indentpomappingRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool PutIndentPoMapping(IndentPoMapping arg)
        {
            bool result = false;
            try
            {
                ProductManager productManager = new ProductManager();
                product product = new product();
                product = productManager.GetId(arg.ProductId);
                IndentPoMapping indentPoMapping = indentpomappingRepository.Table.Where(p => p.IndentPoMapId == arg.IndentPoMapId).FirstOrDefault();
                if (indentPoMapping != null)
                {
                    indentPoMapping.IndentPoMapId = arg.IndentPoMapId;
                    indentPoMapping.SupplierId = arg.SupplierId;
                    indentPoMapping.ProductId = arg.ProductId;
                    indentPoMapping.IndentId = arg.IndentNumber;
                    indentPoMapping.IndentProductId = arg.ProductId;
                    indentPoMapping.StoreCode = product.StoreId;
                    indentPoMapping.UnitPrice = arg.UnitPrice;
                    indentPoMapping.IndentQty = arg.IndentQty;
                    indentPoMapping.PoQty = arg.PoQty;
                    indentPoMapping.PoDate = arg.PoDate;
                    indentPoMapping.Status = arg.Status;
                    indentPoMapping.IsActive = true;
                    indentPoMapping.WithIndentReference = true;
                    indentPoMapping.UomId = product.UomMasterId;
                    indentPoMapping.TaxValue = arg.TaxValue;
                    indentPoMapping.DiscountPercentage = arg.DiscountPercentage;
                    indentPoMapping.TaxPercentage = arg.TaxPercentage;
                    indentPoMapping.DiscountValue = arg.DiscountValue;
                    indentPoMapping.Subtotal = arg.Subtotal;
                    indentPoMapping.TotalValue = arg.TotalValue;
                    indentPoMapping.TaxInclusive = true;
                    indentPoMapping.PoNumber = arg.PoNumber;
                    indentPoMapping.IndentNumber = arg.IndentNumber;

                    indentPoMapping.CreatedDate = arg.CreatedDate;
                    indentPoMapping.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        indentPoMapping.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    indentpomappingRepository.Update(indentPoMapping);
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
        public bool PostPoHeader(PoHeader arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                poheaderRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool PostPoDetail(PoDetail arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                podetailRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Delete(int indentpomapid)
        {
            bool result = false;
            try
            {
                IndentPoMapping model = indentpomappingRepository.GetById(indentpomapid);
                //model.IsActive = false;

                indentpomappingRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public IndentPoMapping GetIndentPoMapid(int indentpomapid)
        {
            IndentPoMapping Indentpolist = new IndentPoMapping();
            if (indentpomapid != 0)
            {
                try
                {
                    Indentpolist = indentpomappingRepository.Table.Where(x => x.IndentPoMapId == indentpomapid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Indentpolist;
        }
        public List<IndentPoMappingsp> GetindentPoMappingList()
        {
            List<IndentPoMappingsp> indentlistcart = new List<IndentPoMappingsp>();
            try
            {
                indentlistcart = indentpomappingRepository.GetindentPoMapping();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return indentlistcart;
        }
        public IndentPoMappingsp GetindentPoMappingId(int indentpomapid)
        {
            IndentPoMappingsp indentlistcart = new IndentPoMappingsp();
            try
            {
                indentlistcart = indentpomappingRepository.GetindentPoMapping().Where(x => x.IndentPoMapId == indentpomapid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return indentlistcart;
        }
        public List<IndentPoMapping> GetindentPoMappingForTableData(int indentpomapid)
        {
            List<IndentPoMapping> indentlistcart = new List<IndentPoMapping>();
            try
            {
                indentlistcart = indentpomappingRepository.Table.Where(x => x.IndentPoMapId == indentpomapid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return indentlistcart;
        }
        public List<IndentPoMapping> GetIndentpomapList(int ponumber)
        {
            List<IndentPoMapping> indentCarts = new List<IndentPoMapping>();
            indentCarts = indentpomappingRepository.Table.Where(x => x.PoNumber == ponumber).ToList();
            return indentCarts;
        }
        public int GetNextPONumberFromDatabase()
        {
            int latestNumber = 0; // Initialize with a default value
            try
            {
                latestNumber = indentpomappingRepository.Table.Max(p => p.PoNumber);
                latestNumber++;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return latestNumber;
        }
    }
}