using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("tbl_City")]
    public class City:BaseEntity2
    {
        [Key]
        public Guid CityCodePK { get; set; }

        [ForeignKey("State")]
        public Guid StateCodeFK { get; set; }      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityCode { get; set; }
        public string CityName { get; set; }
        

        public string Createdby { get; set; }

        public string Updatedby { get; set; }

        public string Deletedby { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

    }

}
