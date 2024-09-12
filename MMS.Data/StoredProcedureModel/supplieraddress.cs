using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("supplieraddress", Schema = "public")]
    public class supplieraddress
    {
        public int addresshd_id { get; set; }
        public int supplierid { get; set; }

        public string ContactName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string suppliername { get; set; }

        public string suppliercode { get; set; }

        public string CityName { get; set; }

        public string addtypename { get; set; }

        public string Address1 { get; set; }
        public string zipcode{ get; set; }

        public string StateName { get; set; }

        public string CountryName { get; set; }
    }
}
