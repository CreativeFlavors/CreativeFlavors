using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("materialgroupmaster", Schema = "public")]
    public class materialgroupmaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }

        [Column("groupcode")]
        public string GroupCode { get; set; }

        [Column("groupname")]
        public string GroupName { get; set; }

        [Column("subgroup")]
        public string SubGroup { get; set; }

        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }

        [Column("issubstance")]
        public bool IsSubstance { get; set; }

        [Column("issize")]
        public bool IsSize { get; set; }

        [Column("iscolor")]
        public bool IsColor { get; set; }

        [Column("isconsumption")]
        public bool IsConsumption { get; set; }

        [Column("isinspection")]
        public bool IsInspection { get; set; }

        [Column("isreservation")]
        public bool IsReservation { get; set; }

        [Column("isdisplay")]
        public bool IsDisplay { get; set; }

        [Column("isbatchcode")]
        public bool IsBatchcode { get; set; }

        [Column("ismultipleunits")]
        public bool IsMultipleUnits { get; set; }

        [Column("isleathertype")]
        public bool IsLeatherType { get; set; }

        [Column("storename")]
        public int StoreName { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
