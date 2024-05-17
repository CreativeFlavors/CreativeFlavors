using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("paymentmethod", Schema = "public")]
    public class paymentmethod : BaseEntity
    { 
        [Column("id")]
        public int Id { get; set; }
        [Column("paymentstype")]
        public string Paymentstype { get; set; }
        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }


    }
}
