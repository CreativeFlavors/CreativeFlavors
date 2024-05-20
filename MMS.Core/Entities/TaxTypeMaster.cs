using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("taxtypemaster", Schema = "public")]
    public class TaxTypeMaster : BaseEntity
    {
        [Column("taxmasterid")]
        public int TaxMasterID { get; set; }
        [Column("taxname")]
        public string TaxName { get; set; }
        [Column("taxmode")]
        public string TaxMode { get; set; }
        [Column("taxvalue")]
        public string TaxValue { get; set; }
        [Column("taxcode")]
        public string TaxCode { get; set; }
        [Column("taxref")]
        public string TaxRef { get; set; }
        [Column("form")]
        public string Form { get; set; }
        [Column("taxon")]
        public string TaxOn { get; set; }
        [Column("taxvaluemode")]
        public string TaxValueMode { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
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
    }
}
