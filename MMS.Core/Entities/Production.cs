using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("production", Schema = "public")]
    public class Production : BaseEntity
        {
            [Column("productionid")]
            public int ProductionId { get; set; }

            [Column("productiondate")]
            public DateTime? ProductionDate { get; set; }

            [Column("productioncode")]
            public string ProductionCode { get; set; }

            [Column("productionqty")]
            public decimal? ProductionQty { get; set; }

            [Column("productionstatus")]
            public int ProductionStatus { get; set; }

            [Column("productid")]
            public int ProductId { get; set; }

            [Column("minqty")]
            public decimal MinQty { get; set; }

            [Column("maxqty")]
            public decimal MaxQty { get; set; }

            [Column("requiredqty")]
            public decimal RequiredQty { get; set; }

            [Column("storecode")]
            public int StoreCode { get; set; }

            [Column("productionduedate")]
            public DateTime? ProductionDueDate { get; set; }

           [Column("productionfullfilldate")]
            public DateTime? ProductionFullfillDate { get; set; }

            [Column("refdocno")]
            public decimal RefDocNo { get; set; }

            [Column("refdocdate")]
            public DateTime? RefDocDate { get; set; }

            [Column("status1code")]
            public string Status1Code { get; set; }

            [Column("status1date")]
            public DateTime? Status1Date { get; set; }

            [Column("status1by")]
            public string Status1By { get; set; }

            [Column("status2code")]
            public string Status2Code { get; set; }

            [Column("status2date")]
            public DateTime? Status2Date { get; set; }

            [Column("status2by")]
            public string Status2By { get; set; }

            [Column("status3code")]
            public string Status3Code { get; set; }

            [Column("status3date")]
            public DateTime? Status3Date { get; set; }

            [Column("status3by")]
            public string Status3By { get; set; }

            [Column("createddate")]
            public DateTime? CreatedDate { get; set; }

            [Column("updateddate")]
            public DateTime? UpdatedDate { get; set; }

            [Column("deleteddate")]
            public DateTime? DeletedDate { get; set; }

            [Column("createdby")]
            public string CreatedBy { get; set; }

            [Column("updatedby")]
            public string UpdatedBy { get; set; }

            [Column("deletedby")]
            public string DeletedBy { get; set; }

        [Column("productions")]
        public bool Productions { get; set; }

        [Column("subassembly")]
        public bool SubAssembly { get; set; }

        [Column("inprogress")]
        public bool Inprogress { get; set; }

        [Column("productionperday")]
        public decimal? ProductionPerDay { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("quantitytomanufacture")]
        public decimal? QuantityToManufacture { get; set; }

        [Column("availabletomanufacture")]
        public decimal? AvailableToManufacture { get; set; }
    }

    }

