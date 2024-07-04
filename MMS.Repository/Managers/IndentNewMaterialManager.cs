using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Data.Mapping.StockMap;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
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
        public IndentNewMaterialManager()
        {
            indentheaderRepository = unitOfWork.Repository<Indentheader>();
            indentdetailRepository = unitOfWork.Repository<Indentdetail>();

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
                    indentheader.StoreCode = arg.StoreCode;
                    indentheader.SupplierCode = arg.SupplierCode;
                    indentheader.PoRefNumber = arg.PoRefNumber;
                    indentheader.Items = arg.Items;
                    indentheader.Quantity = arg.Quantity;
                    indentheader.ExtendedValue = arg.ExtendedValue;
                    indentheader.DiscountValue = arg.DiscountValue;
                    indentheader.SubtotalValue = arg.SubtotalValue;
                    indentheader.TaxValue = arg.TaxValue;
                    indentheader.TotalValue = arg.TotalValue;
                    indentheader.ExpDeliveryDate = arg.ExpDeliveryDate;
                    indentheader.PayByDate = arg.PayByDate;
                    indentheader.FulfillDate = arg.FulfillDate;
                    indentheader.Status = arg.Status;
                    indentheader.IndentNo = arg.IndentNo;
                    indentheader.IndentQty = arg.IndentQty;
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
        public int GetNextIndentNumberFromDatabase()
        {
            int latestNumber = 0; // Initialize with a default value
            try
            {
                latestNumber = indentheaderRepository.Table.Max(p => p.IndentNo);
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
