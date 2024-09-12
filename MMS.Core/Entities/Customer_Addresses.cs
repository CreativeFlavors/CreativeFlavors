using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("customer_addresses", Schema = "public")]
    public class Customer_Addresses :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("addressdt_id")]
        public int addressdt_id { get; set; }   
        [Column("addresshd_id")]
        public int Addresshd_id { get; set; }
        [Column("addresstypeid")]
        public int addresstypeid { get; set; }    
        [Column("buyerid")]
        public int Buyerid { get; set; }  
        [Column("supplierid")]
        public int supplierid { get; set; }  

        [Column("address1")]
        public string address1 { get; set; }

        [Column("address2")]
        public string address2 { get; set; }

        [Column("address3")]
        public string address3 { get; set; }
        [Column("zipcode")]
        public string ZipCode { get; set; }    
        [Column("vatnumber")]
        public string vatnumber { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isactive")]
        public bool isactive { get; set; }
    }
}
