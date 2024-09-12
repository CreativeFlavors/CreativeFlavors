using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("supplieraddress", Schema = "public")]
    public class SupplierAddress :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("supplieradd_id")]
        public int supplieradd_id { get; set; }

        [Column("supplierid")]
        public int supplierid { get; set; }

        [Column("addresstype")]
        public int AddressType { get; set; }

        [Column("addressvarient")]
        public int? addressvarient { get; set; }
        [Column("contactname")]
        public string ContactName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("cityid")]
        public int CityId { get; set; }

        [Column("stateid")]
        public int StateId { get; set; }

        [Column("countryid")]
        public int CountryId { get; set; }

        [Column("isactive")]
        public bool isActive { get; set; }
        [Column("deleteddate")]
        public DateTime? deleteddate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string deletedby { get; set; }
        [Column("isdeleted")]
        public bool isdeleted { get; set; } = true;
    }
}
