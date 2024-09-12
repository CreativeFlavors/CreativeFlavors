using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.MaterailCategory
{
    public class MaterialCategoryModel
    {
        public int MaterialCategoryMasterId { get; set; }
        public int? Categorytype { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        //public List<MaterialCategoryMaster> MaterialCatgoryList { get; set; }
    }
}