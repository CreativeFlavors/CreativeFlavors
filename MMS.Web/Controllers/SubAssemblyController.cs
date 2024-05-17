using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities;
using MMS.Web.Models.SubAssemblyModel;
using MMS.Web.Models.InvoiceDetails;
using MMS.Web.Models.ProductionBOM;
using iTextSharp.text;

namespace MMS.Web.Controllers
{
    public class SubAssemblyController : Controller
    {
        //
        // GET: /SubAssembly/

        public ActionResult SubAssemblyDetailsGrid(int page = 1, int pageSize = 8)

        {
            SubAssemblyModel List = new SubAssemblyModel();
            ProductionBOM pbmlist = new ProductionBOM();
            ProductionBOMManager PBOMManager = new ProductionBOMManager();
            pbmlist.listProductionBOMModel = PBOMManager.GetSubAssembly();
            List.listProductionBOMModel = pbmlist.listProductionBOMModel.Select(item => new ProductionBOMModel
            {
                Id = item.Id,
                BOMProductId = item.BOMProductId,
                MaterialId = item.MaterialId,
                BOMProductCode = item.BOMProductCode,
                BOMProductName = item.BOMProductName,
                MaterialCategoryMasterid = item.MaterialCategoryMasterid,
                ProductType = item.ProductType,
                UomIdMaster = item.UomIdMaster,
                Bomid = item.Bomid,
                Qty = item.Qty,
                ConsumeunitId = item.ConsumeunitId,
                Description = item.Description,
                IsActive = item.IsActive,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                UpdatedBy = item.UpdatedBy,
                UpdatedDate = item.UpdatedDate,
                Process = item.Process,
                SubAssemblyId = item.SubAssemblyId
                //l
            })
                   .ToList();
            return View("SubAssemblyDetailsGrid", List);
        }

    }
}
