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
    public class InvoiceDetailsManages
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<orderheader> InvoiceDetailsRepository;

        #region Helper Method
        public InvoiceDetailsManages()
        {
            InvoiceDetailsRepository = unitOfWork.Repository<orderheader>();
        }
        public List<orderheader> Get()
        {
            List<orderheader> InvoiceDetailslist = new List<orderheader>();
            try
            {
                InvoiceDetailslist = InvoiceDetailsRepository.Table.Where(x => x.IsActive == true).ToList<orderheader>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return InvoiceDetailslist;
        }
        #endregion
    }
}
