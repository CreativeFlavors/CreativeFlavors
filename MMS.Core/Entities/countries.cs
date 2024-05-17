using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("countries", Schema = "public")]
    public class countries : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("country_code")]
        public string country_code { get; set; }
        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

    }
}
