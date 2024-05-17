using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class Job_Sheet_pair_piecesMaster :BaseEntity
    {
        public int JobSheet_pair_Additional_pieces_id { get; set; }

        public int JW_Name { get; set; }

        public int Process_Name { get; set; }

        public int Material_Name { get; set; }

        public bool Is_value_used { get; set; }

        public int jobsheet_pair_id { get; set; }

        public decimal? pieces_qty { get; set; }
        

     
    }
}
