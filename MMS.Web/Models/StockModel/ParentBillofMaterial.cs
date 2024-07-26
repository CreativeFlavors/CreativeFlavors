using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class ParentBillofMaterial
    {
        public int Bomid { get; set; }
        public string Bomno { get; set; }

        public string Description { get; set; }
        public string MaterialNames { get; set; }
        public string UOMName { get; set; }
        public string productname { get; set; }
        public string productype{ get; set; }
        public string materialcategory { get; set; }
        public string materialgroup { get; set; }
        public DateTime? Date { get; set; }
        public List<Parentbommatertial> bomMaterialGridList { get; set; }
        public List<subassemblydata> bomsubassemblyGridList { get; set; }
        public MaterialOpeningModel MaterialOpeningMaster { get; set; }

        public string Lastbom { get; set; }

        public bool IsActive { get; set; } = true;

        public string Createdby { get; set; }

        public DateTime Createddate { get; set; }

        public string Updatedby { get; set; }

        public DateTime? Updateddate { get; set; }

        public string Deletedby { get; set; }

        public DateTime? Deleteddate { get; set; }
        public int Bommaterialid { get; set; }
        public int subassemblyid { get; set; }
        public int ProductSUBid { get; set; }
        public int Productid { get; set; }

        public int MaterialCategoryid { get; set; }

        public int? MaterialGroupid { get; set; }

        public int? MaterialMasterid { get; set; }

        public int Uomid { get; set; }
        public decimal? availablestock { get; set; }

        public decimal? Requiredqty { get; set; }
    }
}