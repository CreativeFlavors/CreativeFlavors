using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("custaddress", Schema = "public")]
    public class CustAddress : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("addresshd_id")]
        public int Addresshd_id { get; set; }

        [Column("buyerid")]
        public int BuyerId { get; set; }

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
        public bool isActive { get; set; } = false;
        [Column("isdeleted")]
        public bool isdeleted { get; set; } = true;
        [Column("deleteddate")]
        public DateTime? deleteddate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string deletedby { get; set; }

    }
}
