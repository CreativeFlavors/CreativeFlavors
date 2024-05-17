using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("seasonmaster", Schema = "public")]
    public class SeasonMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("seasonmasterid")]

        public int SeasonMasterId { get; set; }
        [Column("seasonname")]
        public string SeasonName { get; set; }
        [Column("springsummeryear")]
        public string SpringSummerYear { get; set; }
        [Column("springdescription")]
        public string SpringDescription { get; set; }
        [Column("periodfrom")]
        public DateTime? PeriodFrom { get; set; }
        [Column("periodto")]
        public DateTime? PeriodTo { get; set; }
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
