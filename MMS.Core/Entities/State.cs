using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("tbl_State")]
    public class State:BaseEntity2
    {
        [Key]
        public Guid StateCodePK { get; set; }

        [ForeignKey("Country")]
        public Guid CountryCodeFK { get; set; }   
        public string StateName { get; set; }      

        public string Createdby { get; set; }

        public string Updatedby { get; set; }

        public string Deletedby { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
    
}
