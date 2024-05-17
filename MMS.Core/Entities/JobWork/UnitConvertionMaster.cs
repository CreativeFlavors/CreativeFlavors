using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class UnitConvertionMaster: BaseEntity
    {
        public int UC_No_Id { get; set; }
        public string UC_No_Code { get; set; }
        public int Store_id_from { get; set; }

        public int Group_From { get; set; }

        public int Category_From { get; set; }

        public int Material_id_From { get; set; }

        public int Store_id_to { get; set; }

        public int Group_To { get; set; }

        public int Category_To { get; set; }

        public int Material_id_To { get; set; }

        public double Noms { get; set; }

        public int Uom1 { get; set; }

        public int Uom2 { get; set; }

        public int No_sheet { get; set; }
        public int No_sheet_Uom { get; set; }

        public decimal Sheet_Value { get; set; }

        public int Sheet_Value_Uom { get; set; }

        public decimal Value { get; set; }

        public int Value_Uom { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
