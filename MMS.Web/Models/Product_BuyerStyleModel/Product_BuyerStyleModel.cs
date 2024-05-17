using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.Product_BuyerStyleModel
{
    public class Product_BuyerStyleModel
    {
        public int ProductOrBuyerStyleId { get; set; }
        public int BuyerName_ProductGroup { get; set; }
        public int BuyerModel_ProductType { get; set; }
        public string BuyerStyle { get; set; }
        public string Last { get; set; }
        public string ProductColour { get; set; }
        public int? Color { get; set; }
        public string OurStyle { get; set; }
        public string SizeRange { get; set; }
        public string BomNo { get; set; }
        public string LeatherName_1 { get; set; }
        public string LeatherName_2 { get; set; }
        public string LeatherName_3 { get; set; }
        public string LeatherName_4 { get; set; }
        public string LeatherName_5 { get; set; }
        public string UOM { get; set; }
        public string Width_Fit { get; set; }
        public string FinishedProductType { get; set; }
        public string StandardPrice { get; set; }
        public string Product_Image { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string UploadError { get; set; }
        public decimal Weight { get; set; }
        public string Destination { get; set; }
        public List<Product_BuyerStyleMaster> Product_BuyerStyleList { get; set; }
    }
}