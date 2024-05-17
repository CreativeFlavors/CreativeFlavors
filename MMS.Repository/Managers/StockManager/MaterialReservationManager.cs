using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class MaterialReservationManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MaterialReservation> MaterialReservationRepository;

        #region Helper Method

        public MaterialReservationManager()
        {
            MaterialReservationRepository = unitOfWork.Repository<MaterialReservation>();
        }

        public MaterialReservation Get(int id)
        {
            return null;
        }

        public List<MaterialReservation> Get()
        {
            List<MaterialReservation> materialReservationlist = new List<MaterialReservation>();
            try
            {
                materialReservationlist = MaterialReservationRepository.Table.ToList<MaterialReservation>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialReservationlist;
        }

        public MaterialReservation GetMaterialResvId(int MaterialReservationId)
        {
            MaterialReservation materialReservation = new MaterialReservation();
            if (MaterialReservationId != 0)
            {
                materialReservation = MaterialReservationRepository.Table.Where(x => x.MaterialReservationId == MaterialReservationId).SingleOrDefault();
            }
            return materialReservation;
        }

        #endregion

        #region Crud Operations

        public bool Post(MaterialReservation arg)
        {
            bool result = false;
            try
            {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedBy = username;
                    MaterialReservationRepository.Insert(arg);
                    result = true;
               
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(MaterialReservation arg)
        {
            bool result = false;
            try
            {

                MaterialReservation model = MaterialReservationRepository.Table.Where(m => m.MaterialReservationId == arg.MaterialReservationId).FirstOrDefault();
                if (model != null)
                {
                    model.MaterialReservationId = arg.MaterialReservationId;
                    model.DocNo = arg.DocNo;
                    model.InternalOrder = arg.InternalOrder;
                    model.PlanWise = arg.PlanWise;
                    model.MaterialCategoryId = arg.MaterialCategoryId;
                    model.MaterialGroupId = arg.MaterialGroupId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.UomId = arg.UomId;
                    model.ColourMasterId = arg.ColourMasterId;
                    model.Quantity = arg.Quantity;
                    model.FreeStock = arg.FreeStock;
                    model.MaterialWise = arg.MaterialWise;
                    model.Summary = arg.Summary;
                    model.MultipleIO = "";
                    model.DisplayIO = arg.DisplayIO;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    MaterialReservationRepository.Update(model);
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

        public bool Delete(int MaterialReservationId)
        {
            bool result = false;
            try
            {
                MaterialReservation model = MaterialReservationRepository.GetById(MaterialReservationId);
                MaterialReservationRepository.Delete(model);
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

    }
}
