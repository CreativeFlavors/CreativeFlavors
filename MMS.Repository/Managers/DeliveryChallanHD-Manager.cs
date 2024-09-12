using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class DeliveryChallanHD_Manager : IDeliveryChallanHDServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<DeliveryChallan_hd> DeliveryChallanHdrep;
        public DeliveryChallanHD_Manager()
        {
            DeliveryChallanHdrep = unitOfWork.Repository<DeliveryChallan_hd>();

        }
        public DeliveryChallan_hd POST(DeliveryChallan_hd arg)
        {
            DeliveryChallan_hd salesorder = new DeliveryChallan_hd();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 0;
                arg.IsDeleted = true;
                DeliveryChallanHdrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<DeliveryChallan_hd> Get()
        {
            List<DeliveryChallan_hd> obj = new List<DeliveryChallan_hd>();
            obj = DeliveryChallanHdrep.Table.ToList<DeliveryChallan_hd>();
            return obj;
        }
        public DeliveryChallan_hd GetSOId(int id)
        {
            DeliveryChallan_hd salesorders = new DeliveryChallan_hd();
            salesorders = DeliveryChallanHdrep.Table.Where(x => x.DCid_hd == id).FirstOrDefault();
            return salesorders;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public salesordercart GettypeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
