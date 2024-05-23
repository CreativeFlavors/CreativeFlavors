using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    [Table("productionjobwork_code_master", Schema = "public")]
    public class ProductionJobwork_Code_Master: BaseEntity
    {
        [Column("productionjobwork_code_id")]
        public int ProductionJobwork_code_id { get; set; }

        [Column("productionjobwork_code")]
        public string ProductionJobwork_Code { get; set; }

        [Column("leather_pairs")]
        public bool Leather_Pairs { get; set; }

        [Column("component_pairs")]
        public bool component_Pairs { get; set; }

        [Column("upper_fullshoes")]
        public bool Upper_Fullshoes { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("production_type")]
        public bool Production_Type { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

    }
}
