using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("machinerymaster", Schema = "public")]
    public class MachineryMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("machinerymasterid")]
        public int MachineryMasterId { get; set; }

        [Column("machinecode")]
        public string MachineCode { get; set; }

        [Column("machinename")]
        public string MachineName { get; set; }

        [Column("make")]
        public string Make { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("asseslistno")]
        public string AssesListNo { get; set; }

        [Column("machineserialno")]
        public string MachineSerialNo { get; set; }

        [Column("kilowatt")]
        public string Kilowatt { get; set; }

        [Column("horsepower")]
        public string HorsePower { get; set; }

        [Column("volt")]
        public string Volt { get; set; }

        [Column("operationusedfor")]
        public string OperationUsedFor { get; set; }

        [Column("specification")]
        public string Specification { get; set; }

        [Column("image")]
        public string Image { get; set; }
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
