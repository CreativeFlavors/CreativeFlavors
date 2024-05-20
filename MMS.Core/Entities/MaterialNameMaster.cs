using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("tbl_materialnamemaster", Schema = "public")]
    public class tbl_materialnamemaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialmasterid")]
        public int MaterialMasterID { get; set; }
        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }
        [Column("materialcode")]
        public string MaterialCode { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }
        [Column("leathermaterilfirstvalue")]
        public string LeatherMaterilFirstValue { get; set; }
        [Column("leathermaterialname")]
        public int? LeatherMaterialName { get; set; }
        [Column("leathermaterillastvalue")]
        public string LeatherMaterilLastValue { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
