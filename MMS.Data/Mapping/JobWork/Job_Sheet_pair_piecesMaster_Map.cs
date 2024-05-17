using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.JobWork
{
    class Job_Sheet_pair_piecesMaster_Map : EntityTypeConfiguration<Job_Sheet_pair_piecesMaster>
    {
        public Job_Sheet_pair_piecesMaster_Map()
        {
            HasKey(t => t.JobSheet_pair_Additional_pieces_id);
            Property(t => t.JobSheet_pair_Additional_pieces_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.JW_Name);
            Property(t => t.Process_Name);
            Property(t => t.Material_Name);
            Property(t => t.Is_value_used);
            Property(t => t.jobsheet_pair_id);
            Property(t => t.pieces_qty);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("Job_Sheet_pair_pieces");
        }
    }
}