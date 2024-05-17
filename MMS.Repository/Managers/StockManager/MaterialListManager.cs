using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.StockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class MaterialListManager 
        //: IBOMMaterialListService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRN_EntityModel> GrnRepository;
        private Repository<BOMMaterialList> BOMMaterialListRepository;

        #region Add/Update/Delete Operation

        public BOMMaterialList Post(BOMMaterialList arg)
        {
            BOMMaterialList bOMMaterialList = new BOMMaterialList();
            try
            {
                if (arg.MaterilBomID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = DateTime.Now;
                    BOMMaterialListRepository.Insert(arg);
                    bOMMaterialList = arg;
                }
                else
                {
                    BOMMaterialList model = BOMMaterialListRepository.Table.Where(p => p.MaterilBomID == arg.MaterilBomID).FirstOrDefault();
                    //MMSContext context = new MMSContext();
                    model.MaterilBomID = arg.MaterilBomID;
                    model.BomID = arg.BomID;
                    model.BomNo = arg.BomNo;
                    model.Date = arg.Date;
                    model.ParentBomNo = arg.ParentBomNo;
                    model.CommnBOM1 = arg.CommnBOM1;
                    model.CommnBOM2 = arg.CommnBOM2;
                    model.CommnBOM3 = arg.CommnBOM3;
                    model.CommnBOM4 = arg.CommnBOM4;
                    model.CommnBOM5 = arg.CommnBOM5;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.SampleNorms = arg.SampleNorms;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.Wastage = arg.Wastage;
                    model.WastageQty = arg.WastageQty;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedBy = model.CreatedBy;
                    BOMMaterialListRepository.Update(model);
                    bOMMaterialList = arg;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);


            }
            return bOMMaterialList;
        }
        

        #endregion
    }
}
